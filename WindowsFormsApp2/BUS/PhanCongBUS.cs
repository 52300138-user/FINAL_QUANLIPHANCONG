using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.BUS
{
    public static class PhanCongBUS
    {
        // === DÙNG CHO TBM ===
        /// TBM: Lấy danh sách GV đã gán cho 1 CV
        public static DataTable GetPhanCongByCongViecID(int congViecId)
        {
            try
            {
                return PhanCongDAL.SelectPhanCongByCongViecID(congViecId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phân công (BUS): " + ex.Message);
                return new DataTable();
            }
        }

        /// TBM: Gán 1 GV mới vào CV
        public static bool AddPhanCong(PhanCongDTO dto)
        {
            if (dto.CongViecID <= 0 || dto.UserID <= 0)
            {
                MessageBox.Show("Công việc hoặc Giảng viên không hợp lệ.", "Lỗi Validation");
                return false;
            }
            if (dto.NgayGiao == null)
            {
                dto.NgayGiao = DateTime.Today; // Tự set ngày giao
            }

            // === BỔ SUNG LOGIC  ===
            if (dto.VaiTro == "ChuTri")
            {
                if (dto.VaiTro == "ChuTri")
                {
                    try
                    {
                        if (PhanCongDAL.CheckIfChuTriExists(dto.CongViecID, 0)) // <<< Dùng 0 cho PC_ID mới
                        {
                            MessageBox.Show("Lỗi: Công việc này đã có 'Chủ trì'.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    catch (Exception ex) { /* ... */ return false; }
                }
            }
            try
            {
                return PhanCongDAL.InsertPhanCong(dto);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UQ_PhanCong"))
                {
                    MessageBox.Show("Lỗi: Giảng viên này đã được phân công cho công việc này rồi.", "Lỗi Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi khi gán phân công (BUS): " + ex.Message);
                }
                return false;
            }
        }

 
        /// TBM: Hủy phân công (Xóa GV khỏi CV)
        public static bool DeletePhanCong(int pc_ID)
        {
            try
            {
                return PhanCongDAL.DeletePhanCong(pc_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy phân công (BUS): " + ex.Message);
                return false;
            }
        }

        // === DÙNG CHO GV ===
        /// GV: Lấy danh sách "Việc Của Tôi"
        public static DataTable GetCongViecByUserID(int userId)
        {
            try
            {
                return PhanCongDAL.SelectCongViecByUserID(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải 'Việc Của Tôi' (BUS): " + ex.Message);
                return new DataTable();
            }
        }

        /// GV: Cập nhật tiến độ
        public static bool UpdateTienDo(PhanCongDTO dto)
        {
            // Validation
            if (dto.PC_ID <= 0 || dto.UserID <= 0)
            {
                MessageBox.Show("Không xác định được công việc hoặc người dùng.", "Lỗi Validation");
                return false;
            }
            if (dto.PhanTram < 0 || dto.PhanTram > 100)
            {
                MessageBox.Show("Phần trăm phải từ 0 đến 100.", "Lỗi Validation");
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.TrangThaiGV))
            {
                MessageBox.Show("Trạng thái không được để trống.", "Lỗi Validation");
                return false;
            }

            try
            {
                return PhanCongDAL.UpdateTienDo(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tiến độ (BUS): " + ex.Message);
                return false;
            }
        }

        /// Lấy danh sách GV để TBM chọn 
        public static DataTable GetGiangVienList()
        {
            try
            {
                return UsersDAL.SelectGiangVienList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Giảng viên (BUS): " + ex.Message);
                return new DataTable();
            }
        }
        public static bool UpdatePhanCong(PhanCongDTO dto)
        {
            // Validation cơ bản
            if (dto.PC_ID <= 0 || dto.UserID <= 0)
            {
                MessageBox.Show("Công việc hoặc Giảng viên không hợp lệ.", "Lỗi Validation");
                return false;
            }

            // === BỔ SUNG LOGIC KIỂM TRA CHỦ TRÌ ===
            if (dto.VaiTro == "ChuTri")
            {
                try
                {
                    if (PhanCongDAL.CheckIfChuTriExists(dto.CongViecID, dto.PC_ID)) // <<< Dùng PC_ID hiện tại
                    {
                        MessageBox.Show("Lỗi: Công việc này đã có 'Chủ trì' khác.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch (Exception ex) { /* ... */ return false; }
            }
            try
            {
                return PhanCongDAL.UpdatePhanCong(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phân công (BUS): " + ex.Message);
                return false;
            }
        }
        public static bool HasAssignments(int congViecId)
        {
            try
            {
                return PhanCongDAL.HasAssignments(congViecId);
            }
            catch
            {
                return false;
            }
        }

        public static List<UserDTO> GetAssignedUsersByCongViec(int congViecId)
        {
            List<UserDTO> result = new List<UserDTO>();
            try
            {
                DataTable dt = PhanCongDAL.GetAssignedUsers(congViecId);
                foreach (DataRow row in dt.Rows)
                {
                    UserDTO user = new UserDTO
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        FullName = row["FullName"].ToString(),
                        Email = row["Email"].ToString()
                    };
                    result.Add(user);
                }
            }
            catch
            {
            }
            return result;
        }

        public static bool DeleteAllByCongViecID(int congViecId)
        {
            try
            {
                return PhanCongDAL.DeleteAllByCongViecID(congViecId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa toàn bộ phân công: " + ex.Message);
                return false;
            }
        }

        public static DateTime? GetNgayGiaoDauTien(int congViecId)
        {
            try
            {
                return PhanCongDAL.GetNgayGiaoDauTien(congViecId);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetChiTietPhanCongByCongViec(int congViecId)
        {
            try { 
                return PhanCongDAL.GetChiTietPhanCongByCongViec(congViecId);
    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Debug");
                throw;
            }
        }

        public static DataTable GetCongViecByStatus(int userId, string statusKey)
        {
            try
            {
                return PhanCongDAL.SelectCongViecByStatus(userId, statusKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải công việc theo trạng thái '{statusKey}': {ex.Message}", "Lỗi Hệ Thống");
                return new DataTable();
            }
        }

        /// GV: Xác nhận chấp nhận công việc (MOI -> DANG_LAM)
        public static bool AcceptAssignment(int pcId, int userId, int congViecId)
        {
            try
            {
                if (PhanCongDAL.AcceptAssignment(pcId, userId))
                {
                    // Kiểm tra và cập nhật trạng thái chung của Công việc
                    CongViecBUS.CheckAndUpdateCongViecStatus(congViecId); // Cần đảm bảo hàm này tồn tại
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chấp nhận công việc: " + ex.Message, "Lỗi Nghiệp vụ");
                return false;
            }
        }

        /// GV: Từ chối nhiệm vụ được giao (MOI -> TU_CHOI)
        public static bool RejectAssignment(int pcId, int userId, string lyDo)
        {
            if (string.IsNullOrWhiteSpace(lyDo))
            {
                MessageBox.Show("Vui lòng nhập lý do từ chối.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                return PhanCongDAL.RejectAssignment(pcId, userId, lyDo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi từ chối công việc: " + ex.Message, "Lỗi Nghiệp vụ");
                return false;
            }
        }

        public static DataTable GetCongViecTuChoi()
        {
            try
            {
                return PhanCongDAL.SelectCongViecTuChoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS Error – GetCongViecTuChoi:\n" + ex.Message);
                return new DataTable();
            }
        }

        // TBM đồng ý — xóa PC
        public static bool ApproveReject(int pcId)
        {
            try
            {
                return PhanCongDAL.ApproveReject(pcId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS Error – ApproveReject:\n" + ex.Message);
                return false;
            }
        }

        // TBM không đồng ý — trả về MOI
        public static bool DisapproveReject(int pcId)
        {
            try
            {
                return PhanCongDAL.DisapproveReject(pcId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS Error – DisapproveReject:\n" + ex.Message);
                return false;
            }
        }
        public static bool UpdateTrangThaiGV(int pcId, string status)
        {
            try
            {
                return PhanCongDAL.UpdateTrangThaiGV(pcId, status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message);
                return false;
            }
        }
        public static void UpdateTrangThaiAfterApprove(int pcId)
        {
            try
            {
                // Lấy thông tin phân công  
                int congViecId = Convert.ToInt32(
                    Db.ExecuteScalar("SELECT CongViecID FROM dbo.PhanCong WHERE PC_ID = @PC_ID",
                    new Dictionary<string, object> { { "@PC_ID", pcId } })
                );

                // Cập nhật trạng thái phân công thành hoàn thành
                PhanCongDAL.UpdateTrangThaiGV(pcId, "HOAN_THANH");

                // Kiểm tra tất cả GV của công việc đã hoàn thành?
                if (PhanCongDAL.AllAssignmentsCompleted(congViecId))
                {
                    CongViecBUS.UpdateTrangThaiCongViec(congViecId, "HOAN_THANH");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi UpdateTrangThaiAfterApprove: " + ex.Message);
            }
        }


        public static bool UpdateTrangThaiAfterReject(int pcId)
        {
            return PhanCongDAL.UpdateTrangThaiGV(pcId, "DANG_LAM");
        }

        public static DataTable GetPhanCongByUser(int userId)
        {
            return PhanCongDAL.GetPhanCongByUser(userId);
        }
        public static DataTable GetSoLuongCongViecTheoGV()
        {
            try
            {
                return PhanCongDAL.GetSoLuongCongViecTheoGV();
            }
            catch (Exception ex)
            {
                throw new Exception("BUS: lỗi lấy số lượng công việc theo GV: " + ex.Message);
            }
        }
        public static DataTable GetCongViecByTrangThai(int userId, string trangThai, string loaiKeHoach)
        {
            return PhanCongDAL.GetCongViecByTrangThai(userId, trangThai, loaiKeHoach);
        }

        public static DataTable SearchCongViecByTrangThai(int userId, string trangThai, string loaiKeHoach, string keyword)
        {
            return PhanCongDAL.SearchCongViecByTrangThai(userId, trangThai, loaiKeHoach, keyword);
        }
        public static DataTable GetAllAssignmentsForChart()
        {
            try
            {
                return PhanCongDAL.GetAllAssignmentsForChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("BUS Error – GetAllAssignmentsForChart:\n" + ex.Message);
                return new DataTable();
            }
        }

    }
}