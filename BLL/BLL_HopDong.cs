using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public static class BLL_HopDong
    {
        public static DataTable GetAllHopDong()
            => DAL_HopDong.GetAllHopDong();

        public static void AddHopDong(DTO_HopDong dto)
            => DAL_HopDong.ThemHopDong(dto);

        public static void UpdateHopDong(DTO_HopDong dto)
            => DAL_HopDong.SuaHopDong(dto);

        public static void DeleteHopDong(string hopDongID)
            => DAL_HopDong.XoaHopDong(hopDongID);
    }
}
