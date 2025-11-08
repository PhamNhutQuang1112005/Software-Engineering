using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class BLL_ExportThongSo
    {
        // Build table: STT | ThongSo | DonVi | GiaTriChuan | ViTri1 | ViTri2 | ViTri3
        public static DataTable BuildExportThongSoTable(BLL_ThongSoQuanTrac bll,
                                                        string donHangID,
                                                        string loaiViTriID)
        {
            var export = CreateBaseTable();

            var dtViTri = bll.GetViTriByDonHang(donHangID);
            if (dtViTri == null || dtViTri.Rows.Count == 0) return export;

            var viTriIds = dtViTri.AsEnumerable()
                                  .Select(r => Convert.ToString(r["ViTriID"]))
                                  .Where(s => !string.IsNullOrEmpty(s))
                                  .Take(3) // chỉ lấy 3 vị trí đầu như yêu cầu hiện tại
                                  .ToList();

            var perViTri = new List<DataTable>();
            foreach (var vt in viTriIds)
            {
                perViTri.Add(bll.GetThongSoByViTriLoai(vt, loaiViTriID) ?? new DataTable());
            }

            var dict = new Dictionary<string, ExportRow>(StringComparer.OrdinalIgnoreCase);

            for (int idx = 0; idx < viTriIds.Count; idx++)
            {
                var dt = perViTri[idx];
                foreach (DataRow r in dt.Rows)
                {
                    string ten = null;
                    if (dt.Columns.Contains("TenLoaiChiTieu")) ten = Convert.ToString(r["TenLoaiChiTieu"]);
                    else if (dt.Columns.Contains("TenChiTieu")) ten = Convert.ToString(r["TenChiTieu"]);
                    if (string.IsNullOrWhiteSpace(ten) && dt.Columns.Contains("TenThongSo"))
                        ten = Convert.ToString(r["TenThongSo"]);
                    if (string.IsNullOrWhiteSpace(ten)) continue;

                    if (!dict.TryGetValue(ten, out var rec))
                    {
                        rec = new ExportRow
                        {
                            ThongSo = ten,
                            DonVi = dt.Columns.Contains("TenDonVi") ? Convert.ToString(r["TenDonVi"]) : "",
                            GiaTriChuan = dt.Columns.Contains("GiaTriQuyChuan") ? Convert.ToString(r["GiaTriQuyChuan"]) : ""
                        };
                        dict[ten] = rec;
                    }

                    string val = null;
                    if (dt.Columns.Contains("GiaTriSo") && r["GiaTriSo"] != DBNull.Value)
                        val = Convert.ToString(r["GiaTriSo"]);
                    if (string.IsNullOrEmpty(val) && dt.Columns.Contains("GiaTri"))
                        val = Convert.ToString(r["GiaTri"]);

                    if (idx == 0) rec.V1 = val;
                    else if (idx == 1) rec.V2 = val;
                    else if (idx == 2) rec.V3 = val;

                    if (string.IsNullOrEmpty(rec.GiaTriChuan) && dt.Columns.Contains("GiaTriQuyChuan"))
                        rec.GiaTriChuan = Convert.ToString(r["GiaTriQuyChuan"]);
                }
            }

            int stt = 1;
            foreach (var row in dict.Values.OrderBy(v => v.ThongSo, StringComparer.OrdinalIgnoreCase))
            {
                var dr = export.NewRow();
                dr["STT"] = stt++;
                dr["ThongSo"] = row.ThongSo;
                dr["DonVi"] = row.DonVi;
                dr["GiaTriChuan"] = row.GiaTriChuan;
                dr["ViTri1"] = row.V1;
                dr["ViTri2"] = row.V2;
                dr["ViTri3"] = row.V3;
                export.Rows.Add(dr);
            }

            return export;
        }

        // Multi: thêm section header cho mỗi loại mẫu, giữ cùng cấu trúc
        public static DataTable BuildExportThongSoTableMulti(BLL_ThongSoQuanTrac bll,
                                                             string donHangID,
                                                             IEnumerable<(string Id, string Ten)> loaiViTriList)
        {
            var export = CreateBaseTable();
            if (loaiViTriList == null) return export;

            foreach (var item in loaiViTriList)
            {
                // Header dòng section
                var header = export.NewRow();
                header["STT"] = DBNull.Value;
                header["ThongSo"] = $"── Loại mẫu: {item.Ten} ──";
                export.Rows.Add(header);

                var per = BuildExportThongSoTable(bll, donHangID, item.Id);
                int stt = 1;
                foreach (DataRow r in per.Rows)
                {
                    var row = export.NewRow();
                    row["STT"] = stt++;
                    row["ThongSo"] = Convert.ToString(r["ThongSo"]);
                    row["DonVi"] = Convert.ToString(r["DonVi"]);
                    row["GiaTriChuan"] = Convert.ToString(r["GiaTriChuan"]);
                    row["ViTri1"] = Convert.ToString(r["ViTri1"]);
                    row["ViTri2"] = Convert.ToString(r["ViTri2"]);
                    row["ViTri3"] = Convert.ToString(r["ViTri3"]);
                    export.Rows.Add(row);
                }

                // dòng trống ngăn cách
                var blank = export.NewRow();
                blank["STT"] = DBNull.Value;
                blank["ThongSo"] = "";
                export.Rows.Add(blank);
            }
            return export;
        }

        private static DataTable CreateBaseTable()
        {
            var export = new DataTable();
            export.Columns.Add("STT", typeof(int));
            export.Columns.Add("ThongSo", typeof(string));
            export.Columns.Add("DonVi", typeof(string));
            export.Columns.Add("GiaTriChuan", typeof(string));
            export.Columns.Add("ViTri1", typeof(string));
            export.Columns.Add("ViTri2", typeof(string));
            export.Columns.Add("ViTri3", typeof(string));
            return export;
        }

        private class ExportRow
        {
            public string ThongSo;
            public string DonVi;
            public string GiaTriChuan;
            public string V1;
            public string V2;
            public string V3;
        }
    }
}
