using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI_main : Form
    {
        public GUI_main()
        {
            InitializeComponent();

            // Bật double buffering cho Form
            this.DoubleBuffered = true;

            // Bật double buffering cho các panel quan trọng
            SetDoubleBuffered(mainPanel);
            SetDoubleBuffered(contentPanel);
            SetDoubleBuffered(sidebar);
            SetDoubleBuffered(header);

            // Tắt shadow (Guna) có thể gây lag khi render
            TryDisableShadow(mainPanel);
            TryDisableShadow(contentPanel);
            TryDisableShadow(sidebar);
            TryDisableShadow(header);

            // Chuẩn bị danh sách nút sidebar (cache) để set theme nhanh
            CacheSidebarButtons();
        }

        // =======================
        // Trạng thái + UC instances
        // =======================
        private bool isDark = true;
        private UC_QuanLyUserS ucQuanLyUsers;
        private UC_QuanLyKhachHang ucQuanLyKhachHang;
        private UC_ThongKe ucThongKeDonHang;
        private UC_QuanLyDonHang ucDonHang;
        private UC_ChinhSuaThongTin 
            ucChinhSuaThongTin;

        // Cache danh sách nút trong sidebar để tránh lặp tìm control mỗi lần
        private List<Guna2Button> sidebarButtons = new List<Guna2Button>();

        private void GUI_main_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định: đặt hình nền ở mainPanel (không đặt ở contentPanel)
            // để tránh phải render lại background khi đổi UC.
            ApplyDarkTheme();
        }

        #region Theme (Dark / Light) - tối ưu để không vẽ nặng
        private void ApplyDarkTheme()
        {
            // Đặt background cho mainPanel (kiểm soát việc vẽ 1 lần)
            mainPanel.BackgroundImage = Properties.Resources.background__dark_;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;

            // contentPanel để trong suốt, UC sẽ hiển thị trên đó
            contentPanel.BackgroundImage = null;
            contentPanel.BackColor = Color.Transparent;

            // Header
            header.FillColor = Color.FromArgb(151, 176, 103);
            header.FillColor2 = Color.FromArgb(47, 82, 73);
            lblUser.ForeColor = Color.White;
            lblVersion.ForeColor = Color.White;

            // Sidebar
            sidebar.FillColor = Color.FromArgb(151, 176, 103);
            sidebar.FillColor2 = Color.FromArgb(47, 82, 73);

            // Buttons (dùng cache list)
            foreach (var btn in sidebarButtons)
            {
                btn.FillColor = Color.DarkSeaGreen;
                btn.ForeColor = Color.Black;
                // Tắt animation nặng nếu có
                btn.Animated = false;
            }

            // Icon
            guna2CircleButton1.Image = Properties.Resources.light_mode;
        }

        private void ApplyLightTheme()
        {
            mainPanel.BackgroundImage = Properties.Resources.background__light_;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;

            contentPanel.BackgroundImage = null;
            contentPanel.BackColor = Color.Transparent;

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
                btn.Animated = false;
            }

            guna2CircleButton1.Image = Properties.Resources.dark_mode;
        }
        #endregion

        #region Utility helpers
        private void SetDoubleBuffered(Control c)
        {
            // Sử dụng reflection để bật thuộc tính non-public DoubleBuffered
            try
            {
                System.Reflection.PropertyInfo pi = c.GetType().GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (pi != null)
                    pi.SetValue(c, true, null);
            }
            catch { /* không bắt lỗi, tiếp tục */ }
        }

        private void TryDisableShadow(Control c)
        {
            // Nếu là Guna2Panel / Guna2Control có ShadowDecoration, tắt nó để giảm render cost.
            try
            {
                var prop = c.GetType().GetProperty("ShadowDecoration");
                if (prop != null)
                {
                    var shadow = prop.GetValue(c);
                    var enabledProp = shadow.GetType().GetProperty("Enabled");
                    if (enabledProp != null)
                        enabledProp.SetValue(shadow, false);
                }
            }
            catch { /* ignore */ }
        }

        private void CacheSidebarButtons()
        {
            // Lấy tất cả Guna2Button trong sidebar một lần (Designer có thể chứa nhiều tầng control)
            sidebarButtons = sidebar.Controls.OfType<Guna2Button>().ToList();
        }
        #endregion

        #region ShowControl tối ưu (không Clear/Add liên tục)
        private void ShowControl(UserControl control)
        {
            if (control == null) return;

            // Ẩn tất cả UC hiện có (chỉ ẩn, không dispose)
            foreach (Control c in contentPanel.Controls.OfType<UserControl>())
            {
                c.Visible = false;
            }

            // Nếu UC chưa tồn tại trong contentPanel thì thêm vào 1 lần
            if (!contentPanel.Controls.Contains(control))
            {
                control.Dock = DockStyle.Fill;       // RẤT QUAN TRỌNG: set Dock cho UC
                control.Margin = new Padding(0);
                control.Padding = new Padding(0);
                control.BackColor = Color.Transparent; // để thấy nền mainPanel
                contentPanel.Controls.Add(control);
            }
            else
            {
                // Đảm bảo Dock = Fill nếu bị ghi đè
                control.Dock = DockStyle.Fill;
            }

            // Hiển thị và đưa lên trước (không remove các UC khác)
            control.Visible = true;
            control.BringToFront();
        }
        #endregion

        #region Event handlers đơn giản
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // Chuyển theme nhẹ nhàng (chỉ đổi màu/ảnh background ở mainPanel)
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

        private void contentPanel_Resize(object sender, EventArgs e){
            foreach (Control ctrl in contentPanel.Controls.OfType<UserControl>()){
                ctrl.SuspendLayout();
                ctrl.Dock = DockStyle.Fill;
                ctrl.Margin = Padding.Empty;
                ctrl.Padding = Padding.Empty;

        // Ép vừa chính xác phần client (loại bỏ border)
                ctrl.Width = contentPanel.ClientSize.Width;
                ctrl.Height = contentPanel.ClientSize.Height;
                ctrl.ResumeLayout();
            }
            foreach (Control ctrl in contentPanel.Controls)
    {
        Debug.WriteLine($"content: {contentPanel.ClientSize}, ctrl: {ctrl.Size}, dock: {ctrl.Dock}, autosize: {ctrl.AutoSize}");
    }
}

        // Các event không dùng thì để trống hoặc xóa nếu muốn
        private void header_Paint(object sender, PaintEventArgs e) { }
        private void contentPanel_Paint(object sender, PaintEventArgs e) { }
        private void mainPanel_Paint(object sender, PaintEventArgs e) { }
        private void sidebar_Paint(object sender, PaintEventArgs e) { }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e) { }
        private void btnThongbao_Click(object sender, EventArgs e) { }
        private void lblVersion_Click(object sender, EventArgs e) { }
        private void lblUser_Click(object sender, EventArgs e) { }
        #endregion

        #region Buttons -> show UC
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

        private void btnThongKeDonHang_Click(object sender, EventArgs e) { }
        private void btnThongKeTienDo_Click(object sender, EventArgs e) {
            if (ucThongKeDonHang == null)
                ucThongKeDonHang = new UC_ThongKe();
            ShowControl(ucThongKeDonHang);
        }
        private void btnDonHang_Click(object sender, EventArgs e) {
            if (ucDonHang == null)
                ucDonHang = new UC_QuanLyDonHang();
            ShowControl(ucDonHang);
        }
        private void btnHopDong_Click(object sender, EventArgs e) { }
        private void btnTrangChu_Click(object sender, EventArgs e) { }
        private void btnQuanLyUser_Click(object sender, EventArgs e) {
        if (ucChinhSuaThongTin == null)
                ucChinhSuaThongTin = new UC_ChinhSuaThongTin();
            ShowControl(ucChinhSuaThongTin);
        }
        #endregion
    }
}
