using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;
using WindowsFormsApp2.Common; // Giả sử PasswordHasher ở đây

namespace WindowsFormsApp2.BUS
{
    public static class UsersBUS
    {
        // === CÁC HÀM HELPER (VALIDATE DATA) ===
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;
            try
            {
                return Regex.IsMatch(email,
                    @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) { return false; }
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return true;
            return Regex.IsMatch(phone, @"^(\+84|0)\d{9,10}$");
        }

        // === HÀM VALIDATE TỔNG ===
        private static bool ValidateUserData(UserDTO user, bool isUpdate)
        {
            if (isUpdate && user.UserID <= 0)
            {
                MessageBox.Show("Lỗi: Không xác định được ID người dùng để cập nhật.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.FullName))
            {
                MessageBox.Show("Họ tên không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.Gender))
            {
                MessageBox.Show("Vui lòng chọn Giới tính.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(user.Role))
            {
                MessageBox.Show("Vui lòng chọn Chức vụ/Vai trò.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Chuyển đổi Role 
            if (user.Role == "Giảng Viên" || user.Role == "Giảng viên") // Thêm check "Giảng viên"
            {
                user.Role = "GV";
            }
            if (user.Role == "Trưởng Bộ Môn")
            {
                user.Role = "TBM";
            }

            // Check TBM (Chốt chặn)
            if (user.Role == "TBM" && !isUpdate)
            {
                try
                {
                    if (UsersDAL.IsRoleExists("TBM"))
                    {
                        MessageBox.Show("Đã có Trưởng Bộ Môn trong hệ thống. Không thể thêm.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra chức vụ TBM: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Check định dạng
            if (!IsValidEmail(user.Email))
            {
                MessageBox.Show("Email không đúng định dạng.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!IsValidPhone(user.SDT))
            {
                MessageBox.Show("SĐT không đúng định dạng (phải là 10-11 số, VD: 09... hoặc +84...).", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check Uniqueness
            try
            {
                int? userIdToCheck = isUpdate ? user.UserID : (int?)null;

                if (UsersDAL.IsUserNameExists(user.UserName, userIdToCheck))
                {
                    MessageBox.Show("Tên đăng nhập này đã tồn tại. Vui lòng chọn tên khác.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(user.Email) && UsersDAL.IsEmailExists(user.Email, userIdToCheck))
                {
                    MessageBox.Show("Email này đã được sử dụng. Vui lòng chọn email khác.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(user.SDT) && UsersDAL.IsSdtExists(user.SDT, userIdToCheck))
                {
                    MessageBox.Show("SĐT này đã được sử dụng. Vui lòng chọn SĐT khác.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra dữ liệu tồn tại: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // === CÁC HÀM PUBLIC CHO UI (CRUD) ===

        public static DataTable GetUsersTable()
        {
            try
            {
                return UsersDAL.SelectUsersTable();
            }
            catch (Exception ex)
            {
                throw new Exception("BUS: Không tải được danh sách người dùng. " + ex.Message, ex);
            }
        }

        public static bool AddUser(UserDTO user)
        {
            if (!ValidateUserData(user, isUpdate: false))
            {
                return false;
            }

            // Hash password
            // Giả sử bro có 1 lớp PasswordHasher tĩnh
            user.PassWord = PasswordHasher.Hash(user.PassWord);

            try
            {
                return UsersDAL.AddUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS: Không thể thêm người dùng.\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UpdateUser(UserDTO user)
        {
            if (!ValidateUserData(user, isUpdate: true))
            {
                return false;
            }
            try
            {
                return UsersDAL.UpdateUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS: Không thể cập nhật người dùng.\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                return UsersDAL.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                // Thường là lỗi FK (ràng buộc khóa ngoại)
                MessageBox.Show("BUS: Không thể xóa người dùng. Có thể người dùng này đang có dữ liệu liên quan.\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                // throw new Exception("BUS: Không thể xóa người dùng. " + ex.Message, ex);
            }
        }
        public static bool SetLock(int userId, bool isLock)
        {
            try
            {
                return UsersDAL.UpdateLock(userId, isLock);
            }
            catch (Exception ex)
            {
                throw new Exception("BUS: Không cập nhật trạng thái khóa. " + ex.Message, ex);
            }
        }

        public static UserDTO GetUserById(int userId)
        {
            try
            {
                return UsersDAL.GetUserById(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS: Lỗi khi lấy thông tin người dùng.\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool CheckIfTBMExists()
        {
            try
            {
                return UsersDAL.IsRoleExists("TBM");
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS: Lỗi khi kiểm tra chức vụ TBM.\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
        public static DataTable GetGiangVienList()
        {
            try
            {
                return UsersDAL.SelectGiangVienList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Giảng viên: " + ex.Message,
                                "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }
        public static DataTable GetAllUsers()
        {
            return UsersDAL.GetAllUsers();
        }
    }
}