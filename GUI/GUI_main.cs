using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
namespace GUI
{
    public partial class GUI_main : Form
    {
        // ============== Biến lớp ==============
        private bool isDark = true;
        private List<Guna2Button> sidebarButtons = new List<Guna2Button>();
        private UserControl currentUC;
        private readonly BLL_TaiKhoan bllTaiKhoan = new BLL_TaiKhoan();
        // Các UC
        private UC_QuanLyUserS ucQuanLyUsers;
        private UC_QuanLyKhachHang ucQuanLyKhachHang;
        private UC_ThongKe ucThongKeDonHang;
        private UC_QuanLyDonHang ucDonHang;
        private UC_ChinhSuaThongTin ucChinhSuaThongTin;
        private UC_QuanLyHopDong ucQuanLyHopDong;
        private UC_QuanLyThongSoDonHang ucQuanLyThongSoDonHang;
        private UC_TrangChu ucTrangChu;
        private bool isAdmin = false;
        // Lớp layout riêng
        private Panel backgroundLayer;   // nền ảnh
        private Panel contentLayer;      // chứa UC
        private PictureBox logoCenter;   // logo ở giữa
        //Lớp Đăng nhập
        private readonly string TenDangNhap;
        
        // ============== Constructor ==============
        public GUI_main()
        {
            InitializeComponent();
            btnQuanLyUsers.Visible = false;  // ẩn mặc định cho chắc
            btnQuanLyUsers.Enabled = false;
            TenDangNhap = string.Empty;
            

            // Double buffering toàn form + panel chính
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetDoubleBuffered(mainPanel);
            SetDoubleBuffered(contentPanel);
            SetDoubleBuffered(sidebar);
            SetDoubleBuffered(header);

            // Tắt shadow của một số control Guna
            TryDisableShadow(mainPanel);
            TryDisableShadow(contentPanel);
            TryDisableShadow(sidebar);
            TryDisableShadow(header);

            CacheSidebarButtons();
        }
        public GUI_main(string id) : this()
{
    // Lưu lại thông tin người đăng nhập
    TenDangNhap  = id ?? string.Empty;
    
}
        // Chống nhấp nháy cho toàn form
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        // ============== Form Load ==============
        private void GUI_main_Load(object sender, EventArgs e)
        {
            InitContentLayout(); // tạo layout chuẩn cho contentPanel
            ApplyDarkTheme();    // mặc định dark
            ResolveUserHeaderFromUsername();
            // Mặc định mở trang chủ (nếu muốn)
            if (ucTrangChu == null) ucTrangChu = new UC_TrangChu();
            ShowControl(ucTrangChu);
            
    
        }

        // ============================ Khởi tạo layout contentPanel ============================
        private void InitContentLayout()
        {
            if (contentPanel == null) return;

            contentPanel.SuspendLayout();
            try
            {
                contentPanel.Controls.Clear();

                // Lớp nền
                backgroundLayer = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                SetDoubleBuffered(backgroundLayer);

                // Lớp nội dung (trong suốt)
                contentLayer = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };
                SetDoubleBuffered(contentLayer);

                // Logo trung tâm (hiện khi ở Trang chủ)
                logoCenter = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Anchor = AnchorStyles.None,
                    BackColor = Color.Transparent,
                    Image = Properties.Resources.logo_bg_removebg_preview,
                    Size = new Size(360, 240) // tùy chỉnh
                };

                

                // Ghép lớp: nền -> nội dung -> logo (logo nằm trên contentLayer)
                contentPanel.Controls.Add(contentLayer);
                contentPanel.Controls.Add(backgroundLayer);
        
