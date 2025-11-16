using System;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Shapes;
using PdfSharp.Fonts;
using PdfSharp.Drawing;
using BLL.Font;

namespace BLL
{
    public static class BLL_PDFExporter
    {
        private const string FontName = "Times New Roman";
        // two separate logos: small (upper-left) and large (center watermark)
        private const string LogoSmallRelativePath = "icon\\logo bg1.png";
        private const string LogoLargeRelativePath = "icon\\logo bg2.png";
        private static readonly object RenderLock = new object();

        public static bool ExportKetQuaPhanTich(string donHangID,
                                                string tenLoaiMauList,
                                                DataTable thongSoTable,
                                                string filePath)
        {
            if (string.IsNullOrWhiteSpace(donHangID)) return false;
            if (string.IsNullOrWhiteSpace(filePath)) return false;
            if (thongSoTable == null || thongSoTable.Rows.Count == 0) return false;

            lock (RenderLock)
            {
                if (GlobalFontSettings.FontResolver == null)
                    GlobalFontSettings.FontResolver = new TimesFontResolver();

                var dtDonHang = BLL_DonHang.GetAllDonHang();
                var dtHopDong = BLL_HopDong.GetAllHopDong();
                var dtKhachHang = BLL_KhachHang.GetAllKhachHang();
                var bllThongSo = new BLL_ThongSoQuanTrac();
                var dtViTri = bllThongSo.GetViTriByDonHang(donHangID);

                var donHangRow = dtDonHang?.AsEnumerable()
                    .FirstOrDefault(r => string.Equals(Convert.ToString(r["DonHangID"]), donHangID, StringComparison.OrdinalIgnoreCase));
                if (donHangRow == null) return false;

                string maDonHang = Safe(donHangRow, "MaDonHang");
                string hopDongID = Safe(donHangRow, "HopDongID");
                string ngayLayMau = DateFormatted(donHangRow, "NgayLayMau");
                string ngayTraKQ = DateFormatted(donHangRow, "NgayDuKienTraKetQua");
                string diaDiemQuanTrac = Safe(donHangRow, "DiaChi");

                var hopDongRow = dtHopDong?.AsEnumerable()
                    .FirstOrDefault(r => string.Equals(Convert.ToString(r["HopDongID"]), hopDongID, StringComparison.OrdinalIgnoreCase));
                string khachHangID = hopDongRow != null ? Safe(hopDongRow, "KhachHangID") : string.Empty;

                var khRow = dtKhachHang?.AsEnumerable()
                    .FirstOrDefault(r => string.Equals(Convert.ToString(r["KhachHangID"]), khachHangID, StringComparison.OrdinalIgnoreCase));
                string tenKhachHang = khRow != null ? Safe(khRow, "TenCongTy") : string.Empty;
                string diaChiKhachHang = khRow != null ? Safe(khRow, "DiaChi") : string.Empty;

                // Prefer the lower label field (MaViTri) when present; fallback to DiaChi (not TenViTri as requested)
                var viTriTenList = dtViTri?.AsEnumerable()
                    .Select(r =>
                    {
                        string ma = dtViTri.Columns.Contains("MaViTri") ? Safe(r, "MaViTri") : string.Empty;
                        string diachi = dtViTri.Columns.Contains("DiaChi") ? Safe(r, "DiaChi") : string.Empty;
                        return !string.IsNullOrWhiteSpace(ma) ? ma : diachi;
                    })
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Take(3)
                    .ToList() ?? new System.Collections.Generic.List<string>();

                var doc = new Document();
                ConfigureDocument(doc);
                var section = doc.AddSection();
                ConfigureSection(section);

                // Add two logos (small logo reserved for header cell only)
                AddLogosSimple(doc, section, LogoSmallRelativePath, LogoLargeRelativePath);

                AddHeader(section);
                AddTitle(section);
                AddInfoBlock(section, maDonHang, tenKhachHang, diaChiKhachHang, diaDiemQuanTrac,
                             tenLoaiMauList, viTriTenList, ngayLayMau, ngayTraKQ);

                // Add table (pass position names so headers show actual values)
                AddThongSoTable(section, thongSoTable, viTriTenList);
                AddChuThich(section);
                AddFooterKy(section);

                var renderer = new PdfDocumentRenderer(unicode: true) { Document = doc };
                renderer.RenderDocument();

                // Draw background image under existing page content using PdfSharp (prepend draws beneath content)
                var bgPath = ResolveImagePath(LogoLargeRelativePath);
                if (!string.IsNullOrWhiteSpace(bgPath) && File.Exists(bgPath))
                {
                    var pdf = renderer.PdfDocument;
                    foreach (var page in pdf.Pages)
                    {
                        try
                        {
                            using (var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend))
                            using (var ximg = XImage.FromFile(bgPath))
                            {
                                double desiredWidthPt = XUnit.FromCentimeter(12).Point;
                                double aspect = ximg.PixelHeight / (double)ximg.PixelWidth;
                                double desiredHeightPt = desiredWidthPt * aspect;

                                double x = (page.Width.Point - desiredWidthPt) / 2.0;
                                double y = (page.Height.Point - desiredHeightPt) / 2.0; // centered on page

                                gfx.DrawImage(ximg, x, y, desiredWidthPt, desiredHeightPt);
                            }
                        }
                        catch { /* ignore per-page errors */ }
                    }
                }

                renderer.PdfDocument.Save(filePath);
            }

