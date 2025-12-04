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

namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    public partial class FormThemGV_Detail : Form
    {
        private int currentCongViecId;

        // Constructor nhận ID Công việc (Không nhận UserID, vì UserID sẽ được chọn trong Form)
        public FormThemGV_Detail(int congViecId)
        {
            InitializeComponent();
            this.currentCongViecId = congViecId;
            this.Text = "Thêm/Tạo mới Giảng viên và Phân công";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_CongViec_TextChanged(object sender, EventArgs e)
        {
            // hiển thị công việc hiện tại
        }

        private void cbb_ChonGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox dùng để chọn giảng viên, nếu không chọn cho phép nhập tay
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Số điện thoại
        }

        private void lbl_UserID_Current_TextChanged(object sender, EventArgs e)
        {
            // current user id
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Email
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vai Trò (Chủ Trì, Hỗ Trợ)
        }

        private void txt_GhiChuTBM_TextChanged(object sender, EventArgs e)
        {
            // Ghi Chú
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void radioButton_nam_CheckedChanged(object sender, EventArgs e)
        {
            // radio Button nam và nữ
        }

        private void comboBox_chucvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // chuc vi của giảng viên, sẽ có Trưởng Bộ Môn (Người đang phân việc có thể tự phân việc cho bản thân họ), Giảng viên và Khác
        }

        private void FormThemGV_Detail_Load(object sender, EventArgs e)
        {
            LoadGiangVienComboBox();
        }

        private void LoadGiangVienComboBox()
        {
            try
            {
                DataTable dt = PhanCongBUS.GetGiangVienList();

                // Thêm dòng cho phép tạo mới
                DataRow dr = dt.NewRow();
                dr["UserID"] = 0;
                dr["FullName"] = "(Tạo mới Giảng viên)";
                dt.Rows.InsertAt(dr, 0);

                cbb_ChonGV.DataSource = dt;
                cbb_ChonGV.DisplayMember = "FullName";
                cbb_ChonGV.ValueMember = "UserID";
                cbb_ChonGV .SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách GV: " + ex.Message); }
        }

        private void button_them_Click(object sender, EventArgs e)
        {

        }
    }
}
