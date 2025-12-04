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

namespace WindowsFormsApp2.Forms.FormPlan.ChildPlan
{
    public partial class FormEditPlan : Form
    {
        private int currentPlanId;
        private PlanDTO currentPlan;

        public FormEditPlan(int planId)
        {
            InitializeComponent();
            this.currentPlanId = planId;
        }
        private void NgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            NgayKetThuc.Value = NgayBatDau.Value.AddMonths(3);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ controls
            var dto = new PlanDTO
            {
                KeHoachID = this.currentPlanId, // <-- Quan trọng
                TenKeHoach = textBox_TenKeHoach.Text,
                Loai = cbb_Loai.SelectedValue as string,
                NgayBatDau = NgayBatDau.Value,
                NgayKetThuc = NgayKetThuc.Value,
                NguoiTaoID = this.currentPlan.NguoiTaoID // Giữ nguyên người tạo cũ
            };

            try
            {
                // Gọi BUS (BUS sẽ tự validate)
                if (PlansBUS.UpdatePlan(dto))
                {
                    MessageBox.Show("Cập nhật kế hoạch thành công!");
                    DialogResult = DialogResult.OK; // Báo cho Form cha (FormPlan)
                    Close();
                }
                // Nếu false, BUS đã show lỗi
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi cập nhật kế hoạch: " + ex.Message);
            }
        }

        private void FormEditPlan_Load(object sender, EventArgs e)
        {
            // 1. Load ComboBox (Giống Form Add)
            var loaiKeHoachList = new[]
            {
                new { Key = "HocKy",  Value = "Kế hoạch học kỳ" },
                new { Key = "NamHoc", Value = "Kế hoạch năm học" },
                new { Key = "DeTai",  Value = "Kế hoạch đề tài nghiên cứu" },
                new { Key = "SuKien", Value = "Kế hoạch sự kiện học thuật" },
                new { Key = "Khac",   Value = "Khác" }
            };
            cbb_Loai.DataSource = loaiKeHoachList;
            cbb_Loai.DisplayMember = "Value";
            cbb_Loai.ValueMember = "Key";
            NgayBatDau.ValueChanged += NgayBatDau_ValueChanged;

            // 2. Load dữ liệu cũ của Kế hoạch
            try
            {
                this.currentPlan = PlansBUS.GetPlanById(this.currentPlanId);
                if (this.currentPlan == null)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy kế hoạch này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 3. Fill dữ liệu vào controls
                textBox_TenKeHoach.Text = this.currentPlan.TenKeHoach;
                cbb_Loai.SelectedValue = this.currentPlan.Loai; // Tự động chọn đúng
                NgayBatDau.Value = this.currentPlan.NgayBatDau;
                NgayKetThuc.Value = this.currentPlan.NgayKetThuc;

                // (Đổi tên nút Thêm -> Lưu)
                btn_Them.Text = "Lưu thay đổi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết kế hoạch: " + ex.Message);
                this.Close();
            }
        }
    }
}
