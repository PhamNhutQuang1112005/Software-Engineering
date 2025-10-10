using System;
using System.Drawing;

namespace UI
{
    public static class ThemeManager
    {
        // Cờ kiểm tra Dark hay Light mode
        public static bool IsDarkMode { get; private set; } = true;

        // Sự kiện thông báo khi theme thay đổi
        public static event Action ThemeChanged;

        // Hàm toggle theme
        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            ThemeChanged?.Invoke(); // Báo cho tất cả UserControl
        }

        // Các màu có thể dùng lại
        public static Color LightFill1 = ColorTranslator.FromHtml("#c6d870"); // xanh-vàng pastel
        public static Color LightFill2 = ColorTranslator.FromHtml("#eff5d2"); // sáng hơn
        public static Color LightText = Color.Black;

        // 🌑 Màu nền Dark mode
        public static Color DarkBack = Color.Transparent;
        public static Color DarkText = Color.White;
    }
}
