using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BLL;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_ThongKe : UserControl
    {
        private DataTable _donHang;   // nguồn đơn hàng
        private DataTable _trangThai; // lookup TrangThaiID -> TenTrangThai

        public UC_ThongKe()
        {
            InitializeComponent();
            this.Load += UC_ThongKe_Load;
        }
        private readonly Btnbeautifull _theme = new Btnbeautifull()
        {
            Text = Color.White,
            Outline = Color.FromArgb(120, 195, 170),
            SearchFill = Color.Azure,
            SearchText = Color.Black,
            SearchPlaceholder = Color.Black
        };

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            PillStyler.Combo(guna2ComboBox4, _theme);
            PillStyler.Combo(guna2ComboBox3, _theme);
            // 1) Lấy dữ liệu gốc
            _donHang  = BLL_DonHang.GetAllDonHang();
            _trangThai = BLL_DonHang.GetAllTrangThaiDonHang();

            // 2) (tuỳ chọn) lọc theo thời gian nếu bạn có UI From/To
            DateTime? from = null, to = null; // gán từ combobox/datepicker nếu muốn
            _donHang = FilterByDate(_donHang, from, to, DetectDateColumn(_donHang));

            // 3) Đếm theo nhóm tình trạng
            var counts = CountByStatus(_donHang, _trangThai);

            // 4) Vẽ biểu đồ
            DrawCharts(counts);

            // (tuỳ chọn) Nếu có DataGridView chi tiết quá hạn:
            // dgvQuaHan.DataSource = FilterByStatus(_donHang, _trangThai, "Quá hạn");
            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.Add("(Tất cả)");
            guna2ComboBox3.Items.Add("Quý 1");
            guna2ComboBox3.Items.Add("Quý 2");
            guna2ComboBox3.Items.Add("Quý 3");
            guna2ComboBox3.Items.Add("Quý 4");

            guna2ComboBox3.IntegralHeight = false;
            guna2ComboBox3.MaxDropDownItems = 4;
            guna2ComboBox3.DropDownHeight = 100;

            // Gán event TRƯỚC khi set SelectedIndex
            guna2ComboBox3.SelectedIndexChanged += FilterAndRedraw;
            guna2ComboBox3.SelectedIndex = 0; // đặt ở cuối cùng

            // --- Combo Năm ---
            guna2ComboBox4.Items.Clear();
            guna2ComboBox4.Items.Add("(Tất cả)");
            int currentYear = DateTime.Now.Year;
            for (int year = 2018; year <= currentYear + 10; year++)
                guna2ComboBox4.Items.Add(year.ToString());

            guna2ComboBox4.IntegralHeight = false;
            guna2ComboBox4.MaxDropDownItems = 6;
            guna2ComboBox4.DropDownHeight = 100;

            // Gán event TRƯỚC khi set SelectedIndex
            guna2ComboBox4.SelectedIndexChanged += FilterAndRedraw;
            guna2ComboBox4.SelectedIndex = 0; // đặt ở cuối cùng
        }
        private void FilterAndRedraw(object sender, EventArgs e)
        {
            try
            {
                int? nam = null;
                string quy = null;

                // Lấy quý
                if (guna2ComboBox3.Text != "(Tất cả)")
                    quy = guna2ComboBox3.Text;

                // Lấy năm
                if (guna2ComboBox4.Text != "(Tất cả)" && int.TryParse(guna2ComboBox4.Text, out int y))
                    nam = y;

                if (_donHang == null || _donHang.Rows.Count == 0)
                    return;

                // ✅ Lấy toàn bộ hợp đồng từ BLL (chỉ gọi 1 lần)
                DataTable hopDongTable = BLL_HopDong.GetAllHopDong();

                var filteredRows = _donHang.AsEnumerable().Where(dh =>
                {
                    // Kiểm tra NgayTao
                    if (dh["NgayTao"] == DBNull.Value) return false;
                    DateTime ngayTao = Convert.ToDateTime(dh["NgayTao"]);

                    // --- Tìm hợp đồng tương ứng ---
                    if (dh["HopDongID"] == DBNull.Value) return false;
                    string hopDongID = dh["HopDongID"].ToString();

                    DataRow hopDongRow = null;
                    foreach (DataRow r in hopDongTable.Rows)
                    {
                        if (r["HopDongID"].ToString() == hopDongID)
                        {
                            hopDongRow = r;
                            break;
                        }
                    }

                    if (hopDongRow == null || hopDongRow["NgayBatDau"] == DBNull.Value || hopDongRow["NgayKetThuc"] == DBNull.Value)
                        return false;

                    DateTime ngayBD = Convert.ToDateTime(hopDongRow["NgayBatDau"]);
                    DateTime ngayKT = Convert.ToDateTime(hopDongRow["NgayKetThuc"]);

                    // --- Lọc theo năm ---
                    bool matchNam = !nam.HasValue || (nam.Value >= ngayBD.Year && nam.Value <= ngayKT.Year);

                    // --- Lọc theo quý ---
                    int quyThang = (ngayTao.Month - 1) / 3 + 1;
                    bool matchQuy = string.IsNullOrEmpty(quy) ||
                                    (quy == "Quý 1" && quyThang == 1) ||
                                    (quy == "Quý 2" && quyThang == 2) ||
                                    (quy == "Quý 3" && quyThang == 3) ||
                                    (quy == "Quý 4" && quyThang == 4);

                    return matchNam && matchQuy;
                });

                // --- Chuyển về DataTable ---
                DataTable filtered = filteredRows.Any() ? filteredRows.CopyToDataTable() : _donHang.Clone();

                // --- Cập nhật biểu đồ ---
                var counts = CountByStatus(filtered, _trangThai);
                DrawCharts(counts);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc thống kê: " + ex.Message);
            }
        }


        private void DrawCharts(Dictionary<string, int> counts)
{
    int late  = counts.ContainsKey("Quá hạn")    ? counts["Quá hạn"]    : 0;
    int doing = counts.ContainsKey("Đang xử lý") ? counts["Đang xử lý"] : 0;
    int done  = counts.ContainsKey("Hoàn thành") ? counts["Hoàn thành"] : 0;

    // ===== BAR =====
    foreach (Series s in BieuDo1.Series) s.Points.Clear();

    var sLate  = BieuDo1.Series.FindByName("Quá hạn");
    var sDoing = BieuDo1.Series.FindByName("Đang xử lý") ?? BieuDo1.Series.FindByName("Đang xử lí");
    var sDone  = BieuDo1.Series.FindByName("Hoàn thành");

    if (sDoing != null) sDoing.Name = "Đang xử lý";      // chuẩn hóa tên hiển thị

    sLate?.Points.AddXY(1, late);        // cột 1 = Quá hạn
    sDoing?.Points.AddXY(2, doing);      // cột 2 = Đang xử lý
    sDone?.Points.AddXY(3, done);        // cột 3 = Hoàn thành

    var area = BieuDo1.ChartAreas[0];
    area.AxisX.Minimum = 0;
    area.AxisX.Maximum = 4;
    area.AxisX.Interval = 1;
    area.RecalculateAxesScale();

    // ===== PIE =====
    var pie = chart2.Series["SeriesPie"];
    if (pie != null)
    {
        pie.Points.Clear();
        AddPiePoint(pie, "Quá hạn", late);
        AddPiePoint(pie, "Đang xử lý", doing);
        AddPiePoint(pie, "Hoàn thành", done);
    }
}

