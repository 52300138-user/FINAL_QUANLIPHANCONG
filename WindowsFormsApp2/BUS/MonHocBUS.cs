using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;
// (Giả sử CongViecDAL cũng nằm trong DAL)

namespace WindowsFormsApp2.BUS
{
    public static class MonHocBUS
    {

        // === HÀM VALIDATE CHUNG ===
        public static (string TenNhom, string TenTo) GenerateNextNhomTo(string maMonHoc, string hocKy)
        {
            try
            {
                // 1. Đếm số lớp Lý thuyết (chỉ đếm lớp có TenTo = NULL)
                int currentCountOfLT = MonHocDAL.CountClassesByMaMonHoc(maMonHoc, hocKy);

                // Lớp tiếp theo (Nhom) LUÔN LÀ currentCountOfLT + 1
                int nextIndex = currentCountOfLT + 1;

                // 2. Định dạng chuỗi (N01, N02...)
                string formattedIndex = nextIndex.ToString("D2");

                // 3. Ghép chuỗi (TenNhom và TenTo của nhóm mới sẽ cùng số)
                string nhom = "N" + formattedIndex;
                string to = "T" + formattedIndex;

                return (nhom, to);
            }
            catch (Exception)
            {
                // Nếu lỗi, trả về N01, T01 (Giả sử đây là lớp đầu tiên)
                return ("N01", "T01");
            }
        }
        public static string GetTenMonHocByMa(string maMonHoc)
        {
            if (string.IsNullOrWhiteSpace(maMonHoc)) return null;

            try
            {
                // Gọi đến DAL để lấy tên môn học
                return MonHocDAL.GetTenMonHocByMaMonHoc(maMonHoc.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nhưng không chặn luồng
                MessageBox.Show("Lỗi khi tìm kiếm tên môn học: " + ex.Message, "Lỗi Hệ Thống");
                return null;
            }
        }

        public static int CalculateSoTietLT(int soTinChi)
        {
            // 3 tín chỉ LT = 45 tiết. Tức là 1 TC = 15 Tiết LT.
            return soTinChi * 15;
        }

        /// Tính số tiết Thực hành theo số tín chỉ (1 TC = 10 Tiết TH)
        public static int CalculateSoTietTH(int soTinChi)
        {
            // 1 tín chỉ TH = 10 tiết.
            return soTinChi * 10;
        }
        private static bool ValidateMonHocData(MonHocDTO dto)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(dto.MaMonHoc))
            {
                MessageBox.Show("Mã môn học không được để trống (lỗi sinh mã).", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.TenMonHoc))
            {
                MessageBox.Show("Tên môn học không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.HocKy))
            {
                MessageBox.Show("Học Kỳ không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.TenNhom))
            {
                MessageBox.Show("Tên Nhóm không được để trống.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra giá trị số
            if (dto.SoTinChi <= 0)
            {
                MessageBox.Show("Số tín chỉ phải lớn hơn 0.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dto.SoTiet_LT < 0 || dto.SoTiet_TH < 0)
            {
                MessageBox.Show("Số tiết không được là số âm.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }



        // === CÁC HÀM CRUD (GỌI TỪ UI) ===

        public static DataTable GetMonHocTable()
        {
            try { return MonHocDAL.SelectMonHocTable(); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách môn học (BUS): " + ex.Message);
                return new DataTable();
            }
        }

        public static MonHocDTO GetMonHocById(int id)
        {
            try { return MonHocDAL.GetMonHocById(id); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy chi tiết môn học (BUS): " + ex.Message);
                return null;
            }
        }

        public static bool AddMonHoc(MonHocDTO dto, int soLopTH)
        {
            if (!ValidateMonHocData(dto)) return false;

            try
            {
                bool success = true;

                // 1. TẠO LỚP LÝ THUYẾT (Luôn có)
                dto.TenTo = null; // Set TenTo = NULL cho lớp LT
                if (!MonHocDAL.InsertMonHoc(dto))
                {
                    MessageBox.Show("Không thể tạo lớp Lý thuyết.", "Lỗi DB");
                    success = false;
                }

                // 2. TẠO CÁC TỔ THỰC HÀNH (Nếu có Tiết TH VÀ có Tổ TH > 0)
                if (dto.SoTiet_TH > 0 && soLopTH > 0)
                {
                    for (int i = 1; i <= soLopTH; i++)
                    {
                        // Sinh TenTo: T01, T02, ...
                        string tenTo = "T" + i.ToString("D2");
                        dto.TenTo = tenTo;

                        // Insert từng tổ TH
                        if (!MonHocDAL.InsertMonHoc(dto))
                        {
                            MessageBox.Show($"Lỗi khi tạo Tổ Thực hành {tenTo}.", "Lỗi DB");
                            success = false;
                        }
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm môn học (BUS): " + ex.Message);
                return false;
            }
        }

        public static bool UpdateMonHoc(MonHocDTO dto, bool isBulkUpdate)
        {
            // Kiểm tra validation (giữ nguyên logic cũ)
            if (string.IsNullOrWhiteSpace(dto.TenMonHoc))
            {
                MessageBox.Show("Tên môn học không được để trống.");
                return false;
            }
            if (dto.SoTinChi <= 0)
            {
                MessageBox.Show("Số tín chỉ phải lớn hơn 0.");
                return false;
            }

            try
            {
                bool success;

                if (isBulkUpdate)
                {
                    // Nếu người dùng chọn Yes: Gọi hàm cập nhật hàng loạt (chỉ sửa các thuộc tính chung)
                    success = MonHocDAL.UpdateAll(dto);
                }
                else
                {
                    // Nếu người dùng chọn No: Gọi hàm cập nhật riêng lẻ (sửa tất cả các trường, bao gồm Học Kỳ, Ghi Chú)
                    success = MonHocDAL.UpdateMonHoc(dto);
                }

                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật môn học (BUS): " + ex.Message);
                return false;
            }
        }

        // Giữ lại hàm cũ để tránh lỗi nếu có nơi gọi không truyền tham số
        public static bool UpdateMonHoc(MonHocDTO dto)
        {
            // Mặc định là cập nhật riêng lẻ nếu không có quyết định từ UI
            return UpdateMonHoc(dto, false);
        }

        public static bool DeleteMonHoc(int id)
        {
            try
            {
                if (CongViecDAL.IsMonHocInUse(id))
                {
                    MessageBox.Show("Không thể xóa. Môn học này đang được sử dụng trong Công việc.", "Lỗi Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra ràng buộc Môn học (BUS): " + ex.Message);
                return false;
            }
            try
            {
                return MonHocDAL.DeleteMonHoc(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa môn học (BUS): " + ex.Message);
                return false;
            }
        }

        // Search
        public static DataTable SearchMonHoc(string keyword, string hocKy)
        {
            try
            {
                return MonHocDAL.SearchMonHoc(keyword, hocKy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Môn học: " + ex.Message, "Lỗi Hệ Thống");
                return new DataTable();
            }
        }

        public static DataTable GetAllMonHoc()
        {
            return MonHocDAL.GetAllMonHoc();
        }
    }
}