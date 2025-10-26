using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public static class BLL_HopDong
    {
        public static DataTable GetAllHopDong()
            => DAL_HopDong.GetAllHopDong();

        public static void ThemHopDong(DTO_HopDong dto)
            => DAL_HopDong.ThemHopDong(dto);

        public static void SuaHopDong(DTO_HopDong dto)
            => DAL_HopDong.SuaHopDong(dto);

        public static void XoaHopDong(string hopDongID)
            => DAL_HopDong.XoaHopDong(hopDongID);
    }
}
