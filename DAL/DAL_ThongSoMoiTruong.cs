using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_ThongSoMoiTruong
    {
        // ✅ Đọc dữ liệu hiển thị
        public DataTable GetThongSoMoiTruongView()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                     
                        ts.TenThongSo AS [Tên thông số],
                        ts.DonVi AS [Đơn vị],
                        ts.GiaTri AS [Vị trí đo 1],
                        ts.GiaTriQuyChuan AS [Vị trí đo 2],
                        ts.KetLuan AS [Vị trí đo 3],
                        ts.GiaTriSo AS [Giá trị chuẩn],
                        ts.NguoiPhanTichID AS [Phòng phụ trách]
                    FROM ThongSoMoiTruong ts
                    
                ";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // ✅ Thêm mới
        public bool InsertOrUpdate(string tenThongSo, string donViId, string giaTri, string giaTriQuyChuan, string ketLuan, int giaTriSo,string phongphutrach)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = @"
IF EXISTS (SELECT 1 FROM ThongSoMoiTruong WHERE TenThongSo = @TenThongSo)
BEGIN
    UPDATE ThongSoMoiTruong
    SET DonVi = @DonViID,
        GiaTri = @GiaTri,
        GiaTriQuyChuan = @GiaTriQuyChuan,
        KetLuan = @KetLuan,
        GiaTriSo = @GiaTriSo,
        NguoiPhanTichID = @phongphutrach
    WHERE TenThongSo = @TenThongSo
END
ELSE
BEGIN
    INSERT INTO ThongSoMoiTruong 
    (TenThongSo, DonVi, GiaTri, GiaTriQuyChuan, KetLuan, GiaTriSo, NguoiPhanTichID)
    VALUES 
    (@TenThongSo, @DonViID, @GiaTri, @GiaTriQuyChuan, @KetLuan, @GiaTriSo, @phongphutrach)
END";



                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);
                cmd.Parameters.AddWithValue("@DonViID", donViId);
                cmd.Parameters.AddWithValue("@GiaTri", giaTri);
                cmd.Parameters.AddWithValue("@GiaTriQuyChuan", giaTriQuyChuan);
                cmd.Parameters.AddWithValue("@KetLuan", ketLuan);
                cmd.Parameters.AddWithValue("@GiaTriSo", giaTriSo);
                cmd.Parameters.AddWithValue("@phongphutrach", phongphutrach);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        // ✅ Xóa (nên xóa theo ID thay vì tên)
        public bool XoaThongSo(string tenThongSo)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "DELETE FROM ThongSoMoiTruong WHERE TenThongSo = @TenThongSo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenThongSo", tenThongSo);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
