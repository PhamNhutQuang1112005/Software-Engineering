using System;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using BLL.Font;

namespace BLL
{
    public static class BLL_PDFExporter
    {
        private const string FontName = "Times New Roman";
        private const string LogoRelativePath = "icon\\logo bg2.png"; // phải có trong output bin
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

                var viTriTenList = dtViTri?.AsEnumerable()
                    .Select(r => Safe(r, "TenViTri"))
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Take(3)
                    .ToList() ?? new System.Collections.Generic.List<string>();

                var doc = new Document();
                ConfigureDocument(doc);
                var section = doc.AddSection();
                ConfigureSection(section);

                // Logo
                AddLogosSimple(doc, section, LogoRelativePath);

                AddHeader(section);
                AddTitle(section);
                AddInfoBlock(section, maDonHang, tenKhachHang, diaChiKhachHang, diaDiemQuanTrac,
                             tenLoaiMauList, viTriTenList, ngayLayMau, ngayTraKQ);
                AddThongSoTable(section, thongSoTable);
                AddChuThich(section);
                AddFooterKy(section);

                var renderer = new PdfDocumentRenderer(unicode: true) { Document = doc };
                renderer.RenderDocument();
                renderer.PdfDocument.Save(filePath);
            }

            BLL_TapKetQua.LogPdfExport(donHangID, filePath);
            return true;
        }

        // Thêm logo và log trạng thái để debug
        private static void AddLogosSimple(Document doc, Section section, string relativePath)
        {
            var path = ResolveImagePath(relativePath);
            doc.Info.Subject = "LogoPath=" + (path ?? "NOT_FOUND"); // metadata debug

            var header = section.Headers.Primary;
            if (path == null)
            {
                // hiển thị dòng debug ngay trong PDF để thấy lỗi
                var debugP = header.AddParagraph("(NO LOGO FOUND: " + relativePath + ")");
                debugP.Format.Font.Size = 8;
                debugP.Format.Font.Color = Colors.Red;
                return;
            }

            try
            {
                // Logo nhỏ góc trái
                var pTop = header.AddParagraph();
                var imgSmall = pTop.AddImage(path);
                imgSmall.LockAspectRatio = true;
                imgSmall.Width = Unit.FromCentimeter(3.5);
                pTop.Format.Alignment = ParagraphAlignment.Left;
                pTop.Format.SpaceAfter = 0;

                // Logo lớn ở giữa (PNG nên tự có alpha)
                var pCenter = header.AddParagraph();
                var imgLarge = pCenter.AddImage(path);
                imgLarge.LockAspectRatio = true;
                imgLarge.Width = Unit.FromCentimeter(12);
                pCenter.Format.Alignment = ParagraphAlignment.Center;
                pCenter.Format.SpaceAfter = 0;
            }
            catch (Exception ex)
            {
                var err = header.AddParagraph("(LOGO ERROR: " + ex.Message + ")");
                err.Format.Font.Size = 8;
                err.Format.Font.Color = Colors.Red;
            }
        }

        private static string ResolveImagePath(string relative)
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var candidates = new[]
                {
                    relative,
                    Path.Combine(baseDir, relative ?? string.Empty),
                    Path.Combine(baseDir, "icon", "logo_bg.png"),
                    Path.GetFullPath(Path.Combine(baseDir, "..", "..", "GUI", "icon", "logo_bg.png")),
                    Path.GetFullPath(Path.Combine(baseDir, "..", "icon", "logo_bg.png")),
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

        private static void AddHeader(Section section)
        {
            var p = section.AddParagraph("CÔNG TY CỔ PHẦN KIỂM NGHIỆM MÔI TRƯỜNG GREENSOL VIỆT NAM");
            p.Format.Font.Size = 12;
            p.Format.Font.Bold = true;
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.SpaceAfter = Unit.FromMillimeter(2);

            var p2 = section.AddParagraph("Địa chỉ: Số 123, Đường Nguyễn Văn Cừ, Phường 4, Quận 5, TP. Hồ Chí Minh\nSĐT: 0901 234 567 | Email: contact@ecotest.vn");
            p2.Format.Font.Size = 12;
            p2.Format.Alignment = ParagraphAlignment.Center;
            p2.Format.SpaceAfter = Unit.FromCentimeter(0.5);
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
            sb.AppendLine($"Vị trí lấy mẫu: {string.Join("; ", viTriTenList)}");
            sb.AppendLine($"Ngày lấy mẫu: {ngayLayMau}");
            sb.Append($"Ngày trả kết quả: {ngayTraKetQua}");

            var p = section.AddParagraph(sb.ToString());
            p.Format.Font.Size = 13;
            p.Format.SpaceAfter = Unit.FromCentimeter(0.8);
        }

        private static void AddThongSoTable(Section section, DataTable table)
        {
            string[] cols = { "STT", "ThongSo", "DonVi", "GiaTriChuan", "ViTri1", "ViTri2", "ViTri3" };
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

            var header = t.AddRow();
            header.HeadingFormat = true;
            header.Format.Font.Bold = true;
            header.Format.Alignment = ParagraphAlignment.Center;
            header.TopPadding = 2;
            header.BottomPadding = 2;

            for (int i = 0; i < present.Count; i++)
                header.Cells[i].AddParagraph(GetHeaderName(present[i]));

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
                    string val = r[colName] == DBNull.Value ? string.Empty : Convert.ToString(r[colName]);
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
