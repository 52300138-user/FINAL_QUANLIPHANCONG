using System;
using System.Data;
using System.Collections.Generic;
using WindowsFormsApp2.DTO;
using WindowsFormsApp2.DAL; 

namespace WindowsFormsApp2.DAL
{
    public static class MonHocDAL
    {
        /// Lấy toàn bộ Môn học cho DataGridView
        public static DataTable SelectMonHocTable()
        {
            string query = "SELECT * FROM dbo.MonHoc ORDER BY MaMonHoc";
            return Db.ExecuteQuery(query);
        }

        /// Lấy 1 MonHocDTO bằng ID (cho Form Sửa)
        public static MonHocDTO GetMonHocById(int monHocId)
        {
            string query = "SELECT * FROM dbo.MonHoc WHERE MonHocID = @MonHocID";
            var parameters = new Dictionary<string, object>
            {
                { "@MonHocID", monHocId }
            };

            DataTable dt = Db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new MonHocDTO
                {
                    MonHocID = Convert.ToInt32(row["MonHocID"]),
                    MaMonHoc = row["MaMonHoc"].ToString(),
                    TenMonHoc = row["TenMonHoc"].ToString(),
                    TenNhom = row["TenNhom"].ToString(),
                    TenTo = row["TenTo"].ToString(),
                    HocKy = row["HocKy"].ToString(),
                    GhiChu = row["GhiChu"].ToString(),
                    SoTinChi = Convert.ToInt32(row["SoTinChi"]),
                    SoTiet_LT = Convert.ToInt32(row["SoTiet_LT"]),
                    SoTiet_TH = Convert.ToInt32(row["SoTiet_TH"])
                };
            }
            return null;
        }

        // thêm
        public static bool InsertMonHoc(MonHocDTO dto)
        {
            string query = @"
                INSERT INTO dbo.MonHoc (MaMonHoc, TenMonHoc, TenNhom, TenTo, HocKy, GhiChu, SoTinChi, SoTiet_LT, SoTiet_TH) 
                VALUES (@MaMonHoc, @TenMonHoc, @TenNhom, @TenTo, @HocKy, @GhiChu, @SoTinChi, @SoTiet_LT, @SoTiet_TH)";

            var parameters = new Dictionary<string, object>
            {
                { "@MaMonHoc", dto.MaMonHoc },
                { "@TenMonHoc", dto.TenMonHoc },
                { "@TenNhom", dto.TenNhom },
                { "@TenTo", dto.TenTo },
                { "@HocKy", dto.HocKy },
                { "@GhiChu", dto.GhiChu },
                { "@SoTinChi", dto.SoTinChi },
                { "@SoTiet_LT", dto.SoTiet_LT },
                { "@SoTiet_TH", dto.SoTiet_TH }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }


        // cập nhật
        public static bool UpdateMonHoc(MonHocDTO dto)
        {
            // Không cần cập nhật MaMonHoc, TenNhom, TenTo vì chúng là READONLY (chỉ cập nhật nội dung)
            string query = @"
                UPDATE dbo.MonHoc SET
                    TenMonHoc = @TenMonHoc,
                    HocKy = @HocKy,   
                    GhiChu = @GhiChu,   
                    SoTinChi = @SoTinChi,
                    SoTiet_LT = @SoTiet_LT,
                    SoTiet_TH = @SoTiet_TH
                WHERE MonHocID = @MonHocID";

            var parameters = new Dictionary<string, object>
            {
                { "@TenMonHoc", dto.TenMonHoc },
                { "@HocKy", dto.HocKy },
                { "@GhiChu", dto.GhiChu },
                { "@SoTinChi", dto.SoTinChi },
                { "@SoTiet_LT", dto.SoTiet_LT },
                { "@SoTiet_TH", dto.SoTiet_TH },
                { "@MonHocID", dto.MonHocID }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        /// Xóa Môn học
        public static bool DeleteMonHoc(int monHocId)
        {
            // (Sau này cần check logic FK_CV_MonHoc)
            string query = "DELETE FROM dbo.MonHoc WHERE MonHocID = @MonHocID";
            var parameters = new Dictionary<string, object>
            {
                { "@MonHocID", monHocId }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static int CountClassesByMaMonHoc(string maMonHoc, string hocKy)
        {
            
            string query = "SELECT COUNT(1) FROM dbo.MonHoc WHERE MaMonHoc = @MaMonHoc AND HocKy = @HocKy AND TenTo IS NULL";
            var parameters = new Dictionary<string, object>
            {
                { "@MaMonHoc", maMonHoc },
                { "@HocKy", hocKy }
            };

            object result = Db.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result);
        }

        public static string GetLastMaMonHoc()
        {
            string query = @"
                SELECT TOP 1 MaMonHoc 
                FROM dbo.MonHoc 
                WHERE MaMonHoc LIKE 'IT%' 
                ORDER BY MaMonHoc DESC";

            object result = Db.ExecuteScalar(query);
            return result?.ToString();
        }

        // Search
        public static DataTable SearchMonHoc(string keyword, string hocKy)
        {
            // Keyword sẽ được dùng cho cả MaMonHoc và TenMonHoc
            string query = @"
                SELECT * FROM dbo.MonHoc 
                WHERE 
                    (@HocKy IS NULL OR HocKy = @HocKy)
                    AND (@Keyword IS NULL OR MaMonHoc LIKE @Keyword OR TenMonHoc LIKE @Keyword)
                ORDER BY MaMonHoc;
            ";

            string searchKeyword = string.IsNullOrWhiteSpace(keyword) ? null : "%" + keyword.Trim() + "%";
            string searchHocKy = string.IsNullOrWhiteSpace(hocKy) || hocKy == "Tất cả" ? null : hocKy.Trim();

            var parameters = new Dictionary<string, object>
            {
                { "@Keyword", searchKeyword },
                { "@HocKy", searchHocKy }
            };

            return Db.ExecuteQuery(query, parameters);
        }

        public static string GetTenMonHocByMaMonHoc(string maMonHoc)
        {
            string query = "SELECT TOP 1 TenMonHoc FROM dbo.MonHoc WHERE MaMonHoc = @MaMonHoc";
            var parameters = new Dictionary<string, object>
            {
                { "@MaMonHoc", maMonHoc }
            };

            object result = Db.ExecuteScalar(query, parameters);

            // Trả về tên môn học nếu tìm thấy, ngược lại trả về null
            return result?.ToString();
        }

        public static bool UpdateAll(MonHocDTO dto)
        {
            // Chỉ cập nhật các trường được phép sửa hàng loạt
            string query = @"
                UPDATE dbo.MonHoc SET
                    TenMonHoc = @TenMonHoc,
                    SoTinChi = @SoTinChi,
                     SoTiet_LT = @SoTiet_LT,
                    SoTiet_TH = @SoTiet_TH
                WHERE 
                    MaMonHoc = @MaMonHoc 
                    AND HocKy = @HocKy"; 

            var parameters = new Dictionary<string, object>
            {
                { "@TenMonHoc", dto.TenMonHoc },
                { "@SoTinChi", dto.SoTinChi },
                { "@SoTiet_LT", dto.SoTiet_LT },
                { "@SoTiet_TH", dto.SoTiet_TH },
                { "@MaMonHoc", dto.MaMonHoc },
                { "@HocKy", dto.HocKy } 
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static DataTable GetAllMonHoc()
        {
            string query = @"
                SELECT 
                    MonHocID,
                    TenMonHoc,
                    HocKy
                FROM 
                    dbo.MonHoc
            ";

            return Db.ExecuteQuery(query);
        }
    }
}