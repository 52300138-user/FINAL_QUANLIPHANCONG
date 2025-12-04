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
using WindowsFormsApp2.Common;
using WindowsFormsApp2.DTO;
using WindowsFormsApp2.Forms.FormSetting.childSetting;
using WindowsFormsApp2.Forms.Main;

namespace WindowsFormsApp2.Forms.FormSetting
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {

            this.AutoScaleMode = AutoScaleMode.None;

            InitializeComponent();

            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.ForeColor = Color.Black;
                    //btn.BackColor = ThemeColor.PrimaryColor;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }



        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form MyInfomation như child form trong Form Setting
                var myInfoForm = new childSetting.MyInfomation.MyInfomation(Program.CurrentUserId);
                myInfoForm.TopLevel = false;
                myInfoForm.FormBorderStyle = FormBorderStyle.None;
                myInfoForm.Dock = DockStyle.Fill;

                this.Controls.Clear();
                this.Controls.Add(myInfoForm);
                myInfoForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở thông tin cá nhân: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_HoTro_Click(object sender, EventArgs e)
        {
            string readmeContent = @"
            Hệ thống Quản lý Công việc & Phân công Giảng viên (CalSmart)
            Để được hỗ trợ tốt nhất, liên hệ với chúng tôi thông qua gmail:
            abc@gmail.com
            hoặc:
            xyz@gmail.com
            ";

            MessageBox.Show(readmeContent, "Hỗ trợ & Thông tin Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            var parentForm = this.ParentForm;
            if(parentForm != null)
            {
                parentForm.Close();

            }
            else
            {
                Application.Exit();
            }
        }
    }
}
