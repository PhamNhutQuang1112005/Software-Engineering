using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class UC_ThongKe : UserControl
    {
        private readonly Btnbeautifull _theme = new Btnbeautifull()
        {
            Text = Color.White,
            Outline = Color.FromArgb(120, 195, 170),
            SearchFill = Color.Azure,
            SearchText = Color.Black,
            SearchPlaceholder = Color.Black
        };
        public UC_ThongKe()
        {
            InitializeComponent();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {
            
            
            PillStyler.Combo(guna2ComboBox1, _theme);
            PillStyler.Combo(guna2ComboBox2, _theme);
            PillStyler.Combo(guna2ComboBox3, _theme);
            PillStyler.Combo(guna2ComboBox4, _theme);
            
        }
    }
}
