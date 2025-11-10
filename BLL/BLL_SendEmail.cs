using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;
using DTO;

namespace BLL
{
    public class BLL_SendEmail
    {
        private readonly DAL_SendEmail _dalEmail = new DAL_SendEmail();
        private readonly DAL_TaiKhoan _dal = new DAL_TaiKhoan();

        // Lưu OTP tạm thời (bộ nhớ RAM – có thể thay bằng DB nếu cần)
        private readonly Dictionary<string, (string Otp, DateTime ExpireTime)> _otpCache
            = new Dictionary<string, (string, DateTime)>();

        // ===== Gửi OTP =====
        public bool SendOtp(string toEmail, out string otp)
        {
            otp = new Random().Next(100000, 999999).ToString();

            bool sent = _dalEmail.SendEmailOtp(toEmail, otp);
            if (sent)
            {
                Console.WriteLine($"[BLL] Gửi OTP tới {toEmail} thành công: {otp}");

                // Lưu OTP kèm thời hạn (VD: 5 phút)
                _otpCache[toEmail] = (otp, DateTime.Now.AddMinutes(5));
            }
            else
            {
                Console.WriteLine($"[BLL] Gửi OTP tới {toEmail} thất bại.");
            }

            return sent;
        }

        // ===== Xác thực OTP =====
        public bool VerifyOtp(string toEmail, string userInputOtp)
        {
            if (_otpCache.ContainsKey(toEmail))
            {
                var (savedOtp, expireTime) = _otpCache[toEmail];

                if (DateTime.Now > expireTime)
                {
                    _otpCache.Remove(toEmail);
                    return false;
                }

                if (savedOtp == userInputOtp)
                {
                    _otpCache.Remove(toEmail);
                    return true;
                }
            }
            return false;
        }

        public BLL_SendEmail()
        {
            _dalEmail = new DAL_SendEmail();
        }


        public void GuiMailDonHangSapHetHan(List<DonHang> sapHet)
        {
            try
            {
                // Lấy email trưởng phòng từ DB
                var dt = DAL_SendEmail.LayEmailTruongPhongKinhDoanh();
                if (dt == null || dt.Rows.Count == 0)
                    throw new Exception("Không tìm thấy email trưởng phòng kinh doanh!");

                string emailTruongPhong = dt.Rows[0]["Email"].ToString();

                // Gộp nội dung đơn hàng thành chuỗi
                var sb = new StringBuilder();
                sb.AppendLine("Kính gửi Trưởng phòng Kinh doanh,");
                sb.AppendLine();
                sb.AppendLine("Các đơn hàng sau sắp hết hạn xử lý:");
                sb.AppendLine("--------------------------------------------------");

                foreach (var dh in sapHet)
                {
                    sb.AppendLine($"• Mã đơn: {dh.MaDon}");
                    sb.AppendLine($"  Khách hàng: {dh.KhachHang}");
                    sb.AppendLine($"  Dự kiến trả KQ: {dh.NgayHetHan:dd-MM-yyyy}");
                    sb.AppendLine($"  Ghi chú: {(string.IsNullOrWhiteSpace(dh.GhiChu) ? "(Không có)" : dh.GhiChu)}");
                    sb.AppendLine();
                }

                sb.AppendLine("--------------------------------------------------");
                sb.AppendLine("Vui lòng kiểm tra và xử lý kịp thời.");
                sb.AppendLine("\nTrân trọng.");

                // Gửi mail
                var dalMail = new DAL_SendEmail();
                bool sent = dalMail.SendEmail(
                    emailTruongPhong,
                    "THÔNG BÁO: Các đơn hàng sắp hết hạn",
                    sb.ToString()
                );

                if (!sent)
                    throw new Exception("Không gửi được email thông báo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi mail đơn hàng sắp hết hạn: " + ex.Message);
            }
        }
        public class DonHang
        {
            public string MaDon { get; set; }
            public string KhachHang { get; set; }
            public string GhiChu { get; set; }
            public DateTime NgayHetHan { get; set; }
            public string HopDongID { get; set; }

            // Constructor gốc
            public DonHang(string ma, string khachHang, string a, string b, DateTime ngayHetHan, string hopDongId)
            {
                MaDon = ma;
                KhachHang = khachHang;
                NgayHetHan = ngayHetHan;
                HopDongID = hopDongId;
            }

            // 👇 Thêm constructor rỗng này để LINQ .Select() dùng được
            public DonHang() { }
        }

    }
}