using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ThongSoMoiTruong
    {
        // Keys
        public string TenThongSo { get; set; } // key string TS_xxx
        public string ViTriID    { get; set; }

        // FK values
        public string LoaiChiTieuID  { get; set; }
        public string DonViID        { get; set; }
        public string LoaiPhanTichID { get; set; }
        public string NguoiPhanTichID { get; set; }
        public string ThauPhuID      { get; set; }

        // Display names
        public string TenLoaiChiTieu   { get; set; }
        public string TenDonVi         { get; set; }
        public string TenLoaiPhanTich  { get; set; }
        public string TenNguoiPhanTich { get; set; }
        public string TenThauPhu       { get; set; }

        // Values
        public string GiaTri         { get; set; }        // text value
        public decimal? GiaTriSo     { get; set; }        // numeric value
        public decimal? GiaTriQuyChuan { get; set; }      // numeric threshold
        public string KetLuan        { get; set; }
    }
}
