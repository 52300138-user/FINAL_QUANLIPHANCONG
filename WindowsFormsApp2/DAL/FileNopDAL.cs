using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WindowsFormsApp2.DAL
{
    internal class FileNopDAL
    {
        public static bool InsertFileNop(int pcId, string fileName, string filePath)
        {
            string query = @"
        INSERT INTO dbo.FileNop (PC_ID, FileName, FilePath)
        VALUES (@PC_ID, @FileName, @FilePath)";

            var parameters = new Dictionary<string, object>()
        {
            { "@PC_ID", pcId },
            { "@FileName", fileName },
            { "@FilePath", filePath }
        };
        
            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static DataTable GetFilesChoDuyet(int? userId = null, DateTime? createdDate = null)
        {
            string query = @"
            SELECT fn.FileID, fn.PC_ID, fn.FileName, fn.FilePath, fn.CreatedAt,
                   u.FullName AS TenGV, cv.TenCongViec
            FROM dbo.FileNop fn
            INNER JOIN dbo.PhanCong pc ON fn.PC_ID = pc.PC_ID
            INNER JOIN dbo.[User] u ON pc.UserID = u.UserID
            INNER JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
            WHERE fn.TrangThaiDuyet = N'CHO_DUYET'
        ";

            var parameters = new Dictionary<string, object>();

            if (userId.HasValue)
            {
                query += " AND pc.UserID = @UserID";
                parameters.Add("@UserID", userId.Value);
            }

            if (createdDate.HasValue)
            {
                query += " AND CONVERT(date, fn.CreatedAt) = @Date";
                parameters.Add("@Date", createdDate.Value.Date);
            }

            query += " ORDER BY fn.CreatedAt DESC";

            return Db.ExecuteQuery(query, parameters);
        }
        public static DataTable GetFileById(int fileId)
        {
            string query = @"
            SELECT fn.FileID, fn.PC_ID, fn.FileName, fn.FilePath, fn.FileType, fn.FileSizeKB,
                   fn.TrangThaiDuyet, fn.GhiChuDuyet, fn.CreatedAt,
                   u.FullName AS TenGV,
                   cv.TenCongViec
            FROM dbo.FileNop fn
            INNER JOIN dbo.PhanCong pc ON fn.PC_ID = pc.PC_ID
            INNER JOIN dbo.[User] u ON pc.UserID = u.UserID
            INNER JOIN dbo.CongViec cv ON pc.CongViecID = cv.CongViecID
            WHERE fn.FileID = @FileID
        ";

            var parameters = new Dictionary<string, object>
            {
                { "@FileID", fileId }
            };

                return Db.ExecuteQuery(query, parameters);
        }
        public static bool ApproveFile(int fileId, int userId, string comment)
        {
            string query = @"
                UPDATE dbo.FileNop
                SET TrangThaiDuyet = N'DA_DUYET',
                    GhiChuDuyet = @Comment,
                    ApprovedBy = @UserID,
                    ApprovedAt = SYSDATETIME()
                WHERE FileID = @FileID";

            var parameters = new Dictionary<string, object>
            {
                { "@FileID", fileId },
                { "@UserID", userId },
                { "@Comment", comment }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool RejectFile(int fileId, int userId, string comment)
        {
            string query = @"
                UPDATE dbo.FileNop
                SET TrangThaiDuyet = N'TU_CHOI',
                    GhiChuDuyet = @Comment,
                    ApprovedBy = @UserID,
                    ApprovedAt = SYSDATETIME()
                WHERE FileID = @FileID";

            var parameters = new Dictionary<string, object>
            {
                { "@FileID", fileId },
                { "@UserID", userId },
                { "@Comment", comment }
            };

            return Db.ExecuteNonQuery(query, parameters) > 0;
        }

        public static string GetTenGVByPCID(int pcId)
        {
            string query = @"
                SELECT u.FullName
                FROM dbo.PhanCong pc
                JOIN dbo.[User] u ON pc.UserID = u.UserID
                WHERE pc.PC_ID = @PC_ID";

            var param = new Dictionary<string, object>()
            {
                {"@PC_ID", pcId}
            };

            DataTable dt = Db.ExecuteQuery(query, param);
            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0]["FullName"].ToString();
        }


    }

}
