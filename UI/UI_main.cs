using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        private QuanLyUSers.QuanLyUsers ucQuanLyUsers;
        private ThongKeTienDo.ThongKe ucThongKeTienDo;
        private QuanLyHopDong.QuanLyHopDong ucHopDong;
        public QuanLyUSers.ThongBao thongBaoUC;
        private QuanLyDonHang.QuanLyDonHang ucDonHang;
        private QuanLyKhachHang.QuanLyKhachHang ucKhachHang;
        private ChinhSuaThongTin.ChinhSuaUser ucChinhSuaThongTinUser;
        // ========================
        // 🏗️ Hàm khởi tạo form chính
        // ========================
        public UI_main()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
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
            foreach (var btn in this.sidebar.Controls.OfType<Guna.UI2.WinForms.Guna2Button>())
            {
                btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;

                // 🌈 Màu mặc định
                btn.FillColor = System.Drawing.Color.DarkSeaGreen;
                btn.ForeColor = System.Drawing.Color.Black;

                // 🌟 Khi hover
                btn.HoverState.FillColor = System.Drawing.Color.WhiteSmoke;
                btn.HoverState.ForeColor = System.Drawing.Color.ForestGreen;

                // ✅ Khi được chọn
                btn.CheckedState.FillColor = System.Drawing.Color.White;
                btn.CheckedState.ForeColor = System.Drawing.Color.ForestGreen;

                // ✨ Hiệu ứng bóng sáng khi chọn
                btn.ShadowDecoration.Enabled = true;
                btn.ShadowDecoration.Color = System.Drawing.Color.ForestGreen;
                btn.ShadowDecoration.Depth = 5;
                btn.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            }
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
            // Ẩn (hoặc gỡ bỏ) toàn bộ UC cũ
            foreach (Control c in contentPanel.Controls.OfType<UserControl>())
            {
                c.Visible = false;
            }

            // Nếu UC này chưa tồn tại thì thêm vào
            if (!contentPanel.Controls.Contains(control))
            {
                control.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(control);
            }

            // Hiển thị UC mới
            control.Visible = true;
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
            ThemeManager.ToggleTheme();
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
            if (ucKhachHang == null)
                ucKhachHang = new QuanLyKhachHang.QuanLyKhachHang();
            ShowControl(ucKhachHang);
        }

        // 👉 Nút Hợp Đồng
        private void btnHopDong_Click(object sender, EventArgs e)
        {
            if (ucHopDong == null)
                ucHopDong = new QuanLyHopDong.QuanLyHopDong();
            ShowControl(ucHopDong);
        }

        // 👉 Nút Đơn Hàng
        private void btnDonHang_Click(object sender, EventArgs e)
        {
            if (ucDonHang == null)
                ucDonHang = new QuanLyDonHang.QuanLyDonHang();
            ShowControl(ucDonHang);
        }

        // 👉 Nút Thống Kê Tiến Độ
        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            if (ucThongKeTienDo == null)
                ucThongKeTienDo = new ThongKeTienDo.ThongKe();
            ShowControl(ucThongKeTienDo);
        }

        // 👉 Nút Thống Kê Đơn Hàng
        private void btnThongKeDonHang_Click(object sender, EventArgs e)
        {

        }

     

        // 👉 Nút Quản Lý Users (nhiều user)
        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            if (ucQuanLyUsers == null)
                ucQuanLyUsers = new QuanLyUSers.QuanLyUsers();
            ShowControl(ucQuanLyUsers);
        }

        // ========================
        // Các sự kiện phụ (không quan trọng)
        // ========================
        private void sidebar_Paint(object sender, PaintEventArgs e) { }
        private void contentPanel_Paint(object sender, PaintEventArgs e) { }
        private void lblUser_Click(object sender, EventArgs e) { }
        private Timer thongBaoTimer = new Timer();
        private bool isThongBaoVisible = false;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (thongBaoUC == null)
            {
                thongBaoUC = new QuanLyUSers.ThongBao
                {
                    Width = 425,
                    Height = this.ClientSize.Height,
                    Top = 0,
                    Left = this.ClientSize.Width,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                this.Controls.Add(thongBaoUC);
                thongBaoUC.BringToFront();

                thongBaoTimer.Interval = 10;
                thongBaoTimer.Tick += (s, ev) =>
                {
                    int speed = 25;
                    int visibleX = this.ClientSize.Width - thongBaoUC.Width;
                    int hiddenX = this.ClientSize.Width;

                    if (isThongBaoVisible)
                    {
                        if (thongBaoUC.Left > visibleX)
                            thongBaoUC.Left -= speed;
                        else
                        {
                            thongBaoUC.Left = visibleX;
                            thongBaoTimer.Stop();
                        }
                    }
                    else
                    {
                        if (thongBaoUC.Left < hiddenX)
                            thongBaoUC.Left += speed;
                        else
                        {
                            thongBaoUC.Left = hiddenX;
                            thongBaoTimer.Stop();
                            thongBaoUC.Visible = false;
                        }
                    }
                };
            }

            if (!isThongBaoVisible)
            {
                thongBaoUC.Visible = true;
                thongBaoUC.BringToFront();
            }

            isThongBaoVisible = !isThongBaoVisible;
            thongBaoTimer.Start();
        }

        // 🧩 Khi form thay đổi kích thước (phóng to / thu nhỏ)
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (thongBaoUC == null) return;

            int visibleX = this.ClientSize.Width - thongBaoUC.Width;
            int hiddenX = this.ClientSize.Width;

            thongBaoUC.Height = this.ClientSize.Height;
            thongBaoUC.Left = isThongBaoVisible ? visibleX : hiddenX;
        }



        private void guna2CirclePictureBox1_Click(object sender, EventArgs e) { }

        private void btnQuanLyUser_Click_1(object sender, EventArgs e)
        {
            if (ucChinhSuaThongTinUser == null)
                ucChinhSuaThongTinUser = new ChinhSuaThongTin.ChinhSuaUser();
            ShowControl(ucChinhSuaThongTinUser);
        }
    }
}
