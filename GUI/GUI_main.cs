﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

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
        private UC_ChinhSuaThongTin ucChinhSuaThongTin;
        private UC_QuanLyHopDong ucQuanLyHopDong;
        private UC_TrangChu ucTrangChu;
        private List<Guna2Button> sidebarButtons = new List<Guna2Button>();
        private UserControl currentUC;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED - double buffer toàn form
                return cp;
            }
        }

        private void GUI_main_Load(object sender, EventArgs e)
        {
            ApplyDarkTheme();
        }

        #region Theme (Dark / Light)
        private void ApplyDarkTheme()
        {
            mainPanel.BackgroundImage = Properties.Resources.background__dark_;
            mainPanel.BackgroundImageLayout = ImageLayout.Stretch;

            contentPanel.BackgroundImage = null;
            contentPanel.BackColor = Color.Transparent;

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
                btn.Animated = false;
            }

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

        #region Utility Helpers
        private void SetDoubleBuffered(Control c)
        {
            try
            {
                PropertyInfo pi = c.GetType().GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                if (pi != null)
                    pi.SetValue(c, true, null);
            }
            catch { }
        }

        private void TryDisableShadow(Control c)
        {
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
            catch { }
        }

        private void CacheSidebarButtons()
        {
            sidebarButtons = sidebar.Controls.OfType<Guna2Button>().ToList();
        }
        #endregion

        #region ShowControl + CenterLayout
        private void ShowControl(UserControl uc)
        {
            if (uc == null) return;

            if (!contentPanel.Controls.Contains(uc))
            {
                uc.AutoScaleMode = AutoScaleMode.None;
                uc.Anchor = AnchorStyles.None;
                uc.BackColor = Color.Transparent;

                CenterControl(uc);
                contentPanel.Controls.Add(uc);
            }

            if (currentUC != null && currentUC != uc)
                currentUC.Visible = false;

            uc.Visible = true;
            uc.BringToFront();
            CenterControl(uc);
            currentUC = uc;
        }

        private void CenterControl(Control ctrl)
        {
            if (ctrl == null || contentPanel == null) return;

            int x = (contentPanel.ClientSize.Width - ctrl.Width) / 2;
            int y = (contentPanel.ClientSize.Height - ctrl.Height) / 2;

            if (ctrl.Width > contentPanel.ClientSize.Width || ctrl.Height > contentPanel.ClientSize.Height)
            {
                ctrl.Dock = DockStyle.Fill;
            }
            else
            {
                ctrl.Dock = DockStyle.None;
                ctrl.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
        }
        #endregion

        #region Events
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

        private void contentPanel_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in contentPanel.Controls.OfType<UserControl>())
            {
                if (ctrl.Visible)
                    CenterControl(ctrl);
            }
        }
        #endregion

        #region Buttons -> Show UC
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

        private void btnThongKeTienDo_Click(object sender, EventArgs e)
        {
            if (ucThongKeDonHang == null)
                ucThongKeDonHang = new UC_ThongKe();
            ShowControl(ucThongKeDonHang);
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

        private void btnQuanLyUser_Click(object sender, EventArgs e)
        {
            if (ucChinhSuaThongTin == null)
                ucChinhSuaThongTin = new UC_ChinhSuaThongTin();
            ShowControl(ucChinhSuaThongTin);
        }
        #endregion

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            if (ucTrangChu == null)
                ucTrangChu = new UC_TrangChu();
            ShowControl(ucTrangChu);
        }
    }
}
