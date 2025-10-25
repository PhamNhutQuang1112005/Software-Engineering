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
           

           

            // Sau khi Config (hoặc nếu file đã tồn tại), chạy form đăng nhập
            Application.Run(new GUI_Form_DangNhap());
            
        }
    }
}
