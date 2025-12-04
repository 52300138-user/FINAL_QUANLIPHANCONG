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
    public partial class FormWorded : Form
    {
        public FormWorded()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormWorded_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadData();

        }
        private void SetupGrid()
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
                HeaderText = "Công Việc"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VaiTro",
                HeaderText = "Vai Trò"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PhanTram",
                HeaderText = "Tiến Độ (%)"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HanChot",
                HeaderText = "Hạn Chót",
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThaiGV",
                HeaderText = "Trạng Thái"
            });
        }

        private void LoadData()
        {
            try
            {
                int userId = Program.CurrentUserId;
                DataTable dt = PhanCongBUS.GetCongViecByStatus(userId, "HOAN_THANH");

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải công việc đã hoàn thành: " + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbb_HocKy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_TenCongViec_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {

        }
    }
}
