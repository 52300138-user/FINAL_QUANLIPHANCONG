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
    public partial class FormWorking : Form
    {
        private DataTable _sourceData;
        public FormWorking()
        {
            InitializeComponent();
            button_Nop.Visible = false;

        }
        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PC_ID",
                Name = "colPC_ID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CongViecID",
                Name = "colCongViecID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenCongViec",
                Name = "colTenCongViec",
                HeaderText = "Công Việc",
                FillWeight = 200
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VaiTro",
                HeaderText = "Vai Trò",
                FillWeight = 80
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HanChot",
                HeaderText = "Hạn Chót",
                FillWeight = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThaiGV",
                Name = "colTrangThaiGV",
                HeaderText = "Trạng Thái",
                FillWeight = 100
            });
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
        private void FormWorking_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
             LoadLoaiKeHoachCombo();
            LoadData();
            UpdateActionButtonsVisibility(false);
        }

        private void button_Nop_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn công việc muốn nộp báo cáo.", "Lỗi");
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            int pcId = Convert.ToInt32(row.Cells["colPC_ID"].Value);
            int cvId = Convert.ToInt32(row.Cells["colCongViecID"].Value);
            string status = row.Cells["colTrangThaiGV"].Value.ToString();

            if (status != "DANG_LAM")
            {
                MessageBox.Show($"Chỉ có thể nộp báo cáo cho công việc đang ở trạng thái 'Đang làm'. (Trạng thái hiện tại: {status})", "Cảnh báo");
                return;
            }

            // Mở Form NỘP KẾT QUẢ
            // Giả định FormNopKetQua tồn tại và có constructor FormNopKetQua(int pcId)
            using (var dlg = new FormNopFile(pcId, cvId))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Reload Grid sau khi nộp
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Logic ẩn/hiện nút Nộp khi có hàng được chọn và trạng thái là DANG_LAM
            bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
            string status = isRowSelected ? dataGridView1.SelectedRows[0].Cells["colTrangThaiGV"].Value.ToString() : null;

            // Chỉ hiện Nộp nếu là DANG_LAM (Hoặc BI_TRA_LAI nếu muốn GV nộp lại)
            UpdateActionButtonsVisibility(isRowSelected && status == "DANG_LAM");
        }
        private void UpdateActionButtonsVisibility(bool isVisible)
        {
            button_Nop.Visible = isVisible;
            btn_Dong.Visible = true;
        }

        private void cbb_HocKy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            try
            {
                int userId = Program.CurrentUserId;
                // === LỌC CỐT LÕI: LẤY TRẠNG THÁI DANG_LAM ===
                _sourceData = PhanCongBUS.GetCongViecByStatus(userId, "DANG_LAM");

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

            string loaiKey = cbb_HocKy.SelectedValue as string;
            string keyword = txt_TenCongViec.Text.Trim().ToLower();

            var rows = _sourceData.AsEnumerable();

            // 1. Lọc theo Loại Kế hoạch (Key)
            if (!string.IsNullOrEmpty(loaiKey))
            {
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
        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbb_HocKy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txt_TenCongViec_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();

        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            ApplyFilter();

        }
    }
}
