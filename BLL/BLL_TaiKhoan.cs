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
        public DataTable LayTatCaNguoiDung()
        {
            return DAL_TaiKhoan.GetAllNguoiDung();
        }
         public void ThemNguoiDung(string tenDN, string matKhau, string hoTen,
                                  string sdt, string email, string vaiTroID, string phongBanID)
            => DAL_TaiKhoan.ThemNguoiDung(tenDN, matKhau, hoTen, sdt, email, vaiTroID, phongBanID);

        

        public DataTable GetAllVaiTro()
{
    return DAL_TaiKhoan.GetAllVaiTro();
}

public DataTable GetAllPhongBan()
{
    return DAL_TaiKhoan.GetAllPhongBan();
}
        public void SuaNguoiDung(string id, string tenDN, string matKhau, string hoTen,
                         string sdt, string email, string vaiTroID, string phongBanID)
{
    DAL_TaiKhoan.SuaNguoiDung(id, tenDN, matKhau, hoTen, sdt, email, vaiTroID, phongBanID);
}

public void XoaNguoiDung(string id)
{
    DAL_TaiKhoan.XoaNguoiDung(id);
}
        public DataTable TimKiemNguoiDung(string keyword)
{
    return DAL_TaiKhoan.TimKiemNguoiDung(keyword);
}
    }

}
