using System;
using System.IO;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnection
    {
        private static readonly string configPath = "config.txt";

        public static string GetConnectionString()
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Không tìm thấy file cấu hình kết nối!");

            string[] lines = File.ReadAllLines(configPath);
            if (lines.Length < 3)
                throw new Exception("File cấu hình không hợp lệ!");

            string authen = lines[0].Trim().ToLower();
            string server = lines[1].Trim();
            string db = lines[2].Trim();

            if (authen == "windows")
            {
                return $"Data Source={server};Initial Catalog={db};Integrated Security=True;TrustServerCertificate=True;";
            }
            else if (authen == "sql" && lines.Length >= 5)
            {
                string uid = lines[3].Trim();
                string pw = lines[4].Trim();
                return $"Data Source={server};Initial Catalog={db};User ID={uid};Password={pw};TrustServerCertificate=True;";
            }

            throw new Exception("Cấu hình không hợp lệ hoặc thiếu thông tin.");
        }

        public static SqlConnection GetConnection()
        {
            string connStr = GetConnectionString();
            return new SqlConnection(connStr);
        }
    }
}
