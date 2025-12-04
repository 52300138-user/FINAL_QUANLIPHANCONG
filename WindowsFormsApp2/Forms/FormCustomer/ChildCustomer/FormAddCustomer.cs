using System;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DTO;

// Xóa các using không cần thiết (DAL, SqlClient...)

namespace WindowsFormsApp2.Forms.ChildCustomer
{
    public partial class FormAddCustomer : Form
    {
        public FormAddCustomer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = textBox_hoten.Text;
            string email = textBox_email.Text;
            string diaChi = textBox_diachi.Text;
            string tenDangNhap = textBox_tendn.Text;
            string sdt = textBox_sdt.Text;
            string matKhau = textBox_mk.Text;

            string gioiTinh = radioButton_nam.Checked ? "Nam" : "Nữ";
            string chucVu = comboBox_chucvi.SelectedItem?.ToString();

            var user = new UserDTO
            {
                FullName = hoTen,
                Email = email,
                Address = diaChi,
                UserName = tenDangNhap,
                SDT = sdt,
                PassWord = matKhau,
                Gender = gioiTinh,
                Role = chucVu,
                IsLocked = false,
            };
            try
            {
                bool result = UsersBUS.AddUser(user);

                if (result)
                {
                    MessageBox.Show("Thêm người dùng thành công!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi thêm: " + ex.Message);
            }
        }

        private void FormAddCustomer_Load_1(object sender, EventArgs e)
        {

            radioButton_nam.Checked = true;

            comboBox_chucvi.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox_chucvi.Items.Clear();

            comboBox_chucvi.Items.Add("Giảng viên");

            bool tbmExists = false;
            try
            { 
                tbmExists = UsersBUS.CheckIfTBMExists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chức vụ: " + ex.Message);
                tbmExists = true;
            }

            if (!tbmExists)
            {
                comboBox_chucvi.Items.Add("Trưởng Bộ Môn");
            }
 
            if (comboBox_chucvi.Items.Count > 0)
            {
                comboBox_chucvi.SelectedIndex = 0; // Chọn "Giảng viên"
            }
        }
    }
}