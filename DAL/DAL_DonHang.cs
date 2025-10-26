using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_DonHang
    {
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        public static DataTable GetAllDonHang()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllDonHang", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static DataTable GetDonHangByID(string donHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetDonHangByID", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable(); da.Fill(dt); return dt;
                }
            }
        }

        public static void ThemDonHang(string donHangID, string maDonHang, string hopDongID, string trangThaiID, string moTa, string khachHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_ThemDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID",  ToDb(donHangID));
                cmd.Parameters.AddWithValue("@MaDonHang",  ToDb(maDonHang));
                cmd.Parameters.AddWithValue("@HopDongID",  ToDb(hopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID",ToDb(trangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu",     ToDb(moTa));
                cmd.Parameters.AddWithValue("@IDKhachHang",ToDb(khachHangID));
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public static void CapNhatDonHang(string oldDonHangID, string newDonHangID, string maDonHang, string hopDongID, string trangThaiID, string ghiChu, string khachHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_UpdateDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@OldDonHangID", oldDonHangID);
                cmd.Parameters.AddWithValue("@NewDonHangID", newDonHangID);
                cmd.Parameters.AddWithValue("@MaDonHang",    maDonHang);
                cmd.Parameters.AddWithValue("@HopDongID",    hopDongID);
                cmd.Parameters.AddWithValue("@TrangThaiID",  trangThaiID);
                cmd.Parameters.AddWithValue("@GhiChu",      (object)ghiChu      ?? DBNull.Value);
cmd.Parameters.AddWithValue("@IDKhachHang", (object)khachHangID ?? DBNull.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public static void XoaDonHang(string donHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_XoaDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetAllTrangThaiDonHang()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllTrangThaiDonHang", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static DataTable GetAllHopDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllHopDong_Short", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static string GetMaDonHangTuDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_TaoMaDonHangTuDong", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Convert.ToString(reader["MaDonHangMoi"]);
                }
            }
            return null;
        }
    }
}
