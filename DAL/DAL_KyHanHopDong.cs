using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class DAL_KyHanHopDong
    {
        public static DataTable GetAllKyHanHopDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_GetAllKyHanHopDong", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
