using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    public partial class FormDetailPlan : Form
    {
        private readonly int _congViecId;
        private CongViecDTO _currentCV;
        private PlanDTO _currentPlan;
        private DataTable _dtChiTiet;

        public FormDetailPlan(int congViecId)
        {
            InitializeComponent();
            _congViecId = congViecId;
        }


        private void FormDetailPlan_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                SetupGrid();
                BindDataToControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết công việc:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void LoadData()
        {
            // Lấy công việc
            _currentCV = CongViecBUS.GetCongViecById(_congViecId);
            if (_currentCV == null)
            {
                throw new Exception("Không tìm thấy công việc.");
            }

            // Lấy kế hoạch (nếu có)
            if (_currentCV.KeHoachID.HasValue)
            {
                _currentPlan = PlansBUS.GetPlanById(_currentCV.KeHoachID.Value);
            }

            // Lấy danh sách phân công chi tiết
            _dtChiTiet = PhanCongBUS.GetChiTietPhanCongByCongViec(_congViecId);
            if (_dtChiTiet == null)
                _dtChiTiet = new DataTable();
        }

        private void SetupGrid()
        {
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // STT (không bind, sẽ fill ở DataBindingComplete)
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUserID",
                DataPropertyName = "UserID",
                Visible = false
            });
                
            var colStt = new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "colSTT",
                Width = 50
            };
            dgvChiTiet.Columns.Add(colStt);

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Họ và tên",
                Name = "colHoTen",
                DataPropertyName = "HoTenGV",
                Width = 150
            });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Chức vụ",
                Name = "colChucVu",
                DataPropertyName = "ChucVu",
                Width = 120
            });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nhiệm vụ",
                Name = "colNhiemVu",
                DataPropertyName = "NhiemVu",
                Width = 120
            });

            var colNgayBD = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày bắt đầu",
                Name = "colNgayBatDau",
                DataPropertyName = "NgayBatDau",
                Width = 100
            };
            colNgayBD.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvChiTiet.Columns.Add(colNgayBD);

            var colNgayKT = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày kết thúc",
                Name = "colNgayKetThuc",
                DataPropertyName = "NgayKetThuc",
                Width = 100
            };
            colNgayKT.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvChiTiet.Columns.Add(colNgayKT);

            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng thái",
                Name = "colTrangThai",
                DataPropertyName = "TrangThai",
                Width = 100
            });

            var colThoiGianNop = new DataGridViewTextBoxColumn
            {
                HeaderText = "Thời gian nộp",
                Name = "colThoiGianNop",
                DataPropertyName = "ThoiGianNop",
                Width = 130
            };
            colThoiGianNop.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvChiTiet.Columns.Add(colThoiGianNop);

            dgvChiTiet.DataSource = _dtChiTiet;
            dgvChiTiet.DataBindingComplete += DgvChiTiet_DataBindingComplete;
        }

        private void DgvChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Fill STT
            for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
            {
                dgvChiTiet.Rows[i].Cells["colSTT"].Value = (i + 1).ToString();
            }
        }

        private void BindDataToControls()
        {
            // Loại kế hoạch: tạm hiển thị tên kế hoạch (nếu có), nếu không để trống
            if (_currentPlan != null)
                txt_LoaiKeHoach.Text = _currentPlan.TenKeHoach;
            else
                txt_LoaiKeHoach.Text = "";

            txt_TenCV.Text = _currentCV.TenCongViec ?? "";

            // Người giao
            var nguoiGiao = UsersBUS.GetUserById(_currentCV.NguoiGiaoID);
            txt_NguoiGiao.Text = nguoiGiao?.FullName ?? "";

            // Ngày giao: lấy từ PhanCong (ngày giao đầu tiên) hoặc CreatedAt
            DateTime? ngayGiao = PhanCongBUS.GetNgayGiaoDauTien(_congViecId);
            if (ngayGiao.HasValue)
                dtp_NgayGiao.Value = ngayGiao.Value;
            else
                dtp_NgayGiao.Value = _currentCV.CreatedAt;

            // Ngày nộp
            if (_currentCV.HanChot.HasValue)
                dtp_NgayNop.Value = _currentCV.HanChot.Value;
            else
                dtp_NgayNop.Value = DateTime.Today;

            // Mức ưu tiên (text Việt)
            txt_MucUuTien.Text = ConvertPriorityToText(_currentCV.MucUuTien);

            // Ghi chú
            txt_GhiChu.Text = _currentCV.MoTa ?? "";
        }

        private string ConvertPriorityToText(string muc)
        {
            switch (muc)
            {
                case "LOW": return "Thấp";
                case "MED": return "Trung bình";
                case "HIGH": return "Cao";
                default: return "";
            }
        }

        private void btn_XuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dtChiTiet == null || _dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel file (*.xlsx)|*.xlsx";
                    sfd.FileName = $"ChiTietCongViec_{_currentCV.CongViecID}_{_currentCV.TenCongViec}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportHelper.ExportCongViecDetailToExcel(_currentCV, _dtChiTiet, sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_XuatPdf_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("Đang Bảo Trì");
            try
            {
                if (_dtChiTiet == null || _dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF file (*.pdf)|*.pdf";
                    sfd.FileName = $"ChiTietCongViec_{_currentCV.CongViecID}_{_currentCV.TenCongViec}.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportHelper.ExportCongViecDetailToPdf(_currentCV, _dtChiTiet, sfd.FileName);
                        MessageBox.Show("Xuất PDF thành công.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HandleShowProfile(int rowIndex)
        {
            try
            {
                if (rowIndex < 0) return;

                var row = dgvChiTiet.Rows[rowIndex];
                if (row.IsNewRow) return;

                var cell = row.Cells["colUserID"].Value;
                if (cell == null || cell == DBNull.Value) return;

                int userId = Convert.ToInt32(cell);
                var user = UsersBUS.GetUserById(userId);
                if (user == null) return;

                txt_HoTenGV.Text = user.FullName ?? "";
                txt_EmailGV.Text = user.Email ?? "";
                txt_SDT.Text = user.SDT ?? "";
                txt_GioiTinh.Text = user.Gender ?? "";
                txt_NhiemVu.Text = user.Role ?? "";
                is_BoMon.Text = user.IsLocked ? "Khóa" : "Hoạt động";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin GV:\n" + ex.Message,
                                 "Lỗi UI",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }

        private void dgvChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            HandleShowProfile(e.RowIndex);
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_SDT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
