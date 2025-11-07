using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BLL;

namespace GUI
{
    public partial class UC_PopupThongBao : UserControl
    {
        private List<DonHang> donHangSapHetHan;
        private List<DonHang> donHangQuaHan;

        // Theo dõi tab hiện tại để áp bộ lọc đúng nguồn
        private enum TabMode { SapHetHan, QuaHan }
        private TabMode _currentTab = TabMode.SapHetHan;

        public UC_PopupThongBao()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(40, 70, 55);

            // Khởi tạo rỗng, sẽ nạp từ DB khi Load
            donHangSapHetHan = new List<DonHang>();
            donHangQuaHan = new List<DonHang>();
        }

        private void UC_PopupThongBao_Load(object sender, EventArgs e)
        {
            RefreshDataFromDb();
            btnSapHetHan.PerformClick(); // mặc định tab đầu tiên
        }

        private void btnSapHetHan_Click(object sender, EventArgs e)
        {
            // luôn nạp mới để phản ánh trạng thái cập nhật trong DB
            RefreshDataFromDb();
            _currentTab = TabMode.SapHetHan;
            btnSapHetHan.FillColor = Color.FromArgb(90, 130, 90);
            btnQuaHan.FillColor = Color.Transparent;
            ApplySearchFilter();
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            // luôn nạp mới để phản ánh trạng thái cập nhật trong DB
            RefreshDataFromDb();
            _currentTab = TabMode.QuaHan;
            btnQuaHan.FillColor = Color.FromArgb(90, 130, 90);
            btnSapHetHan.FillColor = Color.Transparent;
            ApplySearchFilter();
        }

        // Nạp dữ liệu từ BLL và lọc theo Ngày dự kiến trả kết quả + Trạng thái
        private void RefreshDataFromDb()
        {
            try
            {
                var dtHD = BLL_HopDong.GetAllHopDong();                 // để lấy TenCongTy
                var dtDH = BLL_DonHang.GetAllDonHang();                 // chứa NgayDuKienTraKetQua, TrangThaiID
                var dtTT = BLL_DonHang.GetAllTrangThaiDonHang();        // map TrangThaiID -> TenTrangThai

                // Build index HopDongID -> TenCongTy
                var hdIndex = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (dtHD != null)
                {
                    foreach (DataRow r in dtHD.Rows)
                    {
                        string id = ToStr(r, "HopDongID");
                        if (string.IsNullOrWhiteSpace(id)) continue;
                        string tenCty = dtHD.Columns.Contains("TenCongTy") ? ToStr(r, "TenCongTy") : null;
                        if (!hdIndex.ContainsKey(id))
                            hdIndex[id] = tenCty ?? "(N/A)";
                    }
                }

                // Build index TrangThaiID -> TenTrangThai
                var ttIndex = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (dtTT != null)
                {
                    foreach (DataRow r in dtTT.Rows)
                    {
                        string id = Convert.ToString(r["TrangThaiID"] ?? "");
                        string name = Convert.ToString(r["TenTrangThai"] ?? "");
                        if (!string.IsNullOrEmpty(id) && !ttIndex.ContainsKey(id))
                            ttIndex[id] = name;
                    }
                }

                var today = DateTime.Today;
                var sapHet = new List<DonHang>();
                var quaHan = new List<DonHang>();

                if (dtDH != null)
                {
                    foreach (DataRow r in dtDH.Rows)
                    {
                        // Ngày dự kiến trả kết quả phải có
                        if (!dtDH.Columns.Contains("NgayDuKienTraKetQua") || r["NgayDuKienTraKetQua"] == DBNull.Value)
                            continue;

                        DateTime duKien = Convert.ToDateTime(r["NgayDuKienTraKetQua"]).Date;
                        int days = (duKien - today).Days;

                        // Trạng thái
                        string ttId = FirstNonEmpty(r, "TrangThaiID") ?? "";
                        string ttName = ttIndex.TryGetValue(ttId, out var nm) ? nm : "";

                        // Bỏ qua cả 2 tab nếu Hoàn thành
                        if (string.Equals(ttName, "Hoàn thành", StringComparison.OrdinalIgnoreCase))
                            continue;

                        string hopDongId = FirstNonEmpty(r, "HopDongID");
                        // Chỉ hiển thị tên đơn hàng (MaDonHang)
                        string maDon = FirstNonEmpty(r, "MaDonHang", "DonHangID", "MaDon") ?? "(N/A)";
                        string khach = "(N/A)";
                        if (!string.IsNullOrWhiteSpace(hopDongId) && hdIndex.TryGetValue(hopDongId, out var tenCty))
                            khach = tenCty;
                        // Ghi chú
                        string ghiChu = r.Table.Columns.Contains("GhiChu") ? Convert.ToString(r["GhiChu"] ?? "") : null;

                        var item = new DonHang(maDon, khach, null, null, duKien, hopDongId);
                        item.GhiChu = ghiChu;

                        // Sắp hết hạn: chỉ khi Đang xử lý và 0..3 ngày
                        if (string.Equals(ttName, "Đang xử lý", StringComparison.OrdinalIgnoreCase) && days >= 0 && days <= 3)
                        {
                            sapHet.Add(item);
                        }
                        // Quá hạn: < 0 (trừ Hoàn thành đã loại ở trên)
                        else if (days < 0)
                        {
                            quaHan.Add(item);
                        }
                    }
                }

                donHangSapHetHan = sapHet;
                donHangQuaHan = quaHan;
            }
            catch
            {
                donHangSapHetHan = new List<DonHang>();
                donHangQuaHan = new List<DonHang>();
            }
        }

