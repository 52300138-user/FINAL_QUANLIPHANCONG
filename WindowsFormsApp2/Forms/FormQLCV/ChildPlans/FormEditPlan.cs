using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    public partial class FormEditPlan : Form
    {
        private readonly int _congViecId;
        private CongViecDTO _currentCV;
        private PlanDTO _currentPlan;
        private DataTable _dtFullMonHoc;

        public FormEditPlan(int congViecId)
        {
            InitializeComponent();
            _congViecId = congViecId;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void FormEditPlan_Load(object sender, EventArgs e)
        {
            try
            {
                _currentCV = CongViecBUS.GetCongViecById(_congViecId);
                if (_currentCV == null)
                {
                    MessageBox.Show("Không tìm thấy công việc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                if (_currentCV.KeHoachID.HasValue)
                {
                    try
                    {
                        _currentPlan = PlansBUS.GetPlanById(_currentCV.KeHoachID.Value);
                    }
                    catch
                    {
                        _currentPlan = null;
                    }
                }

                try
                {
                    _dtFullMonHoc = MonHocBUS.GetMonHocTable();
                }
                catch
                {
                    _dtFullMonHoc = new DataTable();
                }

                LoadLoaiCombo();       
                cbb_Loai.SelectedValue = _currentCV.Loai;  
                cbb_Loai.Enabled = false;
                UpdatePanelVisibility();
                FillCommonInfo();
                LoadMucUuTienCombo();
                FillDetailByLoai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form sửa công việc:\n" + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void LoadLoaiCombo()
        {
            var loaiList = new[]
            {
                new { Key = "GiangDay", Value = "Giảng dạy" },
                new { Key = "NghienCuu", Value = "Nghiên cứu" },
                new { Key = "SuKien", Value = "Sự kiện" },
                new { Key = "HanhChinh", Value = "Hành chính" }
            };

            cbb_Loai.DataSource = loaiList;
            cbb_Loai.DisplayMember = "Value";
            cbb_Loai.ValueMember = "Key";
            cbb_Loai.SelectedValue = _currentCV.Loai;
            cbb_Loai.Enabled = false;
        }

        private void LoadMucUuTienCombo()
        {
            var priList = new[]
            {
                new { Key = "LOW", Value = "Thấp" },
                new { Key = "MED", Value = "Trung bình" },
                new { Key = "HIGH", Value = "Cao" }
            };

            cbb_MucUuTien.DataSource = priList;
            cbb_MucUuTien.DisplayMember = "Value";
            cbb_MucUuTien.ValueMember = "Key";
            cbb_MucUuTien.SelectedValue = string.IsNullOrEmpty(_currentCV.MucUuTien) ? "MED" : _currentCV.MucUuTien;
        }

        private void FillCommonInfo()
        {
            txt_TenCongViec.Text = _currentCV.TenCongViec ?? string.Empty;

            var nguoiGiao = UsersBUS.GetUserById(_currentCV.NguoiGiaoID);
            txt_NguoiGiao.Text = nguoiGiao?.FullName ?? "(Không rõ)";
            txt_NguoiGiao.ReadOnly = true;

            DateTime? ngayGiao = PhanCongBUS.GetNgayGiaoDauTien(_congViecId);
            if (ngayGiao.HasValue)
                dtp_NgayGiao.Value = ngayGiao.Value;
            else
                dtp_NgayGiao.Value = _currentCV.CreatedAt;

            dtp_NgayGiao.Enabled = false;

            if (_currentCV.HanChot.HasValue)
                dtp_NgayNop.Value = _currentCV.HanChot.Value;
            else
                dtp_NgayNop.Value = DateTime.Today;

            if (_currentPlan != null)
            {
                dtp_NgayNop.MinDate = _currentPlan.NgayBatDau;
                dtp_NgayNop.MaxDate = _currentPlan.NgayKetThuc;
            }

            txt_LyDoSua.Clear();
        }

        private void LoadMonHocComboGiangDay()
        {
            if (_dtFullMonHoc == null || _dtFullMonHoc.Rows.Count == 0)
            {
                cbb_MonHoc.DataSource = null;
                return;
            }

            var distinctMonHoc = _dtFullMonHoc.AsEnumerable()
                .GroupBy(r => r.Field<string>("MaMonHoc"))
                .Select(g => new
                {
                    MonHocID = g.First().Field<int>("MonHocID"),
                    MaMonHoc = g.Key,
                    TenMonHoc = g.First().Field<string>("TenMonHoc")
                })
                .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("MonHocID", typeof(int));
            dt.Columns.Add("MaMonHoc", typeof(string));
            dt.Columns.Add("TenMonHoc", typeof(string));

            DataRow drNull = dt.NewRow();
            drNull["MonHocID"] = DBNull.Value;
            drNull["MaMonHoc"] = string.Empty;
            drNull["TenMonHoc"] = "(Không chọn Môn học)";
            dt.Rows.Add(drNull);

            foreach (var item in distinctMonHoc)
            {
                DataRow r = dt.NewRow();
                r["MonHocID"] = item.MonHocID;
                r["MaMonHoc"] = item.MaMonHoc;
                r["TenMonHoc"] = item.TenMonHoc;
                dt.Rows.Add(r);
            }

            cbb_MonHoc.DataSource = dt;
            cbb_MonHoc.DisplayMember = "TenMonHoc";
            cbb_MonHoc.ValueMember = "MaMonHoc";
        }

        private void LoadLopPhuTrachComboGiangDay(string maMonHoc, int? currentMonHocId)
        {
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                cbb_LopPhuTrach.DataSource = null;
                num_SoTiet.Value = 0;
                return;
            }

            var filtered = _dtFullMonHoc.AsEnumerable()
                .Where(r => r.Field<string>("MaMonHoc") == maMonHoc)
                .Select(r => new
                {
                    MonHocID = r.Field<int>("MonHocID"),
                    TenNhom = r.Field<string>("TenNhom"),
                    TenTo = r.Field<string>("TenTo"),
                    SoTietTH = r.Field<int>("SoTiet_TH"),
                    SoTietLT = r.Field<int>("SoTiet_LT")
                })
                .ToList();

            List<int> assignedIds = CongViecBUS.GetAssignedMonHocIds();

            var available = filtered
                .Where(c => !assignedIds.Contains(c.MonHocID) || c.MonHocID == currentMonHocId)
                .Select(c => new
                {
                    c.MonHocID,
                    TenLopPhuTrach = string.IsNullOrEmpty(c.TenTo) ? c.TenNhom : c.TenNhom + " " + c.TenTo,
                    SoTiet = string.IsNullOrEmpty(c.TenTo) ? c.SoTietLT : c.SoTietTH
                })
                .ToList();

            DataTable dtLop = new DataTable();
            dtLop.Columns.Add("MonHocID", typeof(int));
            dtLop.Columns.Add("TenLopPhuTrach", typeof(string));
            dtLop.Columns.Add("SoTiet", typeof(int));

            DataRow drNull = dtLop.NewRow();
            drNull["MonHocID"] = DBNull.Value;
            drNull["TenLopPhuTrach"] = "(Chọn Nhóm/Tổ)";
            drNull["SoTiet"] = 0;
            dtLop.Rows.Add(drNull);

            foreach (var item in available)
            {
                DataRow r = dtLop.NewRow();
                r["MonHocID"] = item.MonHocID;
                r["TenLopPhuTrach"] = item.TenLopPhuTrach;
                r["SoTiet"] = item.SoTiet;
                dtLop.Rows.Add(r);
            }

            cbb_LopPhuTrach.DataSource = dtLop;
            cbb_LopPhuTrach.DisplayMember = "TenLopPhuTrach";
            cbb_LopPhuTrach.ValueMember = "MonHocID";

            if (currentMonHocId.HasValue)
                cbb_LopPhuTrach.SelectedValue = currentMonHocId.Value;
            else
                cbb_LopPhuTrach.SelectedIndex = 0;

            if (cbb_LopPhuTrach.SelectedItem is DataRowView drv && drv["SoTiet"] is int soTiet)
                num_SoTiet.Value = soTiet;
            else
                num_SoTiet.Value = 0;
        }

        private void FillDetailByLoai()
        {
            string loai = _currentCV.Loai;

            if (loai == "GiangDay")
            {
                LoadMonHocComboGiangDay();

                if (_currentCV.MonHocID.HasValue)
                {
                    var row = _dtFullMonHoc.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("MonHocID") == _currentCV.MonHocID.Value);

                    if (row != null)
                    {
                        string maMonHoc = row.Field<string>("MaMonHoc");
                        cbb_MonHoc.SelectedValue = maMonHoc;
                        LoadLopPhuTrachComboGiangDay(maMonHoc, _currentCV.MonHocID);
                    }
                    else
                    {
                        LoadLopPhuTrachComboGiangDay(null, null);
                    }
                }
                else
                {
                    LoadLopPhuTrachComboGiangDay(null, null);
                }

                if (_currentCV.SoTiet.HasValue)
                    num_SoTiet.Value = _currentCV.SoTiet.Value;
                else
                    num_SoTiet.Value = 0;
            }
            else if (loai == "NghienCuu")
            {
                txt_MaDeTai.Text = _currentCV.MaDeTai ?? string.Empty;
            }
            else if (loai == "SuKien")
            {
                txt_DiaDiem.Text = _currentCV.DiaDiem ?? string.Empty;
            }
        }

        private void UpdatePanelVisibility()
        {
            string loai = cbb_Loai.SelectedValue?.ToString();

            if (pnl_GiangDay != null) pnl_GiangDay.Visible = false;
            if (pnl_NghienCuu != null) pnl_NghienCuu.Visible = false;
            if (pnl_SuKien != null) pnl_SuKien.Visible = false;

            if (loai == "GiangDay" && pnl_GiangDay != null)
                pnl_GiangDay.Visible = true;
            else if (loai == "NghienCuu" && pnl_NghienCuu != null)
                pnl_NghienCuu.Visible = true;
            else if (loai == "SuKien" && pnl_SuKien != null)
                pnl_SuKien.Visible = true;

            if (pnl_GiangDay.Visible) pnl_GiangDay.BringToFront();
            if (pnl_NghienCuu.Visible) pnl_NghienCuu.BringToFront();
            if (pnl_SuKien.Visible) pnl_SuKien.BringToFront();
        }

        private bool IsChiTietPanelChanged()
        {
            if (_currentCV.Loai == "GiangDay")
            {
                int? newMonHocId = null;
                if (cbb_LopPhuTrach.SelectedValue != null && cbb_LopPhuTrach.SelectedValue != DBNull.Value)
                    newMonHocId = Convert.ToInt32(cbb_LopPhuTrach.SelectedValue);

                int oldMonHocId = _currentCV.MonHocID ?? 0;
                int newMonHocIdVal = newMonHocId ?? 0;

                if (newMonHocIdVal != oldMonHocId)
                    return true;

                int oldSoTiet = _currentCV.SoTiet ?? 0;
                int newSoTiet = (int)num_SoTiet.Value;

                if (newSoTiet != oldSoTiet)
                    return true;
            }

            if (_currentCV.Loai == "NghienCuu")
            {
                string oldMa = _currentCV.MaDeTai ?? string.Empty;
                string newMa = txt_MaDeTai.Text.Trim();
                if (!string.Equals(oldMa, newMa, StringComparison.Ordinal))
                    return true;
            }

            if (_currentCV.Loai == "SuKien")
            {
                string oldDiaDiem = _currentCV.DiaDiem ?? string.Empty;
                string newDiaDiem = txt_DiaDiem.Text.Trim();
                if (!string.Equals(oldDiaDiem, newDiaDiem, StringComparison.Ordinal))
                    return true;
            }

            return false;
        }

        private void SendEmailNotifyRemoved(string email, string fullName, string tenCongViec)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("kocoemail367@gmail.com");
                mail.Subject = "[EMTs]-[Thông báo có sự thay đổi phân công công việc]";
                mail.Body = "CÓ CÔNG VIỆC ĐƯỢC THAY ĐỔI. VUI LÒNG TRUY CẬP ỨNG DỤNG EMTs ĐỂ CẬP NHẬT"; 

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("kocoemail367@gmail.com", "uzan ioyf eskt ukoy");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi mail thông báo: " + ex.Message);
            }
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string tenCV = txt_TenCongViec.Text.Trim();
                if (string.IsNullOrWhiteSpace(tenCV))
                {
                    MessageBox.Show("Tên công việc không được để trống.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_TenCongViec.Focus();
                    return;
                }

                if (_currentPlan != null)
                {
                    DateTime d = dtp_NgayNop.Value.Date;
                    if (d < _currentPlan.NgayBatDau.Date || d > _currentPlan.NgayKetThuc.Date)
                    {
                        MessageBox.Show("Ngày nộp phải nằm trong thời gian của kế hoạch.", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string lyDo = txt_LyDoSua.Text.Trim();
                if (string.IsNullOrWhiteSpace(lyDo))
                {
                    var ask = MessageBox.Show("Bạn chưa nhập lý do sửa. Tiếp tục lưu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ask != DialogResult.Yes)
                        return;
                }

                bool chiTietThayDoi = IsChiTietPanelChanged();

                if (chiTietThayDoi && PhanCongBUS.HasAssignments(_congViecId))
                {
                    var askClear = MessageBox.Show(
                        "Công việc này đã được phân công...\n" +
                        "YES: Xóa toàn bộ phân công và gửi thông báo.\n" +
                        "NO: Giữ phân công, chỉ thông báo thay đổi.\n" +
                        "CANCEL: Hủy thao tác.",
                        "Xác nhận",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (askClear == DialogResult.Cancel)
                    {
                        return;
                    }

                    var assignedUsers = PhanCongBUS.GetAssignedUsersByCongViec(_congViecId);

                    // Người dùng chọn YES → xoá phân công
                    if (askClear == DialogResult.Yes)
                    {
                        if (!PhanCongBUS.DeleteAllByCongViecID(_congViecId))
                        {
                            MessageBox.Show("Không thể xoá phân công cũ.");
                            return;
                        }
                    }

                    // Cả YES và NO đều có thể gửi mail
                    if (assignedUsers != null && assignedUsers.Count > 0)
                    {
                        var askMail = MessageBox.Show(
                            "Bạn có muốn gửi email thông báo cho giảng viên?",
                            "Gửi thông báo",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (askMail == DialogResult.Yes)
                        {
                            foreach (var user in assignedUsers)
                            {
                                if (!string.IsNullOrWhiteSpace(user.Email))
                                    SendEmailNotifyRemoved(user.Email, user.FullName, tenCV);
                            }
                        }
                    }
                }


                var dto = new CongViecDTO
                {
                    CongViecID = _currentCV.CongViecID,
                    KeHoachID = _currentCV.KeHoachID,
                    NguoiGiaoID = _currentCV.NguoiGiaoID,
                    CreatedAt = _currentCV.CreatedAt,
                    TenCongViec = tenCV,
                    Loai = _currentCV.Loai,
                    MucUuTien = cbb_MucUuTien.SelectedValue?.ToString() ?? _currentCV.MucUuTien,
                    HanChot = dtp_NgayNop.Value.Date,
                    TrangThai = _currentCV.TrangThai
                };

                string oldNote = _currentCV.MoTa ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(lyDo))
                {
                    string timeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    string extra = "\r\n[" + timeStamp + "] " + lyDo;
                    dto.MoTa = oldNote + extra;
                }
                else
                {
                    dto.MoTa = oldNote;
                }

                dto.MonHocID = _currentCV.MonHocID;
                dto.LopPhuTrach = _currentCV.LopPhuTrach;
                dto.SoTiet = _currentCV.SoTiet;
                dto.MaDeTai = _currentCV.MaDeTai;
                dto.DiaDiem = _currentCV.DiaDiem;

                if (_currentCV.Loai == "GiangDay" && pnl_GiangDay != null)
                {
                    int? newMonHocId = null;
                    if (cbb_LopPhuTrach.SelectedValue != null && cbb_LopPhuTrach.SelectedValue != DBNull.Value)
                        newMonHocId = Convert.ToInt32(cbb_LopPhuTrach.SelectedValue);

                    dto.MonHocID = newMonHocId;
                    dto.LopPhuTrach = cbb_LopPhuTrach.Text.Trim();
                    dto.SoTiet = (int)num_SoTiet.Value;
                }
                else if (_currentCV.Loai == "NghienCuu" && pnl_NghienCuu != null)
                {
                    dto.MaDeTai = txt_MaDeTai.Text.Trim();
                }
                else if (_currentCV.Loai == "SuKien" && pnl_SuKien != null)
                {
                    dto.DiaDiem = txt_DiaDiem.Text.Trim();
                }

                bool ok = CongViecBUS.UpdateCongViec(dto);
                if (ok)
                {
                    MessageBox.Show("Cập nhật công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật công việc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi lưu công việc:\n" + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanelVisibility();
        }

        private void cbb_MonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentCV.Loai != "GiangDay")
                return;

            if (cbb_MonHoc.SelectedItem is DataRowView drv)
            {
                string maMonHoc = drv["MaMonHoc"].ToString();
                LoadLopPhuTrachComboGiangDay(maMonHoc, _currentCV.MonHocID);
            }
        }

        private void cbb_LopPhuTrach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LopPhuTrach.SelectedItem is DataRowView drv && drv["SoTiet"] is int soTiet)
                num_SoTiet.Value = soTiet;
        }
    }
}