using DAL;
using System.Data;

namespace BLL
{
    public class BLL_TaiKhoan
    {
        private readonly DAL_TaiKhoan dalTaiKhoan = new DAL_TaiKhoan();

        public DataTable DangNhap(string tenDangNhap, string matKhau)
        {
            return dalTaiKhoan.DangNhap(tenDangNhap, matKhau);
        }
    }
}
