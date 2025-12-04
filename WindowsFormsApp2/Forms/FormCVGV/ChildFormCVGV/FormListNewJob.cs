using Microsoft.VisualBasic;
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

namespace WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV
{
    public partial class FormListNewJob : Form
    {
        private DataTable _sourceData;
        public FormListNewJob()
        {
            InitializeComponent();
            // Gán sự kiện cho Grid và các nút hành động
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả hàng
            dataGridView1.ReadOnly = true;
        }
        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            // Định nghĩa các cột (Sử dụng data từ PhanCongBUS.GetCongViecByStatus)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PC_ID", Name = "colPC_ID", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CongViecID", Name = "colCongViecID", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenCongViec", Name = "colTenCongViec", HeaderText = "Công Việc", FillWeight = 200 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "VaiTro", HeaderText = "Vai Trò", FillWeight = 80 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayGiao", HeaderText = "Ngày Giao", FillWeight = 100, DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HanChot", HeaderText = "Hạn Chót", FillWeight = 100, DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThaiGV", Name = "colTrangThaiGV", HeaderText = "Trạng Thái", FillWeight = 100 });
        }

        private void UpdateActionButtonsVisibility(bool isVisible)
        {
            // button_Them là nút Đồng ý, button1 là nút Từ chối
            button_Them.Visible = isVisible;
            button1.Visible = isVisible;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadData()
        {
            // Tắt sự kiện SelectionChanged để tránh vòng lặp khi LoadData()
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            try
            {
                int userId = Program.CurrentUserId;
                // Load toàn bộ dữ liệu trạng thái MOI vào nguồn (_sourceData)
                // Giả định PhanCongBUS.GetCongViecByStatus chỉ lấy data và không lọc
                _sourceData = PhanCongBUS.GetCongViecByStatus(userId, "MOI");

                ApplyFilter(); // Áp dụng lọc ngay sau khi load thô
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu nguồn công việc: {ex.Message}", "Lỗi Hệ Thống");
                _sourceData = new DataTable();
            }
            finally
            {
                // Bật lại sự kiện
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
                dataGridView1_SelectionChanged(null, null); // Kích hoạt hiển thị nút lần cuối
            }
        }

        private void ApplyFilter()
        {
            if (_sourceData == null) return;

            string loaiKey = cbb_HocKy.SelectedValue as string; // Lấy mã Key loại KH
            string keyword = txt_TenCongViec.Text.Trim().ToLower();

            var rows = _sourceData.AsEnumerable();

            // 1. Lọc theo Loại Kế hoạch (Key)
            if (!string.IsNullOrEmpty(loaiKey))
            {
                // Giả định cột LoaiKeHoach có tồn tại trong _sourceData (từ DAL)
                rows = rows.Where(r => r.Field<string>("Loai") == loaiKey);
            }

            // 2. Lọc theo Tên Công việc (Keyword)
            if (!string.IsNullOrEmpty(keyword))
            {
                rows = rows.Where(r => r.Field<string>("TenCongViec").ToLower().Contains(keyword));
            }

            // Gán dữ liệu đã lọc vào Grid
            dataGridView1.DataSource = rows.Any() ? rows.CopyToDataTable() : null;
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var row = dataGridView1.SelectedRows[0];
            int pcId = Convert.ToInt32(row.Cells["colPC_ID"].Value);
            int congViecId = Convert.ToInt32(row.Cells["colCongViecID"].Value);
            int userId = Program.CurrentUserId;
            string tenCV = row.Cells["colTenCongViec"].Value.ToString();

            var result = MessageBox.Show($"Xác nhận chấp nhận công việc: '{tenCV}'?",
                                          "Đồng ý Công việc",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Đồng ý: Cập nhật TrangThaiGV = DANG_LAM
                    if (PhanCongBUS.AcceptAssignment(pcId, userId, congViecId))
                    {
                        MessageBox.Show("Đã chấp nhận công việc. Công việc chuyển sang 'Đang làm'.", "Thành công");
                        ApplyFilter(); // Reload
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chấp nhận công việc: " + ex.Message, "Lỗi DB");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var row = dataGridView1.SelectedRows[0];
            int pcId = Convert.ToInt32(row.Cells["colPC_ID"].Value);
            int userId = Program.CurrentUserId;
            string tenCV = row.Cells["colTenCongViec"].Value.ToString();

            // === YÊU CẦU NHẬP LÝ DO TỪ CHỐI ===
            string lyDo = Interaction.InputBox(
                $"Nhập Lý do Từ chối công việc '{tenCV}' (Bắt buộc):",
                "Từ chối Công việc",
                "Tôi không thể đảm nhiệm công việc này vì lý do...",
                -1, -1);

            if (!string.IsNullOrWhiteSpace(lyDo))
            {
                try
                {
                    // Từ chối: Cập nhật TrangThaiGV = BI_TRA_LAI
                    if (PhanCongBUS.RejectAssignment(pcId, userId, lyDo))
                    {
                        MessageBox.Show($"Đã từ chối công việc '{tenCV}'. Chuyển sang 'Trả lại'.", "Thành công");
                        ApplyFilter(); // Reload
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi từ chối công việc: " + ex.Message, "Lỗi DB");
                }
            }
            else
            {
                MessageBox.Show("Lý do từ chối không được để trống. Thao tác bị hủy.", "Hủy bỏ");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
            UpdateActionButtonsVisibility(isRowSelected);
        }

        private void FormListNewJob_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadLoaiKeHoachCombo();
            LoadData();     // chỉ load 1 lần
            UpdateActionButtonsVisibility(false);
        }

        private void cbb_HocKy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void LoadLoaiKeHoachCombo()
        {
            var loaiKeHoachList = new[]
            {
                new { Key = (string)null, Value = "Tất cả" },
                new { Key = "HocKyI",  Value = "Kế hoạch học kỳ I" },
                new { Key = "HocKyII", Value = "Kế hoạch học kỳ II" },
                new { Key = "HocKyHe", Value = "Kế hoạch học kỳ Hè" },
                new { Key = "NamHoc",  Value = "Kế hoạch năm học" },
                new { Key = "DeTai",   Value = "Kế hoạch đề tài nghiên cứu" },
                new { Key = "SuKien",  Value = "Kế hoạch sự kiện học thuật" },
                new { Key = "Khac",    Value = "Khác" }
            };

            cbb_HocKy.DataSource = loaiKeHoachList;
            cbb_HocKy.DisplayMember = "Value";
            cbb_HocKy.ValueMember = "Key";
            cbb_HocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_HocKy.SelectedIndex = 0;
        }

        private void txt_TenCongViec_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
