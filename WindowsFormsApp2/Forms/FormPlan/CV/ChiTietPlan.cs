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
using WindowsFormsApp2.Forms.FormPlan.CV.ChildCV;

namespace WindowsFormsApp2.Forms.FormPlan.CV
{
    public partial class ChiTietPlan : Form
    {
        private int currentKeHoachId;
        public ChiTietPlan(int keHoachId)
        {
            InitializeComponent();
            this.currentKeHoachId = keHoachId;
            // Gán sự kiện cho các nút
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            //button_them.Click += button_them_Click;
            //button_sua.Click += button_sua_Click;
            //button_xoa.Click += button_xoa_Click;
        }

        private void ChiTietPlan_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            // Khai báo các cột (Giống FormCongViec cũ)
            // Bro cần đảm bảo các cột này khớp với DB và code cũ
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", Name = "colID", DataPropertyName = "CongViecID", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Công Việc", Name = "colTen", DataPropertyName = "TenCongViec", FillWeight = 300 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Loại", Name = "colLoai", DataPropertyName = "Loai", FillWeight = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", Name = "colTrangThai", DataPropertyName = "TrangThai", FillWeight = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hạn Chót", Name = "colHanChot", DataPropertyName = "HanChot", FillWeight = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Người Giao", Name = "colNguoiGiao", DataPropertyName = "NguoiGiao", FillWeight = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mô Tả", Name = "colMoTa", DataPropertyName = "MoTa", FillWeight = 100 });

            LoadTheme();
            LoadCongViec();
            UpdateTitleLabel();

            // Ẩn/Hiện nút
            button_sua.Visible = false;
            button_xoa.Visible = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // READONLY
            dataGridView1.ReadOnly = true;

            // DISABLE ADD NEW ROW
            dataGridView1.AllowUserToAddRows = false;

            // KHÔNG CHO PHÉP XÓA DÒNG 
            dataGridView1.AllowUserToDeleteRows = false;

            // Tắt tính năng cho phép resize cột/dòng bằng chuột
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
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

        private void LoadCongViec()
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            try
            {
                DataTable dt = CongViecBUS.SelectCongViecByKeHoachID(this.currentKeHoachId);
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Công việc: " + ex.Message, "Lỗi Hệ Thống");
            }
            finally
            {
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            }
        }
        private void UpdateTitleLabel()
        {
            const string baseTitle = "Quản Lý Công Việc Của";
            string planName = "(Lỗi tải/Không tìm thấy)";

            try
            {
                PlanDTO plan = PlansBUS.GetPlanById(this.currentKeHoachId);
                if (plan != null)
                {
                    planName = plan.TenKeHoach;
                }
            }
            catch (Exception)
            {
                // Bỏ qua lỗi và giữ nguyên tên lỗi
            }

            // Gán cho Label (Giả định Label là label5)
            label5.Text = $"{baseTitle} {planName}";
        }

        
      

      

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
            //button_sua.Visible = isRowSelected;
            //button_xoa.Visible = isRowSelected;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_them_Click_1(object sender, EventArgs e)
        {
            try
            {
                // === FIX: TRUYỀN ID KẾ HOẠCH CHO FORM CON ===
                using (var dlg = new FormAddCongViec(this.currentKeHoachId)) // Truyền ID
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadCongViec();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm công việc: " + ex.Message, "Lỗi Dữ Liệu");
            }

        }

        private void button_sua_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0) return;
            try
            {
                int congViecId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);
                if (congViecId == 0) return;

                using (var dlg = new FormEditCongViec(congViecId))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadCongViec();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa công việc: " + ex.Message, "Lỗi Dữ Liệu");
            }
        }

        private void button_xoa_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            try
            {
                int congViecId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);
                string tenCV = dataGridView1.SelectedRows[0].Cells["colTen"].Value.ToString();

                if (congViecId == 0) return;

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa công việc: \n{tenCV}?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    if (CongViecBUS.DeleteCongViec(congViecId))
                    {
                        MessageBox.Show("Xóa công việc thành công!");
                        LoadCongViec();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa công việc: " + ex.Message, "Lỗi Dữ Liệu");
            }

        }
    }
}
