using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public static class DAL_HopDong
    {
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        public static DataTable GetAllHopDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_GetAllHopDong", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static void ThemHopDong(DTO_HopDong dto)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_ThemHopDong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HopDongID",   SqlDbType.NVarChar, 50).Value = dto.HopDongID;
                cmd.Parameters.Add("@MaHopDong",   SqlDbType.NVarChar, 50).Value = dto.MaHopDong;
                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = dto.KhachHangID;
                cmd.Parameters.Add("@NgayKy",      SqlDbType.Date).Value        = dto.NgayKy.Date;
                cmd.Parameters.Add("@KyHanID",     SqlDbType.NVarChar, 50).Value = dto.KyHanID;
                cmd.Parameters.Add("@NgayBatDau",  SqlDbType.Date).Value        = (object)dto.NgayBatDau ?? DBNull.Value;
                cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value        = (object)dto.NgayKetThuc ?? DBNull.Value;
                cmd.Parameters.Add("@TrangThai",   SqlDbType.NVarChar, 50).Value = ToDb(dto.TrangThai);
                cmd.Parameters.Add("@GhiChu",      SqlDbType.NVarChar).Value     = ToDb(dto.GhiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void SuaHopDong(DTO_HopDong dto)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_HopDong_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HopDongID",   SqlDbType.NVarChar, 50).Value = dto.HopDongID;
                cmd.Parameters.Add("@MaHopDong",   SqlDbType.NVarChar, 50).Value = dto.MaHopDong;
                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = dto.KhachHangID;
                cmd.Parameters.Add("@NgayKy",      SqlDbType.Date).Value        = dto.NgayKy.Date;
                cmd.Parameters.Add("@KyHanID",     SqlDbType.NVarChar, 50).Value = dto.KyHanID;
                cmd.Parameters.Add("@NgayBatDau",  SqlDbType.Date).Value        = (object)dto.NgayBatDau ?? DBNull.Value;
                cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value        = (object)dto.NgayKetThuc ?? DBNull.Value;
                cmd.Parameters.Add("@TrangThai",   SqlDbType.NVarChar, 50).Value = ToDb(dto.TrangThai);
                cmd.Parameters.Add("@GhiChu",      SqlDbType.NVarChar).Value     = ToDb(dto.GhiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void XoaHopDong(string hopDongID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_XoaHopDong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HopDongID", SqlDbType.NVarChar, 50).Value = hopDongID;
                cmd.Parameters.Add("@XoaCung",   SqlDbType.Bit).Value = 0;
                cmd.Parameters.Add("@Cascade",   SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.NVarChar, 50).Value = DBNull.Value;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
