using System.Data;
using DAL;

namespace BLL
{
    public static class BLL_DonHang
    {
        public static DataTable GetAllDonHang()
            => DAL_DonHang.GetAllDonHang();

        public static void ThemDonHang(string maDonHang, string hopDongID, string trangThaiID, string moTa)
            => DAL_DonHang.ThemDonHang(maDonHang, hopDongID, trangThaiID, moTa);

        public static void XoaDonHang(string donHangID)
            => DAL_DonHang.XoaDonHang(donHangID);

        public static DataTable GetAllTrangThaiDonHang()
            => DAL_DonHang.GetAllTrangThaiDonHang();

        public static DataTable GetAllHopDong()
            => DAL_DonHang.GetAllHopDong();
    }
}
