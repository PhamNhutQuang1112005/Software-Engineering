// ================================================
// SmoothFlowLayoutPanel.cs  (.NET Framework 4.7.2)
// FlowLayoutPanel cuộn mượt, hạn chế flicker, không "rách nền" khi cuộn.
// ================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Common
{
    /// <summary>
    /// FlowLayoutPanel bật double-buffer + WS_EX_COMPOSITED
    /// và tự vẽ lại nền của tổ tiên (ancestor) theo offset cuộn,
    /// giúp nền không bị "xé" khi scroll.
    /// </summary>
    public class SmoothFlowLayoutPanel : FlowLayoutPanel
    {
        public SmoothFlowLayoutPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true; // ok vì đang ở subclass

            AutoScroll = true;
            WrapContents = true;
        }

        /// <summary>
        /// Thêm WS_EX_COMPOSITED để giảm flicker toàn cây control.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate(); // ép vẽ lại nền khi đổi kích thước
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            SuspendLayout();
            base.OnScroll(se);
            ResumeLayout();
            Invalidate();             // vẽ lại chính nó
            Parent?.Invalidate(true); // và cha (trường hợp gradient/bo góc)
        }

        /// <summary>
        /// Vẽ lại BackgroundImage của ancestor theo vị trí cuộn.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Control bgOwner = FindBackgroundOwner(this);
            if (bgOwner != null && bgOwner.BackgroundImage != null)
            {
                Point off = AutoScrollPosition;               // X/Y âm khi cuộn
                Point rel = PointToAncestor(bgOwner, this);   // panel so với owner

                e.Graphics.TranslateTransform(off.X + rel.X, off.Y + rel.Y);

                Image img = bgOwner.BackgroundImage;
                Rectangle rect = bgOwner.ClientRectangle;

                switch (bgOwner.BackgroundImageLayout)
                {
                    case ImageLayout.Stretch:
                        e.Graphics.DrawImage(img, rect);
                        break;
                    case ImageLayout.Center:
                        var pt = new Point(
                            rect.Left + (rect.Width - img.Width) / 2,
                            rect.Top + (rect.Height - img.Height) / 2);
                        e.Graphics.DrawImage(img, pt);
                        break;
                    case ImageLayout.None:
                    default:
                        e.Graphics.DrawImage(img, rect.Location);
                        break;
                }

                e.Graphics.ResetTransform();
                return; // đã tự vẽ nền, không gọi base
            }

            base.OnPaintBackground(e);
        }

        private static Control FindBackgroundOwner(Control start)
        {
            Control cur = start.Parent;
            while (cur != null)
            {
                if (cur.BackgroundImage != null)
                    return cur;
                cur = cur.Parent;
            }
            return null;
        }

        // Vector từ ancestor tới child (để dịch nền đúng vị trí)
        private static Point PointToAncestor(Control ancestor, Control child)
        {
            Point p = Point.Empty;
            Control cur = child.Parent;
            while (cur != null && cur != ancestor)
            {
                p.Offset(cur.Left, cur.Top);
                cur = cur.Parent;
            }
            return new Point(-p.X, -p.Y);
        }

        /// <summary>
        /// (Tuỳ chọn) Bước cuộn nhỏ hơn cho cảm giác mượt.
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int delta = Math.Sign(e.Delta) * 40; // 40px mỗi notch
            AutoScrollPosition = new Point(-AutoScrollPosition.X,
                                           -(AutoScrollPosition.Y + delta));
        }
    }
}
