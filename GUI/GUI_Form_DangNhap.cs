using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GUI

{
    public partial class GUI_Form_DangNhap : Form
    {
        public GUI_Form_DangNhap()
        {
            InitializeComponent();
            SmoothUI.Apply(this);
            guna2TabControl1.Region = new Region(new Rectangle(0, 20, guna2TabControl1.Width, guna2TabControl1.Height - 5));

            guna2TabControl1.Region = new Region(new Rectangle(0, 10, guna2TabControl1.Width, guna2TabControl1.Height - 90));




            this.label10.Click += new System.EventHandler(this.label10_Click);
            this.label11.Click += new System.EventHandler(this.label11_Click);
            guna2TabControl1.Appearance = TabAppearance.FlatButtons;
            guna2TabControl1.ItemSize = new Size(0, 1);
            guna2TabControl1.SizeMode = TabSizeMode.Fixed;
            guna2TabControl1.SelectedTab = tabPage3;
            guna2TabControl1.BackColor = Color.Transparent;
            guna2TabControl1.TabStop = false;
            guna2TabControl1.Region = new Region(new Rectangle(0, 20, guna2TabControl1.Width, guna2TabControl1.Height - 5));

            guna2TabControl1.Region = new Region(new Rectangle(0, 10, guna2TabControl1.Width, guna2TabControl1.Height - 90));


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void hoten_email_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void matkhau_Click(object sender, EventArgs e)
        {

        }

        private void anh_minhhoa_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ĐăngNhập_Click(object sender, EventArgs e)
        {
            
        }

        private void ĐăngKí_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
guna2TabControl1.SelectedTab = tabPage2;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage2;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage1;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage3;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage3;
        }

        private void GUI_Form_DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage4;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage1;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            guna2TabControl1.SelectedTab = tabPage6;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GUI_main gm = new GUI_main();
            gm.ShowDialog();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QuênMậtKhẩu_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            GUI_main gm = new GUI_main();
            gm.ShowDialog();
        }
    }
}
