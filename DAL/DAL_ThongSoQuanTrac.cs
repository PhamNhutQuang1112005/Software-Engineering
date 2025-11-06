using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using DTO;

namespace DAL
{
    public class DAL_ThongSoQuanTrac
    {
        private SqlConnection NewConn()
        {
            return DBConnection.GetConnection();
        }

        // ===== Helpers (4.7.2 safe) =====
        private static object ToDbDecimal(object raw)
        {
            if (raw == null || raw == DBNull.Value) return DBNull.Value;
            if (raw is decimal) return raw;

            string s = Convert.ToString(raw);
            if (string.IsNullOrWhiteSpace(s)) return DBNull.Value;
            s = s.Trim().Replace(',', '.');

            decimal val;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                return val;
            return DBNull.Value;
        }

        private static void AddDecimal(SqlCommand cmd, string name, object raw, byte precision, byte scale)
        {
            SqlParameter p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = precision;
            p.Scale = scale;
            p.Value = ToDbDecimal(raw);
        }

        // ===== Lookup (DataTable) =====
        public DataTable GetAllLoaiChiTieu()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllLoaiChiTieu", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllDonVi()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllDonVi", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllLoaiPhanTich()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllLoaiPhanTich", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetAllLoaiViTri()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllLoaiViTri", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        // ===== Lookup (DTO) =====
        public List<DTO_LoaiChiTieu> GetAllLoaiChiTieuDTO_()
        {
            List<DTO_LoaiChiTieu> list = new List<DTO_LoaiChiTieu>();
            DataTable dt = GetAllLoaiChiTieu();
            foreach (DataRow r in dt.Rows)
            {
                DTO_LoaiChiTieu x = new DTO_LoaiChiTieu();
                x.LoaiChiTieuID = Convert.ToString(r["LoaiChiTieuID"]);
                x.TenChiTieu    = Convert.ToString(r["TenChiTieu"]);
                if (dt.Columns.Contains("DonViID"))
                    x.DonViID = Convert.ToString(r["DonViID"]);
                list.Add(x);
            }
            return list;
        }

        public List<DTO_DonVi> GetAllDonViDTO_()
        {
            List<DTO_DonVi> list = new List<DTO_DonVi>();
            DataTable dt = GetAllDonVi();
            foreach (DataRow r in dt.Rows)
            {
                DTO_DonVi x = new DTO_DonVi();
                x.DonViID  = Convert.ToString(r["DonViID"]);
                x.TenDonVi = Convert.ToString(r["TenDonVi"]);
                list.Add(x);
            }
            return list;
        }

        public List<DTO_LoaiPhanTich> GetAllLoaiPhanTichDTO_()
        {
            List<DTO_LoaiPhanTich> list = new List<DTO_LoaiPhanTich>();
            DataTable dt = GetAllLoaiPhanTich();
            foreach (DataRow r in dt.Rows)
            {
                DTO_LoaiPhanTich x = new DTO_LoaiPhanTich();
                x.LoaiPhanTichID = Convert.ToString(r["LoaiPhanTichID"]);
                x.TenLoai        = Convert.ToString(r["TenLoai"]);
                list.Add(x);
            }
            return list;
        }

        public List<DTO_LoaiViTri> GetAllLoaiViTriDTO_()
        {
            List<DTO_LoaiViTri> list = new List<DTO_LoaiViTri>();
            DataTable dt = GetAllLoaiViTri();
            foreach (DataRow r in dt.Rows)
            {
                DTO_LoaiViTri x = new DTO_LoaiViTri();
                x.LoaiViTriID = Convert.ToString(r["LoaiViTriID"]);
                x.TenLoai     = Convert.ToString(r["TenLoai"]);
                list.Add(x);
            }
            return list;
        }

        // ===== Vị trí =====
        public void EnsureViTriAndThongSo(string donHangID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_TaoViTriVaThongSoTheoDonHang", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                cn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetViTriByDonHang(string donHangID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetViTriByDonHang", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        // ===== Loại vị trí theo Vị trí =====
        public DataTable GetLoaiViTriByViTri(string viTriID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetLoaiViTriByViTri", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }
        // DAL_ThongSoQuanTrac.cs
public bool UpdateTenVaDiaChiViTri(string viTriID, string tenViTri, string diaChi)
{
    using (SqlConnection cn = NewConn())
    using (SqlCommand cmd = new SqlCommand("dbo.sp_UpdateViTriTenDiaChi", cn))
    {
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ViTriID", viTriID);
        cmd.Parameters.AddWithValue("@TenViTri", string.IsNullOrWhiteSpace(tenViTri) ? (object)DBNull.Value : tenViTri);
        cmd.Parameters.AddWithValue("@DiaChi",   string.IsNullOrWhiteSpace(diaChi)   ? (object)DBNull.Value : diaChi);

        cn.Open();
        cmd.ExecuteNonQuery();      // có NOCOUNT ON có thể trả -1/0
        return true;                // ⇐ coi là OK miễn không có exception
    }
}


        public string AddLoaiViTriToViTri(string viTriID, string tenLoai)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_AddLoaiViTriToViTri", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@TenLoai", tenLoai);

                SqlParameter outId = new SqlParameter("@LoaiViTriID", SqlDbType.NVarChar, 50);
                outId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outId);

