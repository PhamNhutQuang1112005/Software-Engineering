using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           string configPath = "config.txt";

            // Kiểm tra file cấu hình tồn tại hay chưa
            if (!File.Exists(configPath))
            {
                // Chưa có => mở form Config để tạo database / cấu hình
                Application.Run(new GUI_Form_Config());
            }

           

            // Sau khi Config (hoặc nếu file đã tồn tại), chạy form đăng nhập
            Application.Run(new GUI_Form_DangNhap());
            
        }
    }
}