private static void AddPiePoint(Series pie, string label, int value)
{
    int idx = pie.Points.AddXY(label, value);
    var pt = pie.Points[idx];
    pt.LegendText = label;       // Tên hiển thị trong Legend
    pt.Label = "#PERCENT{P1}";   // Nhãn hiển thị trên lát cắt
}

        // ===================== Helper gộp trong UC =====================
        private static string DetectDateColumn(DataTable dt)
        {
            if (dt == null) return null;
            string[] candidates = { "NgayKetThuc", "NgayLayMau", "NgayDuKienTraKetQua", "NgayTao", "NgayLap", "NgayCapNhat" };
            foreach (var c in candidates) if (dt.Columns.Contains(c)) return c;
            foreach (DataColumn col in dt.Columns) if (col.DataType == typeof(DateTime)) return col.ColumnName;
            return null;
        }

        private static string NormalizeStatus(string tenTrangThai, DateTime? ngayDuKien, DateTime? ngayTraThucTe)
        {
            if (ngayTraThucTe.HasValue) return "Hoàn thành"; // ưu tiên theo ngày trả thực tế
            if (ngayDuKien.HasValue && ngayDuKien.Value.Date < DateTime.Today) return "Quá hạn";

            var s = (tenTrangThai ?? "").Trim().ToLowerInvariant().Replace("xử lí", "xử lý");
            if (s.Contains("hoàn thành")) return "Hoàn thành";
            if (s.Contains("quá hạn"))    return "Quá hạn";
            if (s.Contains("đang") && s.Contains("xử")) return "Đang xử lý";
            return CultureInfo.GetCultureInfo("vi-VN").TextInfo.ToTitleCase(string.IsNullOrEmpty(s) ? "Khác" : s);
        }

        private static Dictionary<string, string> BuildTrangThaiMap(DataTable dtTrangThai)
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (dtTrangThai == null) return map;
            foreach (DataRow r in dtTrangThai.Rows)
            {
                var id = Convert.ToString(r["TrangThaiID"]);
                var name = Convert.ToString(r["TenTrangThai"]);
                if (!string.IsNullOrEmpty(id)) map[id] = name;
            }
            return map;
        }

        private static DataTable FilterByDate(DataTable src, DateTime? from, DateTime? to, string dateCol = null)
        {
            if (src == null || src.Rows.Count == 0) return src;
            var col = dateCol ?? DetectDateColumn(src);
            if (string.IsNullOrEmpty(col) || !src.Columns.Contains(col)) return src;

            var dv = new DataView(src);
            var parts = new List<string>();
            if (from.HasValue) parts.Add($"[{col}] >= #{from.Value:MM/dd/yyyy}#");
            if (to.HasValue)   parts.Add($"[{col}] < #{to.Value.AddDays(1):MM/dd/yyyy}#");
            dv.RowFilter = string.Join(" AND ", parts);
            return dv.ToTable();
        }

        private static Dictionary<string, int> CountByStatus(DataTable donHang, DataTable trangThai = null)
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["Quá hạn"] = 0,
                ["Đang xử lý"] = 0,
                ["Hoàn thành"] = 0
            };
            if (donHang == null || donHang.Rows.Count == 0) return result;

            var map = BuildTrangThaiMap(trangThai);
            bool hasTen = donHang.Columns.Contains("TenTrangThai");
            bool hasID  = donHang.Columns.Contains("TrangThaiID");

            foreach (DataRow r in donHang.Rows)
            {
                DateTime? duKien = donHang.Columns.Contains("NgayDuKienTraKetQua") && r["NgayDuKienTraKetQua"] != DBNull.Value
                                   ? (DateTime?)Convert.ToDateTime(r["NgayDuKienTraKetQua"]) : null;
                DateTime? traTT  = donHang.Columns.Contains("NgayTraThucTe") && r["NgayTraThucTe"] != DBNull.Value
                                   ? (DateTime?)Convert.ToDateTime(r["NgayTraThucTe"]) : null;

                string ten = null;
                if (hasTen) ten = Convert.ToString(r["TenTrangThai"]);
                else if (hasID)
                {
                    var id = Convert.ToString(r["TrangThaiID"]);
                    if (!string.IsNullOrEmpty(id) && map.TryGetValue(id, out var nm)) ten = nm;
                }

                var key = NormalizeStatus(ten, duKien, traTT);
                if (!result.ContainsKey(key)) result[key] = 0;
                result[key]++;
            }
            return result;
        }

        private static DataTable FilterByStatus(DataTable donHang, DataTable trangThai, string groupName)
        {
            if (donHang == null) return null;
            var res = donHang.Clone();
            var map = BuildTrangThaiMap(trangThai);
            bool hasTen = donHang.Columns.Contains("TenTrangThai");
            bool hasID  = donHang.Columns.Contains("TrangThaiID");

            foreach (DataRow r in donHang.Rows)
            {
                DateTime? duKien = donHang.Columns.Contains("NgayDuKienTraKetQua") && r["NgayDuKienTraKetQua"] != DBNull.Value
                                   ? (DateTime?)Convert.ToDateTime(r["NgayDuKienTraKetQua"]) : null;
                DateTime? traTT  = donHang.Columns.Contains("NgayTraThucTe") && r["NgayTraThucTe"] != DBNull.Value
                                   ? (DateTime?)Convert.ToDateTime(r["NgayTraThucTe"]) : null;

                string ten = null;
                if (hasTen) ten = Convert.ToString(r["TenTrangThai"]);
                else if (hasID)
                {
                    var id = Convert.ToString(r["TrangThaiID"]);
                    if (!string.IsNullOrEmpty(id) && map.TryGetValue(id, out var nm)) ten = nm;
                }

                var key = NormalizeStatus(ten, duKien, traTT);
                if (string.Equals(key, groupName, StringComparison.OrdinalIgnoreCase))
                    res.ImportRow(r);
            }
            return res;
        }

        private static DataTable GroupByMonth(DataTable donHang, string dateCol = null)
        {
            var dt = new DataTable();
            dt.Columns.Add("Thang", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            if (donHang == null || donHang.Rows.Count == 0) return dt;

            var col = dateCol ?? DetectDateColumn(donHang);
            if (string.IsNullOrEmpty(col) || !donHang.Columns.Contains(col)) return dt;

            var dict = new Dictionary<string, int>();
            foreach (DataRow r in donHang.Rows)
            {
                if (r[col] == DBNull.Value) continue;
                var key = Convert.ToDateTime(r[col]).ToString("yyyy-MM");
                dict[key] = dict.ContainsKey(key) ? dict[key] + 1 : 1;
            }
            foreach (var kv in dict.OrderByDescending(k => k.Key))
                dt.Rows.Add(kv.Key, kv.Value);

            return dt;
        }
        // =================== End Helper ===================
    }
}