                contentPanel.PerformLayout();
            }
            finally
            {
                contentPanel.ResumeLayout(true);
            }

        }
        // ============================ Hiển thị Tên Đăng nhập ============================
       private void ResolveUserHeaderFromUsername()
{
    if (string.IsNullOrWhiteSpace(TenDangNhap)) return;

    var info = bllTaiKhoan.GetUserHeaderByUsername(TenDangNhap);
    if (info == null)
    {
        lblUser.Text = TenDangNhap;
        isAdmin = false;
        btnQuanLyUsers.Visible = false;
        btnQuanLyUsers.Enabled = false;
       
        return;
    }

    
lblUser.Text = $"Xin chào {info.HoVaTen}";


    // chỉ admin khi VaiTroID == "VT001"
    isAdmin = string.Equals(info.VaiTroID, "VT001", StringComparison.OrdinalIgnoreCase);

    btnQuanLyUsers.Visible = isAdmin;   // ⟵ ẩn hẳn khi không phải admin
    btnQuanLyUsers.Enabled = isAdmin;

    
}


        // ============================ Hiển thị UC ============================
        private void ShowControl(UserControl uc)
        {
            if (uc == null || contentLayer == null) return;

            contentLayer.SuspendLayout();
            try
            {
                // Thêm UC nếu chưa có parent
                if (uc.Parent != contentLayer)
                {
                    uc.Dock = DockStyle.Fill;
                    uc.AutoScaleMode = AutoScaleMode.None;
                    uc.BackColor = Color.Transparent;
                    uc.Visible = false; // tạm ẩn tránh nháy
                    contentLayer.Controls.Add(uc);
                }

                // Ẩn UC cũ
                if (currentUC != null && currentUC != uc && !currentUC.IsDisposed)
                    currentUC.Visible = false;

                // Hiện UC mới
                uc.Visible = true;
                uc.BringToFront();
                currentUC = uc;

                // Logo chỉ hiện ở Trang chủ
                bool isHome = (uc == ucTrangChu)
                              || uc is UC_TrangChu
                              || string.Equals(uc.Name, "UC_TrangChu", StringComparison.OrdinalIgnoreCase);

                if (logoCenter != null && !logoCenter.IsDisposed)
                    logoCenter.Visible = isHome;
            }
            finally
            {
                contentLayer.ResumeLayout(true);
            }
        }

        // ==================== Theme (Dark / Light) ====================
        private void ApplyDarkTheme()
        {
            // Nền cho contentPanel (không để ở mainPanel để tránh chồng)
            if (contentPanel.BackgroundImage != null)
            {
                contentPanel.BackgroundImage = Properties.Resources.Sườn_UI__dark_;
                contentPanel.BackgroundImageLayout = ImageLayout.Stretch; // hoặc Zoom
            }

            // Xóa nền chỗ khác để không chồng ảnh
            if (mainPanel != null)
            {
                mainPanel.BackgroundImage = null;
                mainPanel.BackgroundImageLayout = ImageLayout.None;
            }

            // Header / Sidebar
            header.FillColor = Color.FromArgb(151, 176, 103);
            header.FillColor2 = Color.FromArgb(47, 82, 73);
            lblUser.ForeColor = Color.White;
            lblVersion.ForeColor = Color.White;

            sidebar.FillColor = Color.FromArgb(151, 176, 103);
            sidebar.FillColor2 = Color.FromArgb(47, 82, 73);

            foreach (var btn in sidebarButtons)
            {
                // Bật chế độ radio để tự quản checked
                btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;

                // Màu thường
                btn.FillColor = Color.DarkSeaGreen;
                btn.ForeColor = Color.Black;

                // Màu hover
                btn.HoverState.FillColor = Color.WhiteSmoke;
                btn.HoverState.ForeColor = Color.ForestGreen;

                // Màu khi được chọn
                btn.CheckedState.FillColor = Color.White;
                btn.CheckedState.ForeColor = Color.ForestGreen;
            }

            guna2CircleButton1.Image = Properties.Resources.light_mode;

            if (logoCenter != null)
                logoCenter.Image = Properties.Resources.logo_bg_removebg_preview;

            isDark = true;
        }

        private void ApplyLightTheme()
        {
            if (contentPanel != null)
            {
                contentPanel.BackgroundImage = Properties.Resources.Sườn_UI__light_;
                contentPanel.BackgroundImageLayout = ImageLayout.Stretch; // hoặc Zoom
            }

            if (mainPanel != null)
            {
                mainPanel.BackgroundImage = null;
                mainPanel.BackgroundImageLayout = ImageLayout.None;
            }

            header.FillColor = ColorTranslator.FromHtml("#c6d870");
            header.FillColor2 = ColorTranslator.FromHtml("#eff5d2");
            lblUser.ForeColor = Color.Black;
            lblVersion.ForeColor = Color.DimGray;

            sidebar.FillColor = ColorTranslator.FromHtml("#c6d870");
            sidebar.FillColor2 = ColorTranslator.FromHtml("#eff5d2");

            foreach (var btn in sidebarButtons)
            {
                btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;

                // Màu thường
                btn.FillColor = Color.LemonChiffon;
                btn.ForeColor = Color.Black;

                // Màu hover
                btn.HoverState.FillColor = Color.WhiteSmoke;
                btn.HoverState.ForeColor = Color.ForestGreen;

                // Màu khi được chọn
                btn.CheckedState.FillColor = Color.White;
                btn.CheckedState.ForeColor = Color.ForestGreen;
            }

            guna2CircleButton1.Image = Properties.Resources.dark_mode;

            if (logoCenter != null)
                logoCenter.Image = Properties.Resources.logo_bg_removebg_preview;

            isDark = false;
        }

        // ========================= Helpers (Buffer + Shadow) =========================
        private void SetDoubleBuffered(Control c)
        {
            if (c == null) return;
            try
            {
                var pi = c.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi?.SetValue(c, true, null);
            }
            catch { /* ignore */ }
        }

        private void TryDisableShadow(Control c)
        {
            if (c == null) return;
            try
            {
                var prop = c.GetType().GetProperty("ShadowDecoration");
                var shadow = prop?.GetValue(c);
                var enabledProp = shadow?.GetType().GetProperty("Enabled");
                enabledProp?.SetValue(shadow, false);
            }
            catch { /* ignore */ }
        }

        private void CacheSidebarButtons()
        {
            sidebarButtons = GetAllDescendants(sidebar).OfType<Guna2Button>().ToList();
        }

        // Duyệt tất cả control con (kể cả lồng sâu)
        private static IEnumerable<Control> GetAllDescendants(Control root)
        {
            if (root == null) yield break;
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var child in GetAllDescendants(c)) yield return child;
            }
        }

        // Đặt trạng thái nút đang hoạt động trên sidebar
        private void SetActiveSidebarButton(Guna2Button btn)
        {
            if (btn == null) return;

            // Bỏ chọn tất cả
            foreach (var b in sidebarButtons)
            {
                if (b == null || b.IsDisposed) continue;
                b.Checked = false;
            }

            // Chọn nút hiện tại
            btn.Checked = true;
        }

        // =============== Sidebar Buttons ===============
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (isDark) ApplyLightTheme();
            else ApplyDarkTheme();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (ucTrangChu == null) ucTrangChu = new UC_TrangChu();
            ShowControl(ucTrangChu);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnQuanLyUsers_Click(object sender, EventArgs e)
        {
            if (ucQuanLyUsers == null) ucQuanLyUsers = new UC_QuanLyUserS();
            ShowControl(ucQuanLyUsers);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            if (ucQuanLyKhachHang == null) ucQuanLyKhachHang = new UC_QuanLyKhachHang();
            ShowControl(ucQuanLyKhachHang);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            if (ucDonHang == null) ucDonHang = new UC_QuanLyDonHang();
            ShowControl(ucDonHang);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            if (ucQuanLyHopDong == null) ucQuanLyHopDong = new UC_QuanLyHopDong();
            ShowControl(ucQuanLyHopDong);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            if (ucThongKeDonHang == null) ucThongKeDonHang = new UC_ThongKe();
            ShowControl(ucThongKeDonHang);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnThongKeDonHang_Click(object sender, EventArgs e)
        {
            if (ucQuanLyThongSoDonHang == null) ucQuanLyThongSoDonHang = new UC_QuanLyThongSoDonHang();
            ShowControl(ucQuanLyThongSoDonHang);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        private void btnQuanLyUser_Click(object sender, EventArgs e)
        {
            if (ucChinhSuaThongTin == null) ucChinhSuaThongTin = new UC_ChinhSuaThongTin(TenDangNhap);
            ShowControl(ucChinhSuaThongTin);
            SetActiveSidebarButton(sender as Guna2Button);
        }

        // ================= Popup thông báo =================
        private void btnThongbao_Click(object sender, EventArgs e)
        {
            var popup = new UC_PopupThongBao();

            var frm = new Form
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

            var t = new Timer { Interval = 15 };
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
            // Không cần vẽ tay, đã có backgroundLayer lo hình nền.
        }

        private void lblUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
