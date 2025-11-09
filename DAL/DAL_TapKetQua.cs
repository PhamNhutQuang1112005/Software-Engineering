using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class DAL_TapKetQua
    {
        public static void ThemTapKetQua(string ketQuaID,
                                         string donHangID,
                                         string tenFile,
                                         string duongDan,
                                         string loaiFile,
                                         string generatedBy = null,
                                         string trangThaiXuat = null)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("sp_ThemTapKetQua", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KetQuaID", SqlDbType.NVarChar, 50).Value = ketQuaID;
                cmd.Parameters.Add("@DonHangID", SqlDbType.NVarChar, 50).Value = donHangID;
                cmd.Parameters.Add("@TenFile", SqlDbType.NVarChar, 255).Value = tenFile;
                cmd.Parameters.Add("@DuongDan", SqlDbType.NVarChar, 500).Value = (object)duongDan ?? DBNull.Value;
                cmd.Parameters.Add("@LoaiFile", SqlDbType.NVarChar, 50).Value = (object)loaiFile ?? DBNull.Value;
                cmd.Parameters.Add("@GeneratedBy", SqlDbType.NVarChar, 50).Value = (object)generatedBy ?? DBNull.Value;
                cmd.Parameters.Add("@TrangThaiXuat", SqlDbType.NVarChar, 50).Value = (object)trangThaiXuat ?? DBNull.Value;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
