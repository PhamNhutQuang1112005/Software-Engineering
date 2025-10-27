// PillStyler.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    // Chủ đề màu dùng chung
    public class Btnbeautifull
    {
        public Color Text { get; set; } = Color.WhiteSmoke;
        public Color Outline { get; set; } = Color.DodgerBlue;

        // Cho ô tìm kiếm
        public Color SearchFill { get; set; } = Color.Azure;
        public Color SearchText { get; set; } = Color.Black;
        public Color SearchPlaceholder { get; set; } = Color.Black;

        // Màu menu item ComboBox (Dark)
        public Color ItemBack { get; set; } = Color.FromArgb(25, 25, 25);
        public Color ItemFore { get; set; } = Color.WhiteSmoke;
        public Color ItemSelectedBack { get; set; } = Color.FromArgb(45, 45, 45);
        public Font ButtonFont { get; set; } = new Font("Segoe UI", 14f, FontStyle.Bold);
    }

    public static class PillStyler
    {
        // ====== API công khai ======

        public static void Button(Guna2Button btn, Btnbeautifull theme)
        {
            if (btn == null || theme == null) return;

            btn.BackColor   = Color.Transparent;
            btn.FillColor   = Color.Transparent;
            btn.ForeColor   = theme.Text;

            btn.BorderColor = theme.Outline;
            btn.BorderThickness = 1;
             btn.Font = theme.ButtonFont;
            btn.AutoRoundedCorners = true;
            btn.BorderRadius = Math.Max(18, btn.Height / 2);

            btn.HoverState.FillColor   = Color.FromArgb(24, theme.Outline);
            btn.HoverState.BorderColor = theme.Outline;
            btn.PressedColor           = Color.FromArgb(40, theme.Outline);

            btn.ShadowDecoration.Enabled = false;

            WireResize(btn);
        }

        public static void Combo(Guna2ComboBox cb, Btnbeautifull theme)
        {
            if (cb == null || theme == null) return;

            cb.BackColor     = Color.Transparent;
            cb.FillColor     = Color.FromArgb(0, 0, 0, 0); // trong suốt
            cb.BorderColor   = theme.Outline;
            cb.ForeColor     = theme.Text;

            cb.AutoRoundedCorners = true;
            cb.BorderRadius = Math.Max(18, cb.Height / 2);

            cb.DrawMode = DrawMode.OwnerDrawFixed;
            cb.ItemHeight = 30;

            cb.FocusedColor = theme.Outline;
            cb.FocusedState.BorderColor = theme.Outline;

            cb.ItemsAppearance.BackColor         = theme.ItemBack;
            cb.ItemsAppearance.ForeColor         = theme.ItemFore;
            cb.ItemsAppearance.SelectedBackColor = theme.ItemSelectedBack;

            cb.ShadowDecoration.Enabled = false;

            WireResize(cb);
        }

        public static void SearchBox(Guna2TextBox txt, Btnbeautifull theme, string placeholder)
        {
            if (txt == null || theme == null) return;

            txt.BackColor   = Color.Transparent;
            txt.FillColor   = theme.SearchFill;
            txt.BorderColor = theme.Outline;
            txt.ForeColor   = theme.SearchText;

            txt.PlaceholderText      = placeholder ?? "";
            txt.PlaceholderForeColor = theme.SearchPlaceholder;

            txt.AutoRoundedCorners = true;
            txt.BorderRadius = Math.Max(20, txt.Height / 2);
            txt.FocusedState.BorderColor = theme.Outline;

            txt.ShadowDecoration.Enabled = false;

            WireResize(txt, minRadius: 20);
        }

        /// <summary>
        /// Quét toàn bộ control con và áp style theo loại (Button, ComboBox, TextBox).
        /// onlyTagged=true: chỉ áp cho control có Tag chứa "pill".
        /// Với TextBox, nếu Tag chứa "search" thì áp kiểu SearchBox.
        /// </summary>
        public static void ApplyAll(Control root, Btnbeautifull theme, bool onlyTagged = false)
        {
            if (root == null || theme == null) return;

            foreach (Control c in GetAllChildren(root))
            {
                if (onlyTagged && !(c.Tag is string s && s.IndexOf("pill", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;

                if (c is Guna2Button b)
                {
                    Button(b, theme);
                }
                else if (c is Guna2ComboBox cb)
                {
                    Combo(cb, theme);
                }
                else if (c is Guna2TextBox tb && (tb.Tag?.ToString()?.Contains("search") == true))
                {
                    SearchBox(tb, theme, tb.PlaceholderText);
                }
            }
        }

        // ====== Helpers ======

        private static void WireResize(Control c, int minRadius = 18)
        {
            c.SizeChanged -= OnSizeChanged;
            c.SizeChanged += OnSizeChanged;
            c.Tag = MergeTag(c.Tag, $"minr:{minRadius}");
        }

        private static void OnSizeChanged(object sender, EventArgs e)
        {
            switch (sender)
            {
                case Guna2Button b:
                    b.AutoRoundedCorners = true;
                    b.BorderRadius = Math.Max(ParseMinRadius(b.Tag, 18), b.Height / 2);
                    break;
                case Guna2ComboBox cb:
                    cb.AutoRoundedCorners = true;
                    cb.BorderRadius = Math.Max(ParseMinRadius(cb.Tag, 18), cb.Height / 2);
                    break;
                case Guna2TextBox t:
                    t.AutoRoundedCorners = true;
                    t.BorderRadius = Math.Max(ParseMinRadius(t.Tag, 20), t.Height / 2);
                    break;
            }
        }

        private static int ParseMinRadius(object tag, int fallback)
        {
            if (tag is string s)
            {
                foreach (var part in s.Split(';'))
                {
                    if (part.StartsWith("minr:", StringComparison.OrdinalIgnoreCase) &&
                        int.TryParse(part.Substring(5), out var val))
                        return val;
                }
            }
            return fallback;
        }

        private static string MergeTag(object oldTag, string add)
        {
            var old = oldTag?.ToString();
            if (string.IsNullOrEmpty(old)) return add;
            if (old.Contains(add)) return old;
            return old + ";" + add;
        }

        private static System.Collections.Generic.IEnumerable<Control> GetAllChildren(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (var cc in GetAllChildren(c))
                    yield return cc;
            }
        }
    }
}
