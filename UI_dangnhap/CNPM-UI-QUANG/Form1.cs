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

namespace CNPM_UI_QUANG
{
    public partial class Form1 : Form
    {
        private Color normalColor = ColorTranslator.FromHtml("#fddc59");
        private Color hoverColor = ColorTranslator.FromHtml("#ffdc4f");
        private Color clickColor = ColorTranslator.FromHtml("#dbbf4e");

        public Form1()
        {
            InitializeComponent();
            // gắn event Paint cho button1 để custom vẽ
            this.btndangnhap.Paint += new PaintEventHandler(this.button1_Paint);

            // gắn sự kiện hover
            this.btndangnhap.MouseEnter += (s, e) =>
            {
                btndangnhap.BackColor = hoverColor;
                btndangnhap.Invalidate(); // vẽ lại
            };

            this.btndangnhap.MouseLeave += (s, e) =>
            {
                btndangnhap.BackColor = normalColor;
                btndangnhap.Invalidate();
            };

            this.btndangnhap.MouseDown += (s, e) =>
            {
                btndangnhap.BackColor = clickColor;
                btndangnhap.Invalidate();
            };

            this.btndangnhap.MouseUp += (s, e) =>
            {
                btndangnhap.BackColor = hoverColor;
                btndangnhap.Invalidate();
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int radius = 40;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btndangnhap.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btndangnhap.Width - radius, btndangnhap.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btndangnhap.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            // áp region bo cong cho button
            btndangnhap.Region = new Region(path);

            // set style để tự vẽ
            btndangnhap.FlatStyle = FlatStyle.Flat;
            btndangnhap.FlatAppearance.BorderSize = 0; // tắt viền mặc định
            btndangnhap.BackColor = normalColor;
            btndangnhap.ForeColor = Color.Black;
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            int radius = 40;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btndangnhap.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btndangnhap.Width - radius, btndangnhap.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btndangnhap.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // tô nền bo góc (theo BackColor hiện tại)
            using (SolidBrush brush = new SolidBrush(btndangnhap.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // vẽ viền bo góc (đen, dày 2px)
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }

            // vẽ chữ
            TextRenderer.DrawText(e.Graphics, btndangnhap.Text, btndangnhap.Font,
                btndangnhap.ClientRectangle, btndangnhap.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
    }
}
