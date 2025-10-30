using System;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class BLL_ThongSoMoiTruong
    {
        private readonly DAL_ThongSoMoiTruong dal = new DAL_ThongSoMoiTruong();

        // ✅ Lấy danh sách hiển thị
        public DataTable GetThongSoMoiTruongView()
        {
            return dal.GetThongSoMoiTruongView();
        }

        // ✅ Thêm mới
        public bool LuuThongSoMoiTruong(string tenThongSo, string donViId, string giaTri, string giaTriQuyChuan, string ketLuan, int giaTriSo,string phongphutrach)
        {
            return dal.InsertOrUpdate(tenThongSo, donViId, giaTri, giaTriQuyChuan, ketLuan, giaTriSo,phongphutrach);
        }

        // ✅ Xóa
        public bool XoaThongSo(string tenThongSo)
        {
            DAL_ThongSoMoiTruong dal = new DAL_ThongSoMoiTruong();
            return dal.XoaThongSo(tenThongSo);
        }

    }
}
