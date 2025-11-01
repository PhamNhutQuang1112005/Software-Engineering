using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_ThongSoQuanTrac
    {
        private SqlConnection NewConn() => DBConnection.GetConnection();

        // ===== Lookup =====
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

        public bool InsertThongSoMoi(string viTriID, string loaiChiTieuID,
                                     string donViID, string loaiPhanTichID, string nguoiPhanTichID)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_InsertThongSoMoi", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                string ten = "TS_" + Guid.NewGuid().ToString("N").Substring(0, 8);

                cmd.Parameters.AddWithValue("@ViTriID", viTriID);
                cmd.Parameters.AddWithValue("@TenThongSo", ten);
                cmd.Parameters.AddWithValue("@LoaiChiTieuID", loaiChiTieuID);
                cmd.Parameters.AddWithValue("@DonViID", donViID);
                cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID);
                cmd.Parameters.AddWithValue("@NguoiPhanTichID", nguoiPhanTichID);

                cn.Open();
                cmd.ExecuteNonQuery(); // NOCOUNT ON -> rowsAffected có thể -1

                using (var verify = new SqlCommand(
                    "SELECT COUNT(1) FROM dbo.ThongSoMoiTruong WHERE TenThongSo=@TS", cn))
                {
                    verify.Parameters.AddWithValue("@TS", ten);
                    return Convert.ToInt32(verify.ExecuteScalar()) > 0;
                }
            }
        }

        public bool InsertThongSoMoi_WithGivenKey(
            string viTriID, string tenThongSo, string loaiChiTieuID,
            string donViID, string loaiPhanTichID, string nguoiPhanTichID,
            string thauPhuID = null)
        {
            using (var cn = DBConnection.GetConnection())
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

                    using (var verify = new SqlCommand(
                        "SELECT COUNT(1) FROM dbo.ThongSoMoiTruong WHERE TenThongSo=@TS", cn))
                    {
                        verify.Parameters.AddWithValue("@TS", tenThongSo);
                        var exists = Convert.ToInt32(verify.ExecuteScalar()) > 0;

                        if (exists && !string.IsNullOrEmpty(thauPhuID))
                        {
                            using (var cmd2 = new SqlCommand(
                                "UPDATE dbo.ThongSoMoiTruong SET ThauPhuID=@TP WHERE TenThongSo=@TS", cn))
                            {
                                cmd2.Parameters.AddWithValue("@TP", thauPhuID);
                                cmd2.Parameters.AddWithValue("@TS", tenThongSo);
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        return exists;
                    }
                }
            }
        }

        public int UpdateThongSo_GiaTri_KetLuan(string tenThongSo, object giaTri, object ketLuan)
        {
            using (var cn = NewConn())
            using (var cmd = new SqlCommand("dbo.sp_UpdateThongSoMoiTruong", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                cmd.Parameters.AddWithValue("@GiaTri", (object)giaTri ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KetLuan", (object)ketLuan ?? DBNull.Value);
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cập nhật động các cột có mặt trong DataTable thay đổi (full update).
        /// Không đòi hỏi stored procedure; dùng UPDATE động để an toàn.
        /// </summary>
        public int UpdateThongSo_Full(
            string tenThongSo,
            object loaiChiTieuID,  bool hasLct,
            object donViID,        bool hasDv,
            object loaiPhanTichID, bool hasLpt,
            object nguoiPhanTichID,bool hasNd,
            object giaTri,         bool hasGT,
            object giaTriSo,       bool hasGTS,
            object giaTriQuyChuan, bool hasGTQ,
            object ketLuan,        bool hasKL)
        {
            var sets = new List<string>();
            if (hasLct) sets.Add("LoaiChiTieuID=@LoaiChiTieuID");
            if (hasDv)  sets.Add("DonViID=@DonViID");
            if (hasLpt) sets.Add("LoaiPhanTichID=@LoaiPhanTichID");
            if (hasNd)  sets.Add("NguoiPhanTichID=@NguoiPhanTichID");
            if (hasGT)  sets.Add("GiaTri=@GiaTri");
            if (hasGTS) sets.Add("GiaTriSo=@GiaTriSo");
            if (hasGTQ) sets.Add("GiaTriQuyChuan=@GiaTriQuyChuan");
            if (hasKL)  sets.Add("KetLuan=@KetLuan");

            if (sets.Count == 0) return 0;

            string sql = $"UPDATE dbo.ThongSoMoiTruong SET {string.Join(", ", sets)} WHERE TenThongSo=@TenThongSo";

            using (var cn = NewConn())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);

                if (hasLct) cmd.Parameters.AddWithValue("@LoaiChiTieuID",  loaiChiTieuID  ?? (object)DBNull.Value);
                if (hasDv)  cmd.Parameters.AddWithValue("@DonViID",        donViID        ?? (object)DBNull.Value);
                if (hasLpt) cmd.Parameters.AddWithValue("@LoaiPhanTichID", loaiPhanTichID ?? (object)DBNull.Value);
                if (hasNd)  cmd.Parameters.AddWithValue("@NguoiPhanTichID",nguoiPhanTichID?? (object)DBNull.Value);
                if (hasGT)  cmd.Parameters.AddWithValue("@GiaTri",         giaTri         ?? (object)DBNull.Value);
                if (hasGTS) cmd.Parameters.AddWithValue("@GiaTriSo",       giaTriSo       ?? (object)DBNull.Value);
                if (hasGTQ) cmd.Parameters.AddWithValue("@GiaTriQuyChuan", giaTriQuyChuan ?? (object)DBNull.Value);
                if (hasKL)  cmd.Parameters.AddWithValue("@KetLuan",        ketLuan        ?? (object)DBNull.Value);

                cn.Open();
                return cmd.ExecuteNonQuery();
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

        public void ClearPools() => DBConnection.CloseAllConnections();
    }
}
