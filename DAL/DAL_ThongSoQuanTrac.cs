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
        public DataTable GetAllThongSoMoiTruong()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllThongSoMoiTruong", cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                cn.Open(); da.Fill(dt);
                return dt;
            }
        }
        public DataTable GetAllViTriLayMau()
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("GetAllViTriLayMau", cn))
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
                x.TenChiTieu = Convert.ToString(r["TenChiTieu"]);
                x.PhongBan = Convert.ToString(r["PhongBan"]);
                x.GiaTriChuan = Convert.ToString(r["GiaTriChuan"]);
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
                x.DonViID = Convert.ToString(r["DonViID"]);
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
                x.TenLoai = Convert.ToString(r["TenLoai"]);
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
                x.TenLoai = Convert.ToString(r["TenLoai"]);
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
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(diaChi) ? (object)DBNull.Value : diaChi);

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

        // Transactional insert + clone across DonHang
        public string InsertThongSoAndCloneAcrossDonHang(
    string donHangID,
    string sourceViTriID,
    string loaiViTriID,
    string loaiChiTieuID,
    string donViID,
    string loaiPhanTichID,
    string nguoiPhanTichID,
    string thauPhuID,
    float? giaTriQuyChuan)
{
    if (string.IsNullOrWhiteSpace(donHangID)) throw new ArgumentNullException(nameof(donHangID));
    if (string.IsNullOrWhiteSpace(sourceViTriID)) throw new ArgumentNullException(nameof(sourceViTriID));

    using (SqlConnection cn = NewConn())
    {
        cn.Open();
        using (SqlTransaction tx = cn.BeginTransaction())
        {
            try
            {
                // 1) Lấy tất cả ViTriID của đơn hàng bằng SP có sẵn: [dbo].[sp_GetViTriByDonHang]
                var viTriList = new List<string>();
                using (SqlCommand cmd = new SqlCommand("[dbo].[sp_GetViTriByDonHang]", cn, tx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DonHangID", donHangID);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            viTriList.Add(rdr.GetString(0));
                        }
                    }
                }

                if (viTriList.Count == 0)
                {
                    tx.Commit();
                    return null;
                }

                // Helper: insert 1 thông số cho 1 vị trí
                Func<string, string> insertOne = (vtId) =>
                {
                    string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                    using (SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertThongSoMoi_WithLoai]", cn, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViTriID", vtId);
                        cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                        cmd.Parameters.AddWithValue("@TenThongSo", key);
                        cmd.Parameters.AddWithValue("@LoaiChiTieuID", (object)loaiChiTieuID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DonViID", (object)donViID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LoaiPhanTichID", (object)loaiPhanTichID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NguoiPhanTichID", (object)nguoiPhanTichID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThauPhuID",
                            string.IsNullOrWhiteSpace(thauPhuID) ? (object)DBNull.Value : thauPhuID);

                        var p = cmd.Parameters.Add("@GiaTriQuyChuan", SqlDbType.Float);
                        p.Value = giaTriQuyChuan.HasValue
                            ? (object)giaTriQuyChuan.Value
                            : (object)DBNull.Value;

                        cmd.ExecuteNonQuery();
                        return key;
                    }
                };

                string sourceKey = null;

                // 2) Với mỗi vị trí trong đơn hàng
                foreach (var vt in viTriList)
                {
                    // 2.1) Check mapping ViTri_LoaiViTri bằng SP mới: dbo.sp_ViTri_LoaiViTri_Exists
                    int hasMapping;
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ViTri_LoaiViTri_Exists", cn, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViTriID", vt);
                        cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);

                        hasMapping = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }
                    if (hasMapping == 0)
                        continue; // vị trí này không gắn loại vị trí đó => bỏ qua

                    // 2.2) Check đã có thông số này cho (ViTriID + LoaiViTriID + LoaiChiTieuID) chưa
                    int existed;
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ThongSo_Exists_ByViTriLoaiChiTieu", cn, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViTriID", vt);
                        cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                        cmd.Parameters.AddWithValue("@LoaiChiTieuID", (object)loaiChiTieuID ?? DBNull.Value);

                        existed = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }
                    if (existed > 0)
                        continue; // đã có rồi cho đúng loại vị trí này

                    // 2.3) Insert
                    var key = insertOne(vt);

                    if (string.Equals(vt, sourceViTriID, StringComparison.OrdinalIgnoreCase))
                        sourceKey = key;
                }

                // 3) Nếu vì lý do gì sourceKey vẫn null => xử lý riêng sourceViTriID
                if (sourceKey == null)
                {
                    // 3.1) Check mapping cho source
                    int hasMapping;
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ViTri_LoaiViTri_Exists", cn, tx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViTriID", sourceViTriID);
                        cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);

                        hasMapping = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    if (hasMapping > 0)
                    {
                        // 3.2) Check đã có thông số cho source chưa
                        int existed;
                        using (SqlCommand cmd2 = new SqlCommand("dbo.sp_ThongSo_Exists_ByViTriLoaiChiTieu", cn, tx))
                        {
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@ViTriID", sourceViTriID);
                            cmd2.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                            cmd2.Parameters.AddWithValue("@LoaiChiTieuID", (object)loaiChiTieuID ?? DBNull.Value);

                            existed = Convert.ToInt32(cmd2.ExecuteScalar() ?? 0);
                        }

                        if (existed == 0)
                        {
                            sourceKey = insertOne(sourceViTriID);
                        }
                    }
                }

                tx.Commit();
                return sourceKey;
            }
            catch
            {
                try { tx.Rollback(); } catch { }
                throw;
            }
        }
    }
}


        // DAL_ThongSoQuanTrac.cs
        public string InsertThongSoMoi_ReturnKey_WithLoai(
            string viTriID, string loaiViTriID, string loaiChiTieuID, string donViID,
            string loaiPhanTichID, string nguoiPhanTichID, string thauPhuID, float? giaTriQuyChuan)
        {
            string key = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertThongSoMoi_WithLoai", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@LoaiViTriID", loaiViTriID);
                cmd.Parameters.AddWithValue("@TenThongSo", key);
                cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DonViID", donViID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NguoiPhanTichID", nguoiPhanTichID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ThauPhuID", string.IsNullOrWhiteSpace(thauPhuID) ? (object)DBNull.Value : (object)thauPhuID);

                var p = cmd.Parameters.Add("@GiaTriQuyChuan", SqlDbType.Float);
                p.Value = giaTriQuyChuan.HasValue ? (object)giaTriQuyChuan.Value : (object)DBNull.Value;

                cn.Open();
                // don't depend on returned int (SP uses NOCOUNT); if no exception, treat as success
                cmd.ExecuteNonQuery();
                return key;
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
                // Stored procedures may use SET NOCOUNT ON (ExecuteNonQuery returns -1),
                // so treat non-exceptional execution as success.
                return ok >= 0;
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
                    bool hasDv = changes.Columns.Contains("DonViID");
                    bool hasLpt = changes.Columns.Contains("LoaiPhanTichID");
                    bool hasNd = changes.Columns.Contains("NguoiPhanTichID");
                    bool hasGT = changes.Columns.Contains("GiaTri");
                    bool hasGTS = changes.Columns.Contains("GiaTriSo");
                    bool hasGTQ = changes.Columns.Contains("GiaTriQuyChuan");
                    bool hasKL = changes.Columns.Contains("KetLuan");
                    bool hasTP = changes.Columns.Contains("ThauPhuID");
                    bool hasLV = changes.Columns.Contains("LoaiViTriID");

                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ThongSo_UpdateFull", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TenThongSo", ten);
                        cmd.Parameters.AddWithValue("@UpdLoaiChiTieuID", hasLct ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdDonViID", hasDv ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdLoaiPhanTichID", hasLpt ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdNguoiPhanTichID", hasNd ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTri", hasGT ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTriSo", hasGTS ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdGiaTriQuyChuan", hasGTQ ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdKetLuan", hasKL ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdThauPhuID", hasTP ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UpdLoaiViTriID", hasLV ? 1 : 0);

                        if (hasLct) cmd.Parameters.AddWithValue("@LoaiChiTieuID", r["LoaiChiTieuID"] ?? (object)DBNull.Value);
                        if (hasDv) cmd.Parameters.AddWithValue("@DonViID", r["DonViID"] ?? (object)DBNull.Value);
                        if (hasLpt) cmd.Parameters.AddWithValue("@LoaiPhanTichID", r["LoaiPhanTichID"] ?? (object)DBNull.Value);
                        if (hasNd) cmd.Parameters.AddWithValue("@NguoiPhanTichID", r["NguoiPhanTichID"] ?? (object)DBNull.Value);
                        if (hasTP) cmd.Parameters.AddWithValue("@ThauPhuID", r["ThauPhuID"] ?? (object)DBNull.Value);
                        if (hasLV) cmd.Parameters.AddWithValue("@LoaiViTriID", r["LoaiViTriID"] ?? (object)DBNull.Value);

                        if (hasGT) cmd.Parameters.AddWithValue("@GiaTri", r["GiaTri"] ?? (object)DBNull.Value);
                        if (hasGTS) AddDecimal(cmd, "@GiaTriSo", r["GiaTriSo"], 18, 4);
                        if (hasGTQ) AddDecimal(cmd, "@GiaTriQuyChuan", r["GiaTriQuyChuan"], 18, 4);
                        if (hasKL) cmd.Parameters.AddWithValue("@KetLuan", r["KetLuan"] ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        // ===== Insert Loại Chỉ Tiêu =====
        public bool InsertLoaiChiTieu(DTO_LoaiChiTieu dto)
        {
            using (SqlConnection cn = NewConn())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertLoaiChiTieu", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoaiChiTieuID", dto.LoaiChiTieuID);
                cmd.Parameters.AddWithValue("@TenChiTieu", dto.TenChiTieu);
                cmd.Parameters.AddWithValue("@DonViID", dto.DonViID);
                cmd.Parameters.AddWithValue("@PhongBan", dto.PhongBan);

                if (string.IsNullOrWhiteSpace(dto.GiaTriChuan))
                    cmd.Parameters.AddWithValue("@GiaTriChuan", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@GiaTriChuan", dto.GiaTriChuan);

                cn.Open();
                cmd.ExecuteNonQuery();   // nếu lỗi sẽ throw exception
                return true;             // đến được đây nghĩa là thành công
            }
        }


    }
}