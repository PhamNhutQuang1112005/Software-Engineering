using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UI.TrangChu;

namespace UI
{
    public partial class UI_main : Form
    {
        // ========================
        // ⚙️ Biến trạng thái theme hiện tại (true = Dark mode, false = Light mode)
        // ========================
        private bool isDark = true;

        // ========================
        // 📂 Biến lưu các UserControl để tái sử dụng (tránh tạo lại mỗi lần bấm)
        // ========================
        private TrangChu.Trangcur1 ucTrangChu;
        

        // ========================
        // 🏗️ Hàm khởi tạo form chính
        // ========================
        public UI_main()
        {
            InitializeComponent();
        }

        // ========================
        // 🔗 Các button public để form khác có thể truy cập
        // ========================
        public Guna2Button BtnTrangChu => btnTrangChu;
        public Guna2Button BtnKhachHang => btnKhachHang;
        public Guna2Button BtnHopDong => btnHopDong;
        public Guna2Button BtnDonHang => btnDonHang;
        public Guna2Button BtnThongKeTienDo => btnThongKeTienDo;
        public Guna2Button BtnThongKeDonHang => btnThongKeDonHang;
        public Guna2Button BtnQuanLyUsers => btnQuanLyUsers;
        public Guna2Button BtnQuanLyUser => btnQuanLyUser;
        public Guna2Button BtnThongBao => btnThongbao;

        // ========================
        // 🚀 Sự kiện khi Form load lần đầu
        // ========================
        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Khi khởi động, bật Dark mode mặc định
            ApplyDarkTheme();

            // Tạo UserControl Trang Chủ lần đầu và hiển thị
            ucTrangChu = new TrangChu.Trangcur1();
            ucTrangChu.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(ucTrangChu);
            ucTrangChu.BringToFront(); // Đưa lên trên cùng
        }

        // ========================
        // 🧩 Hàm hiển thị UserControl (chuyển trang)
        // ========================
        private void ShowControl(UserControl control)
        {
            // Đảm bảo UserControl chiếm toàn bộ panel nội dung
            control.Dock = DockStyle.Fill;

            // Nếu chưa có trong contentPanel thì thêm vào
            if (!contentPanel.Controls.Contains(control))
                contentPanel.Controls.Add(control);

            // Đưa control cần hiển thị lên trên cùng
            control.BringToFront();
        }

        // ========================
        // 🌑 Áp dụng giao diện Dark mode
        // ========================
        private void ApplyDarkTheme()
        {
            // Màu nền chính
            mainPanel.BackColor = Color.FromArgb(30, 40, 30);
            contentPanel.BackgroundImage = Properties.Resources.background__dark_;

            // Header (phần đầu trang)
            header.FillColor = Color.FromArgb(151, 176, 103);
            header.FillColor2 = Color.FromArgb(47, 82, 73);
            lblUser.ForeColor = Color.White;
            lblVersion.ForeColor = Color.White;

            // Sidebar (thanh bên trái)
            sidebar.FillColor = Color.FromArgb(151, 176, 103);
            sidebar.FillColor2 = Color.FromArgb(47, 82, 73);

            // Nút trong sidebar
            foreach (var btn in sidebar.Controls.OfType<Guna2Button>())
            {
                btn.FillColor = Color.DarkSeaGreen;
                btn.ForeColor = Color.Black;
            }

            // Đổi icon nút chuyển theme sang biểu tượng Light
            guna2CircleButton1.Image = Properties.Resources.light_mode;
        }

        // ========================
        // 🌕 Áp dụng giao diện Light mode
        // ========================
        private void ApplyLightTheme()
        {
            // Nền chính
            mainPanel.BackColor = Color.WhiteSmoke;
            contentPanel.BackgroundImage = Properties.Resources.background__light_;

            // Header gradient sáng (#c6d870 → #eff5d2)
            header.FillColor = ColorTranslator.FromHtml("#c6d870");
            header.FillColor2 = ColorTranslator.FromHtml("#eff5d2");
            lblUser.ForeColor = Color.Black;
            lblVersion.ForeColor = Color.DimGray;

            // Sidebar sáng tương tự
            sidebar.FillColor = ColorTranslator.FromHtml("#c6d870");
            sidebar.FillColor2 = ColorTranslator.FromHtml("#eff5d2");

            // Nút trong sidebar nền sáng, chữ đen
            foreach (var btn in sidebar.Controls.OfType<Guna2Button>())
            {
                btn.FillColor = Color.LemonChiffon;
                btn.ForeColor = Color.Black;
            }

            // Đổi icon nút chuyển theme sang biểu tượng Dark
            guna2CircleButton1.Image = Properties.Resources.dark_mode;
        }

        // ========================
        // 🔁 Xử lý khi bấm nút chuyển theme
        // ========================
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (isDark)
            {
                ApplyLightTheme();
                isDark = false;
            }
            else
            {
                ApplyDarkTheme();
                isDark = true;
            }
        }

        // ========================
        // 🎯 Sự kiện các nút menu bên trái
        // ========================

        // 👉 Nút Trang Chủ
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (ucTrangChu == null)
                ucTrangChu = new TrangChu.Trangcur1();

            ShowControl(ucTrangChu);
        }

        // 👉 Nút Khách Hàng
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Khách Hàng, tạo và hiển thị tại đây
        }

        // 👉 Nút Hợp Đồng
        private void btnHopDong_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Hợp Đồng, tạo và hiển thị tại đây
        }

        // 👉 Nút Đơn Hàng
        private void btnDonHang_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Đơn Hàng, tạo và hiển thị tại đây
        }

        // 👉 Nút Thống Kê Tiến Độ
        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Thống Kê Tiến Độ, tạo và hiển thị tại đây
        }

        // 👉 Nút Thống Kê Đơn Hàng
        private void btnThongKeDonHang_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Thống Kê Đơn Hàng, tạo và hiển thị tại đây
        }

        // 👉 Nút Quản Lý User (1 user)
        private void btnQuanLyUser_Click(object sender, EventArgs e)
        {
            // TODO: Khi có form Quản Lý User, tạo và hiển thị tại đây
        }

        // 👉 Nút Quản Lý Users (nhiều user)
        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            
        }

        // ========================
        // Các sự kiện phụ (không quan trọng)
        // ========================
        private void sidebar_Paint(object sender, PaintEventArgs e) { }
        private void contentPanel_Paint(object sender, PaintEventArgs e) { }
        private void lblUser_Click(object sender, EventArgs e) { }
        private void guna2Button1_Click(object sender, EventArgs e) { }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e) { }
    }
}
