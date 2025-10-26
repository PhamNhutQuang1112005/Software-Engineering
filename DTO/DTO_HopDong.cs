using System;

namespace DTO
{
    public class DTO_HopDong
    {
        public string HopDongID   { get; set; }
        public string MaHopDong   { get; set; }
        public string KhachHangID { get; set; }
        public DateTime NgayKy    { get; set; }
        public string KyHanID     { get; set; }

        public DateTime? NgayBatDau  { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TrangThai      { get; set; }   // nullable ở DB -> để string (có thể null)
        public string GhiChu         { get; set; }   // nullable ở DB -> để string (có thể null)
    }
}
