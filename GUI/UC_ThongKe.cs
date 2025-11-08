using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;

namespace GUI
{
    public partial class UC_ThongKe : UserControl
    {
        private DataTable _donHang;   // nguồn đơn hàng

        public UC_ThongKe()
        {
            InitializeComponent();
            this.Load += UC_ThongKe_Load;
        }

        private readonly Btnbeautifull _theme = new Btnbeautifull()
        {
            Text = System.Drawing.Color.White,
            Outline = System.Drawing.Color.FromArgb(120, 195, 170),
            SearchFill = System.Drawing.Color.Azure,
            SearchText = System.Drawing.Color.Black,
            SearchPlaceholder = System.Drawing.Color.Black
        };

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            BieuDo1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            BieuDo1.Titles.Clear();
            Title t = new Title("BIỂU ĐỒ CỘT VỀ TÌNH TRẠNG ĐƠN HÀNG", Docking.Top,
                                new Font("Segoe UI", 12, FontStyle.Bold), Color.White);
            BieuDo1.Titles.Add(t);
            BieuDo1.Legends[0].Docking = Docking.Bottom;
            BieuDo1.Legends[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);
            chart2.Titles.Clear();
            Title t1 = new Title("BIỂU ĐỒ TRÒN VỀ TÌNH TRẠNG ĐƠN HÀNG", Docking.Top,
                                new Font("Segoe UI", 12, FontStyle.Bold), Color.White);
            chart2.Titles.Add(t1);
            // Style combobox
            PillStyler.Combo(guna2ComboBox4, _theme); // Năm
            PillStyler.Combo(guna2ComboBox3, _theme); // Quý

            // 1) Lấy dữ liệu gốc + bỏ IsDeleted
            _donHang = SafeFilterNotDeleted(BLL_DonHang.GetAllDonHang());

            // 2) Setup combo Quý
            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.Add("(Tất cả)");
            guna2ComboBox3.Items.Add("Quý 1");
            guna2ComboBox3.Items.Add("Quý 2");
            guna2ComboBox3.Items.Add("Quý 3");
            guna2ComboBox3.Items.Add("Quý 4");
            guna2ComboBox3.IntegralHeight = false;
            guna2ComboBox3.MaxDropDownItems = 4;
            guna2ComboBox3.DropDownHeight = 100;
            guna2ComboBox3.SelectedIndexChanged += FilterAndRedraw;
            guna2ComboBox3.SelectedIndex = 0;

            // 3) Setup combo Năm
            guna2ComboBox4.Items.Clear();
            guna2ComboBox4.Items.Add("(Tất cả)");
            int currentYear = DateTime.Now.Year;
            for (int year = 2018; year <= currentYear + 10; year++)
                guna2ComboBox4.Items.Add(year.ToString());
            guna2ComboBox4.IntegralHeight = false;
            guna2ComboBox4.MaxDropDownItems = 6;
            guna2ComboBox4.DropDownHeight = 100;
            guna2ComboBox4.SelectedIndexChanged += FilterAndRedraw;
            guna2ComboBox4.SelectedIndex = 0;

            // 4) Tính & vẽ lần đầu (lọc theo NgayLayMau, đếm theo TrangThaiID)
            ApplyFiltersAndRedraw();
        }

        private void FilterAndRedraw(object sender, EventArgs e)
        {
            try { ApplyFiltersAndRedraw(); }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lọc thống kê: " + ex.Message); }
        }

        private void ApplyFiltersAndRedraw()
        {
            // Đọc bộ lọc từ UI
            int? nam = null;
            int? quy = null;
            if (guna2ComboBox4.Text != "(Tất cả)" && int.TryParse(guna2ComboBox4.Text, out int y)) nam = y;
            if (guna2ComboBox3.Text.StartsWith("Quý ") &&
                int.TryParse(guna2ComboBox3.Text.Replace("Quý ", ""), out int q) &&
                q >= 1 && q <= 4)
                quy = q;

            // Luôn reload đơn hàng mới nhất + bỏ IsDeleted
            var src = SafeFilterNotDeleted(BLL_DonHang.GetAllDonHang());

            // Lọc theo Năm/Quý dựa trên NgayLayMau (chỉ LỌC, không quyết định trạng thái)
            var filtered = FilterByNgayLayMauQuarterYear(src, nam, quy);

            // Đếm THUẦN theo TrangThaiID (TT001/2/3/4)
            var counts = CountByTrangThaiID(filtered);

            // Vẽ biểu đồ
            DrawCharts(counts);
        }

        // =================== Vẽ biểu đồ ===================

