using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ThongSoMoiTruong
    {
        public DataTable GetThongSoMoiTruongView()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = @"
            SELECT 
                ts.TenThongSo AS [Tên],
                dv.TenDonVi AS [Đơn vị],
                ts.GiaTri AS [Vị trí đo 1],
                ts.GiaTriQuyChuan AS [Vị trí đo 2],
                ts.KetLuan AS [Vị trí đo 3],
                ts.GiaTriSo AS [Giá trị chuẩn]
            FROM ThongSoMoiTruong ts
            LEFT JOIN DonVi dv ON ts.DonViID = dv.DonViID;
        ";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }


    }
}
