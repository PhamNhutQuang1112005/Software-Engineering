using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DAL_DonHang
    {
        // Helpers
        private static object ToDb(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s;
        private static object ToDb(DateTime? d) => d.HasValue ? (object)d.Value : DBNull.Value;
        private static object ToDbObj(object o) => o ?? DBNull.Value;

        // ====== GETTERS ======
        public static DataTable GetAllDonHang()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllDonHang", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static DataTable GetDonHangByID(string donHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetDonHangByID", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable(); da.Fill(dt); return dt;
                }
            }
        }

        public static DataTable GetAllTrangThaiDonHang()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllTrangThaiDonHang", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static DataTable GetAllHopDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_GetAllHopDong_Short", conn) { CommandType = CommandType.StoredProcedure })
            using (var da   = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static string GetMaDonHangTuDong()
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_TaoMaDonHangTuDong", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Convert.ToString(reader["MaDonHangMoi"]);
                }
            }
            return null;
        }

        // ====== LEGACY API (giữ để tương thích) ======
        public static void ThemDonHang(string donHangID, string maDonHang, string hopDongID, string trangThaiID, string moTa, string khachHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_ThemDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID",   ToDb(donHangID));
                cmd.Parameters.AddWithValue("@MaDonHang",   ToDb(maDonHang));
                cmd.Parameters.AddWithValue("@HopDongID",   ToDb(hopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID", ToDb(trangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu",      ToDb(moTa));
                cmd.Parameters.AddWithValue("@IDKhachHang", ToDb(khachHangID));

                // Các tham số mới để SP không lỗi khi bạn đã nâng cấp SP (cho phép null)
                cmd.Parameters.AddWithValue("@DiaChi",      DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayLayMau",  DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDuKienTraKetQua", DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTraThucTe",       DBNull.Value);

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public static void CapNhatDonHang(string oldDonHangID, string newDonHangID, string maDonHang, string hopDongID, string trangThaiID, string ghiChu, string khachHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_UpdateDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@OldDonHangID", oldDonHangID);
                cmd.Parameters.AddWithValue("@NewDonHangID", newDonHangID);
                cmd.Parameters.AddWithValue("@MaDonHang",    ToDb(maDonHang));
                cmd.Parameters.AddWithValue("@HopDongID",    ToDb(hopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID",  ToDb(trangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu",       ToDb(ghiChu));
                cmd.Parameters.AddWithValue("@IDKhachHang",  ToDb(khachHangID));

                // Tham số mới để SP không lỗi (có thể null)
                cmd.Parameters.AddWithValue("@DiaChi",      DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayLayMau",  DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDuKienTraKetQua", DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTraThucTe",       DBNull.Value);

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        public static void XoaDonHang(string donHangID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_XoaDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID", donHangID);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // ====== NEW API dùng DTO ======
        public static void ThemDonHang(DTO_DonHang dh)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_ThemDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@DonHangID",   ToDb(dh.DonHangID));
                cmd.Parameters.AddWithValue("@MaDonHang",   ToDb(dh.MaDonHang));
                cmd.Parameters.AddWithValue("@HopDongID",   ToDb(dh.HopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID", ToDb(dh.TrangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu",      ToDb(dh.GhiChu));
                cmd.Parameters.AddWithValue("@IDKhachHang", ToDb(dh.IDKhachHang));

                cmd.Parameters.AddWithValue("@DiaChi",      ToDb(dh.DiaChi));
                cmd.Parameters.AddWithValue("@NgayLayMau",  ToDb(dh.NgayLayMau));
                // Nếu GUI đã cộng sẵn +15 thì truyền xuống; nếu để null SP sẽ tự cộng (xem phần SQL)
                cmd.Parameters.AddWithValue("@NgayDuKienTraKetQua", ToDb(dh.NgayDuKienTraKetQua));
                // THÊM: không cho nhập ngày trả thực tế → luôn null
                cmd.Parameters.AddWithValue("@NgayTraThucTe", DBNull.Value);

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Update không đổi ID
        public static void CapNhatDonHang(string oldDonHangID, DTO_DonHang dh)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd  = new SqlCommand("sp_UpdateDonHang", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@OldDonHangID", oldDonHangID);
                cmd.Parameters.AddWithValue("@NewDonHangID", ToDb(dh.DonHangID));
                cmd.Parameters.AddWithValue("@MaDonHang",    ToDb(dh.MaDonHang));
                cmd.Parameters.AddWithValue("@HopDongID",    ToDb(dh.HopDongID));
                cmd.Parameters.AddWithValue("@TrangThaiID",  ToDb(dh.TrangThaiID));
                cmd.Parameters.AddWithValue("@GhiChu",       ToDb(dh.GhiChu));
                cmd.Parameters.AddWithValue("@IDKhachHang",  ToDb(dh.IDKhachHang));

                cmd.Parameters.AddWithValue("@DiaChi",      ToDb(dh.DiaChi));
                cmd.Parameters.AddWithValue("@NgayLayMau",  ToDb(dh.NgayLayMau));
                cmd.Parameters.AddWithValue("@NgayDuKienTraKetQua", ToDb(dh.NgayDuKienTraKetQua));
                // SỬA: cho phép nhập Ngày trả thực tế (nếu user không check thì null)
                cmd.Parameters.AddWithValue("@NgayTraThucTe", ToDb(dh.NgayTraThucTe));

                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
    }
}