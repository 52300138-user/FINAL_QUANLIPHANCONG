using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DAL;

namespace WindowsFormsApp2.Forms.Auth
{
    public partial class Quenmatkhau : Form
    {
        public Quenmatkhau()
        {
            InitializeComponent();
            // this.Load += Quenmatkhau_Load;

            // button_gui.Click += button_gui_Click;
            // button_dmk.Click += button_dmk_Click;
            // button_huy.Click += (s, e) => this.Close();
          
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Không cần xử lý gì cũng được
        }

        private void button_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Quenmatkhau_Load(object sender, EventArgs e)
        {
            guna2Panel1.BorderRadius = 20;
            guna2Panel1.BorderColor = Color.White;
            guna2Panel1.FillColor = Color.FromArgb(200, 27, 27, 27);
            guna2Panel1.BackColor = Color.Transparent;

            var textboxes = new[] { textbox_tk, textbox_otp, textbox_mkm, textbox_xnmk };
            foreach (var tb in textboxes)
            {
                tb.BorderRadius = 0;
                tb.BorderThickness = 0;
                tb.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
                tb.FocusedState.BorderColor = Color.Black;
                tb.HoverState.BorderColor = Color.Gray;
                tb.FillColor = Color.Gainsboro;
                tb.ForeColor = Color.Black;
                tb.PlaceholderText = "Nhập nội dung...";
            }
            textbox_mkm.UseSystemPasswordChar = true;
            textbox_xnmk.UseSystemPasswordChar = true;
        }

        // GỬI OTP
        private void button_gui_Click(object sender, EventArgs e)
        {
            string acc = textbox_tk.Text.Trim();
            if (string.IsNullOrWhiteSpace(acc))
            {
                MessageBox.Show("Vui lòng nhập Email hoặc Tài khoản!");
                return;
            }

            // SỬA: Gọi AuthBUS (thay vì UsersBUS)
            var (ok, msg, email) = AuthBUS.ValidateAccount(acc);
            if (!ok)
            {
                MessageBox.Show(msg);
                return;
            }

            try
            {
                // SỬA: Gọi AuthBUS
                var (otp, expireAt) = AuthBUS.GenerateOTP();
                AuthBUS.SendOTP(email, otp, expireAt);

                MessageBox.Show("✅ OTP đã gửi đến Email (hiệu lực 5 phút)!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi OTP thất bại: " + ex.Message);
            }
        }

        private void button_dmk_Click(object sender, EventArgs e)
        {
            try
            {
                // SỬA: Gọi AuthBUS
                AuthBUS.ValidateOTP(textbox_otp.Text.Trim());

                if (textbox_mkm.Text != textbox_xnmk.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!");
                    return;
                }

                // SỬA: Gọi AuthBUS
                if (AuthBUS.ChangePassword(textbox_tk.Text.Trim(), textbox_mkm.Text.Trim()))
                {
                    MessageBox.Show("✅ Đổi mật khẩu thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ Không thể đổi mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                // (AuthBUS.ValidateOTP ném exception nếu sai)
                MessageBox.Show(ex.Message);
            }
        }
    }
}
