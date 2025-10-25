using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_DonHang
    {
        // ================== HỖ TRỢ ==================
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        // ================== LẤY DANH SÁCH ==================
        public static DataTable GetAllDonHang()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DonHangID, MaDonHang, HopDongID, TrangThaiID, GhiChu FROM DonHang WHERE ISNULL(IsDeleted, 0) = 0", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ================== THÊM ĐƠN HÀNG ==================
        public static void ThemDonHang(string maDonHang, string hopDongID, string trangThaiID, string moTa)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO DonHang (MaDonHang, HopDongID, TrangThaiID, GhiChu, NgayTao, IsDeleted)
                VALUES (@MaDonHang, @HopDongID, @TrangThaiID, @GhiChu, GETDATE(), 0)", conn))
            {
                cmd.Parameters.AddWithValue("@MaDonHang", ToDb(maDonHang));
                cmd.Parameters.AddWithValue("@HopDongID", ToDb(hopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID", ToDb(trangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu", ToDb(moTa));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ================== XÓA ĐƠN HÀNG ==================
        public static void XoaDonHang(string donHangID)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE DonHang SET IsDeleted = 1, DeletedAt = GETDATE() WHERE DonHangID = @DonHangID", conn))
            {
                cmd.Parameters.AddWithValue("@DonHangID", ToDb(donHangID));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ================== LẤY TRẠNG THÁI ==================
        public static DataTable GetAllTrangThaiDonHang()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TrangThaiID, TenTrangThai FROM TrangThaiDonHang", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ================== LẤY HỢP ĐỒNG ==================
        public static DataTable GetAllHopDong()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT HopDongID FROM HopDong", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
