using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;

namespace WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV
{
    public partial class FormNopFile : Form
    {
        private readonly int _pcId;
        private readonly int _cvId;
        private string _selectedFilePath = null;

        public FormNopFile(int pcId, int cvId)
        {
            InitializeComponent();
            _pcId = pcId;
            _cvId = cvId;
        }

        private void FormNopFile_Load(object sender, EventArgs e)
        {

        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "PDF files (*.pdf)|*.pdf";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = ofd.FileName;
                    lbl_File.Text = Path.GetFileName(_selectedFilePath);
                }
            }
        }

        private void btnNopKetQua_Click(object sender, EventArgs e)
        {
            if (_selectedFilePath == null)
            {
                MessageBox.Show("Vui lòng chọn file trước!", "Thiếu dữ liệu");
                return;
            }

            try
            {
                // ===== 1. KIỂM TRA ĐỊNH DẠNG PDF =====
                if (Path.GetExtension(_selectedFilePath).ToLower() != ".pdf")
                {
                    MessageBox.Show("Chỉ chấp nhận file PDF!",
                                    "Sai định dạng",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                // ===== 2. KIỂM TRA DUNG LƯỢNG 100MB =====
                const long MAX_FILE_SIZE = 100 * 1024 * 1024; // 100 MB
                FileInfo fi = new FileInfo(_selectedFilePath);

                if (fi.Length > MAX_FILE_SIZE)
                {
                    MessageBox.Show("File vượt quá dung lượng tối đa 100MB!",
                                    "Dung lượng quá lớn",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                // ===== 3. TIẾP TỤC LƯU FILE =====
                string rootFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "QuanLyPhanCong", "Uploads", $"PC_{_pcId}");

                Directory.CreateDirectory(rootFolder);

                string tenGV = FileNopBUS.GetTenGVByPCID(_pcId);
                tenGV = CleanFileName(tenGV);

                string newFileName = $"FileNop_{DateTime.Now:yyyyMMddHHmmss}_{tenGV}.pdf";
                string destPath = Path.Combine(rootFolder, newFileName);

                File.Copy(_selectedFilePath, destPath, true);

                if (FileNopBUS.InsertFileNop(_pcId, newFileName, destPath))
                {
                    PhanCongBUS.UpdateTrangThaiGV(_pcId, "CHO_DUYET");

                    MessageBox.Show("Đã nộp file và chờ duyệt từ Trưởng Bộ Môn!",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nộp file: " + ex.Message, "Lỗi");
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string CleanFileName(string input)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c.ToString(), "_");
            }
            return input.Trim();
        }

    }
}
