using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
    public partial class UC_QuanLyKhachHang : UserControl
    {
        public UC_QuanLyKhachHang()
        {
            InitializeComponent();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            GUI_FormThemKhach gUI_FormThemKhach = new GUI_FormThemKhach();
            gUI_FormThemKhach.ShowDialog();
        }

        private void UC_QuanLyKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}
