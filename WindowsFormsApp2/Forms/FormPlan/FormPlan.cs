using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.Forms.FormPlan.ChildPlan;
using WindowsFormsApp2.Forms.FormPlan.CV;

namespace WindowsFormsApp2.Forms.FormPlan
{
    public partial class FormPlan : Form
    {
        public FormPlan()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
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
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                    label5.ForeColor = ThemeColor.SecondaryColor;
                }
            }
        }

        // ====== DATA ======
        private void LoadPlans()
        {
            try
            {
                DataTable dt = PlansBUS.GetPlans();

                if (!dt.Columns.Contains("STT"))
                    dt.Columns.Add("STT", typeof(int));

                for (int i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i]["STT"] = i + 1;

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải kế hoạch: " + ex.Message);
            }
        }


        private void FormReporting_Load(object sender, EventArgs e)
        {
            // DataGridView config
            dataGridView1.AutoGenerateColumns = false; // không tự động tạo cột
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // chọn cả hàng
            dataGridView1.MultiSelect = false; // không cho chọn nhiều hàng
            dataGridView1.ReadOnly = true; // chỉ đọc
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //Tự động co dãn
            dataGridView1.AllowUserToAddRows = false; // Ẩn hàng trống để thêm mới ở dưới cùng
            dataGridView1.AutoGenerateColumns = false;
            // Clean start
            dataGridView1.Columns.Clear();

            // (Ẩn) KeHoachID để thao tác nội bộ
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "KeHoachID",
                Name = "colKeHoachID",
                DataPropertyName = "KeHoachID",
                Visible = false,
                ReadOnly = true
            });

            // STT (hiển thị)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "colSTT",
                DataPropertyName = "STT",  // sẽ thêm vào DataTable
                ReadOnly = true,
                Width = 60
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên kế hoạch",
                Name = "colTenKeHoach",
                DataPropertyName = "TenKeHoach",
                ReadOnly = true
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Loại (Học kỳ/Năm học/…)",
                Name = "colLoai",
                DataPropertyName = "Loai",
                ReadOnly = true
            });

            var colNgayBD = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày bắt đầu",
                Name = "colNgayBatDau",
                DataPropertyName = "NgayBatDau",
                ReadOnly = true
            };
            colNgayBD.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colNgayBD);

            var colNgayKT = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày kết thúc",
                Name = "colNgayKetThuc",
                DataPropertyName = "NgayKetThuc",
                ReadOnly = true
            };
            colNgayKT.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colNgayKT);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Người tạo",
                Name = "colNguoiTao",
                DataPropertyName = "NguoiTao",   // FullName
                ReadOnly = true
            });

            var colNgayTao = new DataGridViewTextBoxColumn
            {
                HeaderText = "Ngày tạo",
                Name = "colNgayTao",
                DataPropertyName = "NgayTao",
                ReadOnly = true
            };
            colNgayTao.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colNgayTao);

            LoadTheme();
            LoadPlans();
            button_sua.Visible = false;
            button_xoa.Visible = false;
            btn_ChiTiet.Visible = false;
        }

        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
            button_sua.Visible = isRowSelected;
            button_xoa.Visible = isRowSelected;
            btn_ChiTiet.Visible = isRowSelected;
        }

       

       

       

        private void button_them_Click_1(object sender, EventArgs e)
        {
            using (var dlg = new ChildPlan.FormAddPlan())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Sau khi thêm thành công → refresh
                    LoadPlans();
                }
            }
        }

        private void button_sua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            try
            {
                int keHoachId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colKeHoachID"].Value);
                if (keHoachId == 0) return;

                // Mở form Sửa (FormEditPlan) và truyền ID
                using (var dlg = new ChildPlan.FormEditPlan(keHoachId))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        // Nếu Form Sửa báo OK (đã lưu)
                        LoadPlans(); // Refresh lại grid
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ID kế hoạch: " + ex.Message);
            }
        }

        private void button_xoa_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            try
            {
                int keHoachId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colKeHoachID"].Value);
                string tenKeHoach = dataGridView1.SelectedRows[0].Cells["colTenKeHoach"].Value.ToString();

                if (keHoachId == 0) return;

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa kế hoạch: \n{tenKeHoach}?",
                                              "Xác nhận xóa",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    // Gọi BUS (BUS sẽ tự check logic và show lỗi nếu có)
                    if (PlansBUS.DeletePlan(keHoachId))
                    {
                        MessageBox.Show("Xóa kế hoạch thành công!");
                        LoadPlans(); // Refresh lại grid
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa kế hoạch: " + ex.Message);
            }
        }

        private void btn_ChiTiet_Click_1(object sender, EventArgs e)
        {
            try
            {
                int keHoachId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colKeHoachID"].Value);
                if (keHoachId == 0) return;

                // Mở form Cong Viec
                using (var dlg = new CV.ChiTietPlan(keHoachId))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadPlans();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ID kế hoạch: " + ex.Message);
            }
        }
    }
}
