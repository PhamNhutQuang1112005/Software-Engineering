namespace DTO
{
    public class DTO_NguoiDung
    {
        public string NguoiDungID { get; set; }
        public string TenDangNhap { get; set; }
        public string HoVaTen     { get; set; }
        public string DienThoai   { get; set; }
        public string Email       { get; set; }

        public string VaiTroID    { get; set; }   // tra tên qua VaiTroDto khi cần
        public string PhongBanID  { get; set; }   // tra tên qua PhongBanDto khi cần
    }
}
