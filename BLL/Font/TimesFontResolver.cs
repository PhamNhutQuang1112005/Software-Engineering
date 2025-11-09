using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfSharp.Fonts;

namespace BLL.Font
{
    /// <summary>
    /// Font resolver cung cấp Times New Roman + Courier New (đủ 4 style) với fallback Arial / Segoe UI.
    /// Không bao giờ trả về mảng rỗng để tránh FileLoadException/SynchronizationLockException.
    /// </summary>
    public sealed class TimesFontResolver : IFontResolver
    {
        private static readonly string FontsDir = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        // Lazy load một lần
        private static readonly Lazy<FontSet> TimesSet = new Lazy<FontSet>(() => LoadSet(
            new[] { "times.ttf", "times new roman.ttf", "tnr.ttf" },
            new[] { "timesbd.ttf", "times new roman bold.ttf", "tnrb.ttf" },
            new[] { "timesi.ttf", "times new roman italic.ttf", "tnri.ttf" },
            new[] { "timesbi.ttf", "times new roman bold italic.ttf", "tnrbi.ttf" }));

        private static readonly Lazy<FontSet> CourierSet = new Lazy<FontSet>(() => LoadSet(
            new[] { "cour.ttf", "courier new.ttf", "courier.ttf" },
            new[] { "courbd.ttf", "courier new bold.ttf" },
            new[] { "couri.ttf", "courier new italic.ttf" },
            new[] { "courbi.ttf", "courier new bold italic.ttf" }));

        private static readonly Lazy<FontSet> ArialSet = new Lazy<FontSet>(() => LoadSet(
            new[] { "arial.ttf" },
            new[] { "arialbd.ttf", "arial bold.ttf" },
            new[] { "ariali.ttf", "arial italic.ttf" },
            new[] { "arialbi.ttf", "arial bold italic.ttf" }));

        private static readonly Lazy<FontSet> SegoeSet = new Lazy<FontSet>(() => LoadSet(
            new[] { "segoeui.ttf" },
            new[] { "segoeuib.ttf" },
            new[] { "segoeuii.ttf" },
            new[] { "segoeuiz.ttf" }));

        // Nhãn nội bộ cho từng face
        private const string FACE_TIMES_R = "TIMES#R";
        private const string FACE_TIMES_B = "TIMES#B";
        private const string FACE_TIMES_I = "TIMES#I";
        private const string FACE_TIMES_BI = "TIMES#BI";

        private const string FACE_COUR_R = "COUR#R";
        private const string FACE_COUR_B = "COUR#B";
        private const string FACE_COUR_I = "COUR#I";
        private const string FACE_COUR_BI = "COUR#BI";

        // Dùng chung: nếu family không nằm trong danh sách sẽ trả Times
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (string.IsNullOrWhiteSpace(familyName))
                familyName = "Times New Roman";

            // Chuẩn hóa để phân luồng
            string f = familyName.Trim();

            bool wantBold = isBold;
            bool wantItalic = isItalic;

            // Quyết định prefix nhóm
            string group;
            if (f.Equals("Times", StringComparison.OrdinalIgnoreCase) ||
                f.Equals("Times New Roman", StringComparison.OrdinalIgnoreCase))
                group = "TIMES";
            else if (f.Equals("Courier", StringComparison.OrdinalIgnoreCase) ||
                     f.Equals("Courier New", StringComparison.OrdinalIgnoreCase))
                group = "COUR";
            else if (f.Equals("Arial", StringComparison.OrdinalIgnoreCase))
                group = "TIMES"; // vẫn map về Times để đồng nhất PDF embed
            else if (f.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase))
                group = "TIMES"; // tương tự
            else
                group = "TIMES"; // fallback mặc định

