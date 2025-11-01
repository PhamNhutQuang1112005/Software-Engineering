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
            // Nếu muốn luôn dữ liệu mới mỗi lần bấm tab, mở dòng sau
            // RefreshDataFromDb();
            btnSapHetHan.FillColor = Color.FromArgb(90, 130, 90);
            btnQuaHan.FillColor = Color.Transparent;
            LoadDonHang(donHangSapHetHan);
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            // RefreshDataFromDb();
            btnQuaHan.FillColor = Color.FromArgb(90, 130, 90);
            btnSapHetHan.FillColor = Color.Transparent;
            LoadDonHang(donHangQuaHan);
        }

        // Nạp dữ liệu từ BLL và lọc theo Ngày kết thúc hợp đồng
        private void RefreshDataFromDb()
        {
            try
            {
                var dtHD = BLL_HopDong.GetAllHopDong();
                var dtDH = BLL_DonHang.GetAllDonHang();

                // Build index HopDongID -> info (NgayKetThuc, TenCongTy)
                var hdIndex = new Dictionary<string, HopDongInfo>(StringComparer.OrdinalIgnoreCase);
                if (dtHD != null)
                {
                    foreach (DataRow r in dtHD.Rows)
                    {
                        string id = ToStr(r, "HopDongID");
                        if (string.IsNullOrWhiteSpace(id)) continue;

                        DateTime? nkt = r["NgayKetThuc"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["NgayKetThuc"]);
                        string tenCty = dtHD.Columns.Contains("TenCongTy") ? ToStr(r, "TenCongTy") : null;

                        hdIndex[id] = new HopDongInfo { HopDongID = id, TenCongTy = tenCty, NgayKetThuc = nkt };
                    }
                }

                var today = DateTime.Today;
                var sapHet = new List<DonHang>();
                var quaHan = new List<DonHang>();

                if (dtDH != null)
                {
                    foreach (DataRow r in dtDH.Rows)
                    {
                        string hopDongId = FirstNonEmpty(r, "HopDongID");
                        if (string.IsNullOrWhiteSpace(hopDongId)) continue;
                        if (!hdIndex.TryGetValue(hopDongId, out var hd) || !hd.NgayKetThuc.HasValue) continue;

                        int days = (hd.NgayKetThuc.Value.Date - today).Days;

                        string maDon = FirstNonEmpty(r, "DonHangID", "MaDonHang", "MaDon") ?? "(N/A)";
                        string loaiDon = FirstNonEmpty(r, "TenLoaiDon", "LoaiDon") ?? "(N/A)";
                        string phong = FirstNonEmpty(r, "TenPhong", "Phong", "PhongBan", "PhongBanID") ?? "(N/A)";
                        string khach = string.IsNullOrWhiteSpace(hd.TenCongTy) ? "(N/A)" : hd.TenCongTy;

                        var item = new DonHang(maDon, khach, loaiDon, phong, hd.NgayKetThuc.Value, hopDongId);

                        // Sắp hết hạn: 0..3 ngày
                        if (days >= 0 && days <= 3) sapHet.Add(item);
                        // Quá hạn: < 0
                        else if (days < 0) quaHan.Add(item);
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

            var lbl = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                Text = $"Mã đơn hàng: {dh.MaDon}\nKhách hàng: {dh.KhachHang}\nLoại đơn: {dh.LoaiDon}\nPhòng: {dh.Phong}\nNgày hết hạn: {dh.NgayHetHan:dd-MM-yyyy}"
            };

            panel.Controls.Add(lbl);
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
