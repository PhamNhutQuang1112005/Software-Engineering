using System.Data;
using DAL;

namespace BLL
{
    public static class BLL_DonHang
    {
        public static DataTable GetAllDonHang()
            => DAL_DonHang.GetAllDonHang();

        public static void ThemDonHang(string DonHangID,string maDonHang, string hopDongID, string trangThaiID, string moTa,string khachhang)
            => DAL_DonHang.ThemDonHang(DonHangID,maDonHang, hopDongID, trangThaiID, moTa,khachhang);

        public static void XoaDonHang(string donHangID)
            => DAL_DonHang.XoaDonHang(donHangID);

        public static DataTable GetAllTrangThaiDonHang()
            => DAL_DonHang.GetAllTrangThaiDonHang();


        public static DataTable GetAllHopDong()
            => DAL_DonHang.GetAllHopDong();
        public static string SinhMaDonHang()
        {
            return DAL_DonHang.GetMaDonHangTuDong();
        }
        public static void CapNhatDonHang(string oldDonHangID, string newDonHangID, string maDonHang, string hopDongID, string trangThaiID, string ghiChu, string khachHangID)
        {
            DAL_DonHang.CapNhatDonHang(oldDonHangID, newDonHangID, maDonHang, hopDongID, trangThaiID, ghiChu, khachHangID);
        }
        public static DataTable GetDonHangByID(string donHangID)
        {
            return DAL_DonHang.GetDonHangByID(donHangID);
        }


    }
}
