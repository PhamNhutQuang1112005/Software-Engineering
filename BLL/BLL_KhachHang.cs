using System.Data;
using DAL;

namespace BLL
{
    public static class BLL_KhachHang
    {
        public static DataTable GetAllKhachHang()
            => DAL_KhachHang.GetAllKhachHang();

        public static void XoaKhachHang(string id)
            => DAL_KhachHang.XoaKhachHang(id);

        // SỬA KHÁCH HÀNG (khớp sp_KhachHang_Update)
        public static void SuaKhachHang(
            string id,          // KhachHangID
            string ma,          // MaKhachHang
            string ten,         // TenCongTy
            string maSoThue,    // nullable
            string nguoiDaiDien,
            string sdt,
            string email,
            string diachi,
            string ghichu)
            => DAL_KhachHang.SuaKhachHang(id, ma, ten, maSoThue, nguoiDaiDien, sdt, email, diachi, ghichu);

        // THÊM KHÁCH HÀNG (ID tự sinh trong proc)
        public static void ThemKhachHang(
            string ma,          // MaKhachHang
            string ten,         // TenCongTy
            string maSoThue,    // nullable
            string nguoiDaiDien,
            string sdt,
            string email,
            string diachi,
            string ghichu)
            => DAL_KhachHang.ThemKhachHang(ma, ten, maSoThue, nguoiDaiDien, sdt, email, diachi, ghichu);
    }
}
