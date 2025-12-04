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

namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    public partial class FormAddCongViec : Form
    {
        private PlanDTO currentPlan;
        private string planHocKyKey = null;
        private DataTable dtFullMonHoc;

        public FormAddCongViec()
        {
            InitializeComponent();
        }

        private void LoadLoaiComboBox()
        {
            cbb_Loai.DataSource = new[] {
                new { Key = "GiangDay", Value = "Giảng Dạy" },
                new { Key = "NghienCuu", Value = "Nghiên Cứu" },
                new { Key = "SuKien", Value = "Sự Kiện" },
                new { Key = "HanhChinh", Value = "Hành Chính" }
            };
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
            cbb_MucUuTien.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadMonHocData()
        {
            try
            {
                dtFullMonHoc = MonHocBUS.GetMonHocTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu môn học: " + ex.Message);
                dtFullMonHoc = new DataTable();
            }
        }

        private void LoadKeHoachComboBox()
        {
            var dt = PlansBUS.GetKeHoachForComboBox();
            cbbKeHoach.DataSource = dt;
            cbbKeHoach.DisplayMember = "TenKeHoach";
            cbbKeHoach.ValueMember = "KeHoachID";
            cbbKeHoach.SelectedIndex = -1;
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
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                cbb_LopPhuTrach.DataSource = null;
                num_SoTiet.Text = "0";
                return;
            }

            // Lọc đúng môn đang chọn trước
            var filtered = dtFullMonHoc.AsEnumerable()
                .Where(r => r.Field<string>("MaMonHoc") == maMonHoc);

            // Lọc theo học kỳ của kế hoạch (planHocKyKey)
            if (!string.IsNullOrEmpty(planHocKyKey))
                filtered = filtered.Where(r => r.Field<string>("HocKy") == planHocKyKey);

            // Lấy danh sách MonHocID đã phân công
            List<int> assignedIds = CongViecBUS.GetAssignedMonHocIds();

            // Loại bỏ chỉ đúng MonHocID đã phân công
            var available = filtered
                .Where(r => !assignedIds.Contains(r.Field<int>("MonHocID")))
                .Select(r => new
                {
                    MonHocID = r.Field<int>("MonHocID"),
                    TenLop = string.IsNullOrWhiteSpace(r.Field<string>("TenTo"))
                             ? r.Field<string>("TenNhom")
                             : $"{r.Field<string>("TenNhom")} {r.Field<string>("TenTo")}",
                    SoTiet = (r.Field<int?>("SoTiet_LT") ?? 0) +
                             (r.Field<int?>("SoTiet_TH") ?? 0)
                })
                .OrderBy(x => x.TenLop)
                .ToList();

            // Nếu không còn lớp nào để chọn
            if (available.Count == 0)
            {
                cbb_LopPhuTrach.DataSource = null;
                num_SoTiet.Text = "0";
                return;
            }

            cbb_LopPhuTrach.DataSource = available;
            cbb_LopPhuTrach.DisplayMember = "TenLop";
            cbb_LopPhuTrach.ValueMember = "MonHocID";
            cbb_LopPhuTrach.SelectedIndex = 0;
            cbb_Loai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FormAddCongViec_Load(object sender, EventArgs e)
        {
            LoadLoaiComboBox();
            LoadKeHoachComboBox();
            LoadMucUuTienComboBox();
            LoadMonHocData(); // Tải data môn học thô (dtFullMonHoc)

            UpdatePanelVisibility();

            // Sự kiện phụ thuộc kế hoạch mới
            cbbKeHoach.SelectedIndexChanged += cbbKeHoach_SelectedIndexChanged;
            cbb_Loai.SelectedIndexChanged += cbb_Loai_SelectedIndexChanged;
            cbb_MonHoc.SelectedIndexChanged += cbb_MonHoc_SelectedIndexChanged;
            cbb_LopPhuTrach.SelectedIndexChanged += cbb_LopPhuTrach_SelectedIndexChanged;

            if (cbbKeHoach.Items.Count > 0 && cbbKeHoach.SelectedIndex >= 0)
                cbbKeHoach_SelectedIndexChanged(cbbKeHoach, EventArgs.Empty);
        }
        

        private void btn_Them_Click(object sender, EventArgs e)
        {
            var dto = new CongViecDTO();
            if (currentPlan == null)
            {
                MessageBox.Show("Bạn phải chọn Kế hoạch trước khi tạo công việc!");
                return;
            }

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
                    if (cbb_MonHoc.SelectedValue == null || cbb_MonHoc.SelectedValue.ToString() == string.Empty)
                    {
                        MessageBox.Show("VUI LÒNG CHỌN MÔN HỌC.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbb_MonHoc.Focus();
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

        private void cbbKeHoach_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra ID kế hoạch hợp lệ
            // Cần kiểm tra SelectedValue có phải là int hợp lệ không, bỏ qua dòng null
            if (!(cbbKeHoach.SelectedValue is int keHoachID) || keHoachID <= 0)
            {
                currentPlan = null;
                planHocKyKey = null;
                // Reset ràng buộc ngày và môn học
                dtp_HanChot.MinDate = DateTime.MinValue;
                dtp_HanChot.MaxDate = DateTime.MaxValue;

                // Reset data môn học khi không chọn kế hoạch
                LoadMonHocFiltered();
                return;
            }

            // 2. Lấy chi tiết kế hoạch
            currentPlan = PlansBUS.GetPlanById(keHoachID);

            // 3. Cập nhật khóa học kỳ (Mã Key: HocKyI/HocKyII)
            if (currentPlan != null && (currentPlan.Loai == "HocKyI" || currentPlan.Loai == "HocKyII" || currentPlan.Loai == "HocKyHe"))
            {
                planHocKyKey = currentPlan.Loai;
            }
            else
            {
                planHocKyKey = null; // Nếu không phải học kỳ (NamHoc, Khac), không lọc MonHoc
            }

            // 4. Áp dụng ràng buộc ngày và giá trị mặc định
            if (currentPlan != null)
            {
                dtp_HanChot.MinDate = currentPlan.NgayBatDau;
                dtp_HanChot.MaxDate = currentPlan.NgayKetThuc;
                dtp_HanChot.Value = currentPlan.NgayBatDau; // Gán mặc định ngày bắt đầu
            }

            // 5. Load lại danh sách Môn học (đã lọc theo Học kỳ mới)
            LoadMonHocFiltered();
        }

        private void LoadMonHocFiltered()
        {
            if (dtFullMonHoc == null) return;

            // Lọc theo Học kỳ Kế hoạch (planHocKyKey)
            var q = dtFullMonHoc.AsEnumerable();
            // === Lọc theo HocKy ===
            if (!string.IsNullOrEmpty(planHocKyKey))
                q = q.Where(r => r.Field<string>("HocKy") == planHocKyKey);
            // ===================================

            // GroupBy MaMonHoc (chỉ lấy unique item)
            var distinctMonHocData = q
                .GroupBy(r => r.Field<string>("MaMonHoc"))
                .Select(g => g.First())
                .OrderBy(r => r.Field<string>("TenMonHoc"));

            DataTable dt = new DataTable();
            dt.Columns.Add("MonHocID", typeof(int));
            dt.Columns.Add("MaMonHoc", typeof(string));
            dt.Columns.Add("TenMonHoc", typeof(string));

            // Thêm dòng null
            DataRow drNull = dt.NewRow();
            drNull["MonHocID"] = DBNull.Value;
            drNull["MaMonHoc"] = string.Empty;
            drNull["TenMonHoc"] = "(Không chọn Môn học)";
            dt.Rows.Add(drNull);

            foreach (var row in distinctMonHocData)
            {
                dt.ImportRow(row);
            }

            cbb_MonHoc.DataSource = dt;
            cbb_MonHoc.DisplayMember = "TenMonHoc";
            cbb_MonHoc.ValueMember = "MaMonHoc";
            cbb_MonHoc.SelectedIndex = 0;
            cbb_MonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
