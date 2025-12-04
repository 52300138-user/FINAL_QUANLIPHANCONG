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

namespace WindowsFormsApp2.Forms.FormSetting.childSetting
{
    public partial class FormUpdateUser : Form
    {
        private UserDTO currentUser;
        public FormUpdateUser()
        {
            InitializeComponent();
        }

        private void UpdateUser_Load(object sender, EventArgs e)
        {
            this.Shown += FormUpdateUser_Shown;
        }

        private void FormUpdateUser_Shown(object sender, EventArgs e)
        {
            // Tải vai trò và khóa cứng
            comboBox_chucvi.Items.Clear();
            comboBox_chucvi.Items.AddRange(new object[] { "Giảng viên", "Trưởng Bộ Môn" });
            comboBox_chucvi.DropDownStyle = ComboBoxStyle.DropDownList; // Khóa

            try
            {
                // Lấy thông tin cá nhân của người dùng hiện tại
                // Lấy từ biến CurrentUserId đã được gán khi đăng nhập
                currentUser = UsersBUS.GetUserById(Program.CurrentUserId);

                if (currentUser == null)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy thông tin người dùng hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Fill dữ liệu từ DTO vào các controls
                textBox_hoten.Text = currentUser.FullName;
                textBox_tendn.Text = currentUser.UserName;
                textBox_email.Text = currentUser.Email;
                textBox_diachi.Text = currentUser.Address;
                textBox_sdt.Text = currentUser.SDT;

                // Chọn đúng Chức vụ (Dùng DTO.Role: GV/TBM)
                if (currentUser.Role == "GV")
                {
                    comboBox_chucvi.SelectedItem = "Giảng viên";
                }
                else if (currentUser.Role == "TBM")
                {
                    comboBox_chucvi.SelectedItem = "Trưởng Bộ Môn";
                }

                // Chọn đúng Giới tính
                if (currentUser.Gender == "Nam")
                {
                    radioButton_nam.Checked = true;
                }
                else
                {
                    radioButton_nu.Checked = true;
                }

                //  (Không cho sửa Role)
                textBox_tendn.ReadOnly = true;
                comboBox_chucvi.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Program.CurrentPerms.Contains("TBM"))
            {
                MessageBox.Show("Bạn không có quyền cập nhật thông tin cá nhân.", "Lỗi Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentUser.FullName = textBox_hoten.Text.Trim();
            currentUser.Email = textBox_email.Text.Trim();
            currentUser.Address = textBox_diachi.Text.Trim();
            currentUser.Gender = radioButton_nam.Checked ? "Nam" : "Nữ";
            currentUser.SDT = textBox_sdt.Text.Trim();
            try
            {
                bool result = UsersBUS.UpdateUser(currentUser);
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin cá nhân thành công!");
                    this.DialogResult = DialogResult.OK; // Ra tín hiệu cho FormSetting reload
                    this.Close();
                }
                else
                {
                    // Lỗi validation đã được BUS xử lý và show MessageBox
                    MessageBox.Show("Không thể cập nhật. Vui lòng kiểm tra lại dữ liệu nhập.", "Lỗi Cập nhật");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message);
            }
        }
    }
}
