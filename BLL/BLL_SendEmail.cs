﻿using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class BLL_SendEmail
    {
        private readonly DAL_SendEmail _dalEmail = new DAL_SendEmail();

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
    }
}