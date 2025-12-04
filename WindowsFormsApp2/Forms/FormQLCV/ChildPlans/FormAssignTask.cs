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
using WindowsFormsApp2.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    public partial class FormAssignTask : Form
    {
        private int currentCongViecId;
        private CongViecDTO currentCV;
        // Dữ liệu nguồn cho ComboBox Giảng viên
        private DataTable dtGiangVien;

        // Biến cờ tạm để ngăn lỗi focus khi ComboBox mở
        private bool commitEdit = false;

        public FormAssignTask(int congViecId) : this()
        {
            this.currentCongViecId = congViecId;
            this.Load += FormAssignTask_Load;

            // Lấy dữ liệu GV ngay lập tức (Giả định tồn tại UsersBUS)
            try
            {
                // Giả định UsersBUS.GetAllGiangVien() trả về DataTable UserID, FullName
                dtGiangVien = PhanCongBUS.GetGiangVienList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Giảng viên: " + ex.Message);
                dtGiangVien = new DataTable();
            }

        }

        public FormAssignTask()
        {
            InitializeComponent();
        }

        // --- HÀM HỖ TRỢ HIỂN THỊ PROFILE ---
        private void HandleShowProfile(int rowIndex)
        {
            try
            {
                if (rowIndex < 0) return;

                var row = dataGridView1.Rows[rowIndex];
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


        private void HandleDeleteAssignment(int rowIndex)
        {
            int pc_ID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["colPC_ID"].Value);

            var gvCell = dataGridView1.Rows[rowIndex].Cells["colGiangVienSelect"].FormattedValue;
            string tenGV = gvCell?.ToString() ?? "(Không rõ)";

            if (MessageBox.Show($"Xác nhận hủy phân công:\n{tenGV} ?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            if (PhanCongBUS.DeletePhanCong(pc_ID))
                LoadDanhSachDaPhanCong();
        }


        private void LoadAssignmentDetail()
        {
            try
            {
                // A. Lấy DTO của Công việc
                this.currentCV = CongViecBUS.GetCongViecById(this.currentCongViecId);

                if (this.currentCV == null)
                {
                    txt_TenCV.Text = "(CV bị xóa)";
                    txt_LoaiKH.Text = "Lỗi";
                    return;
                }

                // B. Fill ReadOnly Task Details (Panel 2)
                txt_TenCV.Text = this.currentCV.TenCongViec;
                txt_LoaiKH.Text = this.currentCV.Loai;
                dtp_HanChot.Value = this.currentCV.HanChot ?? DateTime.Today.AddDays(30);

                // Người giao (TBM)
                UserDTO nguoiGiao = UsersBUS.GetUserById(Program.CurrentUserId);
                txt_NguoiGiao.Text = nguoiGiao?.FullName ?? "(Lỗi tải User)";

                // Khóa các trường View Only
                txt_TenCV.ReadOnly = true;
                txt_LoaiKH.ReadOnly = true;
                txt_NguoiGiao.ReadOnly = true;
                dtp_HanChot.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết CV: " + ex.Message, "System Crash", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void FormAssignTask_Load(object sender, EventArgs e)
        {
            try
            {
                dtGiangVien = PhanCongBUS.GetGiangVienList();

                SetupDataGridView();
                LoadDanhSachDaPhanCong();
                LoadAssignmentDetail();
                LockProfilePanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải giao diện phân công:\n" + ex.Message,
                                "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 1️ PC_ID (ẩn)
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PC_ID",
                    Name = "colPC_ID",
                    Visible = false
                });

                // 2 UserID (ẩn)
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UserID",
                    Name = "colUserID",
                    Visible = false
                });

                // ⚠ 3 CHỌN GIẢNG VIÊN — ComboBox
                DataGridViewComboBoxColumn colGV = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Giảng viên",
                    Name = "colGiangVienSelect",
                    DataPropertyName = "UserID",
                    DataSource = dtGiangVien,
                    DisplayMember = "FullName",
                    ValueMember = "UserID",
                    Width = 180
                };
                dataGridView1.Columns.Add(colGV);

                // 4 Vai trò
                dataGridView1.Columns.Add(new DataGridViewComboBoxColumn
                {
                    HeaderText = "Nhiệm vụ",
                    Name = "colVaiTro",
                    DataPropertyName = "VaiTro",
                    DataSource = new[] { "ChuTri", "HoTro" },
                    Width = 80
                });

                // 5 Ngày bắt đầu
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Ngày bắt đầu",
                    DataPropertyName = "NgayGiao",
                    Name = "colNgayGiao",
                    ReadOnly = true
                });

                // 6 Ngày kết thúc
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Ngày kết thúc",
                    DataPropertyName = "HanChot",
                    Name = "colHanChot",
                    ReadOnly = true
                });

                // 7 Trạng thái
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThaiGV",
                    Name = "colTrangThaiGV",
                    ReadOnly = true
                });

                // 8 Xóa
                dataGridView1.Columns.Add(new DataGridViewButtonColumn
                {
                    HeaderText = "Xóa",
                    Name = "colXoa",
                    Text = "Xóa",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                });

                // 9 Gửi email
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    HeaderText = "Gửi Mail",
                    Name = "colSendMail",
                    Width = 60
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SetupDataGridView:\n" + ex.Message);
            }
        }

        private void LoadDanhSachDaPhanCong()
        {
            try
            {
                DataTable dt = PhanCongBUS.GetPhanCongByCongViecID(this.currentCongViecId);
                dataGridView1.DataSource = dt;

                // Ẩn cột ID nội bộ lại sau khi bind dữ liệu
                if (dataGridView1.Columns["colPC_ID"] != null)
                    dataGridView1.Columns["colPC_ID"].Visible = false;

                if (dataGridView1.Columns["colUserID"] != null)
                    dataGridView1.Columns["colUserID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phân công: " + ex.Message);
            }
        }
        private void LockProfilePanel()
        {
            txt_HoTenGV.ReadOnly = true;
            txt_EmailGV.ReadOnly = true;
            txt_SDT.ReadOnly = true;
            guna2TextBox6.ReadOnly = true;
            txt_NhiemVu.Enabled = true;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button_Them_Click(object sender, EventArgs e)
        {
            // Nút Thêm chỉ cần kích hoạt việc thêm dòng mới
            if (dataGridView1.AllowUserToAddRows)
            {
                int newRowIndex = dataGridView1.Rows.Count - 1;
                if (newRowIndex >= 0)
                {
                    // Nếu dòng cuối cùng không trống (đã được commit), thêm dòng mới
                    if (dataGridView1.Rows[newRowIndex].IsNewRow)
                    {
                        // Focus vào ô ComboBox Giảng viên của dòng mới để kích hoạt nhập liệu
                        dataGridView1.CurrentCell = dataGridView1.Rows[newRowIndex].Cells["colGiangVienSelect"];
                        dataGridView1.BeginEdit(true);
                    }
                    else
                    {
                        // Nếu dòng mới đã có data, ta cần một cách để thêm dòng hoàn toàn mới.
                        // Thường chỉ cần di chuyển tới dòng mới đã được tạo sẵn khi AllowUserToAddRows = true
                    }
                }
            }
        }
        

        private void button_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Nếu click vào nút XÓA
            if (e.ColumnIndex == dataGridView1.Columns["colXoa"].Index)
            {
                HandleDeleteAssignment(e.RowIndex);
                return;
            }

            // 2. Nếu click vào bất kỳ ô nào khác (SHOW PROFILE)
            HandleShowProfile(e.RowIndex);
        }

        private void txt_TenCV_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                string col = dataGridView1.Columns[e.ColumnIndex].Name;
                if (col != "colGiangVienSelect" && col != "colVaiTro") return;

                var row = dataGridView1.Rows[e.RowIndex];
                if (row.IsNewRow) return;

                if (!(row.Cells["colGiangVienSelect"].Value is int userId) || userId <= 0) return;

                string vaiTro = row.Cells["colVaiTro"].Value?.ToString();
                if (string.IsNullOrEmpty(vaiTro)) return;

                int pc_ID = row.Cells["colPC_ID"].Value == DBNull.Value ? 0 :
                            Convert.ToInt32(row.Cells["colPC_ID"].Value);

                var dto = new PhanCongDTO
                {
                    PC_ID = pc_ID,
                    CongViecID = currentCongViecId,
                    UserID = userId,
                    VaiTro = vaiTro,
                    NgayGiao = DateTime.Today.Date,
                    TrangThaiGV = "MOI"
                };

                // tránh loop sự kiện
                dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;

                bool success = pc_ID == 0
                    ? PhanCongBUS.AddPhanCong(dto)
                    : PhanCongBUS.UpdatePhanCong(dto);

                if (!success)
                {
                    MessageBox.Show("Không thể cập nhật phân công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadDanhSachDaPhanCong(); // rollback UI
                }
                else
                {
                    LoadDanhSachDaPhanCong();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phân công:\n" + ex.Message,
                                "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDanhSachDaPhanCong(); // rollback đảm bảo UI đúng dữ liệu DB
            }
            finally
            {
                // đảm bảo luôn gắn lại event
                dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            }
        }

        private void button_SendMail_Click(object sender, EventArgs e)
        {
            List<UserDTO> selectedUsers = new List<UserDTO>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy giá trị checkbox
                var chk = row.Cells["colSendMail"].Value;
                bool isChecked = chk != null && chk != DBNull.Value && Convert.ToBoolean(chk);

                if (!isChecked) continue;

                // Lấy userID từ colUserID
                var cellUser = row.Cells["colUserID"].Value;
                if (cellUser == null || cellUser == DBNull.Value) continue;

                int userId = Convert.ToInt32(cellUser);
                var user = UsersBUS.GetUserById(userId);

                if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                {
                    selectedUsers.Add(user);
                }
            }

            if (selectedUsers.Count == 0)
            {
                MessageBox.Show("Không có giảng viên nào được chọn để gửi email.");
                return;
            }

            // Gửi email từng người
            foreach (var giangVien in selectedUsers)
            {
                SendEmailToUser(giangVien.Email, giangVien.FullName);
            }

            MessageBox.Show("Gửi email thành công!");
        }

        private void SendEmailToUser(string email, string fullName)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(email);
                mail.From = new System.Net.Mail.MailAddress("kocoemail367@gmail.com");

                mail.Subject = "[EMTs]-[Thông báo phân công công việc]";
                mail.Body = $"Chào {fullName},\n\n" +
                            $"Bạn vừa được giao một công việc mới. " +
                            $"Vui lòng đăng nhập hệ thống để xem chi tiết.\n\n" +
                            $"Trân trọng.";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("kocoemail367@gmail.com", "uzan ioyf eskt ukoy");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi mail: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
