using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DAL_TaiKhoan
    {
        private static SqlConnection NewConn() => DBConnection.GetConnection();
        private static SqlCommand NewSp(SqlConnection conn, string spName)
            => new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure };

        // ===== ĐĂNG NHẬP (trả DTO_NguoiDung) =====
        public DTO_NguoiDung DangNhap(string tenDangNhap, string matKhau)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_DangNhapNguoiDung"))
            {
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 100).Value = tenDangNhap;
                cmd.Parameters.Add("@MatKhau",     SqlDbType.NVarChar, 255).Value = matKhau;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new DTO_NguoiDung
                    {
                        NguoiDungID = r["NguoiDungID"] as string,
                        TenDangNhap = tenDangNhap,
                        HoVaTen     = r["HoVaTen"]   as string,
                        Email       = r["Email"]     as string,
                        DienThoai   = r["DienThoai"] as string,
                        VaiTroID    = r["VaiTroID"]  as string,
                        PhongBanID  = r["PhongBanID"] as string,
                      
                    };
                }
            }
        }

        // ===== HEADER BY USERNAME (không login lại) =====
        public DTO_NguoiDung GetUserHeaderByUsername(string tenDangNhap)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetUserHeaderByUsername"))
            {
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 100).Value = tenDangNhap;
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new DTO_NguoiDung
                    {
                        NguoiDungID = r["NguoiDungID"] as string,
                        TenDangNhap = r["TenDangNhap"] as string,
                        HoVaTen     = r["HoVaTen"]     as string,
                        DienThoai   = r["DienThoai"]   as string,
                        Email       = r["Email"]       as string,
                        VaiTroID    = r["VaiTroID"]    as string,
                        PhongBanID  = r["PhongBanID"]  as string,
                        DiaChi      = r["DiaChi"]      as string,
                        HinhDaiDien = r["HinhDaiDien"] as byte[]

                    };
                }
            }
        }

        // ===== DANH SÁCH / TÌM KIẾM =====
        public DataTable GetAllNguoiDung_DataTable()
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetAllNguoiDung"))
            {
                using (var ad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable TimKiemNguoiDung_DataTable(string keyword)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_TimKiemNguoiDung"))
            {
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = (object)keyword ?? DBNull.Value;
                using (var ad = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    ad.Fill(dt);
                    return dt;
                }
            }
        }

        // ===== CRUD =====
        public void ThemNguoiDung(DTO_NguoiDung dto, string matKhau)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_ThemNguoiDung"))
            {
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 100).Value = dto.TenDangNhap;
                cmd.Parameters.Add("@MatKhauHash", SqlDbType.NVarChar, 255).Value = (object)matKhau ?? DBNull.Value; // không hash theo yêu cầu
                cmd.Parameters.Add("@HoVaTen",     SqlDbType.NVarChar, 200).Value = (object)dto.HoVaTen ?? DBNull.Value;
                cmd.Parameters.Add("@DienThoai",   SqlDbType.NVarChar, 20 ).Value = (object)dto.DienThoai ?? DBNull.Value;
                cmd.Parameters.Add("@Email",       SqlDbType.NVarChar, 100).Value = (object)dto.Email     ?? DBNull.Value;
                cmd.Parameters.Add("@VaiTroID",    SqlDbType.NVarChar, 50 ).Value = dto.VaiTroID;
                cmd.Parameters.Add("@PhongBanID",  SqlDbType.NVarChar, 50 ).Value = (object)dto.PhongBanID ?? DBNull.Value;
                cmd.Parameters.Add("@DiaChi",      SqlDbType.NVarChar, 500).Value = (object)dto.DiaChi    ?? DBNull.Value;
                cmd.Parameters.Add("@HinhDaiDien", SqlDbType.VarBinary      ).Value = (object)dto.HinhDaiDien ?? DBNull.Value;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SuaNguoiDung(DTO_NguoiDung dto, string matKhauNullable)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_SuaNguoiDung"))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.NVarChar, 50 ).Value = dto.NguoiDungID;
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 100).Value = dto.TenDangNhap;
                cmd.Parameters.Add("@MatKhauHash", SqlDbType.NVarChar, 255).Value = (object)matKhauNullable ?? DBNull.Value; // null => giữ mật khẩu cũ
                cmd.Parameters.Add("@HoVaTen",     SqlDbType.NVarChar, 200).Value = (object)dto.HoVaTen ?? DBNull.Value;
                cmd.Parameters.Add("@DienThoai",   SqlDbType.NVarChar, 20 ).Value = (object)dto.DienThoai ?? DBNull.Value;
                cmd.Parameters.Add("@Email",       SqlDbType.NVarChar, 100).Value = (object)dto.Email     ?? DBNull.Value;
                cmd.Parameters.Add("@VaiTroID",    SqlDbType.NVarChar, 50 ).Value = dto.VaiTroID;
                cmd.Parameters.Add("@PhongBanID",  SqlDbType.NVarChar, 50 ).Value = (object)dto.PhongBanID ?? DBNull.Value;
                cmd.Parameters.Add("@DiaChi",      SqlDbType.NVarChar, 500).Value = (object)dto.DiaChi    ?? DBNull.Value;
                cmd.Parameters.Add("@HinhDaiDien", SqlDbType.VarBinary      ).Value = (object)dto.HinhDaiDien ?? DBNull.Value;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void XoaNguoiDung(string nguoiDungID)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_XoaNguoiDung"))
            {
                cmd.Parameters.Add("@NguoiDungID", SqlDbType.NVarChar, 50).Value = nguoiDungID;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ===== TỪ ĐIỂN =====
        public List<DTO_VaiTro> GetAllVaiTro()
        {
            var list = new List<DTO_VaiTro>();
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetAllVaiTro"))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new DTO_VaiTro
                        {
                            VaiTroID  = r["VaiTroID"]  as string,
                            TenVaiTro = r["TenVaiTro"] as string
                        });
                    }
                }
            }
            return list;
        }

        public List<DTO_PhongBan> GetAllPhongBan()
        {
            var list = new List<DTO_PhongBan>();
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetAllPhongBan"))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new DTO_PhongBan
                        {
                            PhongBanID  = r["PhongBanID"]  as string,
                            TenPhongBan = r["TenPhongBan"] as string
                        });
                    }
                }
            }
            return list;
        }

        public DTO_VaiTro GetVaiTroByID(string vaiTroID)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetVaiTroByID"))
            {
                cmd.Parameters.Add("@VaiTroID", SqlDbType.NVarChar, 50).Value = vaiTroID;
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new DTO_VaiTro { VaiTroID = r["VaiTroID"] as string, TenVaiTro = r["TenVaiTro"] as string };
                }
            }
        }

        public DTO_PhongBan GetPhongBanByID(string phongBanID)
        {
            using (var conn = NewConn())
            using (var cmd  = NewSp(conn, "sp_GetPhongBanByID"))
            {
                cmd.Parameters.Add("@PhongBanID", SqlDbType.NVarChar, 50).Value = phongBanID;
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;
                    return new DTO_PhongBan { PhongBanID = r["PhongBanID"] as string, TenPhongBan = r["TenPhongBan"] as string };
                }
            }
        }
          
    }
}
