using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM_UI_QUANG
{
    public partial class dangnhap_matkhaumoi : Form
    {
        public dangnhap_matkhaumoi()
        {
            InitializeComponent();
        }

        private void dangnhap_matkhaumoi_Load(object sender, EventArgs e)
        {

        }

        private void nhap_hoten_email_TextChanged(object sender, EventArgs e)
        {
            guna2PictureBox2.Parent = nhap_hoten_email;
        }
    }
}
