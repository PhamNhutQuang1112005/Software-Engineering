using System;
namespace DTO
{
    public class DTO_DonHang
    {
        public string DonHangID { get; set; }
        public string MaDonHang { get; set; }

        // Liên kết, trạng thái, mô tả
        public string HopDongID { get; set; }
        public string TrangThaiID { get; set; }
        public string GhiChu { get; set; }
        public string IDKhachHang { get; set; }

        // Địa chỉ (mới)
        public string DiaChi { get; set; }

        // Ngày tháng
        public DateTime? NgayLayMau { get; set; }            // nhập khi thêm/sửa
        public DateTime? NgayDuKienTraKetQua { get; set; }   // = Ngày lấy mẫu + 15 (tự tính)
        public DateTime? NgayTraThucTe { get; set; }         // chỉ cho nhập khi SỬA
    }
}
