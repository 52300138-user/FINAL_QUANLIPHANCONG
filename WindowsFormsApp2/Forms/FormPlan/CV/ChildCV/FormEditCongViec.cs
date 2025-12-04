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
    public partial class FormEditCongViec : Form
    {
        private int currentCongViecId;
        private CongViecDTO currentCV;
        private PlanDTO currentPlan; 
        private DataTable dtFullMonHoc;

        public FormEditCongViec(int congViecId)
        {
            InitializeComponent();
            this.currentCongViecId = congViecId;
            try
            {
                dtFullMonHoc = MonHocBUS.GetMonHocTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu môn học: " + ex.Message, "Lỗi Hệ Thống");
            }

            // Gán sự kiện cần thiết
            cbb_Loai.SelectedIndexChanged += cbb_Loai_SelectedIndexChanged;
            cbb_MonHoc.SelectedIndexChanged += cbb_MonHoc_SelectedIndexChanged;
            cbb_LopPhuTrach.SelectedIndexChanged += cbb_LopPhuTrach_SelectedIndexChanged;
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
            // (Giả sử PlansBUS.GetPlans() trả về 'KeHoachID' và 'TenKeHoach')
            DataTable dt = PlansBUS.GetPlans();
            // Thêm 1 dòng "Không có"
            DataRow dr = dt.NewRow();
            dr["KeHoachID"] = DBNull.Value;
            dr["TenKeHoach"] = "(Không thuộc Kế hoạch nào)";
            dt.Rows.InsertAt(dr, 0);

            cbbKeHoach.DataSource = dt;
            cbbKeHoach.DisplayMember = "TenKeHoach";
            cbbKeHoach.ValueMember = "KeHoachID";
        }

        private void LoadMonHocComboBox()
        {
            try
            {
                var distinctMonHoc = dtFullMonHoc.AsEnumerable()
                    .GroupBy(row => row.Field<string>("MaMonHoc"))
                    .Select(group => new
                    {
                        MonHocID = group.First().Field<int>("MonHocID"),
                        MaMonHoc = group.Key,
                        TenMonHoc = group.First().Field<string>("TenMonHoc")
                    }).ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("MonHocID", typeof(int));
                dt.Columns.Add("TenMonHoc", typeof(string));
                dt.Columns.Add("MaMonHoc", typeof(string)); // Cột ẩn

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
                cbb_MonHoc.ValueMember = "MaMonHoc"; // Dùng MaMonHoc làm ValueMember cho cbb_MonHoc
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


        private void FormEditCongViec_Load(object sender, EventArgs e)
        {
            this.currentCV = CongViecBUS.GetCongViecById(this.currentCongViecId);
            if (this.currentCV == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy công việc này.");
                this.Close();
                return;
            }
   
            LoadLoaiComboBox();
            LoadKeHoachComboBox();
            LoadMonHocComboBox();
            LoadMucUuTienComboBox();

        

            // 3. Lấy thông tin Kế hoạch (để ràng buộc Hạn Chót)
            if (this.currentCV.KeHoachID.HasValue)
            {
                try
                {
                    this.currentPlan = PlansBUS.GetPlanById(this.currentCV.KeHoachID.Value);
                }
                catch (Exception) { /* Bỏ qua nếu lỗi tải Plan */ }
            }

            // 4. Fill dữ liệu chung
            textBox_TenCongViec.Text = this.currentCV.TenCongViec;
            cbb_Loai.SelectedValue = this.currentCV.Loai;
            cbbKeHoach.SelectedValue = this.currentCV.KeHoachID ?? (object)DBNull.Value;
            cbb_MucUuTien.SelectedValue = this.currentCV.MucUuTien;

            // Ràng buộc Hạn Chót
            if (this.currentCV.HanChot.HasValue)
            {
                dtp_HanChot.Checked = true;
                dtp_HanChot.Value = this.currentCV.HanChot.Value;
            }
            else
            {
                dtp_HanChot.Checked = false;
            }
            if (this.currentPlan != null)
            {
                dtp_HanChot.MinDate = this.currentPlan.NgayBatDau;
                dtp_HanChot.MaxDate = this.currentPlan.NgayKetThuc;
            }

            txt_GhiChuChung.Text = this.currentCV.MoTa;

            // 5. Fill dữ liệu chi tiết
            if (this.currentCV.Loai == "GiangDay")
            {
                // Tìm Mã Môn Học của lớp/tổ đang sửa (vì cbb_MonHoc dùng Mã Môn Học làm ValueMember)
                var currentMonHoc = dtFullMonHoc.AsEnumerable()
                                    .FirstOrDefault(r => r.Field<int?>("MonHocID") == this.currentCV.MonHocID);

                if (currentMonHoc != null)
                {
                    string maMonHoc = currentMonHoc.Field<string>("MaMonHoc");
                    // Chọn Mã Môn Học trong cbb_MonHoc (trigger sự kiện Changed)
                    cbb_MonHoc.SelectedValue = maMonHoc;

                    // Do sự kiện Changed đã gọi LoadLopPhuTrachComboBox,
                    // ta chỉ cần chọn đúng lớp/tổ trong cbb_LopPhuTrach
                    cbb_LopPhuTrach.SelectedValue = this.currentCV.MonHocID ?? (object)DBNull.Value;
                }

                // num_SoTiet sẽ được tự động điền bởi sự kiện SelectedIndexChanged của cbb_LopPhuTrach
                // Nếu MonHocID NULL, set lại text cũ nếu có
                if (!this.currentCV.MonHocID.HasValue)
                {
                    num_SoTiet.Text = this.currentCV.SoTiet?.ToString();
                }
            }
            else if (this.currentCV.Loai == "NghienCuu")
            {
                txt_MaDeTai.Text = this.currentCV.MaDeTai;
            }
            else if (this.currentCV.Loai == "SuKien")
            {
                txt_DiaDiem.Text = this.currentCV.DiaDiem;
            }

            // 6. Cập nhật UI
            UpdatePanelVisibility();
            btn_Them.Text = "Lưu thay đổi";
        }
        private void LoadLopPhuTrachComboBox(string maMonHoc, int? currentClassId)
        {
            // Giả định cbb_LopPhuTrach là ComboBox
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                cbb_LopPhuTrach.DataSource = null;
                return;
            }

            // 1. Lấy tất cả nhóm/tổ thuộc MaMonHoc
            var filteredClasses = dtFullMonHoc.AsEnumerable()
                .Where(row => row.Field<string>("MaMonHoc") == maMonHoc)
                .Select(row => new
                {
                    MonHocID = row.Field<int>("MonHocID"),
                    TenNhom = row.Field<string>("TenNhom"),
                    TenTo = row.Field<string>("TenTo"),
                    SoTietTH = row.Field<int>("SoTiet_TH"),
                    SoTietLT = row.Field<int>("SoTiet_LT"),
                    TenLopPhuTrach = string.IsNullOrEmpty(row.Field<string>("TenTo")) ?
                                     row.Field<string>("TenNhom") :
                                     $"{row.Field<string>("TenNhom")} {row.Field<string>("TenTo")}"
                }).ToList();

            // 2. Lấy danh sách ID các lớp đã được phân công (LOẠI BỎ ID CỦA CHÍNH CÔNG VIỆC ĐANG SỬA)
            List<int> assignedIds = CongViecBUS.GetAssignedMonHocIds();

            // Lọc bỏ các lớp đã được phân công, nhưng giữ lại lớp hiện tại đang sửa
            var availableClasses = filteredClasses
                .Where(c => !assignedIds.Contains(c.MonHocID) || c.MonHocID == currentClassId)
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
                // Lấy số tiết tương ứng (LT cho Nhóm, TH cho Tổ)
                int soTietThucTe = string.IsNullOrEmpty(item.TenTo) ? item.SoTietLT : item.SoTietTH;
                dtLop.Rows.Add(item.MonHocID, item.TenLopPhuTrach, soTietThucTe);
            }

            cbb_LopPhuTrach.DataSource = dtLop;
            cbb_LopPhuTrach.DisplayMember = "TenLopPhuTrach";
            cbb_LopPhuTrach.ValueMember = "MonHocID";
        }
        private void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanelVisibility();
        }

        private void cbb_MonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Loai.SelectedValue?.ToString() == "GiangDay")
            {
                if (cbb_MonHoc.SelectedItem is DataRowView drv)
                {
                    string maMonHoc = drv["MaMonHoc"].ToString();
                    LoadLopPhuTrachComboBox(maMonHoc, this.currentCV.MonHocID);
                }
            }
        }

        private void cbb_LopPhuTrach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LopPhuTrach.SelectedItem is DataRowView drv && drv["SoTiet"] is int soTiet)
            {
                num_SoTiet.Text = soTiet.ToString();
            }
            else
            {
                num_SoTiet.Text = "";
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu (giống Form Add)
            var dto = new CongViecDTO();
            dto.CongViecID = this.currentCongViecId;

            try
            {
                // 1. Gom data chung
                dto.TenCongViec = textBox_TenCongViec.Text;
                dto.Loai = cbb_Loai.SelectedValue?.ToString();

                // Lấy KeHoachID an toàn (Giữ nguyên cú pháp cũ nếu nó là điểm lỗi nhỏ)
                dto.KeHoachID = cbbKeHoach.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cbbKeHoach.SelectedValue);

                dto.MucUuTien = cbb_MucUuTien.SelectedValue?.ToString();
                dto.HanChot = dtp_HanChot.Checked ? (DateTime?)dtp_HanChot.Value.Date : (DateTime?)null; // Dùng .Date cho an toàn
                dto.MoTa = txt_GhiChuChung.Text;

                // Giữ lại các giá trị gốc không sửa
                dto.NguoiGiaoID = this.currentCV.NguoiGiaoID;
                dto.TrangThai = this.currentCV.TrangThai;

                // Clear các trường chi tiết trước
                dto.MaDeTai = null; dto.DiaDiem = null;
                dto.MonHocID = null; dto.LopPhuTrach = null; dto.SoTiet = null;

                // 2. Gom data chi tiết theo Loại
                if (dto.Loai == "GiangDay")
                {
                    if (cbb_LopPhuTrach.SelectedValue is int monHocId)
                    {
                        dto.MonHocID = monHocId;
                    }
                    else
                    {
                        dto.MonHocID = null;
                    }

                    dto.LopPhuTrach = cbb_LopPhuTrach.Text;

                    int soTietValue;
                    if (int.TryParse(num_SoTiet.Text.Trim(), out soTietValue))
                    {
                        dto.SoTiet = soTietValue;
                    }
                    else if (!string.IsNullOrWhiteSpace(num_SoTiet.Text))
                    {
                        // Nếu không rỗng và không phải số
                        MessageBox.Show("Số Tiết phải là một số nguyên hợp lệ.", "Lỗi Định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        num_SoTiet.Focus();
                        return;
                    }

                    // Validation Giảng Dạy
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

                // 3. Gọi BUS
                if (CongViecBUS.UpdateCongViec(dto))
                {
                    MessageBox.Show("Cập nhật công việc thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi cập nhật CV: " + ex.Message, "Lỗi Hệ Thống");
            }
        }
    }
}
