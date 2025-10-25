using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_PhongBan
    {
        // ================== HỖ TRỢ ==================
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;

        // ================== LẤY DANH SÁCH ==================
        public static DataTable GetAllPhongBan()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PhongBanID, TenPhongBan FROM PhongBan", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}