        private static string ToStr(DataRow r, string col)
        {
            return r.Table.Columns.Contains(col) ? Convert.ToString(r[col] ?? "") : null;
        }

        private static string FirstNonEmpty(DataRow r, params string[] cols)
        {
            foreach (var c in cols)
            {
                if (!r.Table.Columns.Contains(c)) continue;
                var s = Convert.ToString(r[c] ?? "");
                if (!string.IsNullOrWhiteSpace(s)) return s;
            }
            return null;
        }

        private void LoadDonHang(List<DonHang> list)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var dh in list)
                flowLayoutPanel1.Controls.Add(CreateDonHangPanel(dh));
        }

        private Guna2Panel CreateDonHangPanel(DonHang dh)
        {
            var panel = new Guna2Panel
            {
                BorderRadius = 10,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(200, 200, 200),
                Padding = new Padding(10),
                Size = new Size(380, 110),
                BackColor = Color.FromArgb(35, 60, 45),
                Margin = new Padding(5)
            };

            // Tính số ngày còn lại
            int daysLeft = (dh.NgayHetHan.Date - DateTime.Today).Days;
            string countdownText = daysLeft > 0
                ? $"Còn {daysLeft} ngày"
                : daysLeft == 0
                    ? "Hết hạn hôm nay"
                    : $"Quá hạn {Math.Abs(daysLeft)} ngày";

            Color countdownColor = daysLeft < 0
                ? Color.FromArgb(255, 100, 100)
                : (daysLeft == 0 ? Color.FromArgb(255, 180, 60) : Color.FromArgb(160, 220, 120));

            // Header hiển thị đếm ngược (Dock Top, chữ căn phải)
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 24,
                BackColor = Color.Transparent
            };
            var lblCountdown = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = countdownColor,
                Text = countdownText
            };
            header.Controls.Add(lblCountdown);

            // Chỉ hiển thị: MaDonHang, Tên công ty, Dự kiến trả KQ, Ghi chú
            string note = string.IsNullOrWhiteSpace(dh.GhiChu) ? "ko ghi chú" : dh.GhiChu;
            var lbl = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                Text =
                    $"Đơn hàng: {dh.MaDon}\n" +
                    $"KH: {dh.KhachHang}\n" +
                    $"Dự kiến trả KQ: {dh.NgayHetHan:dd-MM-yyyy}\n" +
                    $"Ghi chú: {note}"
            };

            // Thứ tự thêm: Fill trước, rồi Top để Dock hoạt động đúng
            panel.Controls.Add(lbl);
            panel.Controls.Add(header);

            // Tự động điều chỉnh chiều cao panel theo nội dung + header
            void AdjustPanelHeight()
            {
                int availableWidth = Math.Max(50, panel.ClientSize.Width - panel.Padding.Horizontal);
                // đo chiều cao văn bản với word-wrap
                var measured = TextRenderer.MeasureText(
                    lbl.Text,
                    lbl.Font,
                    new Size(availableWidth, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

                int newHeight = panel.Padding.Vertical + header.Height + measured.Height + 4; // thêm 1 khoảng nhỏ để tránh cắt
                if (newHeight > panel.Height)
                {
                    panel.Height = newHeight;
                }
                else
                {
                    // luôn đặt chiều cao tối thiểu hợp lý
                    panel.Height = Math.Max(newHeight, 110);
                }
            }

            AdjustPanelHeight();
            panel.SizeChanged += (s, e) => AdjustPanelHeight();

            return panel;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Thông tin hợp đồng liên kết
        private class HopDongInfo
        {
            public string HopDongID { get; set; }
            public string TenCongTy { get; set; }
            public DateTime? NgayKetThuc { get; set; }
        }

        // Áp bộ lọc theo MaDon dựa trên nội dung thanh tìm kiếm và tab hiện tại
        private void ApplySearchFilter()
        {
            List<DonHang> source = _currentTab == TabMode.SapHetHan ? donHangSapHetHan : donHangQuaHan;
            string term = (ThanhTimKiem != null ? (ThanhTimKiem.Text ?? string.Empty) : string.Empty).Trim();

            if (string.IsNullOrEmpty(term))
            {
                LoadDonHang(source);
                return;
            }

            var filtered = source
                .Where(d => !string.IsNullOrEmpty(d.MaDon) && d.MaDon.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            LoadDonHang(filtered);
        }

        private void ThanhTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }
    }

    // Class dữ liệu đơn hàng hiển thị
    public class DonHang
    {
        public string MaDon { get; set; }
        public string KhachHang { get; set; }
        public string LoaiDon { get; set; }
        public string Phong { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string HopDongID { get; set; }
        public string GhiChu { get; set; }

        public DonHang(string ma, string kh, string loai, string phong, DateTime ngay, string hopDongId)
        {
            MaDon = ma;
            KhachHang = kh;
            LoaiDon = loai;
            Phong = phong;
            NgayHetHan = ngay;
            HopDongID = hopDongId;
        }
    }
}
