using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Application.Run(new GUI_Form_DangNhap());
            Application.Run(new GUI_Form_DangNhap_Moi());
            Application.Run(new GUI_Form_DangNhap_OTP());
            Application.Run(new GUI_Form_DangNhap_SMS());
            Application.Run(new GUI_main());
        }
    }
}
