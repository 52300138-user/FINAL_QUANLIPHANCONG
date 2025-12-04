using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;  // Gọi BUS
using WindowsFormsApp2.DTO;
using WindowsFormsApp2.Forms.FormPlan;

namespace WindowsFormsApp2.Forms.FormPlan.ChildPlan
{
    public partial class FormAddPlan : Form
    {
        public FormAddPlan()
        {
            InitializeComponent();
        }

        private void NgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            NgayKetThuc.Value = NgayBatDau.Value.AddMonths(3);
        }

        private void FormAddPlan_Load(object sender, EventArgs e)
        {
            var loaiKeHoachList = new[]
            {
                new { Key = "HocKyI",  Value = "Kế hoạch học kỳ I" },
                new { Key = "HocKyII",  Value = "Kế hoạch học kỳ II" },
                new { Key = "HocKyHe",  Value = "Kế hoạch học kỳ Hè" },
                new { Key = "NamHoc", Value = "Kế hoạch năm học" },
                new { Key = "DeTai",  Value = "Kế hoạch đề tài nghiên cứu" },
                new { Key = "SuKien", Value = "Kế hoạch sự kiện học thuật" },
                new { Key = "Khac",   Value = "Khác" }
            };

            cbb_Loai.DataSource = loaiKeHoachList;
            cbb_Loai.DisplayMember = "Value";
            cbb_Loai.ValueMember = "Key";
            cbb_Loai.SelectedIndex = 0;

            NgayBatDau.Value = DateTime.Today;
            NgayKetThuc.Value = NgayBatDau.Value.AddMonths(3);

            NgayBatDau.ValueChanged += NgayBatDau_ValueChanged;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            var dto = new PlanDTO
            {
                TenKeHoach = textBox_TenKeHoach.Text, // Gửi thẳng, BUS sẽ .Trim()
                Loai = cbb_Loai.SelectedValue as string,
                NgayBatDau = NgayBatDau.Value,
                NgayKetThuc = NgayKetThuc.Value,
                NguoiTaoID = Program.CurrentUserId // Giả sử Program.CurrentUserId là ID (int) của TBM
            };

            try
            {
                // Gọi BUS (BUS sẽ tự validate và show MessageBox nếu lỗi)
                if (PlansBUS.AddPlan(dto))
                {
                    MessageBox.Show("Thêm kế hoạch thành công!");
                    DialogResult = DialogResult.OK; // Báo cho Form cha (FormPlan) biết để refresh
                    Close();
                }
                // Nếu false, không cần làm gì, BUS đã show lỗi
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi thêm kế hoạch: " + ex.Message);
            }

        }




        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbb_Loai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}