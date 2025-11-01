using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_SendEmail
    {
        private readonly string fromEmail = "ntheviet0@gmail.com"; // Gmail gửi OTP
        private readonly string appPassword = "zkqy lrex ywhb gvro";   // App password 16 ký tự

        private SmtpClient NewSmtpClient()
        {
            return new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, appPassword)
            };
        }

        // ===== GỬI EMAIL OTP =====
        public bool SendEmailOtp(string toEmail, string otp)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail, "Hệ thống xác thực tài khoản");
                    mail.To.Add(toEmail);
                    mail.Subject = "Mã OTP xác thực tài khoản";
                    mail.Body = $"Xin chào!\n\nMã OTP của bạn là: {otp}\nMã có hiệu lực trong 5 phút.\n\nTrân trọng.";

                    using (var smtp = NewSmtpClient())
                    {
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi OTP: " + ex.Message);
                return false;
            }
        }

        // ===== GỬI EMAIL CHUNG (tùy chọn nếu bạn cần sau này) =====
        public bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail, "Hệ thống thông báo");
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;

                    using (var smtp = NewSmtpClient())
                    {
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi mail: " + ex.Message);
                return false;
            }
        }
    }
}
