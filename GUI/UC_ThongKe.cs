using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
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
