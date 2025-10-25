using System;
using System.Data;
using DAL;

namespace BLL
{
    public static class BLL_HopDong
    {
        public static DataTable GetAllHopDong()
            => DAL_HopDong.GetAllHopDong();

        public static void XoaHopDong(string id)
            => DAL_HopDong.XoaHopDong(id);

        // Thêm m?i: truy?n vào HopDongID do code sinh ra
        public static void ThemHopDong(
            string hopDongID,
            string maHopDong,
            string khachHangID,
            DateTime ngayKy,
            string kyHanID,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string trangThai,
            string ghiChu)
            => DAL_HopDong.ThemHopDong(hopDongID, maHopDong, khachHangID, ngayKy, kyHanID, ngayBatDau, ngayKetThuc, trangThai, ghiChu);

        public static void SuaHopDong(
            string hopDongID,
            string maHopDong,
            string khachHangID,
            DateTime ngayKy,
            string kyHanID,
            DateTime? ngayBatDau,
            DateTime? ngayKetThuc,
            string trangThai,
            string ghiChu)
            => DAL_HopDong.SuaHopDong(hopDongID, maHopDong, khachHangID, ngayKy, kyHanID, ngayBatDau, ngayKetThuc, trangThai, ghiChu);
    }
}
