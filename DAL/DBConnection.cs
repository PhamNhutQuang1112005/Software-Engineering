using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnection
    {
        private static readonly string connectionString =
            "Server=tcp:lysql2.database.windows.net,1433;" +
            "Initial Catalog=SQL;" +  // 👉 thay bằng tên DB thật của bạn
            "Persist Security Info=False;" +
            "User ID=SQLadmin;" +
            "Password=P@ssw0rd;" +   // 👉 thay bằng mật khẩu thật
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
