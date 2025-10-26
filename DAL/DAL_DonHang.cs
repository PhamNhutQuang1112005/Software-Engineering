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
    "SELECT DonHangID, MaDonHang, HopDongID, TrangThaiID, GhiChu, IDKhachHang FROM DonHang WHERE ISNULL(IsDeleted, 0) = 0", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ================== THÊM ĐƠN HÀNG ==================
        public static void ThemDonHang(string DonHangID,string maDonHang, string hopDongID, string trangThaiID, string moTa,string khachhang)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO DonHang (DonHangID, MaDonHang,HopDongID, TrangThaiID, GhiChu,IDKhachHang )
                VALUES (@DonHangID, @maDonHang,@hopDongID, @TrangThaiID, @GhiChu,@IDKhachHang)", conn))
            {
                cmd.Parameters.AddWithValue("@DonHangID", ToDb(DonHangID));
                cmd.Parameters.AddWithValue("@maDonHang", ToDb(maDonHang));
                cmd.Parameters.AddWithValue("@hopDongID", ToDb(hopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID", ToDb(trangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu", ToDb(moTa));
                cmd.Parameters.AddWithValue("@IDKhachHang", ToDb(khachhang));

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
        public static string GetMaDonHangTuDong()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_TaoMaDonHangTuDong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["MaDonHangMoi"].ToString();
                    }
                }
            }

            return null; // Trường hợp không có kết quả
        }
        public static void CapNhatDonHang(string oldDonHangID, string newDonHangID, string maDonHang, string hopDongID, string trangThaiID, string ghiChu, string khachHangID)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
        UPDATE DonHang
        SET DonHangID = @NewDonHangID,
            MaDonHang = @MaDonHang,
            HopDongID = @HopDongID,
            TrangThaiID = @TrangThaiID,
            GhiChu = @GhiChu,
            IDKhachHang = @KhachHangID
        WHERE DonHangID = @OldDonHangID", conn))
            {
                cmd.Parameters.AddWithValue("@OldDonHangID", oldDonHangID);
                cmd.Parameters.AddWithValue("@NewDonHangID", newDonHangID);
                cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                cmd.Parameters.AddWithValue("@HopDongID", hopDongID);
                cmd.Parameters.AddWithValue("@TrangThaiID", trangThaiID);
                cmd.Parameters.AddWithValue("@GhiChu", ghiChu ?? "");
                cmd.Parameters.AddWithValue("@KhachHangID", khachHangID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable GetDonHangByID(string donHangID)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM DonHang WHERE DonHangID = @DonHangID", conn))
            {
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }



    }
}
