using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.DAL
{
    internal class PhanCongDAL
    {
        public static DataTable SelectPhanCongByCongViecID(int congViecId)
        {
            // Join PhanCong với User để lấy Tên GV
            string query = @"
                SELECT 
                    pc.PC_ID,
                    pc.UserID,
                    u.FullName,
                    u.Role AS ChucVU,
                    pc.VaiTro,
                    pc.NgayGiao,
                    cv.HanChot,
                    pc.TrangThaiGV
                FROM                 
                    dbo.PhanCong pc
                JOIN 
                    dbo.[User] u ON pc.UserID = u.UserID
                JOIN
                    dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                WHERE 
                    pc.CongViecID = @CongViecID
                ORDER BY 
                    pc.VaiTro DESC
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };

            return Db.ExecuteQuery(query, parameters);
        }

        /// Thêm 1 Giảng viên vào 1 Công việc (TBM gán việc)
        public static bool InsertPhanCong(PhanCongDTO dto)
        {
            // Chỉ insert các trường TBM
            string query = @"
                INSERT INTO dbo.PhanCong 
                    (CongViecID, UserID, VaiTro, NgayGiao, GhiChu_TBM)
                VALUES 
                    (@CongViecID, @UserID, @VaiTro, @NgayGiao, @GhiChu_TBM)";

            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", dto.CongViecID },
                { "@UserID", dto.UserID },
                { "@VaiTro", dto.VaiTro },
                { "@NgayGiao", dto.NgayGiao },
                { "@GhiChu_TBM", dto.GhiChu_TBM }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        /// Xóa 1 Giảng viên khỏi 1 Công việc (TBM hủy gán việc)
        public static bool DeletePhanCong(int pc_ID)
        {
            string query = "DELETE FROM dbo.PhanCong WHERE PC_ID = @PC_ID";
            var parameters = new Dictionary<string, object>
            {
                { "@PC_ID", pc_ID }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        // --- CÁC HÀM CỦA GIẢNG VIÊN ---
        /// Lấy danh sách Công việc CỦA TÔI (của 1 Giảng viên)
        /// (Dùng cho Form 'Việc của tôi' của GV)
        public static DataTable SelectCongViecByUserID(int userId)
        {
            // Join ngược PhanCong với CongViec
            string query = @"
                SELECT 
                    cv.TenCongViec,
                    cv.Loai,
                    cv.HanChot,
                    pc.VaiTro,
                    pc.TrangThaiGV,
                    pc.PhanTram,
                    pc.PC_ID -- (ID của Phân công, rất quan trọng)
                FROM 
                    dbo.PhanCong pc
                JOIN 
                    dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                WHERE 
                    pc.UserID = @UserID 
                    AND cv.TrangThai != N'HOAN_THANH'; -- (Chỉ lấy việc chưa xong)
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@UserID", userId }
            };

            return Db.ExecuteQuery(query, parameters);
        }

        /// Giảng viên Cập nhật tiến độ
        public static bool UpdateTienDo(PhanCongDTO dto)
        {
            string query = @"
                UPDATE dbo.PhanCong SET
                    TrangThaiGV = @TrangThaiGV,
                    PhanTram = @PhanTram,
                    GhiChu_GV = @GhiChu_GV,
                    TepDinhKem_GV = @TepDinhKem_GV,
                    ThoiDiemCapNhatCuoi = SYSDATETIME()
                WHERE 
                    PC_ID = @PC_ID AND UserID = @UserID"; // (Check UserID cho bảo mật)

            var parameters = new Dictionary<string, object>
            {
                { "@TrangThaiGV", dto.TrangThaiGV },
                { "@PhanTram", dto.PhanTram },
                { "@GhiChu_GV", dto.GhiChu_GV },
                { "@TepDinhKem_GV", dto.TepDinhKem_GV },
                { "@PC_ID", dto.PC_ID },
                { "@UserID", dto.UserID } // (GV chỉ được sửa việc của chính mình)
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool CheckIfChuTriExists(int congViecId, int currentPcId)
        {
            // Nếu currentPcId == 0, ta đang INSERT mới (dùng logic cũ)
            if (currentPcId == 0)
            {
                // Logic cũ
                string query = "SELECT COUNT(1) FROM dbo.PhanCong WHERE CongViecID = @CongViecID AND VaiTro = N'ChuTri'";
                var parameters = new Dictionary<string, object> { { "@CongViecID", congViecId } };
                return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
            }
            else
            {
                // Logic mới: Check xem có ChuTri nào khác PC_ID hiện tại không
                string query = "SELECT COUNT(1) FROM dbo.PhanCong WHERE CongViecID = @CongViecID AND VaiTro = N'ChuTri' AND PC_ID != @CurrentPC_ID";
                var parameters = new Dictionary<string, object>
         {
             { "@CongViecID", congViecId },
             { "@CurrentPC_ID", currentPcId }
         };
                return Convert.ToInt32(Db.ExecuteScalar(query, parameters)) > 0;
            }
        }
        public static bool UpdatePhanCong(PhanCongDTO dto)
        {
            string query = @"
        UPDATE dbo.PhanCong SET
            UserID = @UserID, -- Cho phép TBM đổi GV nếu PC_ID có sẵn
            VaiTro = @VaiTro,
            GhiChu_TBM = @GhiChu_TBM
        WHERE 
            PC_ID = @PC_ID";

            var parameters = new Dictionary<string, object>
    {
        { "@UserID", dto.UserID },
        { "@VaiTro", dto.VaiTro },
        { "@GhiChu_TBM", dto.GhiChu_TBM ?? (object)DBNull.Value }, // Đảm bảo xử lý NULL
        { "@PC_ID", dto.PC_ID }
    };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static bool HasAssignments(int congViecId)
        {
            string query = "SELECT COUNT(*) FROM dbo.PhanCong WHERE CongViecID = @CongViecID";
            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };
            int count = Convert.ToInt32(Db.ExecuteScalar(query, parameters));
            return count > 0;
        }

        public static DataTable GetAssignedUsers(int congViecId)
        {
            string query = @"
                SELECT u.UserID, u.FullName, u.Email
                FROM dbo.PhanCong pc
                JOIN dbo.[User] u ON pc.UserID = u.UserID
                WHERE pc.CongViecID = @CongViecID";

            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };

            return Db.ExecuteQuery(query, parameters);
        }

        public static bool DeleteAllByCongViecID(int congViecId)
        {
            string query = "DELETE FROM dbo.PhanCong WHERE CongViecID = @CongViecID";
            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static DateTime? GetNgayGiaoDauTien(int congViecId)
        {
            string query = "SELECT MIN(NgayGiao) FROM dbo.PhanCong WHERE CongViecID = @CongViecID";
            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };

            object result = Db.ExecuteScalar(query, parameters);
            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToDateTime(result);
        }

        public static DataTable GetChiTietPhanCongByCongViec(int congViecId)
        {
            try
            {
                string query = @"
                SELECT 
                    pc.UserID, -- THÊM
                    u.FullName AS HoTenGV,
                    u.Role AS ChucVu,
                    CASE pc.VaiTro 
                        WHEN 'ChuTri' THEN N'Chủ trì'
                        WHEN 'HoTro'  THEN N'Hỗ trợ'
                        ELSE N'' 
                    END AS NhiemVu,
                    pc.NgayGiao AS NgayBatDau,
                    cv.HanChot AS NgayKetThuc, -- sửa theo yêu cầu
                    CASE pc.TrangThaiGV
                        WHEN 'MOI'        THEN N'Mới'
                        WHEN 'DANG_LAM'   THEN N'Đang làm'
                        WHEN 'HOAN_THANH' THEN N'Đã hoàn thành'
                        ELSE N'Khác'
                    END AS TrangThai,
                    pc.ThoiGianNop
                FROM dbo.PhanCong pc
                INNER JOIN dbo.[User] u ON pc.UserID = u.UserID
                INNER JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                WHERE pc.CongViecID = @CongViecID
                ORDER BY u.FullName;
                    ";

                var parameters = new Dictionary<string, object>
                {
                    { "@CongViecID", congViecId }
                };

                return Db.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL GetChiTietPhanCongByCongViec Error: " + ex.Message);
            }
        }
        public static DataTable SelectCongViecByStatus(int userId, string statusKey)
        {
            string query = @"
                SELECT 
                    pc.PC_ID,
                    pc.CongViecID,
                    cv.TenCongViec,
                    cv.Loai,
                    cv.HanChot,
                    pc.VaiTro,
                    pc.TrangThaiGV,
                    pc.PhanTram,
                    pc.NgayGiao
                FROM 
                    dbo.PhanCong pc
                JOIN 
                    dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                WHERE 
                    pc.UserID = @UserID AND pc.TrangThaiGV = @StatusKey
                ORDER BY 
                    cv.HanChot DESC;
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@StatusKey", statusKey }
            };

            try
            {
                return Db.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error (SelectCongViecByStatus): " + ex.Message, ex);
            }
        }

        /// Đếm số lượng GV được gán có trạng thái MOI
        public static int CountPendingAssignments(int congViecId)
        {
            string query = @"
                SELECT COUNT(1) FROM dbo.PhanCong 
                WHERE CongViecID = @CongViecID 
                AND TrangThaiGV = 'MOI'";

            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };

            try
            {
                object result = Db.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error (CountPendingAssignments): " + ex.Message, ex);
            }
        }

        /// GV: Xác nhận chấp nhận công việc (MOI -> DANG_LAM)
        public static bool AcceptAssignment(int pcId, int userId)
        {
            string query = @"
                UPDATE dbo.PhanCong 
                SET TrangThaiGV = 'DANG_LAM', NgayGiao = SYSDATETIME(), ThoiDiemCapNhatCuoi = SYSDATETIME() 
                WHERE PC_ID = @PC_ID AND UserID = @UserID AND TrangThaiGV = 'MOI'";

            var parameters = new Dictionary<string, object>
            {
                { "@PC_ID", pcId },
                { "@UserID", userId }
            };

            try
            {
                return Db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error (AcceptAssignment): " + ex.Message, ex);
            }
        }

        /// GV: Từ chối công việc mới được gán (MOI -> TU_CHOI)
        public static bool RejectAssignment(int pcId, int userId, string lyDo)
        {
            string query = @"
                UPDATE dbo.PhanCong 
                SET TrangThaiGV = 'TU_CHOI', 
                    GhiChu_GV = @LyDo,
                    ThoiDiemCapNhatCuoi = SYSDATETIME()
                WHERE PC_ID = @PC_ID AND UserID = @UserID AND TrangThaiGV = 'MOI'";

            var parameters = new Dictionary<string, object>
            {
                { "@PC_ID", pcId },
                { "@UserID", userId },
                { "@LyDo", lyDo }
            };

            try
            {
                return Db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error (RejectAssignment): " + ex.Message, ex);
            }
        }
        // Lấy danh sách công việc bị từ chối
        public static DataTable SelectCongViecTuChoi()
        {
             string query = @"
             SELECT 
                 pc.PC_ID,
                 pc.CongViecID,
                 cv.TenCongViec,
                 u.FullName AS TenGV,
                 pc.GhiChu_GV AS LyDo,
                 pc.NgayGiao,
                 pc.VaiTro
             FROM dbo.PhanCong pc
             JOIN dbo.[User] u ON pc.UserID = u.UserID
             JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
             WHERE pc.TrangThaiGV = 'TU_CHOI'
             ORDER BY pc.NgayGiao DESC;";

             return Db.ExecuteQuery(query);
        }


             // TBM đồng ý cho GV từ chối → XÓA phân công
        public static bool ApproveReject(int pcId)
        {
            string query = "DELETE FROM dbo.PhanCong WHERE PC_ID = @PC_ID";

            var parameters = new Dictionary<string, object>
        {
            { "@PC_ID", pcId }
        };

             return Db.ExecuteNonQuery(query, parameters) > 0;
        }

    
        // TBM không đồng ý → đẩy CV về MOI cho GV chọn lại
        public static bool DisapproveReject(int pcId)
        {
            string query = @"
               UPDATE dbo.PhanCong
               SET TrangThaiGV = 'MOI',
                   GhiChu_GV = NULL,
                   ThoiDiemCapNhatCuoi = SYSDATETIME()
               WHERE PC_ID = @PC_ID";
        
             var parameters = new Dictionary<string, object>
             {
               { "@PC_ID", pcId }
             };
        
             return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool UpdateTrangThaiGV(int pcId, string status)
        {
            string query = @"
                UPDATE dbo.PhanCong
                SET TrangThaiGV = @TrangThaiGV,
                    ThoiDiemCapNhatCuoi = SYSDATETIME()
                WHERE PC_ID = @PC_ID";

            var parameters = new Dictionary<string, object>
            {
                {"@TrangThaiGV", status},
                {"@PC_ID", pcId}
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool AllAssignmentsCompleted(int congViecId)
        {
            string query = @"
                SELECT COUNT(*) 
                FROM dbo.PhanCong 
                WHERE CongViecID = @CongViecID
                  AND TrangThaiGV <> 'HOAN_THANH'";
            var parameters = new Dictionary<string, object>
            {
                { "@CongViecID", congViecId }
            };

                     int count = Convert.ToInt32(Db.ExecuteScalar(query, parameters));
            return count == 0; // true nếu tất cả đều hoàn thành
        }
        public static DataTable GetPhanCongByUser(int userId)
        {
            string query = @"
                SELECT pc.PC_ID, pc.TrangThaiGV, pc.VaiTro, pc.NgayGiao, cv.TenCongViec, cv.HanChot
                FROM dbo.PhanCong pc
                JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                WHERE pc.UserID = @UserID";

            var param = new Dictionary<string, object>()
            {
                {"@UserID", userId}
            };

            return Db.ExecuteQuery(query, param);
        }
        public static DataTable GetSoLuongCongViecTheoGV()
        {
            string query = @"
            SELECT 
                u.UserID,
                u.FullName,
                SUM(CASE WHEN pc.TrangThaiGV = 'MOI' THEN 1 ELSE 0 END) AS Moi,
                SUM(CASE WHEN pc.TrangThaiGV = 'DANG_LAM' THEN 1 ELSE 0 END) AS DangLam
            FROM PhanCong pc
            JOIN [User] u ON pc.UserID = u.UserID
            WHERE u.IsLocked = 0
              AND u.Role IN ('GV', 'TBM')
            GROUP BY u.UserID, u.FullName
            ORDER BY 
            LTRIM(RIGHT(u.FullName, CHARINDEX(' ', REVERSE(u.FullName) + ' ') - 1))
        ";
            return Db.ExecuteQuery(query);
        }
        public static DataTable SearchCongViecByTrangThai(int userId, string trangThai, string loaiKeHoach, string keyword)
            {
                string query = @"
                SELECT 
                    pc.PC_ID,
                    cv.CongViecID,
                    cv.TenCongViec,
                    pc.VaiTro,
                    pc.NgayGiao,
                    cv.HanChot,
                    pc.TrangThaiGV,
                    kh.Loai AS LoaiKeHoach
                FROM dbo.PhanCong pc
                JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
                JOIN dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
                WHERE 
                    pc.UserID = @UserID
                    AND pc.TrangThaiGV = @TrangThai
                    AND (@LoaiKeHoach IS NULL OR kh.Loai = @LoaiKeHoach)
                    AND (@Keyword IS NULL OR cv.TenCongViec LIKE @Keyword)
                ORDER BY cv.HanChot ASC
                ";

            var parameters = new Dictionary<string, object>()
            {
                { "@UserID", userId },
                { "@TrangThai", trangThai },
                { "@LoaiKeHoach", string.IsNullOrWhiteSpace(loaiKeHoach) ? null : loaiKeHoach },
                { "@Keyword", string.IsNullOrWhiteSpace(keyword) ? null : "%" + keyword.Trim() + "%" }
            };

            return Db.ExecuteQuery(query, parameters);
        }
        public static DataTable GetCongViecByTrangThai(int userId, string trangThai, string loaiKeHoach)
        {
            string query = @"
            SELECT 
                pc.PC_ID,
                cv.CongViecID,
                cv.TenCongViec,
                pc.VaiTro,
                pc.NgayGiao,
                cv.HanChot,
                pc.TrangThaiGV,
                kh.Loai AS LoaiKeHoach
            FROM dbo.PhanCong pc
            JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
            JOIN dbo.KeHoach kh ON cv.KeHoachID = kh.KeHoachID
            WHERE 
                pc.UserID = @UserID
                AND pc.TrangThaiGV = @TrangThai
                AND (@LoaiKeHoach IS NULL OR kh.Loai = @LoaiKeHoach)
            ORDER BY cv.HanChot ASC
            ";

            var parameters = new Dictionary<string, object>()
            {
                { "@UserID", userId },
                { "@TrangThai", trangThai },
                { "@LoaiKeHoach", string.IsNullOrWhiteSpace(loaiKeHoach) ? null : loaiKeHoach }
            };
            return Db.ExecuteQuery(query, parameters);
        }
        public static DataTable GetAllAssignmentsForChart()
        {
            string query = @"
            SELECT 
                u.FullName,
                pc.TrangThaiGV,
                cv.HanChot
            FROM dbo.PhanCong pc
            JOIN dbo.[User] u ON pc.UserID = u.UserID
            JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
            WHERE u.IsLocked = 0
              AND u.Role IN ('GV', 'TBM')
            ORDER BY u.FullName
            ";
     
            return Db.ExecuteQuery(query);
        }

    }
}