        private void DrawCharts(Dictionary<string, int> counts)
        {

            int late = counts.ContainsKey("Quá hạn") ? counts["Quá hạn"] : 0;
            int doing = counts.ContainsKey("Đang xử lý") ? counts["Đang xử lý"] : 0;
            int done = counts.ContainsKey("Hoàn thành") ? counts["Hoàn thành"] : 0;

            // ===== BAR =====
            foreach (Series s in BieuDo1.Series) s.Points.Clear();

            var sLate = BieuDo1.Series.FindByName("Quá hạn");
            var sDoing = BieuDo1.Series.FindByName("Đang xử lý") ?? BieuDo1.Series.FindByName("Đang xử lí");
            var sDone = BieuDo1.Series.FindByName("Hoàn thành");

            if (sDoing != null) sDoing.Name = "Đang xử lý"; // chuẩn hóa

            sLate?.Points.AddXY(1, late);
            sDoing?.Points.AddXY(2, doing);
            sDone?.Points.AddXY(3, done);
            if (sLate != null) sLate.Color = Color.FromArgb(255, 128, 255); // tím nhạt
            if (sDoing != null) sDoing.Color = Color.FromArgb(255, 255, 100); // vàng
            if (sDone != null) sDone.Color = Color.FromArgb(100, 149, 237); // xanh dương
            if (BieuDo1.ChartAreas.Count > 0)
            {
                var area = BieuDo1.ChartAreas[0];
                area.AxisX.Minimum = 0;
                area.AxisX.Maximum = 4;
                area.AxisX.Interval = 1;
                area.RecalculateAxesScale();
            }

            // ===== PIE =====
            var pie = chart2.Series["SeriesPie"];
            if (pie != null)
            {
                pie.Points.Clear();
                pie.ChartType = SeriesChartType.Pie;
                pie.LegendText = "#VALX";       // hiển thị tên trong chú thích
                pie.Label = "#PERCENT{P0}";     // hiển thị phần trăm trong biểu đồ
                pie["PieLabelStyle"] = "Inside";

                if (late > 0)
                {
                    int i = pie.Points.AddXY("Quá hạn", late);
                    pie.Points[i].Color = Color.FromArgb(255, 128, 255); // tím nhạt
                }

                if (doing > 0)
                {
                    int i = pie.Points.AddXY("Đang xử lý", doing);
                    pie.Points[i].Color = Color.FromArgb(255, 255, 100); // vàng
                }

                if (done > 0)
                {
                    int i = pie.Points.AddXY("Hoàn thành", done);
                    pie.Points[i].Color = Color.FromArgb(100, 149, 237); // xanh dương
                }


            }
            chart2.Legends.Clear();
            chart2.Legends.Add("Legend1");
            chart2.Legends[0].Docking = Docking.Bottom;
            chart2.Legends[0].Alignment = StringAlignment.Center;
            chart2.Legends[0].IsDockedInsideChartArea = false;
            chart2.Legends[0].ForeColor = Color.White;
            chart2.Legends[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);
            chart2.Legends[0].BackColor = Color.Transparent;

            pie.Legend = "Legend1";
        }

        private static void AddPiePoint(Series pie, string label, int value)
        {
            int idx = pie.Points.AddXY(label, value);
            var pt = pie.Points[idx];
            pt.LegendText = label;
            pt.Label = "#PERCENT{P1}";
        }

        // =================== Helpers ===================

        // Map cứng trạng thái từ TrangThaiID
        private static string StatusFromID(string id)
        {
            switch ((id ?? "").Trim())
            {
                case "TT001": return "Đang xử lý";
                case "TT002": return "Hoàn thành";
                case "TT003": return "Quá hạn";
                case "TT004": return "Hủy";
                default: return "Khác";
            }
        }

        // Lọc an toàn IsDeleted (bool hoặc int) – không quăng lỗi khi rỗng
        private static DataTable SafeFilterNotDeleted(DataTable src)
        {
            if (src == null || src.Rows.Count == 0) return src;
            if (!src.Columns.Contains("IsDeleted")) return src;

            var rows = src.AsEnumerable().Where(r =>
            {
                if (r["IsDeleted"] == DBNull.Value) return true; // coi như chưa xóa
                if (r["IsDeleted"] is bool b) return !b;
                return Convert.ToInt32(r["IsDeleted"]) == 0;
            });

            return rows.Any() ? rows.CopyToDataTable() : src.Clone();
        }

        // Lọc theo Năm/Quý dựa trên NgayLayMau (chỉ LỌC)
        private static DataTable FilterByNgayLayMauQuarterYear(DataTable src, int? nam, int? quy)
        {
            if (src == null || src.Rows.Count == 0) return src;
            if (!src.Columns.Contains("NgayLayMau")) return src; // không có cột -> trả về nguyên
            if (!nam.HasValue && !quy.HasValue) return src;      // không filter gì

            var rows = src.AsEnumerable().Where(r =>
            {
                if (r["NgayLayMau"] == DBNull.Value) return false; // không có ngày -> loại khi lọc
                DateTime d = Convert.ToDateTime(r["NgayLayMau"]);

                bool matchYear = !nam.HasValue || d.Year == nam.Value;
                bool matchQuarter = !quy.HasValue || ((d.Month - 1) / 3 + 1) == quy.Value;
                return matchYear && matchQuarter;
            });

            return rows.Any() ? rows.CopyToDataTable() : src.Clone();
        }

        // ✅ Đếm THUẦN theo TrangThaiID (ngày KHÔNG quyết định trạng thái)
        private static Dictionary<string, int> CountByTrangThaiID(DataTable donHang)
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["Quá hạn"] = 0,
                ["Đang xử lý"] = 0,
                ["Hoàn thành"] = 0
            };

            if (donHang == null || donHang.Rows.Count == 0) return result;

            foreach (DataRow r in donHang.Rows)
            {
                string id = donHang.Columns.Contains("TrangThaiID") ? Convert.ToString(r["TrangThaiID"]) : null;
                string name = StatusFromID(id);

                // Bỏ qua các trạng thái không đếm (Hủy/Khác)
                if (name == "Hủy" || name == "Khác")
                    continue;

                if (!result.ContainsKey(name))
                    result[name] = 0;

                result[name]++;
            }

            return result;
        }

        // Public API để main/form khác gọi refresh theo bộ lọc hiện tại
        public void RefreshThongKe()
        {
            try { ApplyFiltersAndRedraw(); }
            catch (Exception ex) { MessageBox.Show("Lỗi khi cập nhật thống kê: " + ex.Message); }
        }
        private void BieuDo1_Click(object sender, EventArgs e)
        { }


    }
}
