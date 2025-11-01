using System;
using DAL;

namespace BLL
{
    public class BLL_SendEmail
    {
        private readonly DAL_SendEmail _dalEmail = new DAL_SendEmail();

        // Đặt sẵn email mặc định
        private readonly string defaultEmail = "nguyenthev628@gmail.com";

        public bool SendOtp(out string otp)
        {
            otp = new Random().Next(100000, 999999).ToString();

            bool sent = _dalEmail.SendEmailOtp(defaultEmail, otp);
            if (sent)
            {
                Console.WriteLine($"[BLL] Gửi OTP tới {defaultEmail} thành công: {otp}");
            }

            return sent;
        }
    }
}
