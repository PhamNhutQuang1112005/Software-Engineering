using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DAL;
using DTO;


namespace BLL
{
    public class BLL_TaiKhoan
    {   
        public void NgatKetNoiCSDL()
    {
        DBConnection.CloseAllConnections();
    }
        private readonly DAL_TaiKhoan _dal = new DAL_TaiKhoan();

        // Đăng nhập (không gọi lại nếu đã có session ở GUI)
        public DTO_NguoiDung DangNhap(string tenDangNhap, string matKhau)
        {
            string matKhauHash = HashMatKhau(matKhau);  // 🔐 Hash lại mật khẩu nhập vào
            return _dal.DangNhap(tenDangNhap, matKhauHash);
        }
     

        // Header theo username (không login lại)
        public DTO_NguoiDung GetUserHeaderByUsername(string tenDangNhap) => _dal.GetUserHeaderByUsername(tenDangNhap);

        // Danh sách / tìm kiếm
        public DataTable LayTatCaNguoiDung() => _dal.GetAllNguoiDung_DataTable();
        public DataTable TimKiemNguoiDung(string keyword) => _dal.TimKiemNguoiDung_DataTable(keyword);

        // CRUD
        public void ThemNguoiDung(DTO_NguoiDung dto, string matKhau)
        {
            string matKhauHash = HashMatKhau(matKhau);
            _dal.ThemNguoiDung(dto, matKhauHash);
        }
        
        public void SuaNguoiDung(DTO_NguoiDung dto, string matKhauNullable = null)
        {
            string matKhauHash = HashMatKhau(matKhauNullable);
            _dal.SuaNguoiDung(dto, matKhauHash);
        }
        
        public void XoaNguoiDung(string nguoiDungID) => _dal.XoaNguoiDung(nguoiDungID);

        // Từ điển
        public List<DTO_VaiTro>    GetAllVaiTro()     => _dal.GetAllVaiTro();
        public List<DTO_PhongBan>  GetAllPhongBan()   => _dal.GetAllPhongBan();
        public DTO_VaiTro          GetVaiTroByID(string vaiTroID)       => _dal.GetVaiTroByID(vaiTroID);
        public DTO_PhongBan        GetPhongBanByID(string phongBanID)   => _dal.GetPhongBanByID(phongBanID);
        public bool HasAdminAccount()
        {
            DataTable users = _dal.GetAllNguoiDung_DataTable();
            if (users == null) return false;

            return users.AsEnumerable().Any(u =>
                u["VaiTroID"] != DBNull.Value &&
                string.Equals(u["VaiTroID"].ToString(), "VT001", StringComparison.OrdinalIgnoreCase));
        }
        public bool DoiMatKhauBangEmail(string email, string matKhauMoi, string xacNhanMatKhau)
        {
            // 1️⃣ Lấy tất cả người dùng dưới dạng DataTable
            DataTable dt = _dal.GetAllNguoiDung_DataTable();
            if (dt == null || dt.Rows.Count == 0)
                throw new Exception("Không có người dùng nào trong hệ thống.");

            // 2️⃣ Tìm user theo email
            DataRow row = dt.AsEnumerable()
                            .FirstOrDefault(r => string.Equals(
                                r["Email"].ToString(),
                                email,
                                StringComparison.OrdinalIgnoreCase));

            if (row == null)
                throw new Exception("Không tìm thấy tài khoản với email này.");

            // 3️⃣ Tạo DTO từ DataRow
            DTO_NguoiDung user = new DTO_NguoiDung
            {
                NguoiDungID = row["NguoiDungID"].ToString(),
                TenDangNhap = row["TenDangNhap"].ToString(),
                HoVaTen = row["HoVaTen"].ToString(),
                DienThoai = row["DienThoai"].ToString(),
                Email = row["Email"].ToString(),
                VaiTroID = row["VaiTroID"].ToString(),
                PhongBanID = row["PhongBanID"].ToString()
            };

            // 4️⃣ Kiểm tra mật khẩu mới và xác nhận
            if (matKhauMoi != xacNhanMatKhau)
                throw new Exception("Mật khẩu xác nhận không khớp.");

            string matKhauCu = row["MatKhauHash"].ToString(); // mật khẩu cũ trong DB
            if (matKhauMoi == matKhauCu)
                throw new Exception("Mật khẩu mới không được trùng mật khẩu cũ.");
            string hashMatKhauMoi = HashMatKhau(matKhauMoi);
            // 5️⃣ Cập nhật mật khẩu (lưu thẳng plaintext)
            _dal.SuaNguoiDung(user, hashMatKhauMoi);
            return true;
        }
        private string HashMatKhau(string matKhau)
        {
            if (string.IsNullOrEmpty(matKhau))
                return null;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(matKhau));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

    }
}
