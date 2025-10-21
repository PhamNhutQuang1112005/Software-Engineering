using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    // Base form giúp giảm lag khi load (áp dụng cho Guna UI2)
    public class SmoothFormBase : Form
    {
        public SmoothFormBase()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            // Kích hoạt DoubleBuffered cho toàn form
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);

            // Gọi hàm áp dụng cho các panel con
            EnableDoubleBufferForChildren(this);

            // Ẩn form lúc khởi tạo, chỉ hiển thị khi load xong
            this.Opacity = 0;
            this.Load += SmoothFormBase_Load;
        }

        // Tắt shadow (đặc biệt là Guna UI2 shadow decoration)
        protected void DisableShadow(Control c)
        {
            try
            {
                var prop = c.GetType().GetProperty("ShadowDecoration");
                var shadow = prop?.GetValue(c);
                var enabledProp = shadow?.GetType().GetProperty("Enabled");
                enabledProp?.SetValue(shadow, false);
            }
            catch { /* ignore */ }
        }

        // Tự bật double buffer cho mọi control con
        private void EnableDoubleBufferForChildren(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                try
                {
                    var pi = c.GetType().GetProperty("DoubleBuffered",
                        BindingFlags.Instance | BindingFlags.NonPublic);
                    pi?.SetValue(c, true, null);
                }
                catch { }

                EnableDoubleBufferForChildren(c);
            }
        }

        // Khi form load xong → fade in mượt
        private async void SmoothFormBase_Load(object sender, EventArgs e)
        {
            for (double i = 0; i <= 1; i += 0.05)
            {
                this.Opacity = i;
                await Task.Delay(10);
            }
            this.Opacity = 1;
        }

        // Chống nhấp nháy toàn bộ form
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
