using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_KhachHang
    {
        // Helper: null/empty -> DBNull
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        // ===== Lấy danh sách =====
        public static DataTable GetAllKhachHang()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_LayDanhSachKhachHang", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ===== Xóa =====
        public static void XoaKhachHang(string khachHangId)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_XoaKhachHang", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = khachHangId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===== Sửa =====
        // Phù hợp với script sp_KhachHang_Update bạn đã dùng (có MaKhachHang & MaSoThue)
        public static void SuaKhachHang(
            string khachHangId,
            string maKhachHang,
            string tenCongTy,
            string maSoThue,
            string nguoiDaiDien,
            string dienThoai,
            string email,
            string diaChi,
            string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_KhachHang_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@KhachHangID", SqlDbType.NVarChar, 50).Value = khachHangId;
                cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar, 50).Value = maKhachHang;
                cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 200).Value = tenCongTy;

                cmd.Parameters.Add("@MaSoThue", SqlDbType.NVarChar, 50).Value = ToDb(maSoThue);
                cmd.Parameters.Add("@NguoiDaiDien", SqlDbType.NVarChar, 200).Value = ToDb(nguoiDaiDien);
                cmd.Parameters.Add("@DienThoai", SqlDbType.NVarChar, 20).Value = ToDb(dienThoai);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDb(email);
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 500).Value = ToDb(diaChi);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = ToDb(ghiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===== Thêm (KHÔNG truyền KhachHangID vì proc tự sinh) =====
        public static void ThemKhachHang(
            string maKhachHang,
            string tenCongTy,
            string maSoThue,
            string nguoiDaiDien,
            string dienThoai,
            string email,
            string diaChi,
            string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_ThemKhachHang", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar, 50).Value = maKhachHang;
                cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 200).Value = tenCongTy;

                cmd.Parameters.Add("@MaSoThue", SqlDbType.NVarChar, 50).Value = ToDb(maSoThue);
                cmd.Parameters.Add("@NguoiDaiDien", SqlDbType.NVarChar, 200).Value = ToDb(nguoiDaiDien);
                cmd.Parameters.Add("@DienThoai", SqlDbType.NVarChar, 20).Value = ToDb(dienThoai);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDb(email);
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 500).Value = ToDb(diaChi);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = ToDb(ghiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Tuỳ chọn: nếu proc SELECT @NewID AS KhachHangID ở cuối, dùng hàm này để lấy ID mới.
        public static string ThemKhachHang_LayID(
            string maKhachHang,
            string tenCongTy,
            string maSoThue,
            string nguoiDaiDien,
            string dienThoai,
            string email,
            string diaChi,
            string ghiChu)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_ThemKhachHang", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MaKhachHang", SqlDbType.NVarChar, 50).Value = maKhachHang;
                cmd.Parameters.Add("@TenCongTy", SqlDbType.NVarChar, 200).Value = tenCongTy;
                cmd.Parameters.Add("@MaSoThue", SqlDbType.NVarChar, 50).Value = ToDb(maSoThue);
                cmd.Parameters.Add("@NguoiDaiDien", SqlDbType.NVarChar, 200).Value = ToDb(nguoiDaiDien);
                cmd.Parameters.Add("@DienThoai", SqlDbType.NVarChar, 20).Value = ToDb(dienThoai);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = ToDb(email);
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 500).Value = ToDb(diaChi);
                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = ToDb(ghiChu);

                conn.Open();
                var result = cmd.ExecuteScalar(); // yêu cầu proc có SELECT @NewID
                return result == null ? null : result.ToString();
            }
        }
    }
}
