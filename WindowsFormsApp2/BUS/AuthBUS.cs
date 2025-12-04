using System;
using System.Net;
using System.Net.Mail;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.Common; 
using WindowsFormsApp2.DTO;      

namespace WindowsFormsApp2.BUS
{
    public static class AuthBUS
    {
        private static string generatedOTP;
        private static DateTime otpExpireAt;
        private static int wrongOtpCount;

        public static (bool ok, int userId, string role, string msg) Login(string account, string password)
        {
            try
            {
                var u = UsersDAL.GetUserByAccount(account);

                if (u == null) return (false, 0, null, "Tài khoản không tồn tại.");
                if (u.IsLocked) return (false, 0, null, "Tài khoản đã bị khoá."); 

                var hash = Common.PasswordHasher.Hash(password);
                if (!string.Equals(hash, u.PassWord, StringComparison.OrdinalIgnoreCase))
                    return (false, 0, null, "Sai mật khẩu."); 
                return (true, u.UserID, u.Role, "");
            }
            catch (Exception ex)
            {
                return (false, 0, null, "Không thể đăng nhập lúc này: " + ex.Message);
            }
        }

        /// Dùng cho form Quên mật khẩu: Kiểm tra tài khoản có hợp lệ để gửi OTP không
        public static (bool ok, string msg, string email) ValidateAccount(string acc)
        {
            try
            {
                var user = UsersDAL.GetUserInfo(acc); // Đã gọi hàm DAL "sạch"

                if (user.userId == 0)
                    return (false, "❌ Không tìm thấy tài khoản với Email/Tài khoản này!", null);

                if (user.isLocked)
                    return (false, "🔒 Tài khoản đã bị khoá. Liên hệ quản trị viên.", null);

                if (string.IsNullOrWhiteSpace(user.email))
                    return (false, "Tài khoản này chưa có Email để nhận OTP. Liên hệ quản trị viên.", null);

                return (true, "OK", user.email);
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống khi check tài khoản: " + ex.Message, null);
            }
        }

        /// Tạo OTP 6 chữ số (Hàm helper)
        private static string GenerateOtp6Digits()
        {
            // Logic tạo OTP 6 số
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        /// Tạo OTP 6 chữ số + reset counters
        public static (string otp, DateTime expireAt) GenerateOTP()
        {
            generatedOTP = GenerateOtp6Digits();
            otpExpireAt = DateTime.Now.AddMinutes(5); // 5 phút
            wrongOtpCount = 0;
            return (generatedOTP, otpExpireAt);
        }

        /// Validate OTP
        public static void ValidateOTP(string inputOtp)
        {
            if (string.IsNullOrWhiteSpace(generatedOTP))
                throw new InvalidOperationException("Vui lòng yêu cầu OTP trước.");
            if (DateTime.Now > otpExpireAt)
                throw new InvalidOperationException("OTP đã hết hạn.");
            if (inputOtp?.Trim() != generatedOTP)
            {
                wrongOtpCount++;
                if (wrongOtpCount >= 5)
                {
                    generatedOTP = null; // huỷ OTP
                    throw new InvalidOperationException("Sai OTP quá 5 lần. Vui lòng yêu cầu OTP mới.");
                }
                throw new InvalidOperationException("OTP không đúng!");
            }

            generatedOTP = null; // Thu hồi OTP sau khi dùng
        }

        /// Đổi mật khẩu (trả về true/false)
        public static bool ChangePassword(string acc, string newPassword)
        {
            try
            {
                // Hash mật khẩu mới
                string hashed = PasswordHasher.Hash(newPassword);
                // Gọi hàm DAL "sạch"
                return UsersDAL.UpdatePassword(acc, hashed);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// Gửi email OTP
        public static void SendOTP(string toEmail, string otp, DateTime expireAt)
        {
            // TODO: thay bằng email & app password thực tế
            string fromEmail = "dinhthinguyet060905@gmail.com";
            string appPassword = "antzqidzxerrxvsc";

            using (var mail = new MailMessage())
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                mail.From = new MailAddress(fromEmail, "EMTs");
                mail.To.Add(toEmail);
                mail.Subject = "Mã OTP đổi mật khẩu";
                mail.Body = $"Mã OTP của bạn là: {otp}\nHiệu lực đến: {expireAt:HH:mm dd/MM/yyyy}";

                smtp.Credentials = new NetworkCredential(fromEmail, appPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail); // Hàm này có thể throw exception nếu gửi thất bại
            }
        }
    }
}