using System;
using System.Collections.Generic;
using System.Data;

namespace WindowsFormsApp2.DAL
{
    internal static class BaoCaoDAL
    {
        /// <summary>
        /// Lấy danh sách phân công + công việc + kế hoạch + giảng viên
        /// theo bộ lọc: Loại kế hoạch, Loại công việc, Giảng viên
        /// </summary>
        public static DataTable GetChiTietBaoCaoTongHop(
            string loaiKeHoach,      // HocKyI / HocKyII / NamHoc / DeTai / SuKien / Khac / null
            string loaiCongViec,     // GiangDay / SuKien / DeTai / null
            int? giangVienId         // null = tất cả
        )
        {
            string query = @"
                SELECT
                    pc.PC_ID,
                    u.UserID,
                    u.FullName       AS TenGV,
                    cv.CongViecID,
                    cv.TenCongViec,
                    cv.Loai          AS LoaiCongViec,
                    kh.Loai          AS LoaiKeHoach,
                    pc.TrangThaiGV,
                    pc.NgayGiao,
                    cv.HanChot
                FROM dbo.PhanCong pc
                JOIN dbo.CongViec cv   ON pc.CongViecID = cv.CongViecID
                JOIN dbo.KeHoach kh    ON cv.KeHoachID = kh.KeHoachID
                JOIN dbo.[User] u      ON pc.UserID = u.UserID
                WHERE
                    u.IsLocked = 0
                    AND u.Role IN ('GV', 'TBM')
                    AND (@LoaiKeHoach IS NULL OR kh.Loai = @LoaiKeHoach)
                    AND (@LoaiCongViec IS NULL OR cv.Loai = @LoaiCongViec)
                    AND (@GiangVienID IS NULL OR u.UserID = @GiangVienID)
                ORDER BY
                    u.FullName, cv.TenCongViec, pc.PC_ID;
            ";

            var parameters = new Dictionary<string, object>
            {
                { "@LoaiKeHoach",  string.IsNullOrWhiteSpace(loaiKeHoach) ? (object)DBNull.Value : loaiKeHoach },
                { "@LoaiCongViec", string.IsNullOrWhiteSpace(loaiCongViec) ? (object)DBNull.Value : loaiCongViec },
                { "@GiangVienID",  giangVienId.HasValue ? (object)giangVienId.Value : DBNull.Value }
            };

            return Db.ExecuteQuery(query, parameters);
        }
    }
}
