using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.FormBaoCao
{
    public partial class FormBaoCaoTongHop : Form
    {
        private BaoCaoTongHopResult _currentResult;

        public FormBaoCaoTongHop()
        {
            InitializeComponent();
        }

        private void FormBaoCaoTongHop_Load(object sender, EventArgs e)
        {
            InitLoaiKeHoach();
            InitLoaiCongViec();
            InitGiangVien();

            dgvChiTiet.AutoGenerateColumns = true;
        }

        private void InitLoaiKeHoach()
        {
            var data = new[]
            {
                new { Key = "",         Text = "Tất cả kế hoạch" },
                new { Key = "HocKyI",   Text = "Kế hoạch học kỳ I" },
                new { Key = "HocKyII",  Text = "Kế hoạch học kỳ II" },
                new { Key = "HocKyHe",  Text = "Kế hoạch học kỳ Hè" },
                new { Key = "NamHoc",   Text = "Kế hoạch năm học" },
                new { Key = "DeTai",    Text = "Kế hoạch đề tài nghiên cứu" },
                new { Key = "SuKien",   Text = "Kế hoạch sự kiện học thuật" },
                new { Key = "Khac",     Text = "Khác" }
            };

            cbbLoaiKeHoach.DisplayMember = "Text";
            cbbLoaiKeHoach.ValueMember = "Key";
            cbbLoaiKeHoach.DataSource = data;
        }

        private void InitLoaiCongViec()
        {
            var data = new[]
            {
                new { Key = "",         Text = "Tất cả loại công việc" },
                new { Key = "GiangDay", Text = "Giảng dạy" },
                new { Key = "SuKien",   Text = "Sự kiện" },
                new { Key = "DeTai",    Text = "Đề tài nghiên cứu" }
            };

            cbbLoaiCongViec.DisplayMember = "Text";
            cbbLoaiCongViec.ValueMember = "Key";
            cbbLoaiCongViec.DataSource = data;
        }

        private void InitGiangVien()
        {
            DataTable dt = UsersBUS.GetAllUsers(); 

            // Thêm dòng Tất cả
            DataRow drAll = dt.NewRow();
            drAll["UserID"] = 0;
            drAll["FullName"] = "Tất cả giảng viên";
            dt.Rows.InsertAt(drAll, 0);

            cbbGiangVien.DisplayMember = "FullName";
            cbbGiangVien.ValueMember = "UserID";
            cbbGiangVien.DataSource = dt;
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string loaiKeHoach = cbbLoaiKeHoach.SelectedValue == null
                ? null
                : cbbLoaiKeHoach.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(loaiKeHoach))
                loaiKeHoach = null;

            string loaiCongViec = cbbLoaiCongViec.SelectedValue == null
                ? null
                : cbbLoaiCongViec.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(loaiCongViec))
                loaiCongViec = null;

            int? giangVienId = null;
            if (cbbGiangVien.SelectedValue != null)
            {
                int id;
                if (int.TryParse(cbbGiangVien.SelectedValue.ToString(), out id) && id > 0)
                    giangVienId = id;
            }

            try
            {
                _currentResult = BaoCaoBUS.Generate(loaiKeHoach, loaiCongViec, giangVienId);
                BindSummary(_currentResult);
                BindGrid(_currentResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindSummary(BaoCaoTongHopResult r)
        {
            if (r == null)
            {
                lblTong.Text = "0";
                lblMoi.Text = "0";
                lblDangLam.Text = "0";
                lblChoDuyet.Text = "0";
                lblHoanThanh.Text = "0";
                lblTreHan.Text = "0";
                lblTiLe.Text = "0%";
                lblDaNop.Text = "0";
                lblDaDuyet.Text = "0";
                return;
            }

            lblTong.Text = r.TongCongViec.ToString();
            lblMoi.Text = r.SoMoi.ToString();
            lblDangLam.Text = r.SoDangLam.ToString();
            lblChoDuyet.Text = r.SoChoDuyet.ToString();
            lblHoanThanh.Text = r.SoHoanThanh.ToString();
            lblTreHan.Text = r.SoTreHan.ToString();
            lblTiLe.Text = r.TiLeHoanThanh.ToString("0.0") + "%";
            lblDaNop.Text = r.SoDaNopKetQua.ToString();
            lblDaDuyet.Text = r.SoDaDuyetKetQua.ToString();
        }

        private void BindGrid(BaoCaoTongHopResult r)
        {
            if (r == null || r.ChiTiet == null)
            {
                dgvChiTiet.DataSource = null;
                return;
            }

            dgvChiTiet.DataSource = r.ChiTiet;
        }

        private string BuildMoTaBoLoc()
        {
            string kh = cbbLoaiKeHoach.Text;
            string lcv = cbbLoaiCongViec.Text;
            string gv = cbbGiangVien.Text;

            return string.Format("Loại kế hoạch: {0}; Loại công việc: {1}; Giảng viên: {2}", kh, lcv, gv);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_currentResult == null || _currentResult.ChiTiet == null || _currentResult.ChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel file (*.xlsx)|*.xlsx";
            dlg.FileName = "BaoCaoTongHop.xlsx";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string desc = BuildMoTaBoLoc();
                    ExportHelper.ExportBaoCaoTongHopToExcel(_currentResult, dlg.FileName, "BÁO CÁO TỔNG HỢP CÔNG VIỆC", desc);
                    MessageBox.Show("Xuất Excel thành công.", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (_currentResult == null || _currentResult.ChiTiet == null || _currentResult.ChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF file (*.pdf)|*.pdf";
            dlg.FileName = "BaoCaoTongHop.pdf";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string desc = BuildMoTaBoLoc();
                    ExportHelper.ExportBaoCaoTongHopToPdf(_currentResult, dlg.FileName, "BÁO CÁO TỔNG HỢP CÔNG VIỆC", desc);
                    MessageBox.Show("Xuất PDF thành công.", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất PDF: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}