            try
            {
                // Try to write export record to DB — do not fail the export if logging fails
                BLL_TapKetQua.LogPdfExport(donHangID, filePath);
            }
            catch (Exception /*ex*/)
            {
           
            }

            return true;
        }

        // Add two logos: keep only large centered watermark here; small logo will be placed inside header table cell to align side-by-side
        private static void AddLogosSimple(Document doc, Section section, string smallRelative, string largeRelative)
        {
            var smallPath = ResolveImagePath(smallRelative);
            var largePath = ResolveImagePath(largeRelative);
            doc.Info.Subject = "LogoSmall=" + (smallPath ?? "NOT_FOUND") + ";LogoLarge=" + (largePath ?? "NOT_FOUND");

            var header = section.Headers.Primary;

            // Do not add small/large logo here to avoid layout mismatch; small logo will be placed inside header table cell in AddHeader.

            if (smallPath == null && largePath == null)
            {
                var debugP = header.AddParagraph("(NO LOGO FOUND: " + smallRelative + " & " + largeRelative + ")");
                debugP.Format.Font.Size = 8;
                debugP.Format.Font.Color = Colors.Red;
            }
        }

        private static string ResolveImagePath(string relative)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(relative)) return null;
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var candidates = new[]
                {
                    relative,
                    Path.Combine(baseDir, relative),
                    Path.Combine(baseDir, "icon", Path.GetFileName(relative)),
                    Path.Combine(baseDir, "icon", "logo bg.png"),
                    Path.Combine(baseDir, "icon", "logo bg2.png"),
                    Path.GetFullPath(Path.Combine(baseDir, "..", "..", "GUI", "icon", Path.GetFileName(relative))),
                };
                foreach (var c in candidates)
                {
                    if (!string.IsNullOrWhiteSpace(c) && File.Exists(c))
                        return Path.GetFullPath(c);
                }
                return null;
            }
            catch { return null; }
        }

        private static void ConfigureDocument(Document doc)
        {
            doc.Info.Author = "Hệ thống";
            doc.Info.Title = "Kết quả phân tích";
            var style = doc.Styles["Normal"];
            style.Font.Name = FontName;
            style.Font.Size = 12;
            style.Font.Color = Colors.Black;
        }

        private static void ConfigureSection(Section section)
        {
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Portrait;
            section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            section.PageSetup.RightMargin = Unit.FromCentimeter(2);
            section.PageSetup.TopMargin = Unit.FromCentimeter(2);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2);
        }

        // Header rendered as 2-column table so text doesn't overlap the small logo. Email updated to greensol@gmail.com
        private static void AddHeader(Section section)
        {
            double pageWcm = section.PageSetup.PageWidth.Centimeter;
            double leftCm = section.PageSetup.LeftMargin.Centimeter;
            double rightCm = section.PageSetup.RightMargin.Centimeter;
            double printableCm = Math.Max(10.0, pageWcm - leftCm - rightCm);

            double logoColCm = 3.8; // reserve for small logo
            double extraRightOverflowCm = 6.5; // allow text column to stretch beyond right margin
            double textColCm = Math.Max(6.0, printableCm - logoColCm + extraRightOverflowCm);

            // Use a fixed visual height matching logo (approx). Keep in sync with logo width ratio; use 3.8cm height.
            double headerHeightCm = 3.8;

            var tbl = section.AddTable();
            tbl.Borders.Width = 0;
            tbl.AddColumn(Unit.FromCentimeter(logoColCm));
            tbl.AddColumn(Unit.FromCentimeter(textColCm));

            var row = tbl.AddRow();
            row.Height = Unit.FromCentimeter(headerHeightCm);
            row.HeightRule = RowHeightRule.Exactly;

            // left cell reserved for logo area (place small logo inside the cell)
            var leftCell = row.Cells[0];
            leftCell.VerticalAlignment = VerticalAlignment.Center;
            var smallPath = ResolveImagePath(LogoSmallRelativePath);
            if (!string.IsNullOrWhiteSpace(smallPath))
            {
                var pImg = leftCell.AddParagraph();
                var img = pImg.AddImage(smallPath);
                img.LockAspectRatio = true;
                img.Width = Unit.FromCentimeter(3.5);
                pImg.Format.Alignment = ParagraphAlignment.Left;
                pImg.Format.SpaceBefore = 0;
                pImg.Format.SpaceAfter = 0;
            }
            else
            {
                leftCell.AddParagraph(string.Empty);
            }

            // Right cell: company name and new address, vertically centered and right-aligned
            var right = row.Cells[1];
            right.VerticalAlignment = VerticalAlignment.Center;

            var pName = right.AddParagraph("CÔNG TY CỔ PHẦN KIỂM NGHIỆM MÔI TRƯỜNG GREENSOL VIỆT NAM");
            pName.Format.Font.Size = 12;
            pName.Format.Font.Bold = true;
            pName.Format.Alignment = ParagraphAlignment.Right;
            pName.Format.SpaceBefore = Unit.FromCentimeter(0);

            var pAddr = right.AddParagraph("Địa chỉ: 19 Đ. Nguyễn Hữu Thọ, Tân Hưng, Quận 7, Thành phố Hồ Chí Minh 758307 SĐT:19002024 | Email:greensol@gmail.com");
            pAddr.Format.Font.Size = 10;
            pAddr.Format.Alignment = ParagraphAlignment.Right;
            pAddr.Format.SpaceAfter = Unit.FromCentimeter(0);
        }

        private static void AddTitle(Section section)
        {
            var title = section.AddParagraph("KẾT QUẢ PHÂN TÍCH");
            title.Format.Font.Size = 16;
            title.Format.Font.Bold = true;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = Unit.FromCentimeter(0.6);
        }

        private static void AddInfoBlock(Section section,
                                         string tenDonHang,
                                         string tenKhachHang,
                                         string diaChiKhachHang,
                                         string diaDiemQuanTrac,
                                         string tenLoaiMauList,
                                         System.Collections.Generic.List<string> viTriTenList,
                                         string ngayLayMau,
                                         string ngayTraKetQua)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Tên đơn hàng: {tenDonHang}");
            sb.AppendLine($"Tên khách hàng: {tenKhachHang}");
            sb.AppendLine($"Địa chỉ: {diaChiKhachHang}");
            sb.AppendLine($"Địa điểm quan trắc: {diaDiemQuanTrac}");
            sb.AppendLine($"Tên mẫu: {tenLoaiMauList}");

            // Ensure exactly 3 entries for GTVT1/2/3
            var positions = viTriTenList ?? new System.Collections.Generic.List<string>();
            while (positions.Count < 3) positions.Add(string.Empty);

            sb.AppendLine($"Vị trí lấy mẫu: GTVT1: {positions[0]} ; GTVT2: {positions[1]} ; GTVT3: {positions[2]}");
            sb.AppendLine($"Ngày lấy mẫu: {ngayLayMau}");
            sb.Append($"Ngày trả kết quả: {ngayTraKetQua}");

            var p = section.AddParagraph(sb.ToString());
            p.Format.Font.Size = 13;
            p.Format.SpaceAfter = Unit.FromCentimeter(0.8);
        }

        // GiaTriChuan moved to the end of columns
        private static void AddThongSoTable(Section section, DataTable table, System.Collections.Generic.List<string> positions)
        {
            string[] cols = { "STT", "ThongSo", "DonVi", "ViTri1", "ViTri2", "ViTri3", "GiaTriChuan" };
            var present = cols.Where(c => table.Columns.Contains(c)).ToList();
            if (present.Count == 0) return;

            var t = section.AddTable();
            t.Format.Font.Size = 10;
            t.Borders.Width = 0.5;
            t.Rows.LeftIndent = 0;

            foreach (var c in present)
            {
                var col = t.AddColumn(Unit.FromCentimeter(GetColWidth(c)));
                col.Format.Alignment = ParagraphAlignment.Center;
            }

            // Build nested header: top row with 'Kết quả phân tích' merged over ViTri1-3, second row with position names
            int idxVi1 = present.IndexOf("ViTri1");
            int idxVi2 = present.IndexOf("ViTri2");
            int idxVi3 = present.IndexOf("ViTri3");

            var headerTop = t.AddRow();
            headerTop.HeadingFormat = true;
            headerTop.Format.Font.Bold = true;
            headerTop.Format.Alignment = ParagraphAlignment.Center;
            headerTop.TopPadding = 2;
            headerTop.BottomPadding = 2;

            if (idxVi1 >= 0 && idxVi2 == idxVi1 + 1 && idxVi3 == idxVi1 + 2)
            {
                for (int i = 0; i < present.Count; i++)
                {
                    var cell = headerTop.Cells[i];
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    if (i == idxVi1)
                    {
                        cell.AddParagraph("Kết quả phân tích");
                        cell.MergeRight = 2;
                        cell.Format.Alignment = ParagraphAlignment.Center;
                    }
                    else if (i > idxVi1 && i <= idxVi1 + 2)
                    {
                        // part of merged region; leave empty
                        cell.AddParagraph(string.Empty);
                    }
                    else
                    {
                        // For non-ViTri columns merge down so the header spans two rows
                        cell.AddParagraph(GetHeaderName(present[i]));
                        cell.MergeDown = 1;
                        cell.Format.Alignment = ParagraphAlignment.Center;
                    }
                }

                var headerSub = t.AddRow();
                headerSub.HeadingFormat = true;
                headerSub.Format.Font.Bold = true;
                headerSub.Format.Alignment = ParagraphAlignment.Center;
                headerSub.TopPadding = 2;
                headerSub.BottomPadding = 2;

                // Keep the sub-headers for the ViTri columns as static labels (Vị trí 1/2/3)
                for (int i = 0; i < present.Count; i++)
                {
                    var cell = headerSub.Cells[i];
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    if (i == idxVi1) cell.AddParagraph(GetHeaderName("ViTri1"));
                    else if (i == idxVi2) cell.AddParagraph(GetHeaderName("ViTri2"));
                    else if (i == idxVi3) cell.AddParagraph(GetHeaderName("ViTri3"));
                    else cell.AddParagraph(string.Empty);
                    cell.Format.Alignment = ParagraphAlignment.Center;
                }
            }
            else
            {
                // No contiguous ViTri columns; just create single-row headers
                for (int i = 0; i < present.Count; i++)
                {
                    headerTop.Cells[i].AddParagraph(GetHeaderName(present[i]));
                    headerTop.Cells[i].VerticalAlignment = VerticalAlignment.Center;
                }
            }

            foreach (DataRow r in table.Rows)
            {
                bool isSection = (table.Columns.Contains("STT") && r["STT"] == DBNull.Value)
                                  && table.Columns.Contains("ThongSo")
                                  && Convert.ToString(r["ThongSo"]).Contains("── Loại mẫu:");
                bool isBlank = (table.Columns.Contains("STT") && r["STT"] == DBNull.Value)
                               && table.Columns.Contains("ThongSo")
                               && string.IsNullOrWhiteSpace(Convert.ToString(r["ThongSo"]));

                if (isSection)
                {
                    var rowSec = t.AddRow();
                    rowSec.Format.Font.Bold = true;
                    rowSec.Cells[0].MergeRight = present.Count - 1;
                    string txt = Convert.ToString(r["ThongSo"]).Replace("──", string.Empty).Trim();
                    rowSec.Cells[0].AddParagraph(txt);
                    continue;
                }
                if (isBlank)
                {
                    var rowBlank = t.AddRow();
                    rowBlank.Height = Unit.FromMillimeter(3);
                    continue;
                }

                var row = t.AddRow();
                for (int i = 0; i < present.Count; i++)
                {
                    string colName = present[i];
                    string val = r.Table.Columns.Contains(colName) && r[colName] != DBNull.Value ? Convert.ToString(r[colName]) : string.Empty;
                    var cell = row.Cells[i];
                    cell.AddParagraph(val);
                    cell.Format.Alignment = colName == "STT" ? ParagraphAlignment.Center : ParagraphAlignment.Left;
                }
            }

            section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.6);
        }

        private static double GetColWidth(string col)
        {
            switch (col)
            {
                case "STT": return 1.0;
                case "ThongSo": return 5.0;
                case "DonVi": return 2.0;
                case "GiaTriChuan": return 2.5;
                case "ViTri1":
                case "ViTri2":
                case "ViTri3": return 2.0;
                default: return 2.0;
            }
        }

        private static string GetHeaderName(string col)
        {
            switch (col)
            {
                case "STT": return "STT";
                case "ThongSo": return "Thông số";
                case "DonVi": return "Đơn vị";
                case "GiaTriChuan": return "Giá trị chuẩn";
                case "ViTri1": return "Vị trí 1";
                case "ViTri2": return "Vị trí 2";
                case "ViTri3": return "Vị trí 3";
                default: return col;
            }
        }

        private static void AddChuThich(Section section)
        {
            var p = section.AddParagraph("Chú thích:\n- Vị trí 1:\n- Vị trí 2:\n- Vị trí 3:");
            p.Format.Font.Size = 12;
            p.Format.SpaceAfter = Unit.FromCentimeter(1.2);
        }

        private static void AddFooterKy(Section section)
        {
            var today = DateTime.Today;
            string dateLine = $"TP. HCM, ngày {today:dd} tháng {today:MM} năm {today:yyyy}";

            var tbl = section.AddTable();
            // Dùng full chiều rộng (17cm) để căn phải đối xứng
            tbl.AddColumn(Unit.FromCentimeter(8.5));
            tbl.AddColumn(Unit.FromCentimeter(8.5));

            var rowDate = tbl.AddRow();
            rowDate.Cells[0].MergeRight = 1;
            var pDate = rowDate.Cells[0].AddParagraph(dateLine);
            pDate.Format.Alignment = ParagraphAlignment.Right;
            pDate.Format.Font.Size = 12;
            pDate.Format.SpaceAfter = Unit.FromCentimeter(0.5);

            var rowHeader = tbl.AddRow();
            var left = rowHeader.Cells[0].AddParagraph("Người phân tích");
            left.Format.Font.Bold = true;
            rowHeader.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            var right = rowHeader.Cells[1].AddParagraph("Trưởng phòng");
            right.Format.Font.Bold = true;

            var rowSub = tbl.AddRow();
            rowSub.Cells[0].AddParagraph("(Ký rõ họ, tên)");
            rowSub.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            rowSub.Cells[1].AddParagraph("(Ký rõ họ, tên)");

            var rowSpace = tbl.AddRow();
            rowSpace.Height = Unit.FromCentimeter(2.5);
        }

        private static string Safe(DataRow r, string col)
        {
            if (r == null || !r.Table.Columns.Contains(col) || r[col] == DBNull.Value)
                return string.Empty;
            return Convert.ToString(r[col])?.Trim();
        }

        private static string DateFormatted(DataRow r, string col)
        {
            if (r == null || !r.Table.Columns.Contains(col) || r[col] == DBNull.Value)
                return string.Empty;
            DateTime d;
            if (DateTime.TryParse(Convert.ToString(r[col]), out d))
                return d.ToString("dd/MM/yyyy");
            return string.Empty;
        }
    }
}
