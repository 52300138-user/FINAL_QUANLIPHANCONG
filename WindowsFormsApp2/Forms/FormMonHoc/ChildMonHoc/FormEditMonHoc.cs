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

namespace WindowsFormsApp2.Forms.FormMonHoc.ChildMonHoc
{
    public partial class FormEditMonHoc : Form
    {
        private int currentMonHocId;
        private MonHocDTO currentMH;

        public FormEditMonHoc(int monHocId)
        {
            InitializeComponent();
            this.currentMonHocId = monHocId;
            this.Shown += FormEditMonHoc_Shown;
        }

        private void FormEditMonHoc_Shown(object sender, EventArgs e)
        {
            try
            {
                this.currentMH = MonHocBUS.GetMonHocById(this.currentMonHocId);
                if (currentMH == null)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy môn học để sửa.");
                    this.Close();
                    return;
                }

                // 1. Hủy Event trước khi gán data
                TinChi_LTH.ValueChanged -= TinChi_LTH_ValueChanged;
                TinChi_TH.ValueChanged -= TinChi_TH_ValueChanged;

                // 2. TÍNH NGƯỢC TÍN CHỈ (Input)
                int soTinChiLT = (int)Math.Round((double)currentMH.SoTiet_LT / 15);
                int soTinChiTH = (int)Math.Round((double)currentMH.SoTiet_TH / 10);

                // 3. Gán giá trị vào Input Controls (Tín chỉ)
                TinChi_LTH.Value = soTinChiLT;
                TinChi_TH.Value = soTinChiTH;

                // 4. Gán giá trị vào Output Controls (Số tiết - Dùng giá trị chính xác từ DB)
                SoTiet_LTH.Value = currentMH.SoTiet_LT;
                SoTiet_TH.Value = currentMH.SoTiet_TH;

                // 5. Gắn Event lại
                TinChi_LTH.ValueChanged += TinChi_LTH_ValueChanged;
                TinChi_TH.ValueChanged += TinChi_TH_ValueChanged;

                // 6. Fill data ReadOnly/Text
                txt_MaMon.Text = currentMH.MaMonHoc;
                txt_MaMon.ReadOnly = true;
                txt_TenNhom.Text = currentMH.TenNhom;
                txt_TenNhom.ReadOnly = true;
                txt_TenTo.Text = currentMH.TenTo;
                txt_TenTo.ReadOnly = true;

                txt_TenMon.Text = currentMH.TenMonHoc;
                txt_MoTaMon.Text = currentMH.GhiChu;

                cbb_HocKy.SelectedValue = currentMH.HocKy; // Gán bằng Mã Key
                cbb_HocKy.Enabled = false;

                // 7. Cập nhật trạng thái Tổ
                UpdateTenToVisibility(soTinChiTH);
                UpdateControlsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi Hệ Thống");
                this.Close();
            }
        }

        private void LoadHocKyComboBox()
        {
            var hocKyList = new List<object>
            {
                // Thêm một item rỗng để tránh lỗi SelectedValue ban đầu nếu cần
                new { Key = (string)null, Value = "(Chọn Học Kỳ)" },
                new { Key = "HocKyI", Value = "Học Kỳ I" },
                new { Key = "HocKyII", Value = "Học Kỳ II" },
                new { Key = "HocKyHe", Value = "Học Kỳ Hè" }
            };

            cbb_HocKy.DataSource = hocKyList;
            cbb_HocKy.DisplayMember = "Value";
            cbb_HocKy.ValueMember = "Key";
            cbb_HocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            // ===================================
        }

        private void TinChi_LTH_ValueChanged(object sender, EventArgs e)
        {
            // Ngăn vòng lặp (GUARD)
            TinChi_LTH.ValueChanged -= TinChi_LTH_ValueChanged;

            int lt_tinChi = (int)TinChi_LTH.Value;
            SoTiet_LTH.Value = MonHocBUS.CalculateSoTietLT(lt_tinChi); // Gán vào control Tiết
            UpdateControlsState();
            // Gắn lại Event
            TinChi_LTH.ValueChanged += TinChi_LTH_ValueChanged;
        }

