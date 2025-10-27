using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public class UserMenuPopup
    {
        private readonly Guna2ContextMenuStrip menu;
        private readonly Action onEditProfile;
        private readonly Action onLogout;

        public UserMenuPopup(Action editProfileHandler, Action logoutHandler)
        {
            onEditProfile = editProfileHandler;
            onLogout = logoutHandler;

            menu = new Guna2ContextMenuStrip();
            StyleMenu();

            var itemEdit = new ToolStripMenuItem("Chỉnh sửa thông tin");
            var itemLogout = new ToolStripMenuItem("Đăng xuất");

            itemEdit.Click += (s, e) => onEditProfile?.Invoke();
            itemLogout.Click += (s, e) => onLogout?.Invoke();

            menu.Items.Add(itemEdit);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(itemLogout);
        }

        private void StyleMenu()
        {
            menu.BackColor = Color.FromArgb(45, 45, 48);
            menu.ForeColor = Color.WhiteSmoke;
            menu.RenderStyle.BorderColor = Color.Gray;
            menu.RenderStyle.SelectionBackColor = Color.FromArgb(70, 130, 70);
            menu.RenderStyle.SelectionForeColor = Color.White;
            menu.RenderStyle.ArrowColor = Color.WhiteSmoke;
        }

        public void Show(Control parentButton)
        {
            if (parentButton == null || parentButton.IsDisposed) return;
            menu.Show(parentButton, new Point(0, parentButton.Height));
        }
    }
}