            string face;
            if (group == "TIMES")
            {
                face = wantBold && wantItalic ? FACE_TIMES_BI
                     : wantBold ? FACE_TIMES_B
                     : wantItalic ? FACE_TIMES_I
                     : FACE_TIMES_R;
            }
            else // COUR
            {
                face = wantBold && wantItalic ? FACE_COUR_BI
                     : wantBold ? FACE_COUR_B
                     : wantItalic ? FACE_COUR_I
                     : FACE_COUR_R;
            }

            return new FontResolverInfo(face);
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case FACE_TIMES_R: return PickNonEmpty(TimesSet.Value.Regular, ArialSet.Value.Regular, SegoeSet.Value.Regular);
                case FACE_TIMES_B: return PickNonEmpty(TimesSet.Value.Bold, ArialSet.Value.Bold, SegoeSet.Value.Bold, TimesSet.Value.Regular);
                case FACE_TIMES_I: return PickNonEmpty(TimesSet.Value.Italic, ArialSet.Value.Italic, SegoeSet.Value.Italic, TimesSet.Value.Regular);
                case FACE_TIMES_BI:
                    return PickNonEmpty(TimesSet.Value.BoldItalic, ArialSet.Value.BoldItalic, SegoeSet.Value.BoldItalic,
                                                        TimesSet.Value.Bold, TimesSet.Value.Italic, TimesSet.Value.Regular);

                case FACE_COUR_R: return PickNonEmpty(CourierSet.Value.Regular, ArialSet.Value.Regular, TimesSet.Value.Regular);
                case FACE_COUR_B: return PickNonEmpty(CourierSet.Value.Bold, ArialSet.Value.Bold, CourierSet.Value.Regular);
                case FACE_COUR_I: return PickNonEmpty(CourierSet.Value.Italic, ArialSet.Value.Italic, CourierSet.Value.Regular);
                case FACE_COUR_BI:
                    return PickNonEmpty(CourierSet.Value.BoldItalic, ArialSet.Value.BoldItalic,
                                                        CourierSet.Value.Bold, CourierSet.Value.Italic, CourierSet.Value.Regular);
                default:
                    // Unknown tag => trả Times Regular
                    return PickNonEmpty(TimesSet.Value.Regular, ArialSet.Value.Regular, SegoeSet.Value.Regular);
            }
        }

        // ===== Helper structures =====
        private class FontSet
        {
            public byte[] Regular;
            public byte[] Bold;
            public byte[] Italic;
            public byte[] BoldItalic;
        }

        private static FontSet LoadSet(IEnumerable<string> reg, IEnumerable<string> bold, IEnumerable<string> ital, IEnumerable<string> boldItal)
        {
            return new FontSet
            {
                Regular = TryList(reg),
                Bold = TryList(bold),
                Italic = TryList(ital),
                BoldItalic = TryList(boldItal)
            };
        }

        private static byte[] TryList(IEnumerable<string> candidates)
        {
            if (candidates == null) return null;
            foreach (var f in candidates)
            {
                var bytes = TryLoadFontFile(f);
                if (bytes != null && bytes.Length > 0) return bytes;
            }
            return null;
        }

        private static byte[] TryLoadFontFile(string fileName)
        {
            try
            {
                var full = Path.Combine(FontsDir, fileName);
                if (File.Exists(full))
                {
                    var data = File.ReadAllBytes(full);
                    if (data.Length > 0) return data;
                }

                // Case-insensitive / partial search
                var shortKey = Path.GetFileNameWithoutExtension(fileName);
                var hit = Directory.EnumerateFiles(FontsDir, "*.ttf")
                                   .FirstOrDefault(p => Path.GetFileName(p).IndexOf(shortKey, StringComparison.OrdinalIgnoreCase) >= 0);
                if (hit != null)
                {
                    var data = File.ReadAllBytes(hit);
                    if (data.Length > 0) return data;
                }
            }
            catch { }
            return null;
        }

        private static byte[] PickNonEmpty(params byte[][] sets)
        {
            foreach (var s in sets)
            {
                if (s != null && s.Length > 0) return s;
            }
            // Never return empty
            return new byte[] { 1 };
        }
    }
}