using System.Data;
using DAL;

namespace BLL
{
    public class BLL_PhongBan
    {
        

        public static DataTable GetAllPhongBan()
           => DAL_PhongBan.GetAllPhongBan();
    }
}
