using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace CNPM_UI_QUANG
{
    public class CirclePictureBoxWithBorder : Guna2CirclePictureBox
    {
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderThickness { get; set; } = 3;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(BorderColor, BorderThickness))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
