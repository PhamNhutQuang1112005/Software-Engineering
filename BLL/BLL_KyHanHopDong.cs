using System.Data;
using DAL;

namespace BLL
{
    public static class BLL_KyHanHopDong
    {
        public static DataTable GetAllKyHanHopDong()
            => DAL_KyHanHopDong.GetAllKyHanHopDong();
    }
}
