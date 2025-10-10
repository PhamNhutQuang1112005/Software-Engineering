using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.QuanLyKhachHang
{
    public partial class QuanLyKhachHang : UserControl
    {
        public QuanLyKhachHang()
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        private void ApplyTheme()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(ApplyTheme));
                return;
            }

            // Gom tất cả các panel lại
            var panels = new[]
            {
        guna2GradientPanel1,
        guna2GradientPanel2,
        guna2GradientPanel3,
        guna2GradientPanel4
    };

            // 🌑 DARK MODE
            if (ThemeManager.IsDarkMode)
            {
                foreach (var p in panels)
                {
                    // Panel nền trong suốt
                    p.BackColor = Color.Transparent;
                    p.FillColor = ThemeManager.DarkBack;
                    p.FillColor2 = ThemeManager.DarkBack;

                    // Màu chữ trắng
                    foreach (var lbl in p.Controls.OfType<Label>())
                        lbl.ForeColor = ThemeManager.DarkText;

                    foreach (var lbl in p.Controls.OfType<Guna.UI2.WinForms.Guna2HtmlLabel>())
                        lbl.ForeColor = ThemeManager.DarkText;
                }
            }
            // 🌕 LIGHT MODE
            else
            {
                foreach (var p in panels)
                {
                    // Panel màu sáng pastel
                    p.FillColor = ThemeManager.LightFill1;
                    p.FillColor2 = ThemeManager.LightFill2;

                    // Màu chữ đen
                    foreach (var lbl in p.Controls.OfType<Label>())
                        lbl.ForeColor = ThemeManager.LightText;

                    foreach (var lbl in p.Controls.OfType<Guna.UI2.WinForms.Guna2HtmlLabel>())
                        lbl.ForeColor = ThemeManager.LightText;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            NutThem nutThem = new NutThem();
            nutThem.ShowDialog();
        }
    }

}
