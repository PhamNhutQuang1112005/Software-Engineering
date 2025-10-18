using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

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

            // Dữ liệu mẫu
            donHangSapHetHan = new List<DonHang>
            {
                new DonHang("DH001", "Lê Văn Toàn", "Định kỳ", "Thí nghiệm", "20-5-2025"),
                new DonHang("DH003", "Nguyễn Thị Mai", "Theo Quý", "Khí tượng", "20-5-2029")
            };

            donHangQuaHan = new List<DonHang>
            {
                new DonHang("DH006", "Phạm Văn A", "Định kỳ", "Thí nghiệm", "20-9-2023")
            };
        }

        private void UC_PopupThongBao_Load(object sender, EventArgs e)
        {
            btnSapHetHan.PerformClick(); // mặc định tab đầu tiên
        }

        private void btnSapHetHan_Click(object sender, EventArgs e)
        {
            btnSapHetHan.FillColor = Color.FromArgb(90, 130, 90);
            btnQuaHan.FillColor = Color.Transparent;
            LoadDonHang(donHangSapHetHan);
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            btnQuaHan.FillColor = Color.FromArgb(90, 130, 90);
            btnSapHetHan.FillColor = Color.Transparent;
            LoadDonHang(donHangQuaHan);
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
                Size = new Size(380, 80),
                BackColor = Color.FromArgb(35, 60, 45),
                Margin = new Padding(5)
            };

            var lbl = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.White,
                Text = $"Mã đơn hàng: {dh.MaDon}\nKhách hàng: {dh.KhachHang}\nLoại đơn: {dh.LoaiDon}\nPhòng: {dh.Phong}\nNgày hết hạn: {dh.NgayHetHan}"
            };

            panel.Controls.Add(lbl);
            return panel;
        }
    }

    // Class mô phỏng dữ liệu đơn hàng
    public class DonHang
    {
        public string MaDon { get; set; }
        public string KhachHang { get; set; }
        public string LoaiDon { get; set; }
        public string Phong { get; set; }
        public string NgayHetHan { get; set; }

        public DonHang(string ma, string kh, string loai, string phong, string ngay)
        {
            MaDon = ma;
            KhachHang = kh;
            LoaiDon = loai;
            Phong = phong;
            NgayHetHan = ngay;
        }
    }
}
