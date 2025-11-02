
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using DTO;

namespace DAL
{
    /// <summary>
    /// DAL v3: loại bỏ AddWithValue cho DECIMAL, ép kiểu tường minh và coi chuỗi rỗng/không hợp lệ là NULL.
    /// </summary>
    public class DAL_ThongSoQuanTrac
    {
        private SqlConnection NewConn() => DBConnection.GetConnection();

        // ===== Helpers for numeric parameters =====
        private static object ToDbDecimal(object raw)
        {
            if (raw == null || raw == DBNull.Value) return DBNull.Value;

            if (raw is decimal d) return d;

            var s = Convert.ToString(raw)?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return DBNull.Value;

            // chấp nhận cả dấu , và . ; chuẩn hóa về .
            s = s.Replace(',', '.');

            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
                return val;

            return DBNull.Value; // không parse được -> NULL thay vì gây lỗi
        }

        private static void AddDecimal(SqlCommand cmd, string name, object raw, byte precision = 18, byte scale = 4)
        {
            var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = precision;
            p.Scale = scale;
            p.Value = ToDbDecimal(raw);
        }

        // ===== Lookup (DataTable cho GUI cũ) =====
        public DataTable GetAllLoaiChiTieu()
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetAllLoaiChiTieu", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllDonVi()
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetAllDonVi", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllLoaiPhanTich()
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetAllLoaiPhanTich", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllNguoiDung()
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetAllNguoiDung", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        // ===== DTO_ versions (giữ nguyên như v2) =====
        public List<DTO_LoaiChiTieu> GetAllLoaiChiTieuDTO_()
        {
            var list = new List<DTO_LoaiChiTieu>();
            var dt = GetAllLoaiChiTieu();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new DTO_LoaiChiTieu
                {
                    LoaiChiTieuID = Convert.ToString(r["LoaiChiTieuID"]),
                    TenChiTieu    = Convert.ToString(r["TenChiTieu"]),
                    DonViID      = Convert.ToString(r["DonViID"])
                });
            }
            return list;
        }

        public List<DTO_DonVi> GetAllDonViDTO_()
        {
            var list = new List<DTO_DonVi>();
            var dt = GetAllDonVi();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new DTO_DonVi
                {
                    DonViID  = Convert.ToString(r["DonViID"]),
                    TenDonVi = Convert.ToString(r["TenDonVi"])
                });
            }
            return list;
        }

        public List<DTO_LoaiPhanTich> GetAllLoaiPhanTichDTO_()
        {
            var list = new List<DTO_LoaiPhanTich>();
            var dt = GetAllLoaiPhanTich();
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new DTO_LoaiPhanTich
                {
                    LoaiPhanTichID = Convert.ToString(r["LoaiPhanTichID"]),
                    TenLoai        = Convert.ToString(r["TenLoai"])
                });
            }
            return list;
        }

        public List<DTO_NguoiDung> GetAllNguoiDungDTO_()
        {
            var list = new List<DTO_NguoiDung>();
            var dt = GetAllNguoiDung();
            foreach (DataRow r in dt.Rows)
            {
                var item = new DTO_NguoiDung
                {
                    NguoiDungID = Convert.ToString(r["NguoiDungID"]),
                    HoVaTen     = Convert.ToString(r["HoVaTen"])
                };
                if (dt.Columns.Contains("VaiTroID"))
                {
                    // nếu SP có trả kèm VaiTroID
                    // (không bắt buộc)
                }
                list.Add(item);
            }
            return list;
        }

        // ===== Vị trí & Thông số =====
        public void EnsureViTriAndThongSo(string donHangID)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_TaoViTriVaThongSoTheoDonHang", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetViTriByDonHang(string donHangID)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetViTriByDonHang", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public List<DTO_ViTri> GetViTriByDonHangDTO_(string donHangID)
        {
            var list = new List<DTO_ViTri>();
            var dt = GetViTriByDonHang(donHangID);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new DTO_ViTri
                {
                    ViTriID = Convert.ToString(r["ViTriID"]),
                    TenViTri = Convert.ToString(r["TenViTri"]),
                    DonHangID = dt.Columns.Contains("DonHangID") ? Convert.ToString(r["DonHangID"]) : null
                });
            }
            return list;
        }

        public DataTable GetThongSoByViTri(string viTriID)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_GetThongSoByViTri", cn))
            using (var da  = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                var dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public List<DTO_ThongSoMoiTruong> GetThongSoByViTriDTO_(string viTriID)
        {
            var list = new List<DTO_ThongSoMoiTruong>();
            var dt = GetThongSoByViTri(viTriID);
            foreach (DataRow r in dt.Rows)
            {
                list.Add(new DTO_ThongSoMoiTruong
                {
                    TenThongSo = Convert.ToString(r["TenThongSo"]),
                    ViTriID    = Convert.ToString(r["ViTriID"]),

                    LoaiChiTieuID  = Convert.ToString(r["LoaiChiTieuID"]),
                    DonViID        = Convert.ToString(r["DonViID"]),
                    LoaiPhanTichID = Convert.ToString(r["LoaiPhanTichID"]),
                    NguoiPhanTichID = Convert.ToString(r["NguoiPhanTichID"]),
                    ThauPhuID      = dt.Columns.Contains("ThauPhuID") ? Convert.ToString(r["ThauPhuID"]) : null,

                    TenLoaiChiTieu   = dt.Columns.Contains("TenLoaiChiTieu") ? Convert.ToString(r["TenLoaiChiTieu"]) : null,
                    TenDonVi         = dt.Columns.Contains("TenDonVi") ? Convert.ToString(r["TenDonVi"]) : null,
                    TenLoaiPhanTich  = dt.Columns.Contains("TenLoaiPhanTich") ? Convert.ToString(r["TenLoaiPhanTich"]) : null,
                    TenNguoiPhanTich = dt.Columns.Contains("TenNguoiPhanTich") ? Convert.ToString(r["TenNguoiPhanTich"]) : null,
                    TenThauPhu       = dt.Columns.Contains("TenThauPhu") ? Convert.ToString(r["TenThauPhu"]) : null,

                    GiaTri           = dt.Columns.Contains("GiaTri") && r["GiaTri"] != DBNull.Value ? Convert.ToString(r["GiaTri"]) : null,
                    GiaTriSo         = dt.Columns.Contains("GiaTriSo") && r["GiaTriSo"] != DBNull.Value ? (decimal?)Convert.ToDecimal(r["GiaTriSo"]) : null,
                    GiaTriQuyChuan   = dt.Columns.Contains("GiaTriQuyChuan") && r["GiaTriQuyChuan"] != DBNull.Value ? (decimal?)Convert.ToDecimal(r["GiaTriQuyChuan"]) : null,
                    KetLuan          = dt.Columns.Contains("KetLuan") && r["KetLuan"] != DBNull.Value ? Convert.ToString(r["KetLuan"]) : null
                });
            }
            return list;
        }

        public string InsertThongSoMoi_ReturnKey(string viTriID, string loaiChiTieuID,
                                                 string donViID, string loaiPhanTichID, string nguoiPhanTichID,
                                                 string thauPhuID = null)
        {
            string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            bool ok = InsertThongSoMoi_WithGivenKey(viTriID, key, loaiChiTieuID, donViID, loaiPhanTichID, nguoiPhanTichID, thauPhuID);
            return ok ? key : null;
        }

        public bool InsertThongSoMoi_WithGivenKey(string viTriID, string tenThongSo,
            string loaiChiTieuID, string donViID, string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID = null)
        {
            using (var cn = NewConn())
            {
                cn.Open();
                using (var cmd = new SqlCommand("dbo.sp_InsertThongSoMoi", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                    cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                    cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
                    cmd.Parameters.AddWithValue("@DonViID", donViID);
                    cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID);
                    cmd.Parameters.AddWithValue("@NguoiPhanTichID", nguoiPhanTichID);
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(thauPhuID))
                {
                    using (var cmd2 = new SqlCommand("dbo.sp_ThongSo_UpdateFull", cn))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                        cmd2.Parameters.AddWithValue("@UpdThauPhuID", 1);
                        cmd2.Parameters.AddWithValue("@ThauPhuID", thauPhuID);
                        cmd2.ExecuteNonQuery();
                    }
                }

                using (var verify = new SqlCommand("SELECT COUNT(1) FROM dbo.ThongSoMoiTruong WHERE TenThongSo=@TS", cn))
                {
                    verify.Parameters.AddWithValue("@TS", tenThongSo);
                    return Convert.ToInt32(verify.ExecuteScalar()) > 0;
                }
            }
        }

        public bool DeleteThongSo(string tenThongSo)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_DeleteThongSoMoiTruong", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public string GetDefaultDonViID_ByLoaiChiTieu(string loaiChiTieuID)
{
    if (string.IsNullOrWhiteSpace(loaiChiTieuID)) return null;
    using (var cn = NewConn())
    using (var cmd = new SqlCommand("dbo.sp_GetDonViByLoaiChiTieu", cn))
    {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
        cn.Open();
        var obj = cmd.ExecuteScalar();
        return obj == null || obj == DBNull.Value ? null : obj.ToString();
    }
}

        // ===== Batch Update with robust numeric handling =====
        public void UpdateThongSo_Batch(DataTable changes)
        {
            if (changes == null || changes.Rows.Count == 0) return;

            using (var cn = NewConn())
            {
                cn.Open();
                foreach (DataRow r in changes.Rows)
                {
                    if (r.RowState == DataRowState.Deleted || r.RowState == DataRowState.Unchanged) continue;
                    if (!changes.Columns.Contains("TenThongSo")) continue;

                    string ten = Convert.ToString(r["TenThongSo"]);
                    if (string.IsNullOrWhiteSpace(ten)) continue;

                    bool hasLct = changes.Columns.Contains("LoaiChiTieuID");
                    bool hasDv  = changes.Columns.Contains("DonViID");
                    bool hasLpt = changes.Columns.Contains("LoaiPhanTichID");
                    bool hasNd  = changes.Columns.Contains("NguoiPhanTichID");
                    bool hasGT  = changes.Columns.Contains("GiaTri");
                    bool hasGTS = changes.Columns.Contains("GiaTriSo");
                    bool hasGTQ = changes.Columns.Contains("GiaTriQuyChuan");
                    bool hasKL  = changes.Columns.Contains("KetLuan");
                    bool hasTP  = changes.Columns.Contains("ThauPhuID");

                    using (var cmd = new SqlCommand("dbo.sp_ThongSo_UpdateFull", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenThongSo", ten);

                        cmd.Parameters.AddWithValue("@UpdLoaiChiTieuID",  hasLct ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdDonViID",        hasDv  ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdLoaiPhanTichID", hasLpt ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdNguoiPhanTichID",hasNd  ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTri",         hasGT  ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTriSo",       hasGTS ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTriQuyChuan", hasGTQ ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdKetLuan",        hasKL  ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdThauPhuID",      hasTP  ? 1 : 0);

                        if (hasLct) cmd.Parameters.AddWithValue("@LoaiChiTieuID",  r["LoaiChiTieuID"]  == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["LoaiChiTieuID"]))  ? (object)DBNull.Value : r["LoaiChiTieuID"]);
                        if (hasDv)  cmd.Parameters.AddWithValue("@DonViID",        r["DonViID"]        == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["DonViID"]))        ? (object)DBNull.Value : r["DonViID"]);
                        if (hasLpt) cmd.Parameters.AddWithValue("@LoaiPhanTichID", r["LoaiPhanTichID"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["LoaiPhanTichID"])) ? (object)DBNull.Value : r["LoaiPhanTichID"]);
                        if (hasNd)  cmd.Parameters.AddWithValue("@NguoiPhanTichID",r["NguoiPhanTichID"]== DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["NguoiPhanTichID"]))? (object)DBNull.Value : r["NguoiPhanTichID"]);
                        if (hasGT)  cmd.Parameters.AddWithValue("@GiaTri",         r["GiaTri"]          == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["GiaTri"]))          ? (object)DBNull.Value : r["GiaTri"]);
                        if (hasKL)  cmd.Parameters.AddWithValue("@KetLuan",        r["KetLuan"]         == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["KetLuan"]))         ? (object)DBNull.Value : r["KetLuan"]);
                        if (hasTP)  cmd.Parameters.AddWithValue("@ThauPhuID",      r["ThauPhuID"]       == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(r["ThauPhuID"]))       ? (object)DBNull.Value : r["ThauPhuID"]);

                        // numeric with culture-safe parsing
                        if (hasGTS) AddDecimal(cmd, "@GiaTriSo",       r["GiaTriSo"]);
                        if (hasGTQ) AddDecimal(cmd, "@GiaTriQuyChuan", r["GiaTriQuyChuan"]);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void ClearPools() => DBConnection.CloseAllConnections();
    }
}
