using System;
using System.Collections.Generic;
using System.Data;
using WindowsFormsApp2.DAL; // Giả sử Db.cs ở đây
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.DAL
{
    public static class CongViecDAL
    {
        /// Lấy bảng CV (đã join) cho DataGridView chính
        public static DataTable SelectCongViecTable()
        {
            // Join 3 bảng: CongViec, KeHoach, User (để lấy tên)
            string query = @"
                SELECT 
                    cv.CongViecID,
                    cv.TenCongViec,
                    cv.Loai,
                    cv.TrangThai,
                    cv.MucUuTien,
                    cv.HanChot,
                    kh.TenKeHoach,  -- Tên Kế hoạch
                    u.FullName AS NguoiGiao -- Tên TBM
                FROM 
                    dbo.CongViec cv
                LEFT JOIN 
                    dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
                JOIN 
                    dbo.[User] u ON cv.NguoiGiaoID = u.UserID
                ORDER BY 
                    cv.CreatedAt DESC;
            ";
            return Db.ExecuteQuery(query);
        }

        /// Lấy 1 DTO đầy đủ bằng ID (cho Form Sửa)
        public static CongViecDTO GetCongViecById(int congViecId)
        {
            string query = "SELECT * FROM dbo.CongViec WHERE CongViecID = @CongViecID";
            var parameters = new Dictionary<string, object>
    {
        { "@CongViecID", congViecId }
    };

            DataTable dt = Db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            DateTime? createdAt = null;
            if (!row.IsNull("CreatedAt"))
            {
                var obj = row["CreatedAt"];

                if (obj is DateTime dtVal)
                {
                    createdAt = dtVal;
                }
                else
                {
                    // nếu Db.ExecuteQuery đang trả string
                    DateTime tmp;
                    if (DateTime.TryParse(obj.ToString(), out tmp))
                    {
                        createdAt = tmp;
                    }
                    // nếu vẫn không parse được thì kệ, để null, KHÔNG quăng exception
                }
            }

            // ===== HANCHOT =====
            DateTime? hanChot = null;
            if (!row.IsNull("HanChot"))
            {
                var obj = row["HanChot"];

                if (obj is DateTime dtVal)
                {
                    hanChot = dtVal;
                }
                else
                {
                    DateTime tmp;
                    if (DateTime.TryParse(obj.ToString(), out tmp))
                    {
                        hanChot = tmp;
                    }
                }
            }

            return new CongViecDTO
            {
                CongViecID = Convert.ToInt32(row["CongViecID"]),
                NguoiGiaoID = Convert.ToInt32(row["NguoiGiaoID"]),
                TenCongViec = row["TenCongViec"]?.ToString(),
                Loai = row["Loai"]?.ToString(),
                MucUuTien = row["MucUuTien"]?.ToString(),
                TrangThai = row["TrangThai"]?.ToString(),
                MoTa = row["MoTa"]?.ToString(),
                LopPhuTrach = row["LopPhuTrach"]?.ToString(),
                MaDeTai = row["MaDeTai"]?.ToString(),
                DiaDiem = row["DiaDiem"]?.ToString(),

                // Nếu DTO của em cho phép null thì cho CreatedAt nullable luôn là đẹp
                CreatedAt = createdAt ?? DateTime.MinValue,
                KeHoachID = row.IsNull("KeHoachID") ? (int?)null : Convert.ToInt32(row["KeHoachID"]),
                HanChot = hanChot,
                MonHocID = row.IsNull("MonHocID") ? (int?)null : Convert.ToInt32(row["MonHocID"]),
                SoTiet = row.IsNull("SoTiet") ? (int?)null : Convert.ToInt32(row["SoTiet"])
            };
        }

        // Lấy 1 bảng
        public static DataTable SelectCongViecByKeHoachID(int keHoachId)
        {
            // === SỬA CODE NÀY ĐỂ KHỚP VỚI SelectCongViecTable (JOIN 3 BẢNG) ===
            string query = @"
                SELECT 
                    cv.CongViecID,
                    cv.TenCongViec,
                    cv.Loai,
                    cv.TrangThai,
                    cv.MucUuTien,
                    cv.HanChot,
                    kh.TenKeHoach,  -- Tên Kế hoạch
                    u.FullName AS NguoiGiao -- Tên TBM
                FROM 
                    dbo.CongViec cv
                LEFT JOIN 
                    dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
                JOIN 
                    dbo.[User] u ON cv.NguoiGiaoID = u.UserID
                WHERE 
                    cv.KeHoachID = @KeHoachID -- <<<< ĐIỀU KIỆN LỌC CHÍNH
                ORDER BY 
                    cv.CreatedAt DESC;
            ";

            // Tạo danh sách tham số (parameters)
            var parameters = new Dictionary<string, object>
            {
                { "@KeHoachID", keHoachId }
            };

            // Thực thi truy vấn và trả về DataTable.
            return Db.ExecuteQuery(query, parameters);
        }

        public static bool InsertCongViec(CongViecDTO dto)
        {
            string query = @"
                INSERT INTO dbo.CongViec (
                    KeHoachID, TenCongViec, Loai, HanChot, MucUuTien, TrangThai, NguoiGiaoID, MoTa, 
                    MonHocID, LopPhuTrach, SoTiet, MaDeTai, DiaDiem 
                    -- CreatedAt dùng DEFAULT
                ) VALUES (
                    @KeHoachID, @TenCongViec, @Loai, @HanChot, @MucUuTien, @TrangThai, @NguoiGiaoID, @MoTa,
                    @MonHocID, @LopPhuTrach, @SoTiet, @MaDeTai, @DiaDiem
                )";

            var parameters = new Dictionary<string, object>
            {
                { "@KeHoachID", dto.KeHoachID },
                { "@TenCongViec", dto.TenCongViec },
                { "@Loai", dto.Loai },
                { "@HanChot", dto.HanChot },
                { "@MucUuTien", dto.MucUuTien },
                { "@TrangThai", dto.TrangThai },
                { "@NguoiGiaoID", dto.NguoiGiaoID },
                { "@MoTa", dto.MoTa },
                { "@MonHocID", dto.MonHocID },
                { "@LopPhuTrach", dto.LopPhuTrach },
                { "@SoTiet", dto.SoTiet },
                { "@MaDeTai", dto.MaDeTai },
                { "@DiaDiem", dto.DiaDiem }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool UpdateCongViec(CongViecDTO dto)
        {
            string query = @"
                UPDATE dbo.CongViec SET
                    KeHoachID = @KeHoachID,
                    TenCongViec = @TenCongViec,
                    Loai = @Loai,
                    HanChot = @HanChot,
                    MucUuTien = @MucUuTien,
                    TrangThai = @TrangThai,
                    MoTa = @MoTa,
                    MonHocID = @MonHocID,
                    LopPhuTrach = @LopPhuTrach,
                    SoTiet = @SoTiet,
                    MaDeTai = @MaDeTai,
                    DiaDiem = @DiaDiem
                WHERE CongViecID = @CongViecID";

            var parameters = new Dictionary<string, object>
            {
                { "@KeHoachID", dto.KeHoachID },
                { "@TenCongViec", dto.TenCongViec },
                { "@Loai", dto.Loai },
                { "@HanChot", dto.HanChot },
                { "@MucUuTien", dto.MucUuTien },
                { "@TrangThai", dto.TrangThai },
                { "@MoTa", dto.MoTa },
                { "@MonHocID", dto.MonHocID },
                { "@LopPhuTrach", dto.LopPhuTrach },
                { "@SoTiet", dto.SoTiet },
                { "@MaDeTai", dto.MaDeTai },
                { "@DiaDiem", dto.DiaDiem },
                { "@CongViecID", dto.CongViecID } // Khóa chính
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool DeleteCongViec(int congViecId)
        {
            string query = "DELETE FROM dbo.CongViec WHERE CongViecID = @CongViecID";
            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool IsMonHocInUse(int monHocId)
        {
            string query = "SELECT COUNT(1) FROM dbo.CongViec WHERE MonHocID = @MonHocID";
            var parameters = new Dictionary<string, object>
            {
                { "@MonHocID", monHocId }
            };
            object result = Db.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }
        public static bool IsClassAlreadyAssigned(int monHocId)
        {
            // Tìm kiếm bất kỳ công việc nào (ngoài công việc hiện tại nếu là update)
            string query = "SELECT COUNT(1) FROM dbo.CongViec WHERE MonHocID = @MonHocID";
            var parameters = new Dictionary<string, object>
            {
                { "@MonHocID", monHocId }
            };

            object result = Db.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }
        /// Lấy danh sách các MonHocID (ID của Lớp/Tổ) đã được phân công cho công việc giảng dạy.
        public static List<int> GetAssignedMonHocIds()
        {
            // Lấy DISTINCT MonHocID từ bảng CongViec, loại trừ các giá trị NULL.
            // Dùng IS NOT NULL để chỉ lấy các công việc đã gán cho một lớp/tổ cụ thể.
            string query = "SELECT DISTINCT MonHocID FROM dbo.CongViec WHERE MonHocID IS NOT NULL";

            DataTable dt = Db.ExecuteQuery(query);
            List<int> assignedIds = new List<int>();

            foreach (DataRow row in dt.Rows)
            {
                // Kiểm tra chắc chắn là int trước khi thêm vào list
                if (row["MonHocID"] is int monHocId)
                {
                    assignedIds.Add(monHocId);
                }
                else if (row["MonHocID"] != DBNull.Value)
                {
                    // Thử parse nếu không phải kiểu int native (ít xảy ra)
                    if (int.TryParse(row["MonHocID"].ToString(), out int parsedId))
                    {
                        assignedIds.Add(parsedId);
                    }
                }
            }

            return assignedIds;
        }
        public static DataTable SelectCongViecFiltered(
            DateTime tuNgay,
            DateTime denNgay,
            string keyword,
            int? keHoachId)
        {
            DateTime denNgayFilter;
            string comparisonOperator;

            if (denNgay.Date == new DateTime(9999, 12, 31).Date)
            {
                denNgayFilter = denNgay.Date;
                comparisonOperator = "<=";
            }
            else
            {
                denNgayFilter = denNgay.Date.AddDays(1);
                comparisonOperator = "<";
            }

            string searchKeyword = string.IsNullOrWhiteSpace(keyword) ? null : "%" + keyword.Trim() + "%";

            string query = $@"
            SELECT 
                cv.CongViecID, cv.TenCongViec, cv.Loai, cv.TrangThai, cv.MucUuTien, cv.HanChot,
                kh.TenKeHoach, 
                u.FullName AS NguoiGiao,
                (SELECT STRING_AGG(u2.FullName, ', ') 
                FROM dbo.PhanCong pc 
                JOIN dbo.[User] u2 ON pc.UserID = u2.UserID 
                WHERE pc.CongViecID = cv.CongViecID) AS NguoiDamNhiem
            FROM 
                dbo.CongViec cv
            LEFT JOIN 
                dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
            JOIN 
                dbo.[User] u ON cv.NguoiGiaoID = u.UserID
            WHERE
                cv.HanChot >= @TuNgay 
                AND cv.HanChot {comparisonOperator} @DenNgayFilter
                AND (@Keyword IS NULL OR cv.TenCongViec LIKE @Keyword)
                {(keHoachId.HasValue && keHoachId.Value > 0 ? "AND cv.KeHoachID = @KeHoachID" : "")}
            ORDER BY 
                cv.HanChot ASC;
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@TuNgay", tuNgay.Date },
                { "@DenNgayFilter", denNgayFilter },
                { "@Keyword", searchKeyword ?? (object)DBNull.Value }
            };

            if (keHoachId.HasValue && keHoachId.Value > 0)
                parameters["@KeHoachID"] = keHoachId.Value;

            return Db.ExecuteQuery(query, parameters);
        }
        public static bool UpdateCongViecStatus(int congViecId, string newStatus)
        {
            string query = @"
                 UPDATE dbo.CongViec 
                 SET TrangThai = @NewStatus
                 WHERE CongViecID = @CongViecID 
                 AND TrangThai = 'MOI'"; // Chỉ update nếu nó vẫn là MOI (chưa bắt đầu)

                     var parameters = new Dictionary<string, object>
             {
                 { "@CongViecID", congViecId },
                 { "@NewStatus", newStatus }
             };

              try
              {
                  return Db.ExecuteNonQuery(query, parameters) > 0;
              }
              catch (Exception ex)
              {
                  throw new Exception("DAL Error (UpdateCongViecStatus): " + ex.Message, ex);
              }
        }
        public static DataTable SelectAllCongViec()
        {
            string query = @"
                SELECT 
                    cv.CongViecID,
                    cv.TenCongViec,
                    cv.Loai,
                    cv.TrangThai,
                    cv.MucUuTien,
                    cv.HanChot,
                    kh.TenKeHoach,
                    u1.FullName AS NguoiGiao
                FROM dbo.CongViec cv
                LEFT JOIN dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
                LEFT JOIN dbo.[User] u1 ON cv.NguoiGiaoID = u1.UserID
                ORDER BY cv.CongViecID DESC
            ";

            try
            {
                return Db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL SelectAllCongViec Error: " + ex.Message);
            }
        }
        public static DataTable GetLoaiKeHoachThongKe()
        {
            string query = @"
                SELECT 
                    kh.Loai,
                    COUNT(cv.CongViecID) AS SoLuong
                FROM CongViec cv
                LEFT JOIN KeHoach kh ON cv.KeHoachID = kh.KeHoachID
                GROUP BY kh.Loai
                ORDER BY kh.Loai";

            return Db.ExecuteQuery(query);
        }

    }
}