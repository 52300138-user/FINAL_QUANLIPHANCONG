using System;
using System.Data;
using System.Collections.Generic; // Cần cho Dictionary
using WindowsFormsApp2.DTO;      // Cần cho UserDTO
using WindowsFormsApp2.DAL;     // Cần cho Db.cs (vì nó cùng namespace)

namespace WindowsFormsApp2.DAL
{
    public static class UsersDAL
    {
        // === CÁC HÀM GET DATA ===

        public static DataTable SelectUsersTable()
        {
            string query = @"
                SELECT 
                    UserID AS [ID], 
                    FullName, 
                    UserName, 
                    Email, 
                    Address, 
                    Gender AS [GioiTinh], 
                    Role AS [ChucVu], 
                    CreatedAt,
                    SDT, 
                    CASE 
                        WHEN IsLocked = 1 THEN N'Bị khóa' 
                        ELSE N'Hoạt động' 
                    END AS [TrangThai]
                FROM [dbo].[User];";
            return Db.ExecuteQuery(query);
        }

        public static UserDTO GetUserById(int userId)
        {
            string query = "SELECT * FROM [User] WHERE UserID = @UserID";

            var parameters = new Dictionary<string, object>
            {
                { "@UserID", userId }
            };

            // Gọi Db.cs
            DataTable dt = Db.ExecuteQuery(query, parameters);

            // Check xem có kết quả không
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Map dữ liệu từ DataRow sang UserDTO
                return new UserDTO
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    FullName = row["FullName"].ToString(),
                    UserName = row["UserName"].ToString(),
                    Email = row["Email"].ToString(),
                    Address = row["Address"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Role = row["Role"].ToString(),
                    SDT = row["SDT"].ToString(),
                    IsLocked = Convert.ToBoolean(row["IsLocked"]),
                        CreatedAt = row["CreatedAt"] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(row["CreatedAt"])
                };
            }

            // Nếu dt.Rows.Count == 0 (không tìm thấy)
            return null;
        }