        private void TinChi_TH_ValueChanged(object sender, EventArgs e)
        {
            // Ngăn vòng lặp (GUARD)
            TinChi_TH.ValueChanged -= TinChi_TH_ValueChanged;

            int th_tinChi = (int)TinChi_TH.Value;
            SoTiet_TH.Value = MonHocBUS.CalculateSoTietTH(th_tinChi); // Gán vào control Tiết

            UpdateTenToVisibility(th_tinChi); UpdateControlsState();

            // Gắn lại Event
            TinChi_TH.ValueChanged += TinChi_TH_ValueChanged;
        }


        private void UpdateTenToVisibility(int soTinChiTH)
        {
            if (soTinChiTH > 0)
            {
                txt_TenTo.Visible = true;
            }
            else
            {
                txt_TenTo.Visible = false;
            }
        }
        private void UpdateControlsState()
        {

            // Tín chỉ LT > 0 => Cho phép xem/sửa SoTiet_LT
            bool isLtEnabled = TinChi_LTH.Value > 0;
            SoTiet_LTH.Enabled = isLtEnabled;

            // Tín chỉ TH > 0 => Cho phép xem/sửa SoTiet_TH và Số Tổ
            bool isThEnabled = TinChi_TH.Value > 0;
            SoTiet_TH.Enabled = isThEnabled;

            // Khóa/Mở Số Tổ nếu có tín chỉ TH
            // Cần đảm bảo num_SoTo tồn tại
            num_SoTo.Enabled = isThEnabled;

            // Nếu bị khóa (Tín chỉ = 0), set giá trị về 0 để đảm bảo dữ liệu sạch
            if (!isLtEnabled) SoTiet_LTH.Value = 0;
            if (!isThEnabled)
            {
                SoTiet_TH.Value = 0;
                // Khóa num_SoTo lại
                num_SoTo.Value = 0;
            }
        }

        private void button_Huy_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            MonHocDTO dto = new MonHocDTO();

            try
            {
                // 1. Gom data (Lấy từ các trường Input và ReadOnly)
                dto.MonHocID = this.currentMonHocId;
                dto.MaMonHoc = txt_MaMon.Text;
                dto.TenMonHoc = txt_TenMon.Text.Trim();
                dto.HocKy = cbb_HocKy.SelectedValue?.ToString(); ;

                dto.TenNhom = txt_TenNhom.Text;
                dto.TenTo = txt_TenTo.Visible ? txt_TenTo.Text : null;
                dto.GhiChu = txt_MoTaMon.Text.Trim();

                // 2. Tính tổng tín chỉ (Lấy từ input Tín chỉ)
                dto.SoTinChi = (int)TinChi_LTH.Value + (int)TinChi_TH.Value;
                // 3. Lấy Số tiết (Lấy từ output SoTiet_LTH/TH) <-- FIXED
                dto.SoTiet_LT = (int)SoTiet_LTH.Value;
                dto.SoTiet_TH = (int)SoTiet_TH.Value;

                DialogResult result = MessageBox.Show(
                        "Bạn có muốn áp dụng thay đổi TẤT CẢ các môn có cùng Mã Môn Học (" + dto.MaMonHoc + ") trong (" + dto.HocKy + ") không?\n\nChọn 'Yes' để áp dụng hàng loạt.\nChọn 'No' để chỉ áp dụng cho lớp hiện tại.\nChọn 'Cancel' để hủy bỏ.",
                        "Xác nhận Cập nhật Môn học",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    return; // Hủy bỏ thao tác
                }

                bool isBulkUpdate = (result == DialogResult.Yes);

                // 4. Gọi BUS với lựa chọn của người dùng
                if (MonHocBUS.UpdateMonHoc(dto, isBulkUpdate))
                {
                    MessageBox.Show("Cập nhật môn học thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi lưu: " + ex.Message, "Lỗi Ứng Dụng");
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormEditMonHoc_Load(object sender, EventArgs e)
        {
            LoadHocKyComboBox();

            // Gán sự kiện tính toán tín chỉ
            TinChi_LTH.ValueChanged += TinChi_LTH_ValueChanged;
            TinChi_TH.ValueChanged += TinChi_TH_ValueChanged;
        }

    }
}
