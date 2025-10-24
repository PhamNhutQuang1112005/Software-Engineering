using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLL_ThongSoMoiTruong
    {
        DAL_ThongSoMoiTruong dal = new DAL_ThongSoMoiTruong();

        public DataTable GetThongSoMoiTruongView()
        {
            return dal.GetThongSoMoiTruongView();
        }
    }
}
