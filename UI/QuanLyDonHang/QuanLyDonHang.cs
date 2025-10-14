using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.QuanLyDonHang
{
    public partial class QuanLyDonHang : UserControl
    {
        public QuanLyDonHang()
        {
            InitializeComponent();
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Them f = new Them();
            f.ShowDialog();
        }
    }
}
