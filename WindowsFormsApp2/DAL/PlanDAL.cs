using System;
using System.Data;
using System.Collections.Generic;
using WindowsFormsApp2.DTO;
using WindowsFormsApp2.DAL; // Giả sử Db.cs ở đây

namespace WindowsFormsApp2.DAL
{
    public static class PlanDAL
    {
        // Lấy danh sách kế hoạch

        public static DataTable SelectPlans()
        {
            string query = @"
                SELECT k.KeHoachID, k.TenKeHoach, k.Loai, k.NgayBatDau, k.NgayKetThuc, 
                       u.FullName AS NguoiTao, k.CreatedAt AS NgayTao 
                FROM dbo.KeHoach k 
                JOIN dbo.[User] u ON u.UserID = k.NguoiTaoID 
                ORDER BY k.CreatedAt DESC;";
            return Db.ExecuteQuery(query);
        }

        // === Lấy 1 kế hoạch bằng ID ===
        public static PlanDTO GetPlanById(int keHoachId)
        {
            string query = "SELECT * FROM dbo.KeHoach WHERE KeHoachID = @KeHoachID";
            var parameters = new Dictionary<string, object>
    {
        { "@KeHoachID", keHoachId }
    };

            DataTable dt = Db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            // ====== NgayBatDau ======
            DateTime ngayBatDau = DateTime.MinValue;
            if (!row.IsNull("NgayBatDau"))
            {
                var obj = row["NgayBatDau"];
                if (obj is DateTime dtVal)
                {
                    ngayBatDau = dtVal;
                }
                else
                {
                    DateTime tmp;
                    if (DateTime.TryParse(obj.ToString(), out tmp))
                        ngayBatDau = tmp;
                }
            }

            // ====== NgayKetThuc ======
            DateTime ngayKetThuc = DateTime.MinValue;
            if (!row.IsNull("NgayKetThuc"))
            {
                var obj = row["NgayKetThuc"];
                if (obj is DateTime dtVal)
                {
                    ngayKetThuc = dtVal;
                }
                else
                {
                    DateTime tmp;
                    if (DateTime.TryParse(obj.ToString(), out tmp))
                        ngayKetThuc = tmp;
                }
            }

            // ====== CreatedAt ======
            DateTime createdAt = DateTime.MinValue;
            if (!row.IsNull("CreatedAt"))
            {
                var obj = row["CreatedAt"];
                if (obj is DateTime dtVal)
                {
                    createdAt = dtVal;
                }
                else
                {
                    DateTime tmp;
                    if (DateTime.TryParse(obj.ToString(), out tmp))
                        createdAt = tmp;
                }
            }

            return new PlanDTO
            {
                KeHoachID = Convert.ToInt32(row["KeHoachID"]),
                TenKeHoach = row["TenKeHoach"]?.ToString(),
                Loai = row["Loai"]?.ToString(),
                NguoiTaoID = Convert.ToInt32(row["NguoiTaoID"]),
                NgayBatDau = ngayBatDau,
                NgayKetThuc = ngayKetThuc,
                CreatedAt = createdAt
            };
        }

        // Insert 
        public static bool InsertPlan(PlanDTO dto)
        {
            string query = @"
                INSERT INTO dbo.KeHoach
                (TenKeHoach, Loai, NgayBatDau, NgayKetThuc, NguoiTaoID, CreatedAt)
                VALUES (@TenKeHoach, @Loai, @NgayBatDau, @NgayKetThuc, @NguoiTaoID, GETDATE())";

            var parameters = new Dictionary<string, object>
            {
                { "@TenKeHoach", dto.TenKeHoach },
                { "@Loai", dto.Loai },
                { "@NgayBatDau", dto.NgayBatDau },
                { "@NgayKetThuc", dto.NgayKetThuc },
                { "@NguoiTaoID", dto.NguoiTaoID }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        // Update
        public static bool UpdatePlan(PlanDTO dto)
        {
            string query = @"
                UPDATE dbo.KeHoach
                SET TenKeHoach=@TenKeHoach, Loai=@Loai,
                    NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc
                WHERE KeHoachID=@KeHoachID";

            var parameters = new Dictionary<string, object>
            {
                { "@TenKeHoach", dto.TenKeHoach },
                { "@Loai", dto.Loai },
                { "@NgayBatDau", dto.NgayBatDau },
                { "@NgayKetThuc", dto.NgayKetThuc },
                { "@KeHoachID", dto.KeHoachID }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        // Delete 
        public static bool DeletePlan(int keHoachId)
        {
            string query = "DELETE FROM dbo.KeHoach WHERE KeHoachID=@id";

            var parameters = new Dictionary<string, object>
            {
                { "@id", keHoachId }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static DataTable SelectAllKeHoach()
        {
            string query = @"
            SELECT 
                KeHoachID,
                TenKeHoach,
                Loai,          -- Học kỳ / Năm / ...
                NgayBatDau,
                NgayKetThuc
            FROM KeHoach";

            return Db.ExecuteQuery(query, null);
        }

    }
}