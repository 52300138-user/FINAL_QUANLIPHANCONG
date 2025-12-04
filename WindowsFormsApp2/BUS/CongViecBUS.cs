using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.BUS
{
    public static class CongViecBUS
    {
        /// Hàm "gác cổng" validate
        private static bool ValidateCongViecData(CongViecDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenCongViec))
            {
                MessageBox.Show("Tên công việc không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.Loai))
            {
                MessageBox.Show("Vui lòng chọn Loại công việc.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dto.NguoiGiaoID <= 0)
            {
                MessageBox.Show("Không xác định được Người giao việc (TBM). Vui lòng đăng nhập lại.", "Lỗi User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // (Các logic check sâu hơn, ví dụ: nếu Loai='GiangDay' thì MonHocID không được null...)
            // (Tạm thời check đơn giản)

            return true;
        }

        // === CRUD ===
        public static DataTable GetCongViecTable()
        {
            try
            {
                return CongViecDAL.SelectCongViecTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách công việc (BUS): " + ex.Message);
                return new DataTable();
            }
        }
        // 1 dòng
        public static CongViecDTO GetCongViecById(int id)
        {
            try
            {
                return CongViecDAL.GetCongViecById(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy chi tiết công việc (BUS): " + ex.Message);
                return null;
            }
        }
        // 1 bảng
        public static DataTable SelectCongViecByKeHoachID(int keHoachId)
        {
            try
            {
                return CongViecDAL.SelectCongViecByKeHoachID(keHoachId);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy chi tiết công việc (BUS): " + ex.Message);
                return null;
            }
           
        }

        public static bool AddCongViec(CongViecDTO dto)
        {
            dto.TrangThai = "MOI";
            dto.MucUuTien = dto.MucUuTien ?? "MED";
            dto.TenCongViec = dto.TenCongViec.Trim();

            if (!ValidateCongViecData(dto))
            {
                return false;
            }

            // === THÊM LOGIC KIỂM TRA PHÂN CÔNG GIẢNG DẠY ===
            if (dto.Loai == "GiangDay" && dto.MonHocID.HasValue)
            {
                if (CongViecDAL.IsClassAlreadyAssigned(dto.MonHocID.Value))
                {
                    // Lấy tên lớp (Nhom/To) để thông báo
                    string tenLop = dto.LopPhuTrach ?? "Lớp này";
                    MessageBox.Show($"Lớp **{tenLop}** đã được phân công cho một Giảng viên khác. Vui lòng chọn lớp khác!",
                                    "Lỗi Phân công Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            try
            {
                return CongViecDAL.InsertCongViec(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm công việc (BUS): " + ex.Message);
                return false;
            }
        }

        public static bool UpdateCongViec(CongViecDTO dto)
        {
            dto.TenCongViec = dto.TenCongViec.Trim();

            if (!ValidateCongViecData(dto))
            {
                return false;
            }

            try
            {
                return CongViecDAL.UpdateCongViec(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật công việc (BUS): " + ex.Message);
                return false;
            }
        }

        public static bool DeleteCongViec(int id)
        {
           // TODO: Check logic phân công

            try
            {
                return CongViecDAL.DeleteCongViec(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa công việc (BUS): " + ex.Message);
                return false;
            }
        }

        public static List<int> GetAssignedMonHocIds()
        {
            try
            {
                return CongViecDAL.GetAssignedMonHocIds();
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nhưng vẫn trả về danh sách rỗng để UI không bị crash
                MessageBox.Show("Lỗi khi tải danh sách Lớp đã phân công (BUS): " + ex.Message, "Lỗi Hệ Thống");
                return new List<int>();
            }
        }

        public static DataTable GetCongViecFiltered(
            DateTime tuNgay,
            DateTime denNgay,
            string keyword,
            int? keHoachId)
            {
                try
                {
                return CongViecDAL.SelectCongViecFiltered(tuNgay, denNgay, keyword, keHoachId);
                }
                catch (Exception ex)
                {
                    throw new Exception("BUS: Lỗi khi lọc Công việc. " + ex.Message, ex);
                }
        }
        public static void CheckAndUpdateCongViecStatus(int congViecId)
        {
            try
            {
                // 1. Kiểm tra số lượng người chưa xác nhận (Trạng thái 'MOI')
                int pendingCount = PhanCongDAL.CountPendingAssignments(congViecId);

                if (pendingCount == 0)
                {
                    // 2. Nếu KHÔNG còn ai ở trạng thái MOI, cập nhật CV chung sang DANG_LAM
                    CongViecDAL.UpdateCongViecStatus(congViecId, "DANG_LAM");
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi, không cần show MessageBox ở đây vì nó chạy ngầm
                Console.WriteLine("Lỗi khi kiểm tra và cập nhật trạng thái CV chung: " + ex.Message);
            }
        }

        public static void UpdateTrangThaiCongViec(int congViecId, string newStatus)
        {
            string query = @"
            UPDATE dbo.CongViec 
            SET TrangThai = @TrangThai
            WHERE CongViecID = @CongViecID";

                var parameters = new Dictionary<string, object>
        {
            { "@TrangThai", newStatus },
            { "@CongViecID", congViecId }
        };

                try
                {
                    Db.ExecuteNonQuery(query, parameters);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi UpdateTrangThaiCongViec: " + ex.Message);
                }
        }

        public static DataTable GetAllCongViec()
        {
            try
            {
                return CongViecDAL.SelectAllCongViec();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tất cả công việc: " + ex.Message);
                return new DataTable();
            }
        }
        public static DataTable GetLoaiKeHoachThongKe()
        {
            try
            {
                return CongViecDAL.GetLoaiKeHoachThongKe();
            }
            catch (Exception ex)
            {
                throw new Exception("BUS: Lỗi lấy thống kê Loại Kế Hoạch. " + ex.Message, ex);
            }
        }


    }
}