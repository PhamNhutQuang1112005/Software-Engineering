using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_TaiKhoan
    {
        public DataTable DangNhap(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand("sp_DangNhapNguoiDung", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                return dt;
            }
        }
        public string LayMatKhauTheoID(string id)
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("SELECT MatKhauHash FROM NguoiDung WHERE NguoiDungID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        return cmd.ExecuteScalar()?.ToString();
    }
}

public void CapNhatMatKhau(string id, string newPass)
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("UPDATE NguoiDung SET MatKhauHash = @mk WHERE NguoiDungID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@mk", newPass);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
        // Lấy tất cả người dùng
        public static DataTable GetAllNguoiDung()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllNguoiDung", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public static void ThemNguoiDung(string tenDN, string matKhau, string hoTen,
                                         string sdt, string email, string vaiTroID, string phongBanID)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_ThemNguoiDung", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDN);
                cmd.Parameters.AddWithValue("@MatKhauHash", matKhau);
                cmd.Parameters.AddWithValue("@HoVaTen", hoTen);
                cmd.Parameters.AddWithValue("@DienThoai", (object)sdt ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@VaiTroID", vaiTroID);
                cmd.Parameters.AddWithValue("@PhongBanID", (object)phongBanID ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // (Tuỳ chọn) update người dùng
        

        public static DataTable GetAllVaiTro()
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_GetAllVaiTro", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}

public static DataTable GetAllPhongBan()
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_GetAllPhongBan", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}
        // (Tuỳ chọn) update người dùng
        public static void SuaNguoiDung(string id, string tenDN, string matKhau, string hoTen,
                                string sdt, string email, string vaiTroID, string phongBanID)
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_SuaNguoiDung", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@NguoiDungID", id);
        cmd.Parameters.AddWithValue("@TenDangNhap", tenDN);
        cmd.Parameters.AddWithValue("@MatKhauHash", matKhau);
        cmd.Parameters.AddWithValue("@HoVaTen", hoTen);
        cmd.Parameters.AddWithValue("@DienThoai", (object)sdt ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@VaiTroID", vaiTroID);
        cmd.Parameters.AddWithValue("@PhongBanID", (object)phongBanID ?? DBNull.Value);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}

public static void XoaNguoiDung(string id)
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_XoaNguoiDung", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@NguoiDungID", id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
        public static DataTable TimKiemNguoiDung(string keyword)
{
    using (SqlConnection conn = DBConnection.GetConnection())
    {
        SqlCommand cmd = new SqlCommand("sp_TimKiemNguoiDung", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Keyword", (object)keyword ?? DBNull.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}
    }


}
