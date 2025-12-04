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
using WindowsFormsApp2.Forms.ChildCustomer;
using WindowsFormsApp2.Forms.FormBangDiem.ChildBangDiem;

using System.IO;

namespace WindowsFormsApp2.Forms.FormBangDiem
{
    public partial class FormBangDiem : Form
    {
        public FormBangDiem()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                }
            }
        }

        private void FormBangDiem_Load(object sender, EventArgs e)
        {

            LoadTheme(); // Giữ LoadTheme ở đây 
            // Cấu hình Grid
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;

            SetupGrid();
            LoadGiangVienFilter();
            LoadData();

            // NGĂN FORM TỰ CO GIÃN THEO CONTROL BÊN TRONG
            //this.AutoSize = false;
            //this.AutoSizeMode = AutoSizeMode.GrowOnly; // không co lại vì control nhỏ

            //// Nếu là form con trong dashboard thì thường để:
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.MaximizeBox = false;
        }
    
        
        private void SetupGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FileID", Name = "colFileID", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PC_ID", Name = "colPC_ID", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenCongViec",
                Name = "colTenCongViec",
                HeaderText = "Công Việc",
                FillWeight = 200
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenGV", Name = "ColTenGiangVien", HeaderText = "Người Gửi" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FileName", HeaderText = "Tên File"});
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreatedAt", HeaderText = "Ngày tải lên", DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" } });
        }

        private void LoadGiangVienFilter()
        {
            var dt = UsersBUS.GetGiangVienList();

            cbb_GiangVien.DataSource = dt;
            cbb_GiangVien.DisplayMember = "FullName";
            cbb_GiangVien.ValueMember = "UserID";
            cbb_GiangVien.SelectedIndex = -1;
        }

        private void LoadData()
        {
            int? userId = null;
            if (cbb_GiangVien.SelectedValue != null && cbb_GiangVien.SelectedValue is int)
            {
                userId = (int)cbb_GiangVien.SelectedValue;
            }

            DateTime? ngay = chkLocNgay.Checked ? dtp_NgayUpLoad.Value.Date : (DateTime?)null;

            dataGridView1.DataSource = FileNopBUS.GetFileChoDuyet(userId, ngay);
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            // Tắt sự kiện SelectionChanged để tránh vòng lặp
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            LoadData();

            // Bật lại sự kiện
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            // Sau khi load data, ta cần cập nhật textbox chi tiết
            dataGridView1_SelectionChanged(null, null);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (!dataGridView1.Columns.Contains("colTenCongViec")) return;

            var tenCV = dataGridView1.Rows[e.RowIndex].Cells["colTenCongViec"].Value;
            var tenGV = dataGridView1.Rows[e.RowIndex].Cells["ColTenGiangVien"].Value;

            txt_TenCongViec.Text = tenCV?.ToString() ?? "";

            cbb_GiangVien.Text = tenGV?.ToString() ?? "";
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 file để xem.", "Thông báo");
                return;
            }

            int fileId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colFileID"].Value);

            FormDuyetBD frm = new FormDuyetBD(fileId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // reload lại list sau khi duyệt
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Filter_Changed(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Filter_Changed(sender, e);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                txt_TenCongViec.Text = "";
                return;
            }

            // Lấy thông tin từ hàng được chọn
            var row = dataGridView1.SelectedRows[0];

            var tenCV = row.Cells["colTenCongViec"].Value;
            var tenGV = row.Cells["ColTenGiangVien"].Value;

            txt_TenCongViec.Text = tenCV?.ToString() ?? "";

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 file để tải về.", "Thông báo");
                return;
            }

            try
            {
                int fileId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colFileID"].Value);

                // Lấy thông tin file từ DB
                var dt = FileNopBUS.GetFileById(fileId);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin file trong hệ thống!", "Lỗi");
                    return;
                }

                DataRow row = dt.Rows[0];

                string sourcePath = row["FilePath"].ToString();
                string fileName = row["FileName"].ToString();

                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show("File nguồn không tồn tại! (Có thể do chỉ lưu local)", "Lỗi");
                    return;
                }

                // Hộp thoại chọn nơi lưu
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = fileName;
                    sfd.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string destPath = sfd.FileName;

                        File.Copy(sourcePath, destPath, true);

                        MessageBox.Show("Tải file thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải file: " + ex.Message, "Lỗi");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);

        //    if (this.Parent != null)
        //    {
        //        this.Dock = DockStyle.Fill;
        //        this.Size = this.Parent.ClientSize;
        //    }
        //}

    }
}

