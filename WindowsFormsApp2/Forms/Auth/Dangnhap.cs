using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Forms.Main;

namespace WindowsFormsApp2.Forms.Auth
{
    public partial class Dangnhap : Form
    {
        public int LoggedUserId { get; private set; } = 0;
        //public HashSet<string> LoggedPerms { get; private set; }  
        public string LoggedRole { get; private set; }

        public Dangnhap()
        {
            InitializeComponent();
            this.AcceptButton = button_dangnhap; // Enter để login
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            guna2PanelLogin.BorderRadius = 20;
            guna2PanelLogin.BorderColor = Color.White;
            guna2PanelLogin.FillColor = Color.FromArgb(150,0,0,0);
            guna2PanelLogin.BackColor = Color.Transparent;

            var fill = Color.Gainsboro;
            Textbox_taikhoan.BorderRadius = 0;
            Textbox_taikhoan.BorderThickness = 0;
            Textbox_taikhoan.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            Textbox_taikhoan.FillColor = fill;

            Textbox_matkhau.BorderRadius = 0;
            Textbox_matkhau.BorderThickness = 0;
            Textbox_matkhau.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            Textbox_matkhau.FillColor = fill;
            Textbox_matkhau.UseSystemPasswordChar = true;

            if (Properties.Settings.Default.RememberMe)
            {
                Textbox_taikhoan.Text = Properties.Settings.Default.SavedUser;
                checkbox_giutrangthai.Checked = true;
            }
        }
        public void panel2_Paint(object sender, EventArgs e) { }
        public void label7_Click(object sender, EventArgs e) { }
        public void guna2PanelLogin_Paint(object sender, EventArgs e) { }



           
        public void linkLabel_dangky_LinkClicked_1(object sender, EventArgs e) { }
       

        private void ToggleUI(bool enabled)
        {
            button_dangnhap.Enabled = enabled;
            Textbox_taikhoan.Enabled = enabled;
            Textbox_matkhau.Enabled = enabled;
            Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void linkLabel_dangky_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Dangky().ShowDialog(this);
        }

        private void linkLabel_quenmk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Quenmatkhau().ShowDialog(this);
        }

        private void button_dangnhap_Click_1(object sender, EventArgs e)
        {
            var acc = Textbox_taikhoan.Text.Trim();
            var pw = Textbox_matkhau.Text;

            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }

            try
            {
                ToggleUI(false);
                var (ok, userId, role, msg) = AuthBUS.Login(acc, pw);

                if (!ok)
                {
                    MessageBox.Show(msg, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoggedUserId = userId;
                LoggedRole = role; // Lấy Role (TBM hoặc GV) từ AuthBUS
 
                // Remember me 
                if (checkbox_giutrangthai.Checked)
                {
                    Properties.Settings.Default.SavedUser = acc;
                    Properties.Settings.Default.RememberMe = true;
                }
                else
                {
                    Properties.Settings.Default.SavedUser = "";
                    Properties.Settings.Default.RememberMe = false;
                }
                Properties.Settings.Default.Save();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
            finally
            {
                ToggleUI(true);
            }
        }

        private void guna2PanelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel_dangky_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dangky newForm = new Dangky();
            newForm.ShowDialog();
        }

        private void linkLabel_quenmk_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Quenmatkhau newForm = new Quenmatkhau();
            newForm.ShowDialog();

        }
    }
}
