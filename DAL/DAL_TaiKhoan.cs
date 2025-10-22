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
    }
}
