using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public static class BLL_DonHang
    {
        // ====== GETTERS ======
        public static DataTable GetAllDonHang()
            => DAL_DonHang.GetAllDonHang();

        public static DataTable GetDonHangByID(string donHangID)
            => DAL_DonHang.GetDonHangByID(donHangID);

        public static DataTable GetAllTrangThaiDonHang()
            => DAL_DonHang.GetAllTrangThaiDonHang();

        public static DataTable GetAllHopDong()
            => DAL_DonHang.GetAllHopDong();

        public static string AddMaDonHang()
            => DAL_DonHang.GetMaDonHangTuDong();

        // ====== LEGACY API (giữ nguyên để tương thích code cũ) ======
        public static void DeleteDonHang(string donHangID)
            => DAL_DonHang.XoaDonHang(donHangID);

        // ====== NEW API dùng DTO gọn gàng (có địa chỉ & ngày) ======
        public static void AddDonHang(DTO_DonHang dh)
            => DAL_DonHang.ThemDonHang(dh);

        // Update giữ nguyên DonHangID; nếu bạn muốn đổi ID, dùng overload phía dưới
        public static void CapNhatDonHang(DTO_DonHang dh)
            => DAL_DonHang.CapNhatDonHang(dh.DonHangID, dh);

        // Overload cho phép đổi ID (old -> new)
        public static void UpdateDonHang(string oldDonHangID, DTO_DonHang dh)
            => DAL_DonHang.CapNhatDonHang(oldDonHangID, dh);
    }
}