                cn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToString(outId.Value);
            }
        }

        public bool DeleteLoaiViTriFromViTri(string viTriID, string loaiViTriID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteLoaiViTriFromViTri", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                cn.Open();
                int affected = cmd.ExecuteNonQuery();
                return affected >= 0; // xóa mapping (có thể 0 nếu không tồn tại)
            }
        }

        // ===== Thông số =====
        public DataTable GetThongSoByViTri(string viTriID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetThongSoByViTri", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetThongSoByViTriLoai(string viTriID, string loaiViTriID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetThongSoByViTriLoai", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }

        public string InsertThongSoMoi_ReturnKey(
            string viTriID, string loaiChiTieuID, string donViID,
            string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID)
        {
            string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertThongSoMoi", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@TenThongSo", key);
                cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
                cmd.Parameters.AddWithValue("@DonViID", donViID);
                cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID);
                cmd.Parameters.AddWithValue("@NguoiPhanTichID", nguoiPhanTichID);
                if (string.IsNullOrWhiteSpace(thauPhuID))
                    cmd.Parameters.AddWithValue("@ThauPhuID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ThauPhuID", thauPhuID);

                cn.Open();
                int ok = cmd.ExecuteNonQuery();
                return ok > 0 ? key : null;
            }
        }

        public string InsertThongSoMoi_ReturnKey_WithLoai(
            string viTriID, string loaiViTriID, string loaiChiTieuID, string donViID,
            string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID)
        {
            string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertThongSoMoi_WithLoai", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                cmd.Parameters.AddWithValue("@TenThongSo", key);
                cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
                cmd.Parameters.AddWithValue("@DonViID", donViID);
                cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID);
                cmd.Parameters.AddWithValue("@NguoiPhanTichID", nguoiPhanTichID);
                if (string.IsNullOrWhiteSpace(thauPhuID))
                    cmd.Parameters.AddWithValue("@ThauPhuID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ThauPhuID", thauPhuID);

                cn.Open();
                int ok = cmd.ExecuteNonQuery();
                return ok > 0 ? key : null;
            }
        }

        public bool DeleteThongSo(string tenThongSo)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteThongSoMoiTruong", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                cn.Open();
                int ok = cmd.ExecuteNonQuery();
                return ok > 0;
            }
        }

        public string GetDefaultDonViID_ByLoaiChiTieu(string loaiChiTieuID)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetDonViByLoaiChiTieu", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
                cn.Open();
                object obj = cmd.ExecuteScalar();
                return (obj == null || obj == DBNull.Value) ? null : obj.ToString();
            }
        }

        // ===== Batch update từ DataTable =====
        public void UpdateThongSo_Batch(DataTable changes)
        {
            if (changes == null || changes.Rows.Count == 0) return;

            using (SqlConnection cn = NewConn())
            {
                cn.Open();

                foreach (DataRow r in changes.Rows)
                {
                    if (r.RowState == DataRowState.Deleted || r.RowState == DataRowState.Unchanged) continue;
                    if (!changes.Columns.Contains("TenThongSo")) continue;

                    string ten = Convert.ToString(r["TenThongSo"]);

                    bool hasLct = changes.Columns.Contains("LoaiChiTieuID");
                    bool hasDv  = changes.Columns.Contains("DonViID");
                    bool hasLpt = changes.Columns.Contains("LoaiPhanTichID");
                    bool hasNd  = changes.Columns.Contains("NguoiPhanTichID");
                    bool hasGT  = changes.Columns.Contains("GiaTri");
                    bool hasGTS = changes.Columns.Contains("GiaTriSo");
                    bool hasGTQ = changes.Columns.Contains("GiaTriQuyChuan");
                    bool hasKL  = changes.Columns.Contains("KetLuan");
                    bool hasTP  = changes.Columns.Contains("ThauPhuID");
                    bool hasLV  = changes.Columns.Contains("LoaiViTriID");

                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ThongSo_UpdateFull", cn))
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
                        cmd.Parameters.AddWithValue("@UpdLoaiViTriID",    hasLV  ? 1 : 0);

                        if (hasLct) cmd.Parameters.AddWithValue("@LoaiChiTieuID",  r["LoaiChiTieuID"]  ?? (object)DBNull.Value);
                        if (hasDv)  cmd.Parameters.AddWithValue("@DonViID",        r["DonViID"]        ?? (object)DBNull.Value);
                        if (hasLpt) cmd.Parameters.AddWithValue("@LoaiPhanTichID", r["LoaiPhanTichID"] ?? (object)DBNull.Value);
                        if (hasNd)  cmd.Parameters.AddWithValue("@NguoiPhanTichID",r["NguoiPhanTichID"]?? (object)DBNull.Value);
                        if (hasTP)  cmd.Parameters.AddWithValue("@ThauPhuID",      r["ThauPhuID"]      ?? (object)DBNull.Value);
                        if (hasLV)  cmd.Parameters.AddWithValue("@LoaiViTriID",    r["LoaiViTriID"]    ?? (object)DBNull.Value);

                        if (hasGT)  cmd.Parameters.AddWithValue("@GiaTri", r["GiaTri"] ?? (object)DBNull.Value);
                        if (hasGTS) AddDecimal(cmd, "@GiaTriSo", r["GiaTriSo"], 18, 4);
                        if (hasGTQ) AddDecimal(cmd, "@GiaTriQuyChuan", r["GiaTriQuyChuan"], 18, 4);
                        if (hasKL)  cmd.Parameters.AddWithValue("@KetLuan", r["KetLuan"] ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
