using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;

namespace WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV
{
    public partial class FormGiveBack : Form
    {
        public FormGiveBack()
        {
            InitializeComponent();
        }

        private void FormGiveBack_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
            UpdateButtons(false);   
        }
        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
                DataPropertyName = "TenGV",
                Name = "colTenGV",
                HeaderText = "Người Trả",
                FillWeight = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LyDo",
                Name = "colLyDo",
                HeaderText = "Lý do trả lại",
                FillWeight = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayGiao",
                Name = "colNgayGiao",
                HeaderText = "Ngày giao",
                DefaultCellStyle = { Format = "dd/MM/yyyy" },
                FillWeight = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VaiTro",
                Name = "colVaiTro",
                HeaderText = "Vai trò",
                FillWeight = 80
            });
        }


        private void LoadData()
        {
            dataGridView1.DataSource = PhanCongBUS.GetCongViecTuChoi();
        }

        private void UpdateButtons(bool enabled)
        {
            btn_DongY.Enabled = enabled;
            btn_KhongDongY.Enabled = enabled;
        }
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var row = dataGridView1.SelectedRows[0];

            int pcId = Convert.ToInt32(row.Cells["colPC_ID"].Value);
            string tenCV = row.Cells["colTenCongViec"].Value.ToString();

            if (MessageBox.Show(
                    $"Xác nhận CHO PHÉP trả lại công việc:\n\n{tenCV}?\n(Phân công sẽ bị xóa)",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PhanCongBUS.ApproveReject(pcId);
                LoadData();
                UpdateButtons(false);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool selected = dataGridView1.SelectedRows.Count > 0;
            UpdateButtons(selected);
        }

        private void btn_KhongDongY_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var row = dataGridView1.SelectedRows[0];

            int pcId = Convert.ToInt32(row.Cells["colPC_ID"].Value);
            string tenCV = row.Cells["colTenCongViec"].Value.ToString();

            if (MessageBox.Show(
                    $"KHÔNG đồng ý cho trả lại công việc:\n\n{tenCV}?\n(Trạng thái sẽ quay về MỚI, GV phải vào nhận lại)",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PhanCongBUS.DisapproveReject(pcId);
                LoadData();
                UpdateButtons(false);
            }

        }
    }
}
