using DAL;
using DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;


namespace BLL
{
    public class BLL_TaiKhoan
    {
        private readonly DAL_TaiKhoan _dal = new DAL_TaiKhoan();

        // Đăng nhập (không gọi lại nếu đã có session ở GUI)
        public DTO_NguoiDung DangNhap(string tenDangNhap, string matKhau) => _dal.DangNhap(tenDangNhap, matKhau);

        // Header theo username (không login lại)
        public DTO_NguoiDung GetUserHeaderByUsername(string tenDangNhap) => _dal.GetUserHeaderByUsername(tenDangNhap);

        // Danh sách / tìm kiếm
        public DataTable LayTatCaNguoiDung() => _dal.GetAllNguoiDung_DataTable();
        public DataTable TimKiemNguoiDung(string keyword) => _dal.TimKiemNguoiDung_DataTable(keyword);

        // CRUD
        public void ThemNguoiDung(DTO_NguoiDung dto, string matKhau) => _dal.ThemNguoiDung(dto, matKhau);
        public void SuaNguoiDung(DTO_NguoiDung dto, string matKhauNullable = null) => _dal.SuaNguoiDung(dto, matKhauNullable);
        public void XoaNguoiDung(string nguoiDungID) => _dal.XoaNguoiDung(nguoiDungID);

        // Từ điển
        public List<DTO_VaiTro>    GetAllVaiTro()     => _dal.GetAllVaiTro();
        public List<DTO_PhongBan>  GetAllPhongBan()   => _dal.GetAllPhongBan();
        public DTO_VaiTro          GetVaiTroByID(string vaiTroID)       => _dal.GetVaiTroByID(vaiTroID);
        public DTO_PhongBan        GetPhongBanByID(string phongBanID)   => _dal.GetPhongBanByID(phongBanID);
        public bool HasAdminAccount()
        {
            DataTable users = _dal.GetAllNguoiDung_DataTable();
            if (users == null) return false;

            return users.AsEnumerable().Any(u =>
                u["VaiTroID"] != DBNull.Value &&
                string.Equals(u["VaiTroID"].ToString(), "VT001", StringComparison.OrdinalIgnoreCase));
        }
    }
}
