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
    public partial class FormAddMonHoc : Form
    {
        private string nextTenNhom;
        private string nextTenTo;

        public FormAddMonHoc()
        {
            InitializeComponent();
        }

        private void FormAddMonHoc_Load(object sender, EventArgs e)
        {
            var hocKyList = new[]
                        {
                new { Key = "HocKyI", Value = "Học Kỳ I" },
                new { Key = "HocKyII", Value = "Học Kỳ II" },
                new { Key = "HocKyHe", Value = "Học Kỳ Hè" }
            };

            cbb_HocKy.DataSource = hocKyList;
            cbb_HocKy.DisplayMember = "Value"; // Tên hiển thị
            cbb_HocKy.ValueMember = "Key";    // Mã kỹ thuật (để lưu vào DB)
            cbb_HocKy.DropDownStyle = ComboBoxStyle.DropDownList;

            // 2. Tự động sinh/tính toán (Events)
            txt_MaMon.TextChanged += txt_MaMon_TextChanged;
            cbb_HocKy.SelectedIndexChanged += txt_MaMon_TextChanged;
            TinChi_LTH.ValueChanged += TinChi_LTH_ValueChanged; // Tín chỉ LT -> Tiết LT
            TinChi_TH.ValueChanged += TinChi_TH_ValueChanged; // Tín chỉ TH -> Tiết TH
            txt_MaMon.Leave += txt_MaMon_Leave;

            // 3. Khóa cứng và set mặc định
            txt_MaMon.ReadOnly = false;
            txt_TenNhom.ReadOnly = true;
            txt_TenTo.ReadOnly = true;
            txt_TenTo.Visible = false;

            TinChi_LTH.Value = 3; // Tín chỉ LT
            TinChi_TH.Value = 0; // Tín chỉ TH

            // 4. Load giá trị ban đầu
            UpdateNhomToNames();
            UpdateControlsState();
            SetToolTips();
        }
        private void UpdateNhomToNames()
        {
            string maMonHoc = txt_MaMon.Text.Trim();
            string hocKy = cbb_HocKy.SelectedValue?.ToString();

            if (string.IsNullOrWhiteSpace(maMonHoc) || string.IsNullOrWhiteSpace(hocKy))
            {
                txt_TenNhom.Text = "N01";
                txt_TenTo.Text = "T01";
                return;
            }

            var (tenNhom, tenTo) = MonHocBUS.GenerateNextNhomTo(maMonHoc, hocKy);

            txt_TenNhom.Text = tenNhom;
            txt_TenTo.Text = tenTo;
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
        private void TinChi_LTH_ValueChanged(object sender, EventArgs e)
        {
            int lt_tinChi = (int)TinChi_LTH.Value;
            UpdateControlsState();
            SoTiet_LTH.Value = MonHocBUS.CalculateSoTietLT(lt_tinChi);
            
        }

        private void TinChi_TH_ValueChanged(object sender, EventArgs e)
        {
            int th_tinChi = (int)TinChi_TH.Value;

            SoTiet_TH.Value = MonHocBUS.CalculateSoTietTH(th_tinChi);

            // Logic ẩn/hiện Tổ
            UpdateTenToVisibility(th_tinChi); UpdateControlsState();

            // Nếu có TH (lớn hơn 0), set Số Tổ mặc định là 1 và cập nhật hiển thị ngay lập tức
            if (th_tinChi > 0)
            {
                if (num_SoTo.Value < 1) // Ngăn chặn reset nếu người dùng đã đặt giá trị lớn hơn
                {
                    num_SoTo.Value = 1;
                }
                UpdateTenToDisplay((int)num_SoTo.Value);
            }
            else
            {
                num_SoTo.Value = 0;
                UpdateTenToDisplay(0);
            }
        }

        private void UpdateTenToVisibility(int soTinChiTH)
        {
            // txt_TenTo được sử dụng để hiển thị danh sách các tổ (T01, T02,...)
            // Nó chỉ hiện khi có tín chỉ thực hành
            txt_TenTo.Visible = (soTinChiTH > 0);

            // Kích hoạt/Vô hiệu hóa num_SoTo
            num_SoTo.Enabled = (soTinChiTH > 0);
        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            MonHocDTO dto = new MonHocDTO();

            try
            {
                // 1. Gom data
                dto.MaMonHoc = txt_MaMon.Text.Trim();
                dto.TenMonHoc = txt_TenMon.Text.Trim();
                dto.HocKy = cbb_HocKy.SelectedValue?.ToString();
                dto.TenNhom = txt_TenNhom.Text;
                dto.GhiChu = txt_MoTaMon.Text.Trim();

                // Lấy giá trị số (Tín chỉ là Input, Tiết là Output)
                dto.SoTinChi = (int)TinChi_LTH.Value + (int)TinChi_TH.Value;
                dto.SoTiet_LT = (int)SoTiet_LTH.Value;
                dto.SoTiet_TH = (int)SoTiet_TH.Value;

                // Lấy số lượng Tổ TH (tham số điều khiển vòng lặp)
                int soLopTH = (int)num_SoTo.Value;

                if (dto.SoTiet_TH > 0 && soLopTH < 1)
                {
                    MessageBox.Show("Môn học có Tiết Thực hành (SoTiet_TH > 0) phải có ít nhất 1 Tổ Thực hành (Số Tổ >= 1).", "Lỗi Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    num_SoTo.Focus();
                    return; // Dừng lại nếu validation thất bại
                }
                // 2. Gọi BUS
                if (MonHocBUS.AddMonHoc(dto, soLopTH))
                {
                    MessageBox.Show("Thêm môn học mới thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi lưu: " + ex.Message);
            }
        }

        private void button_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_MaMon_TextChanged(object sender, EventArgs e)
        {
            UpdateNhomToNames();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
        private void SetToolTips()
        {
            // Thêm hướng dẫn cho Mã Môn Học
            toolTip1.SetToolTip(label16,
                "Mã môn học (Ví dụ: IT4501). Dùng để phân biệt các học phần. KHÔNG PHẢI MÃ LỚP.\n LƯU Ý NHẬP ĐÚNG MÃ MÔN HỌC CHO CÁC MÔN HỌC GIỐNG NHAU");

            // Thêm hướng dẫn cho Tín chỉ LT
            toolTip1.SetToolTip(label6,
                "Tín chỉ Lý thuyết. Hệ thống sẽ tự tính: Tín chỉ LT x 15 = Số tiết LT.");

            // Thêm hướng dẫn cho Tín chỉ TH
            toolTip1.SetToolTip(label10,
                "Tín chỉ Thực hành. Hệ thống sẽ tự tính: Tín chỉ TH x 10 = Số tiết TH.");

            // Thêm hướng dẫn cho Tên Nhóm
            toolTip1.SetToolTip(label12,
                "Tên nhóm (Ví dụ: N01). Hệ thống tự sinh dựa trên Mã Môn Học và Học Kỳ.");

            // Thêm hướng dẫn cho Tên Tổ
            toolTip1.SetToolTip(label18,
                "Tên tổ (T01). Chỉ hiển thị và tự sinh khi Môn học có Thực hành (Số tiết TH > 0).");
        }

        private void txt_TenTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void num_SoTo_ValueChanged(object sender, EventArgs e)
        {
            UpdateTenToDisplay((int)num_SoTo.Value);
            UpdateTenToVisibility((int)TinChi_TH.Value);
        }

        private void UpdateTenToDisplay(int soLuongTo)
        {
            if (soLuongTo <= 0)
            {
                txt_TenTo.Text = "";
                return;
            }

            // KHÔNG cần dùng currentNhom để xác định startIndex nữa.
            // Tổ luôn bắt đầu từ T01.

            // Tạo danh sách các tên tổ: T01, T02, T03,...
            List<string> tenToGroup = new List<string>();

            // Vòng lặp luôn bắt đầu từ i = 1 (T01)
            for (int i = 1; i <= soLuongTo; i++)
            {
                // Sinh tên tổ T01, T02, T03, ...
                tenToGroup.Add("T" + i.ToString("D2"));
            }

            // Ghép các tên tổ lại bằng dấu phẩy
            txt_TenTo.Text = string.Join(", ", tenToGroup);
        }

        private void txt_MaMon_Leave(object sender, EventArgs e)
        {
            string maMonHoc = txt_MaMon.Text.Trim();
            if (string.IsNullOrWhiteSpace(maMonHoc)) return;

            // 1. Tìm tên môn học dựa trên mã
            string tenMonHoc = MonHocBUS.GetTenMonHocByMa(maMonHoc);

            // 2. Nếu tìm thấy và trường tên môn học đang trống, thì tự động điền
            if (!string.IsNullOrWhiteSpace(tenMonHoc) && string.IsNullOrWhiteSpace(txt_TenMon.Text))
            {
                txt_TenMon.Text = tenMonHoc;
                 toolTip1.Show("Tên môn học được tự động điền từ dữ liệu cũ.", txt_TenMon, 0, 0, 1000);
            }
        }
    }
}
