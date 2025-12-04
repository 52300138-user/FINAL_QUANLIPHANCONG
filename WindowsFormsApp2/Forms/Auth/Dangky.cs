using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DAL;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.Auth
{
    public partial class Dangky : Form
    {
        //string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
        //           AttachDbFilename=D:\cnpm\btl\DoAnGK-main\DoAnGK-main\WindowsFormsApp2 (2)\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;
        //           Integrated Security=True;
        //           Connect Timeout=30";
   //     private readonly string connStr =
   //@"Data Source=(LocalDB)\MSSQLLocalDB;
   //   AttachDbFilename=D:\2526_HK1\HK1_CNPM\CuoiKy\WindowsFormsApp2 (2)\WindowsFormsApp2 (2)\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;
   //   Integrated Security=True;Connect Timeout=30";


        public Dangky()
        {
           
            InitializeComponent();
            this.Load += FormLoad_Dangky;                 // Gán sự kiện Load
            button_dangky.Click += ButtonDangky_Click;    // Gán sự kiện đăng ký
                  
        }

        private void FormLoad_Dangky(object sender, EventArgs e)
        {
            // Thiết lập panel
            panel_dangky.BorderRadius = 20;
            panel_dangky.BorderColor = Color.White;
            panel_dangky.FillColor = Color.FromArgb(200, 27, 27, 27);
            panel_dangky.BackColor = Color.Transparent;

            Color fillColor = Color.Gainsboro;
            Guna.UI2.WinForms.Guna2TextBox[] textboxes = { textbox_hoten, textbox_tendangnhap, textbox_matkhau,
                textbox_xnmatkhau, textbox_email, textbox_diachi, textbox_sdt };

            foreach (var tb in textboxes)
            {
                tb.BorderRadius = 0;
                tb.BorderThickness = 0;
                tb.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
                tb.FocusedState.BorderColor = Color.Black;
                tb.HoverState.BorderColor = Color.Gray;
                tb.FillColor = fillColor;
                tb.ForeColor = Color.Black;
                tb.PlaceholderText = "Nhập nội dung...";
            }

            textbox_matkhau.UseSystemPasswordChar = true;
            textbox_xnmatkhau.UseSystemPasswordChar = true;
        }

        private void ButtonDangky_Click(object sender, EventArgs e)
        {
            // Bước 1: Chỉ thực hiện các validation CỦA RIÊNG UI
            if (textbox_matkhau.Text != textbox_xnmatkhau.Text)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!");
                return;
            }

           

            string gender = "";
            if (radioNam.Checked)
            {
                gender = "Nam";
            }
            else if (radioNu.Checked) 
            {
                gender = "Nữ";
            }
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }

            // Bước 2: Gom dữ liệu vào DTO
            var user = new UserDTO
            {
                FullName = textbox_hoten.Text,
                UserName = textbox_tendangnhap.Text,
                PassWord = textbox_matkhau.Text, // Gửi pass plain text
                Email = textbox_email.Text,
                Address = textbox_diachi.Text,
                Gender = gender,
                SDT = textbox_sdt.Text,

                // Mặc định vai trò khi đăng ký (BUS sẽ convert sang "GV")
                Role = "Giảng viên",
                IsLocked = false
                // (Không set CreatedAt, DAL sẽ tự set)
            };
            // Bước 3: Gọi BUS (BUS sẽ lo tất cả việc còn lại)
            try
            {
                // UsersBUS.AddUser đã bao gồm:
                // 1. Validate (rỗng, SĐT, email, trùng lặp...)
                // 2. Hash Password
                // 3. Gọi DAL
                bool result = UsersBUS.AddUser(user);

                if (result)
                {
                    MessageBox.Show("Đăng ký thành công!");
                    this.Close();
                }
                // Nếu result = false: Không cần show gì cả,
                // vì UsersBUS đã tự MessageBox.Show lỗi (VD: "Email đã tồn tại")
            }
            catch (Exception ex)
            {
                // Lỗi này là lỗi hệ thống (DB sập...), không phải lỗi nghiệp vụ
                MessageBox.Show("Lỗi hệ thống khi đăng ký: " + ex.Message);
            }

        }


        // Nếu panel_dangky có sự kiện Paint
        private void panel_dangky_Paint(object sender, PaintEventArgs e)
        {
            // Không cần xử lý, để trống
        }

        // Nếu có nút guna2Button2 và sự kiện Click
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Không cần xử lý, để trống
        }

        // Nếu có textbox guna2TextBox6 và sự kiện TextChanged
        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            // Không cần xử lý, để trống
        }

        // Nếu Form có sự kiện Load tên Form3cs_Load
        private void Form3cs_Load(object sender, EventArgs e)
        {
            // Không cần xử lý, để trống
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