        public static UserDTO GetUserByAccount(string acc)
        {
            // Lấy TẤT CẢ thông tin (bao gồm cả password)
            string query = "SELECT * FROM [User] WHERE UserName = @acc OR Email = @acc";
            var parameters = new Dictionary<string, object>
            {
                { "@acc", acc }
            };
            DataTable dt = Db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new UserDTO
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    FullName = row["FullName"].ToString(),
                    UserName = row["UserName"].ToString(),
                    PassWord = row["PassWord"].ToString(), // <-- QUAN TRỌNG
                    Email = row["Email"].ToString(),
                    Address = row["Address"].ToString(),
                    Gender = row["Gender"].ToString(),
                    Role = row["Role"].ToString(),
                    SDT = row["SDT"].ToString(),
                    IsLocked = Convert.ToBoolean(row["IsLocked"])
                };
            }
            return null;
        }

        // === CÁC HÀM CRUD (INSERT, UPDATE, DELETE) ===

        public static bool AddUser(UserDTO user)
        {
            string query = @"
                INSERT INTO [User] 
                (FullName, Email, Address, UserName, SDT, PassWord, Gender, Role, IsLocked, CreatedAt) 
                VALUES 
                (@HoTen, @Email, @DiaChi, @UserName, @SDT, @PassWord, @Gender, @Role, @IsLocked, @CreatedAt)";

            var parameters = new Dictionary<string, object>
            {
                { "@HoTen", user.FullName },
                { "@Email", user.Email },
                { "@DiaChi", user.Address },
                { "@UserName", user.UserName },
                { "@SDT", user.SDT },
                { "@PassWord", user.PassWord },
                { "@Gender", user.Gender },
                { "@Role", user.Role },
                { "@IsLocked", user.IsLocked },
                { "@CreatedAt", DateTime.Now }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool UpdateUser(UserDTO user)
        {
            // 1. Xóa UserName
            // 2. Thêm Address
            string query = @"
                UPDATE [User]
                SET FullName = @FullName, 
                    Email = @Email, 
                    Address = @Address, 
                    Gender = @Gender, 
                    Role = @Role, 
                    SDT = @SDT
                WHERE UserID = @UserID";

            var parameters = new Dictionary<string, object>
            {
                { "@FullName", user.FullName },
                // { "@UserName", user.UserName }, // <-- ĐÃ XÓA
                { "@Email", user.Email },
                { "@Address", user.Address }, // <-- ĐÃ THÊM
                { "@Gender", user.Gender },
                { "@Role", user.Role },
                { "@SDT", user.SDT },
                { "@UserID", user.UserID }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteUser(int userId)
        {
            string query = "DELETE FROM [User] WHERE UserID = @UserID";
            var parameters = new Dictionary<string, object> { { "@UserID", userId } };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool UpdateLock(int userId, bool isLock)
        {
            string query = "UPDATE [dbo].[User] SET IsLocked = @lock WHERE UserID = @id;";
            var parameters = new Dictionary<string, object>
            {
                { "@lock", isLock },
                { "@id", userId }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        // === CÁC HÀM VALIDATE ===

        public static bool IsUserNameExists(string userName, int? excludeUserId = null)
        {
            string query = "SELECT COUNT(1) FROM [User] WHERE UserName = @UserName";
            var parameters = new Dictionary<string, object> { { "@UserName", userName } };
            if (excludeUserId.HasValue)
            {
                query += " AND UserID != @UserId";
                parameters.Add("@UserId", excludeUserId.Value);
            }
            return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
        }

        public static bool IsEmailExists(string email, int? excludeUserId = null)
        {
            string query = "SELECT COUNT(1) FROM [User] WHERE Email = @Email AND Email IS NOT NULL AND Email != ''";
            var parameters = new Dictionary<string, object> { { "@Email", email } };
            if (excludeUserId.HasValue)
            {
                query += " AND UserID != @UserId";
                parameters.Add("@UserId", excludeUserId.Value);
            }
            return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
        }

        public static bool IsSdtExists(string sdt, int? excludeUserId = null)
        {
            string query = "SELECT COUNT(1) FROM [User] WHERE SDT = @SDT AND SDT IS NOT NULL AND SDT != ''";
            var parameters = new Dictionary<string, object> { { "@SDT", sdt } };
            if (excludeUserId.HasValue)
            {
                query += " AND UserID != @UserId";
                parameters.Add("@UserId", excludeUserId.Value);
            }
            return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
        }

        public static bool IsRoleExists(string roleCode)
        {
            string query = "SELECT COUNT(1) FROM [User] WHERE Role = @Role";
            var parameters = new Dictionary<string, object> { { "@Role", roleCode } };
            return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
        }

        public static (int userId, string email, bool isLocked) GetUserInfo(string acc)
        {
            var sql = @"SELECT TOP(1) UserID, Email, IsLocked
                        FROM [User]
                        WHERE Email=@acc OR UserName=@acc;";

            var parameters = new Dictionary<string, object> { { "@acc", acc } };

            DataTable dt = Db.ExecuteQuery(sql, parameters);

            if (dt.Rows.Count == 0)
                return (0, null, false);

            DataRow row = dt.Rows[0];
            int id = Convert.ToInt32(row["UserID"]);
            string email = row.IsNull("Email") ? null : row["Email"].ToString();
            bool isLocked = row.IsNull("IsLocked") ? false : Convert.ToBoolean(row["IsLocked"]);

            return (id, email, isLocked);
        }

        public static bool UpdatePassword(string acc, string hashedPassword)
        {
            var sql = @"UPDATE [User]
                        SET PassWord=@Password
                        WHERE Email=@acc OR UserName=@acc;";

            var parameters = new Dictionary<string, object>
            {
                { "@Password", hashedPassword },
                { "@acc", acc }
            };

            return Db.ExecuteNonQuery(sql, parameters) > 0;
        }
        public static DataTable SelectGiangVienList()
        {
            string query = @"
        SELECT 
            UserID, 
            FullName,
            Role AS [ChucVu]
        FROM 
            dbo.[User]
        WHERE 
            Role IN (N'GV', N'TBM') AND IsLocked = 0
        ORDER BY 
            FullName;
        ";

            return Db.ExecuteQuery(query);
        }
        public static DataTable GetAllUsers()
        {
            string query = "SELECT UserID, FullName, IsLocked FROM dbo.[User]";
            return Db.ExecuteQuery(query);
        }
    }
}