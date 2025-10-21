using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public static class SmoothUI
    {
        public static void Apply(Form form)
        {
            // Bật DoubleBuffered qua reflection (tránh lỗi access)
            SetDoubleBuffered(form, true);

            // Bật cho các control con
            EnableDoubleBufferForChildren(form);

            // Hiệu ứng fade-in nhẹ khi load
            form.Opacity = 0;
            form.Load += async (s, e) =>
            {
                for (double i = 0; i <= 1; i += 0.05)
                {
                    form.Opacity = i;
                    await Task.Delay(10);
                }
                form.Opacity = 1;
            };
        }

        private static void EnableDoubleBufferForChildren(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                SetDoubleBuffered(c, true);
                EnableDoubleBufferForChildren(c);
            }
        }

        private static void SetDoubleBuffered(Control c, bool value)
        {
            try
            {
                typeof(Control)
                    .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(c, value, null);
            }
            catch { /* ignore */ }
        }
    }
}
