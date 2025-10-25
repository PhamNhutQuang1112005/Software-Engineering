using System;
using System.Data;
using System.Data.SqlClient;

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

        // Thêm hợp đồng: nhận HopDongID từ code
        public static void ThemHopDong(
            string hopDongID,
            string maHopDong,
            string khachHangID,
            DateTime ngayKy,
            string kyHanID,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string trangThai,
            string ghiChu)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_ThemHopDong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@HopDongID", SqlDbType.NVarChar, 50).Value = hopDongID;
                cmd.Parameters.Add("@MaHopDong", SqlDbType.NVarChar, 50).Value = maHopDong;
                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = khachHangID;
                cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = ngayKy.Date;
                cmd.Parameters.Add("@KyHanID", SqlDbType.NVarChar, 50).Value = kyHanID;
                cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = (object)ngayBatDau ?? DBNull.Value;
                cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = (object)ngayKetThuc ?? DBNull.Value;
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = ToDb(trangThai);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = ToDb(ghiChu);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void SuaHopDong(
            string hopDongID,
            string maHopDong,
            string khachHangID,
            DateTime ngayKy,
            string kyHanID,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string trangThai,
            string ghiChu)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_HopDong_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HopDongID", SqlDbType.NVarChar, 50).Value = hopDongID;
                cmd.Parameters.Add("@MaHopDong", SqlDbType.NVarChar, 50).Value = maHopDong;
                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = khachHangID;
                cmd.Parameters.Add("@NgayKy", SqlDbType.Date).Value = ngayKy.Date;
                cmd.Parameters.Add("@KyHanID", SqlDbType.NVarChar, 50).Value = kyHanID;
                cmd.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = (object)ngayBatDau ?? DBNull.Value;
                cmd.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = (object)ngayKetThuc ?? DBNull.Value;
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = ToDb(trangThai);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = ToDb(ghiChu);
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
                // Gọi đúng tham số để soft delete + cascade như proc
                cmd.Parameters.Add("@XoaCung", SqlDbType.Bit).Value = 0;
                cmd.Parameters.Add("@Cascade", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
