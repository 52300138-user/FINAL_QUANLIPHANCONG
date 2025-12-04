using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // <-- Giữ nguyên using của bro
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.ChildCustomer
{

    public partial class FormEditCustomer : Form
    {
        private int userId; 
        public FormEditCustomer()
        {
            InitializeComponent();
        }
        public FormEditCustomer(int UserId) : this()
        {
            this.userId = UserId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = new UserDTO
            {
                UserID = userId,
                FullName = textBox_hoten.Text.Trim(),
                UserName = textBox_tendn.Text.Trim(),
                Email = textBox_email.Text.Trim(),
                Address = textBox_diachi.Text.Trim(),
                Gender = radioButton_nam.Checked ? "Nam" : "Nữ",

                
                Role = comboBox_chucvi.SelectedItem?.ToString(), // <-- Sửa

                SDT = textBox_sdt.Text.Trim()
            };

            try
            {
         
                bool result = UsersBUS.UpdateUser(user);
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin người dùng thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật. (BUS trả về false)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật người dùng: " + ex.Message);
            }
        }

        private void FormEditCustomer_Shown(object sender, EventArgs e)
        {
            comboBox_chucvi.Items.Clear();
            comboBox_chucvi.Items.AddRange(new object[] { "Giảng viên", "Trưởng Bộ Môn" });
            comboBox_chucvi.DropDownStyle = ComboBoxStyle.DropDownList; // Khóa
            try
            {
                UserDTO user = UsersBUS.GetUserById(this.userId);

                if (user == null)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin người dùng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Fill dữ liệu từ DTO vào các controls
                textBox_hoten.Text = user.FullName;
                textBox_tendn.Text = user.UserName;
                textBox_email.Text = user.Email;
                textBox_diachi.Text = user.Address;
                textBox_sdt.Text = user.SDT;

                // Chọn đúng Chức vụ trong ComboBox
                if (user.Role == "GV")
                {
                    comboBox_chucvi.SelectedItem = "Giảng viên";
                }
                else if (user.Role == "TBM")
                {
                    comboBox_chucvi.SelectedItem = "Trưởng Bộ Môn";
                }

                // Chọn đúng Giới tính
                if (user.Gender == "Nam")
                {
                    radioButton_nam.Checked = true;
                }
                else
                {
                    radioButton_nu.Checked = true;
                }

                // Logic: Tên đăng nhập là duy nhất, nên khóa không cho sửa
                textBox_tendn.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void FormEditCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}