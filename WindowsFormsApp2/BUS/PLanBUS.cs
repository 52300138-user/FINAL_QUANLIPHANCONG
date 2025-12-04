using System;
using System.Data;
using System.Windows.Forms; // Cần cho MessageBox
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.BUS
{
    public static class PlansBUS
    {
        // === HÀM VALIDATE ===
        private static bool ValidatePlanData(PlanDTO dto)
        {
            if (dto.NguoiTaoID <= 0)
            {
                MessageBox.Show("Không xác định được người tạo. Vui lòng đăng nhập lại.", "Lỗi User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.TenKeHoach))
            {
                MessageBox.Show("Vui lòng nhập tên kế hoạch!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Loai))
            {
                MessageBox.Show("Vui lòng chọn loại kế hoạch!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dto.NgayKetThuc <= dto.NgayBatDau)
            {
                MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu.", "Lỗi logic ngày", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Nếu mọi thứ OK
            return true;
        }

        // === CÁC HÀM CRUD ===

        public static DataTable GetPlans()
        {
            try
            {
                return PlanDAL.SelectPlans();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kế hoạch (BUS): " + ex.Message);
                return new DataTable(); // Trả về bảng rỗng
            }
        }

        public static PlanDTO GetPlanById(int keHoachId)
        {
            try
            {
                return PlanDAL.GetPlanById(keHoachId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy chi tiết kế hoạch (BUS): " + ex.Message);
                return null;
            }
        }

        public static bool AddPlan(PlanDTO dto)
        {
            // Validate
            if (!ValidatePlanData(dto))
            {
                return false; // Dừng nếu validation fail
            }

            // Gọi DAL
            try
            {
                return PlanDAL.InsertPlan(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm kế hoạch (BUS): " + ex.Message);
                return false;
            }
        }

        public static bool UpdatePlan(PlanDTO dto)
        {
            // Validate
            if (!ValidatePlanData(dto))
            {
                return false;
            }

            try
            {
                return PlanDAL.UpdatePlan(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật kế hoạch (BUS): " + ex.Message);
                return false;
            }
        }

        public static bool DeletePlan(int keHoachId)
        {
            // === TODO: CHECK LOGIC CÔNG VIỆC ===
            // (Tạm thời comment, sau này mở ra và code)
            /*
            if (CongViecBUS.IsAnyCongViecNotCompleted(keHoachId))
            {
                MessageBox.Show("Không thể xóa kế hoạch này vì vẫn còn công việc chưa hoàn thành.", "Lỗi logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            */

            try
            {
                return PlanDAL.DeletePlan(keHoachId);
            }
            catch (Exception ex)
            {
                // Lỗi này thường là do FK constraint (công việc đang trỏ tới)
                MessageBox.Show("Lỗi khi xóa kế hoạch (BUS): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static string GetUserFullName(int nguoiTaoID)
        {
            try
            {
                var user = UsersBUS.GetUserById(nguoiTaoID);
                return user?.FullName ?? "(Không rõ)";
            }
            catch (Exception)
            {
                return "(Lỗi)";
            }
        }

            public static DataTable GetAllKeHoach()
            {
                try
                {
                    return PlanDAL.SelectAllKeHoach();
                }
                catch (Exception ex)
                {
                    throw new Exception("BUS: Lỗi khi tải danh sách Kế hoạch. " + ex.Message, ex);
                }
            }

        public static DataTable GetKeHoachForComboBox()
        {
            try 
            { 
                return PlanDAL.SelectAllKeHoach();
            }
            catch (Exception ex)
            {
                throw new Exception("BUS: Lỗi khi tải danh sách Kế hoạch. " + ex.Message, ex);
            }
        }
    }
}