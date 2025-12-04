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
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.FormPlan.CV.ChildCV
{
    public partial class FormAddCongViec : Form
    {
        private PlanDTO currentPlan; // Lưu trữ chi tiết kế hoạch cha

        // Cần thêm cột MaMonHoc vào DataTable của cbb_MonHoc để sử dụng
        private DataTable dtFullMonHoc; // Lưu trữ toàn bộ data môn học để lọc Nhóm/Tổ

        private string planHocKyKey = null; // lọc môn học theo học kỳ
        public FormAddCongViec(int keHoachId)
        {
            InitializeComponent();
            try
            {
                currentPlan = PlansBUS.GetPlanById(keHoachId);
                if (currentPlan != null && (currentPlan.Loai == "HocKyI" || currentPlan.Loai == "HocKyII" || currentPlan.Loai == "HocKyHe"))
                {
                    planHocKyKey = currentPlan.Loai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin Kế hoạch cha: " + ex.Message);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            dtFullMonHoc = MonHocBUS.GetMonHocTable();
        }
        public FormAddCongViec()
        {
            InitializeComponent();
        }

        private void LoadLoaiComboBox()
        {
            var loaiList = new[] {
                new { Key = "GiangDay", Value = "Giảng Dạy" },
                new { Key = "NghienCuu", Value = "Nghiên Cứu" },
                new { Key = "SuKien", Value = "Sự Kiện" },
                new { Key = "HanhChinh", Value = "Hành Chính" }
            };
            cbb_Loai.DataSource = loaiList;
            cbb_Loai.DisplayMember = "Value";
            cbb_Loai.ValueMember = "Key";
        }

        private void LoadMucUuTienComboBox()
        {
            var priList = new[] {
                new { Key = "LOW", Value = "Thấp" },
                new { Key = "MED", Value = "Trung bình" },
                new { Key = "HIGH", Value = "Cao" }
            };
            cbb_MucUuTien.DataSource = priList;
            cbb_MucUuTien.DisplayMember = "Value";
            cbb_MucUuTien.ValueMember = "Key";
            cbb_MucUuTien.SelectedValue = "MED";
        }

        private void LoadKeHoachComboBox()
        {
            if (currentPlan == null)
            {
                cbbKeHoach.DataSource = null;
                cbbKeHoach.Text = "Không xác định Kế hoạch";
                cbbKeHoach.Enabled = false;
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("KeHoachID", typeof(int));
            dt.Columns.Add("TenKeHoach", typeof(string));

            DataRow dr = dt.NewRow();
            dr["KeHoachID"] = currentPlan.KeHoachID;
            dr["TenKeHoach"] = currentPlan.TenKeHoach;
            dt.Rows.Add(dr);

            cbbKeHoach.DataSource = dt;
            cbbKeHoach.DisplayMember = "TenKeHoach";
            cbbKeHoach.ValueMember = "KeHoachID";
            cbbKeHoach.SelectedValue = currentPlan.KeHoachID;
            cbbKeHoach.Enabled = false;
        }

        private void LoadMonHocComboBox()
        {
            try
            {
                IEnumerable<DataRow> monHocQuery = dtFullMonHoc.AsEnumerable();

                // === LỌC MÔN HỌC THEO HỌC KỲ CỦA KẾ HOẠCH ===
                if (!string.IsNullOrEmpty(planHocKyKey))
                {
                    // Chỉ giữ lại các môn học có trường 'HocKy' trùng với Mã Học Kỳ của Kế hoạch
                    monHocQuery = monHocQuery.Where(row => row.Field<string>("HocKy") == planHocKyKey);
                    // Sau khi lọc, mới nhóm các môn học lại
                }

                //Lọc các cột MaMonHoc và TenMonHoc duy nhất
                var distinctMonHoc = dtFullMonHoc.AsEnumerable()
                    .GroupBy(row => row.Field<string>("MaMonHoc"))
                    .Select(group => new
                    {
                        // Lấy ID đầu tiên tìm thấy của Mã Môn Học này
                        MonHocID = group.First().Field<int>("MonHocID"),
                        MaMonHoc = group.Key,
                        TenMonHoc = group.First().Field<string>("TenMonHoc")
                    })
                    .OrderBy(item => item.TenMonHoc)
                    .ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MonHocID", typeof(int));
                dt.Columns.Add("TenMonHoc", typeof(string));
                dt.Columns.Add("MaMonHoc", typeof(string)); // Cột ẩn để lấy mã môn học

                // Thêm dòng "Không chọn Môn học"
                DataRow drNull = dt.NewRow();
                drNull["MonHocID"] = DBNull.Value;
                drNull["TenMonHoc"] = "(Không chọn Môn học)";
                drNull["MaMonHoc"] = string.Empty;
                dt.Rows.Add(drNull);

                foreach (var item in distinctMonHoc)
                {
                    dt.Rows.Add(item.MonHocID, item.TenMonHoc, item.MaMonHoc);
                }

                cbb_MonHoc.DataSource = dt;
                cbb_MonHoc.DisplayMember = "TenMonHoc";
                cbb_MonHoc.ValueMember = "MonHocID";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải Môn Học: " + ex.Message); }
        }

        private void UpdatePanelVisibility()
        {
            string loai = cbb_Loai.SelectedValue?.ToString();

            // Ẩn hết
            pnl_GiangDay.Visible = false;
            pnl_NghienCuu.Visible = false;
            pnl_SuKien.Visible = false;

            // Chỉ hiện cái cần
            if (loai == "GiangDay")
            {
                pnl_GiangDay.Visible = true;
            }
            else if (loai == "NghienCuu")
            {
                pnl_NghienCuu.Visible = true;
            }
            else if (loai == "SuKien")
            {
                pnl_SuKien.Visible = true;
            }
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void LoadLopPhuTrachComboBox(string maMonHoc)
        {
            // Giả định txt_LopPhuTrach đã được thay thế bằng ComboBox tên cbb_LopPhuTrach
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                cbb_LopPhuTrach.DataSource = null;
                return;
            }

            IEnumerable<DataRow> lopPhuTrachQuery = dtFullMonHoc.AsEnumerable();

            // === LỌC LỚP PHỤ TRÁCH BẰNG MÃ MÔN HỌC ===
            lopPhuTrachQuery = lopPhuTrachQuery.Where(row => row.Field<string>("MaMonHoc") == maMonHoc);

            // LỌC CHỒNG: Nếu có Học Kỳ Kế hoạch, ta phải lọc lại lần nữa
            if (!string.IsNullOrEmpty(planHocKyKey))
            {
                // Rất quan trọng: Chỉ giữ lại các nhóm/tổ của đúng học kỳ
                lopPhuTrachQuery = lopPhuTrachQuery.Where(row => row.Field<string>("HocKy") == planHocKyKey);
            }

            // 1. Lọc các nhóm/tổ thuộc MaMonHoc đó
            var filteredClasses = dtFullMonHoc.AsEnumerable()
                .Where(row => row.Field<string>("MaMonHoc") == maMonHoc)
                .Where(row => string.IsNullOrEmpty(planHocKyKey) || row.Field<string>("HocKy") == planHocKyKey)
                .Select(row => new
                {
                    MonHocID = row.Field<int>("MonHocID"),
                    TenNhom = row.Field<string>("TenNhom"),
                    TenTo = row.Field<string>("TenTo"),
                    SoTietTH = row.Field<int>("SoTiet_TH"), // Cần để điền SoTiet
                    SoTietLT = row.Field<int>("SoTiet_LT"),
                    // Tạo tên hiển thị (N01 hoặc N01 T01)
                    TenLopPhuTrach = string.IsNullOrEmpty(row.Field<string>("TenTo")) ?
                                     row.Field<string>("TenNhom") :
                                     $"{row.Field<string>("TenNhom")} {row.Field<string>("TenTo")}"
                }).ToList();

            // 2. Lấy danh sách ID các lớp đã được phân công (từ DB)
            List<int> assignedIds = CongViecBUS.GetAssignedMonHocIds();

            // 3. Lọc bỏ các lớp đã được phân công
            var availableClasses = filteredClasses
                                    .Where(c => !assignedIds.Contains(c.MonHocID))
                                    .ToList();

            DataTable dtLop = new DataTable();
            dtLop.Columns.Add("MonHocID", typeof(int));
            dtLop.Columns.Add("TenLopPhuTrach", typeof(string));
            dtLop.Columns.Add("SoTiet", typeof(int));

            DataRow drNull = dtLop.NewRow();
            drNull["MonHocID"] = DBNull.Value;
            drNull["TenLopPhuTrach"] = "(Chọn Nhóm/Tổ)";
            dtLop.Rows.Add(drNull);

            foreach (var item in availableClasses)
            {
                // Số tiết thực tế là tổng LT + TH, hoặc chỉ TH nếu là Tổ
                int soTietThucTe = item.TenTo == null ? item.SoTietLT : item.SoTietTH;
                dtLop.Rows.Add(item.MonHocID, item.TenLopPhuTrach, soTietThucTe);
            }

            cbb_LopPhuTrach.DataSource = dtLop;
            cbb_LopPhuTrach.DisplayMember = "TenLopPhuTrach";
            cbb_LopPhuTrach.ValueMember = "MonHocID";
        }

        private void FormAddCongViec_Load(object sender, EventArgs e)
        {
            LoadLoaiComboBox();
            LoadKeHoachComboBox();
            LoadMonHocComboBox();
            LoadMucUuTienComboBox();

            // === FIX: RÀNG BUỘC HẠN CHÓT THEO KẾ HOẠCH CHA ===
            if (currentPlan != null)
            {
                // Giả định NgayKetThuc và NgayBatDau có kiểu DateTime (không phải nullable) trong DTO
                // Nếu HanChot không được set (chưa Checked), sẽ dùng giá trị này
                dtp_HanChot.Value = currentPlan.NgayKetThuc;
                dtp_HanChot.MinDate = currentPlan.NgayBatDau;
                dtp_HanChot.MaxDate = currentPlan.NgayKetThuc;
            }

            UpdatePanelVisibility();

            cbb_Loai.SelectedIndexChanged += cbb_Loai_SelectedIndexChanged;
            cbb_MonHoc.SelectedIndexChanged += cbb_MonHoc_SelectedIndexChanged;
            cbb_LopPhuTrach.SelectedIndexChanged += cbb_LopPhuTrach_SelectedIndexChanged; // Thêm sự kiện để điền Số tiết
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            var dto = new CongViecDTO();

            try
            {
                dto.TenCongViec = textBox_TenCongViec.Text;
                dto.Loai = cbb_Loai.SelectedValue?.ToString();
                dto.KeHoachID = cbbKeHoach.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cbbKeHoach.SelectedValue);
                dto.MucUuTien = cbb_MucUuTien.SelectedValue?.ToString();

                dto.HanChot = dtp_HanChot.Checked ? (DateTime?)dtp_HanChot.Value.Date : (DateTime?)null;

                dto.MoTa = txt_GhiChuChung.Text;

                dto.NguoiGiaoID = Program.CurrentUserId;

                if (dto.Loai == "GiangDay")
                {
                    dto.MonHocID = cbb_LopPhuTrach.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cbb_LopPhuTrach.SelectedValue);
                    dto.LopPhuTrach = cbb_LopPhuTrach.Text; // Lưu tên lớp/tổ để hiển thị
                    dto.SoTiet = string.IsNullOrWhiteSpace(num_SoTiet.Text) ? (int?)null : Convert.ToInt32(num_SoTiet.Text);
                    if (!dto.MonHocID.HasValue)
                    {
                        MessageBox.Show("Vui lòng chọn Nhóm/Tổ phụ trách cho công việc Giảng dạy.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (dto.Loai == "NghienCuu")
                {
                    dto.MaDeTai = txt_MaDeTai.Text;
                }
                else if (dto.Loai == "SuKien")
                {
                    dto.DiaDiem = txt_DiaDiem.Text;
                }

                if (CongViecBUS.AddCongViec(dto))
                {
                    MessageBox.Show("Thêm công việc mới thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi định dạng số. Vui lòng kiểm tra lại Số Tiết.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi thêm CV: " + ex.Message);
            }
        }

        private void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanelVisibility();
        }

        private void cbb_LopPhuTrach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LopPhuTrach.SelectedItem is DataRowView drv && drv["SoTiet"] is int soTiet)
            {
                num_SoTiet.Text = soTiet.ToString();
            }
            else
            {
                num_SoTiet.Text = "0";
            }
        }

        private void cbb_MonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Loai.SelectedValue?.ToString() == "GiangDay")
            {
                if (cbb_MonHoc.SelectedItem is DataRowView drv)
                {
                    string maMonHoc = drv["MaMonHoc"].ToString();
                    LoadLopPhuTrachComboBox(maMonHoc);
                }
            }
        }
    }
}
