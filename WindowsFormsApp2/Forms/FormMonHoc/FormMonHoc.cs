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
using WindowsFormsApp2.Forms.FormMonHoc.ChildMonHoc;

namespace WindowsFormsApp2.Forms.FormMonHoc
{
    public partial class FormMonHoc : Form
    {
        public FormMonHoc()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.Load += FormProduct_Load;
            btn_Tim.Click += btn_Tim_Click;
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.ForeColor = Color.White;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.FlatAppearance.BorderColor=ThemeColor.SecondaryColor;
                    label5.ForeColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void LoadMonHoc(string keyword = null, string hocKy = null)
        {
            try
            {
                DataTable dt;
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(hocKy))
                {
                    dt = MonHocBUS.GetMonHocTable();
                }
                else
                {
                    dt = MonHocBUS.SearchMonHoc(keyword, hocKy);
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Môn học: " + ex.Message, "Lỗi Hệ Thống");
            }
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.ReadOnly = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // --- Định nghĩa các cột ---
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ID",
                    Name = "colID",
                    DataPropertyName = "MonHocID",
                    Visible = false
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Học Kỳ",
                    Name = "colHocKy",
                    DataPropertyName = "HocKy",
                    FillWeight = 250
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Mã Môn Học",
                    Name = "colMaMonHoc",
                    DataPropertyName = "MaMonHoc",
                    FillWeight = 100
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tên Môn Học",
                    Name = "colTenMonHoc",
                    DataPropertyName = "TenMonHoc",
                    FillWeight = 250
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tên Nhóm",
                    Name = "colTenNhom",
                    DataPropertyName = "TenNhom",
                    FillWeight = 250
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tên Tổ",
                    Name = "colTenTo",
                    DataPropertyName = "TenTo",
                    FillWeight = 250
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Số TC",
                    Name = "colTinChi",
                    DataPropertyName = "SoTinChi",
                    FillWeight = 70
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tiết LT",
                    Name = "colLT",
                    DataPropertyName = "SoTiet_LT",
                    FillWeight = 70
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tiết TH",
                    Name = "colTH",
                    DataPropertyName = "SoTiet_TH",
                    FillWeight = 70
                });

                LoadTheme();
                LoadHocKyComboBox();
                LoadMonHoc();
                button_sua.Visible = false;
                button_xoa.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo Form Môn Học: " + ex.Message, "Lỗi UI");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
                button_sua.Visible = isRowSelected;
                button_xoa.Visible = isRowSelected;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sự kiện chọn hàng: " + ex.Message, "Lỗi UI"); }
        }
       
        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            try
            {
                int monHocId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);
                string tenMH = dataGridView1.SelectedRows[0].Cells["colTenMonHoc"].Value.ToString();

                if (monHocId == 0) return;

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa môn học: \n{tenMH}?",
                                              "Xác nhận xóa",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    if (MonHocBUS.DeleteMonHoc(monHocId))
                    {
                        MessageBox.Show("Xóa môn học thành công!");
                        LoadMonHoc(); // Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa môn học: " + ex.Message);
            }
        }

        private void button_them_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new FormAddMonHoc())
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadMonHoc();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi mở Form Thêm: " + ex.Message, "Lỗi Ứng Dụng"); }
        }

        private void button_sua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            try
            {
                int monHocId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);
                if (monHocId == 0) return;

                using (var dlg = new FormEditMonHoc(monHocId))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadMonHoc();
                    }
                }
            }
            catch (Exception ex)
            {
                // Thường là lỗi Convert.ToInt32 khi DGV rỗng hoặc lỗi lấy ID
                MessageBox.Show("Lỗi khi sửa môn học: " + ex.Message, "Lỗi Dữ Liệu");
            }

        }

        private void button_xuatfile_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy toàn bộ dữ liệu Môn học từ BUS
                DataTable dt = MonHocBUS.GetMonHocTable();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu môn học để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Mở hộp thoại Save File
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = $"DanhSachMonHoc_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        ExportHelper.ExportDataTableToExcel(dt, filePath, "DanhSachMonHoc");

                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file Excel: " + ex.Message, "Lỗi Hệ Thống");
            }
        }

        private void cbb_HocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Tim_Click(sender, e);
        }

        private void txt_TenMonHoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string keyword = txt_TenMonHoc.Text.Trim();

            // 1. Lấy MÃ KỸ THUẬT (KEY) trực tiếp từ SelectedValue
            string hocKyKey = cbb_HocKy.SelectedValue as string;

            // Nếu SelectedValue là null (chọn "Tất cả"), hocKyKey sẽ là null.
            // Nếu là string rỗng (""), ta cũng coi là null.
            if (string.IsNullOrWhiteSpace(hocKyKey))
            {
                hocKyKey = null;
            }

            // 2. Kiểm tra điều kiện Reset/Tải toàn bộ
            if (string.IsNullOrWhiteSpace(keyword) && hocKyKey == null)
            {
                LoadMonHoc(); // LoadMonHoc() không tham số
            }
            else
            {
                LoadMonHoc(keyword, hocKyKey); // Truyền Mã Key (hoặc null)
            }
        }
        private void LoadHocKyComboBox()
        {
            // Danh sách Key-Value chuẩn
            var hocKyList = new List<object>
                {
                // Thêm mục "Tất cả" với Key là null
                new { Key = (string)null, Value = "Tất cả" }, 
                new { Key = "HocKyI", Value = "Học Kỳ I" },
                new { Key = "HocKyII", Value = "Học Kỳ II" },
                new { Key = "HocKyHe", Value = "Học Kỳ Hè" }
                };

        cbb_HocKy.DataSource = hocKyList;
        cbb_HocKy.DisplayMember = "Value";
        cbb_HocKy.ValueMember = "Key"; // Sẽ trả về null nếu chọn "Tất cả"
        cbb_HocKy.DropDownStyle = ComboBoxStyle.DropDownList;
        cbb_HocKy.SelectedIndex = 0; // Chọn "Tất cả"
        }
    }
}
