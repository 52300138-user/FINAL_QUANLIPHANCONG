using System;
using System.Data;
using System.Collections.Generic; 
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.DAL
{
    public static class dangNhapDAL
    {
        /// Lấy UserDTO bằng UserName hoặc Email (Dùng cho BUS để check login)
        public static UserDTO GetByAccount(string account)
        {
            string query = @"
                SELECT TOP 1 *
                FROM dbo.[User]
                WHERE UserName = @acc OR Email = @acc;
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@acc", account }
            };

            // Dùng ExecuteQuery trả về DataTable
            DataTable dt = DAL.Db.ExecuteQuery(query, parameters);

            // Logic y hệt code cũ (dùng ExecuteReader)
            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0]; // Lấy hàng đầu tiên

            // Map DataRow sang DTO (an toàn hơn)
            return new UserDTO
            {
                UserID = Convert.ToInt32(row["UserID"]),
                FullName = row["FullName"].ToString(),
                UserName = row["UserName"].ToString(),
                PassWord = row["PassWord"].ToString(),
                Email = row["Email"].ToString(),
                Address = row["Address"].ToString(),
                Gender = row["Gender"].ToString(),
                Role = row["Role"].ToString(),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                SDT = row["SDT"].ToString(),
                IsLocked = Convert.ToBoolean(row["IsLocked"])
            };
        }
    }
}