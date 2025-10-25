using DAL;
using System;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BLL_TaiKhoan
    {
        private readonly DAL_TaiKhoan dalTaiKhoan = new DAL_TaiKhoan();
        public bool DoiMatKhauCoKiemTra(string nguoiDungID, string matKhauCu, string matKhauMoi)
{
    // Lấy mật khẩu hash từ DB
        string matKhauHienTai = dalTaiKhoan.LayMatKhauTheoID(nguoiDungID);
         if (matKhauHienTai == null)
            throw new Exception("Không tìm thấy người dùng.");

    // So sánh mật khẩu cũ (có thể hash tùy DB)
    if (matKhauHienTai != matKhauCu)
        return false; // Sai mật khẩu cũ

    // Đúng mật khẩu -> cập nhật mật khẩu mới
    dalTaiKhoan.CapNhatMatKhau(nguoiDungID, matKhauMoi);
    return true;
}


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
        public class ThongTinUser
{
    public string NguoiDungID { get; set; }
    public string TenDangNhap { get; set; }
    public string HoTen       { get; set; }

    public string VaiTroID    { get; set; }
    public string VaiTro      { get; set; }    // ưu tiên TenVaiTro, fallback VaiTroID

    public string PhongBanID  { get; set; }
    public string Phong       { get; set; }    // ưu tiên TenPhongBan, fallback PhongBanID

    public string Sdt         { get; set; }    // DienThoai
    public string Email       { get; set; }
    
}

// ===== BLL: lấy đủ header theo Tên đăng nhập =====
public ThongTinUser GetUserHeaderByUsername(string tenDangNhap)
{
    var dt = TimKiemNguoiDung(tenDangNhap);
    if (dt == null || dt.Rows.Count == 0) return null;

    // Lấy đúng dòng theo TenDangNhap; nếu không có cột/không khớp, lấy dòng đầu
    var row = dt.AsEnumerable()
                .FirstOrDefault(r =>
                    r.Table.Columns.Contains("TenDangNhap") &&
                    string.Equals(Convert.ToString(r["TenDangNhap"]), tenDangNhap, StringComparison.OrdinalIgnoreCase))
             ?? dt.Rows[0];

    // Helper đọc cột an toàn
    string Get(string col) => row.Table.Columns.Contains(col) ? Convert.ToString(row[col]) : null;
    string Tidy(string s)  => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    var dto = new ThongTinUser
    {
        NguoiDungID = Tidy(Get("NguoiDungID")),
        TenDangNhap = Tidy(Get("TenDangNhap")) ?? tenDangNhap,
        HoTen       = Tidy(Get("HoVaTen")) ?? Tidy(Get("HoTen")) ?? tenDangNhap,

        VaiTroID    = Tidy(Get("VaiTroID")),
        VaiTro      = Tidy(Get("TenVaiTro")) ?? Tidy(Get("VaiTroID")),

        PhongBanID  = Tidy(Get("PhongBanID")),
        Phong       = Tidy(Get("TenPhongBan")) ?? Tidy(Get("PhongBanID")),

        Sdt         = Tidy(Get("DienThoai")) ?? Tidy(Get("SoDienThoai")),
        Email       = Tidy(Get("Email")),
        
    };

    return dto;
}
    }


}
