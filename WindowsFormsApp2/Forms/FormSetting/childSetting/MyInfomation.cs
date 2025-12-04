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

namespace WindowsFormsApp2.Forms.FormSetting.childSetting.MyInfomation
{
    public partial class MyInfomation : Form
    {
        private int _currentUserId;
        private UserDTO currentUser;

        public MyInfomation(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
        }

        private void txt_HoVaTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_GioiTinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_SoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_DiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_TenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_MatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_VaiTro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_NgayTao_TextChanged(object sender, EventArgs e)
        {

        }

        private void MyInfomation_Load(object sender, EventArgs e)
        {

        }

        private void MyInfomation_Shown(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin user
                currentUser = UsersBUS.GetUserById(_currentUserId);

                if (currentUser == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // ---- HIỂN THỊ DỮ LIỆU USER ----
                txt_HoVaTen.Text = currentUser.FullName;
                txt_TenDangNhap.Text = currentUser.UserName;
                txt_Email.Text = currentUser.Email;
                txt_DiaChi.Text = currentUser.Address;
                txt_SoDienThoai.Text = currentUser.SDT;
                txt_GioiTinh.Text = currentUser.Gender;

                txt_VaiTro.Text = currentUser.Role == "TBM" ?
                                    "Trưởng Bộ Môn" : "Giảng viên";

                if (currentUser.CreatedAt != null)
                { 
                    if (currentUser.CreatedAt > new DateTime(1900, 1, 1))
                    {
                        txt_NgayTao.Text = currentUser.CreatedAt.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txt_NgayTao.Text = "Chưa cập nhật";
                    }
                }


                // ---- QUẢN LÝ QUYỀN TRUY CẬP ----
                bool isGV = currentUser.Role == "GV";
                ApplyPermissionToControls(this, isGV);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }
        // Hàm duyệt toàn form để set quyền theo Role
        private void ApplyPermissionToControls(Control parent, bool isGV)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.ReadOnly = isGV;

                if (ctrl.Name == "label13")
                    ctrl.Visible = isGV;

                if (ctrl.HasChildren)
                    ApplyPermissionToControls(ctrl, isGV);

                //btn_Luu.Visible = false;
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            var frm = new DoiMatKhau(Program.CurrentUserId);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}

