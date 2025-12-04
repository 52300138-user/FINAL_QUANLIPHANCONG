using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;
using System.IO;

namespace WindowsFormsApp2.Forms.FormBangDiem.ChildBangDiem
{
    public partial class FormDuyetBD : Form
    {
        private int _fileId;
        private int _pcId;
        private string _filePath;
        public FormDuyetBD(int fileId)
        {
            InitializeComponent();
            _fileId = fileId;
        }



        private void close_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void txt_TenCongViec_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbb_GiangVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtp_NgayUpLoad_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_GhiChuTBM_TextChanged(object sender, EventArgs e)
        {

        }

        private void Duyet_Click(object sender, EventArgs e)
        {
            if (!ValidateComment()) return;

            if (MessageBox.Show("Duyệt file này và hoàn thành công việc?",
                                "Xác nhận", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FileNopBUS.ApproveFile(_fileId, txt_GhiChuTBM.Text);
                PhanCongBUS.UpdateTrangThaiAfterApprove(_pcId);

                MessageBox.Show("Đã duyệt và hoàn thành công việc.");
                this.DialogResult = DialogResult.OK;
                Close();
            }   
        }

        private void Tra_Click(object sender, EventArgs e)
        {
            if (!ValidateComment()) return;

            if (MessageBox.Show("Trả về để giảng viên sửa lại?",
                                "Xác nhận", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                FileNopBUS.RejectFile(_fileId, txt_GhiChuTBM.Text);
                PhanCongBUS.UpdateTrangThaiAfterReject(_pcId);

                MessageBox.Show("Đã trả về cho giảng viên.");
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void FormDuyetBD_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataTable dt = FileNopBUS.GetFileById(_fileId);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu file!", "Lỗi");
                this.Close();
                return;
            }

            var row = dt.Rows[0];

            _pcId = Convert.ToInt32(row["PC_ID"]);
            _filePath = row["FilePath"].ToString();

            txt_TenCongViec.Text = row["TenCongViec"].ToString();
            cbb_GiangVien.Text = row["TenGV"].ToString();
            dtp_NgayUpLoad.Value = Convert.ToDateTime(row["CreatedAt"]);
            txt_FileName.Text = row["FileName"].ToString();
            txt_GhiChuTBM.Text = ""; // TBM nhập vào
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Không tìm thấy file, Liên hệ lại với giảng viên!", "Lỗi");
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = _filePath,
                    UseShellExecute = true // mở
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở file: " + ex.Message);
            }
        }


        private bool ValidateComment()
        {
            if (string.IsNullOrWhiteSpace(txt_GhiChuTBM.Text))
            {
                MessageBox.Show("Phải nhập ý kiến phản hồi trước!", "Thiếu dữ liệu");
                return false;
            }
            return true;
        }
    }
}
