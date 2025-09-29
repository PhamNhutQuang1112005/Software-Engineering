using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM_UI_QUANG
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
            Application.Run(new dangnhap_login());
            Application.Run(new dangnhap_sms());
            Application.Run(new dangnhap_otp());
            Application.Run(new dangnhap_matkhaumoi());
        }
    }
}
