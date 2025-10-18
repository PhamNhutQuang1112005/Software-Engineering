using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_main : Form
    {
        // ==============
        // Biến lớp
        // ==============
        private bool isDark = true;
        private List<Guna2Button> sidebarButtons = new List<Guna2Button>();
        private UserControl currentUC;

        // Các UC
        private UC_QuanLyUserS ucQuanLyUsers;
        private UC_QuanLyKhachHang ucQuanLyKhachHang;
        private UC_ThongKe ucThongKeDonHang;
        private UC_QuanLyDonHang ucDonHang;
        private UC_ChinhSuaThongTin ucChinhSuaThongTin;
        private UC_QuanLyHopDong ucQuanLyHopDong;
        private UC_QuanLyThongSoDonHang ucQuanLyThongSoDonHang;
        private UC_TrangChu ucTrangChu;

        // Lớp layout riêng
        private Panel backgroundLayer;
        private Panel contentLayer;
        private PictureBox logoCenter;

        // ==============
        // Constructor
        // ==============
        public GUI_main()
        {
            InitializeComponent();

            // Bật double buffering cho Form và các panel
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetDoubleBuffered(mainPanel);
            SetDoubleBuffered(contentPanel);
            SetDoubleBuffered(sidebar);
            SetDoubleBuffered(header);

            // Tắt shadow của Guna UI2
            TryDisableShadow(mainPanel);
            TryDisableShadow(contentPanel);
            TryDisableShadow(sidebar);
            TryDisableShadow(header);

            CacheSidebarButtons();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        // ==============
        // Form Load
        // ==============
        private void GUI_main_Load(object sender, EventArgs e)
        {
            
            InitContentLayout(); // tạo layout chuẩn
            ApplyDarkTheme();
        }

        // ============================
        // Khởi tạo layout contentPanel
        // ============================
        private void InitContentLayout()
{
    // Nếu đã khởi tạo rồi thì không cần tạo lại
    if (backgroundLayer != null && contentLayer != null && logoCenter != null)
        return;

    contentPanel.Controls.Clear();

    // 1️⃣ Tạo lớp nền
    backgroundLayer = new Panel
    {
        Dock = DockStyle.Fill,
        BackgroundImage = Properties.Resources.background__dark_,
        BackgroundImageLayout = ImageLayout.Stretch
    };

    // 2️⃣ Tạo logo giữa
    logoCenter = new PictureBox
    {
        Image = Properties.Resources.logo_bg_removebg_preview,
        SizeMode = PictureBoxSizeMode.Zoom,
        Size = new Size(280, 280),
        BackColor = Color.Transparent
    };

    // 3️⃣ Tạo lớp chứa UC
    contentLayer = new Panel
    {
        Dock = DockStyle.Fill,
        BackColor = Color.Transparent
    };

    // 4️⃣ Thêm các lớp theo thứ tự: nền -> logo -> UC
    contentPanel.Controls.Add(backgroundLayer);
    contentPanel.Controls.Add(logoCenter);
    contentPanel.Controls.Add(contentLayer);

    // 5️⃣ Canh logo giữa panel
    contentPanel.Resize += (s, e) =>
    {
        if (logoCenter != null)
        {
            logoCenter.Location = new Point(
                (contentPanel.Width - logoCenter.Width) / 2,
                (contentPanel.Height - logoCenter.Height) / 2
            );
        }
    };

    // 6️⃣ Đặt thứ tự hiển thị
    backgroundLayer.SendToBack();  // nền dưới cùng
    logoCenter.BringToFront();     // logo ở giữa
    contentLayer.BringToFront();   // UC nằm trên cùng (nhưng có thể trong suốt)
}

        private void ShowControl(UserControl uc)
        {
            if (uc == null) return;

            if (!contentLayer.Controls.Contains(uc))
            {
                uc.Dock = DockStyle.Fill;
                uc.AutoScaleMode = AutoScaleMode.None;
                uc.BackColor = Color.Transparent;
                contentLayer.Controls.Add(uc);
            }

            // Ẩn UC cũ
            if (currentUC != null && currentUC != uc)
                currentUC.Visible = false;

            // Hiện UC mới
            uc.Visible = true;
            uc.BringToFront();

            currentUC = uc;

            // Ẩn logo khi không ở trang chủ
            logoCenter.Visible = (uc == ucTrangChu);
        }

        // ====================
        // Theme (Dark / Light)
        // ====================
        private void ApplyDarkTheme()
        {
            mainPanel.BackgroundImage = Properties.Resources.background__dark_;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;

            header.FillColor = Color.FromArgb(151, 176, 103);
            header.FillColor2 = Color.FromArgb(47, 82, 73);
            lblUser.ForeColor = Color.White;
            lblVersion.ForeColor = Color.White;

            sidebar.FillColor = Color.FromArgb(151, 176, 103);
            sidebar.FillColor2 = Color.FromArgb(47, 82, 73);

            foreach (var btn in sidebarButtons)
            {
                btn.FillColor = Color.DarkSeaGreen;
                btn.ForeColor = Color.Black;
            }

            guna2CircleButton1.Image = Properties.Resources.light_mode;
            backgroundLayer.BackgroundImage = Properties.Resources.background__dark_;
            if (backgroundLayer != null)
    backgroundLayer.BackgroundImage = Properties.Resources.background__dark_;
if (logoCenter != null)
    logoCenter.Image = Properties.Resources.logo_bg_removebg_preview;

        }

        private void ApplyLightTheme()
        {
            mainPanel.BackgroundImage = Properties.Resources.background__light_;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;

            header.FillColor = ColorTranslator.FromHtml("#c6d870");
            header.FillColor2 = ColorTranslator.FromHtml("#eff5d2");
            lblUser.ForeColor = Color.Black;
            lblVersion.ForeColor = Color.DimGray;

            sidebar.FillColor = ColorTranslator.FromHtml("#c6d870");
            sidebar.FillColor2 = ColorTranslator.FromHtml("#eff5d2");

            foreach (var btn in sidebarButtons)
            {
                btn.FillColor = Color.LemonChiffon;
                btn.ForeColor = Color.Black;
            }

            guna2CircleButton1.Image = Properties.Resources.dark_mode;
            backgroundLayer.BackgroundImage = Properties.Resources.background__light_;
            if (backgroundLayer != null)
    backgroundLayer.BackgroundImage = Properties.Resources.background__light_;
if (logoCenter != null)
    logoCenter.Image = Properties.Resources.logo_bg_removebg_preview;

        }

        // =========================
        // Helpers (Buffer + Shadow)
        // =========================
        private void SetDoubleBuffered(Control c)
        {
            try
            {
                PropertyInfo pi = c.GetType().GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                pi?.SetValue(c, true, null);
            }
            catch { }
        }

        private void TryDisableShadow(Control c)
        {
            try
            {
                var prop = c.GetType().GetProperty("ShadowDecoration");
                var shadow = prop?.GetValue(c);
                var enabledProp = shadow?.GetType().GetProperty("Enabled");
                enabledProp?.SetValue(shadow, false);
            }
            catch { }
        }

        private void CacheSidebarButtons()
        {
            sidebarButtons = sidebar.Controls.OfType<Guna2Button>().ToList();
        }

        // ===============
        // Sidebar Buttons
        // ===============
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

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (ucTrangChu == null)
                ucTrangChu = new UC_TrangChu();
            ShowControl(ucTrangChu);
        }

        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            if (ucQuanLyUsers == null)
                ucQuanLyUsers = new UC_QuanLyUserS();
            ShowControl(ucQuanLyUsers);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            if (ucQuanLyKhachHang == null)
                ucQuanLyKhachHang = new UC_QuanLyKhachHang();
            ShowControl(ucQuanLyKhachHang);
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            if (ucDonHang == null)
                ucDonHang = new UC_QuanLyDonHang();
            ShowControl(ucDonHang);
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            if (ucQuanLyHopDong == null)
                ucQuanLyHopDong = new UC_QuanLyHopDong();
            ShowControl(ucQuanLyHopDong);
        }

        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            if (ucThongKeDonHang == null)
                ucThongKeDonHang = new UC_ThongKe();
            ShowControl(ucThongKeDonHang);
        }

        private void btnThongKeDonHang_Click(object sender, EventArgs e)
        {
            if (ucQuanLyThongSoDonHang == null)
                ucQuanLyThongSoDonHang = new UC_QuanLyThongSoDonHang();
            ShowControl(ucQuanLyThongSoDonHang);
        }

        private void btnQuanLyUser_Click(object sender, EventArgs e)
        {
            if (ucChinhSuaThongTin == null)
                ucChinhSuaThongTin = new UC_ChinhSuaThongTin();
            ShowControl(ucChinhSuaThongTin);
        }

        // =================
        // Popup thông báo
        // =================
        private void btnThongbao_Click(object sender, EventArgs e)
        {
            UC_PopupThongBao popup = new UC_PopupThongBao();

            Form frm = new Form
            {
                Size = popup.Size,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.FromArgb(40, 70, 55),
                Opacity = 0,
                Location = new Point(this.Right - popup.Width - 30, this.Top + 80)
            };
            frm.Controls.Add(popup);

            // Animation fade-in
            Timer t = new Timer { Interval = 15 };
            t.Tick += (s, ev) =>
            {
                frm.Opacity += 0.05;
                if (frm.Opacity >= 0.95) t.Stop();
            };
            t.Start();

            frm.Deactivate += (s, ev) => frm.Close();
            frm.Show();
        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
