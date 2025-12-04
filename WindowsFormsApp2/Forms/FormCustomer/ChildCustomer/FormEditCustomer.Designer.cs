namespace WindowsFormsApp2.Forms.ChildCustomer
{
    partial class FormEditCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_chucvi = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton_nu = new System.Windows.Forms.RadioButton();
            this.radioButton_nam = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_mk = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_diachi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_sdt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_tendn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_hoten = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.comboBox_chucvi);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.radioButton_nu);
            this.panel1.Controls.Add(this.radioButton_nam);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox_mk);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_diachi);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_sdt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox_email);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_tendn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_hoten);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(45, 85);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 334);
            this.panel1.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(387, 178);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Chức vị";
            // 
            // comboBox_chucvi
            // 
            this.comboBox_chucvi.Enabled = false;
            this.comboBox_chucvi.FormattingEnabled = true;
            this.comboBox_chucvi.Items.AddRange(new object[] {
            "Admin",
            "Người dùng"});
            this.comboBox_chucvi.Location = new System.Drawing.Point(529, 175);
            this.comboBox_chucvi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_chucvi.Name = "comboBox_chucvi";
            this.comboBox_chucvi.Size = new System.Drawing.Size(157, 28);
            this.comboBox_chucvi.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(293, 226);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 39);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cập nhật";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Giới tính";
            // 
            // radioButton_nu
            // 
            this.radioButton_nu.AutoSize = true;
            this.radioButton_nu.Location = new System.Drawing.Point(197, 178);
            this.radioButton_nu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton_nu.Name = "radioButton_nu";
            this.radioButton_nu.Size = new System.Drawing.Size(51, 24);
            this.radioButton_nu.TabIndex = 13;
            this.radioButton_nu.TabStop = true;
            this.radioButton_nu.Text = "Nữ";
            this.radioButton_nu.UseVisualStyleBackColor = true;
            // 
            // radioButton_nam
            // 
            this.radioButton_nam.AutoSize = true;
            this.radioButton_nam.Location = new System.Drawing.Point(115, 178);
            this.radioButton_nam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton_nam.Name = "radioButton_nam";
            this.radioButton_nam.Size = new System.Drawing.Size(65, 24);
            this.radioButton_nam.TabIndex = 12;
            this.radioButton_nam.TabStop = true;
            this.radioButton_nam.Text = "Nam";
            this.radioButton_nam.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(387, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mật khẩu";
            // 
            // textBox_mk
            // 
            this.textBox_mk.Location = new System.Drawing.Point(529, 135);
            this.textBox_mk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_mk.Name = "textBox_mk";
            this.textBox_mk.Size = new System.Drawing.Size(157, 27);
            this.textBox_mk.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Địa chỉ";
            // 
            // textBox_diachi
            // 
            this.textBox_diachi.Location = new System.Drawing.Point(115, 130);
            this.textBox_diachi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_diachi.Name = "textBox_diachi";
            this.textBox_diachi.Size = new System.Drawing.Size(157, 27);
            this.textBox_diachi.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(387, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Số điện thoại";
            // 
            // textBox_sdt
            // 
            this.textBox_sdt.Location = new System.Drawing.Point(529, 86);
            this.textBox_sdt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_sdt.Name = "textBox_sdt";
            this.textBox_sdt.Size = new System.Drawing.Size(157, 27);
            this.textBox_sdt.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email";
            // 
            // textBox_email
            // 
            this.textBox_email.Location = new System.Drawing.Point(115, 82);
            this.textBox_email.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_email.Name = "textBox_email";
            this.textBox_email.Size = new System.Drawing.Size(157, 27);
            this.textBox_email.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên đăng nhập";
            // 
            // textBox_tendn
            // 
            this.textBox_tendn.Location = new System.Drawing.Point(529, 41);
            this.textBox_tendn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_tendn.Name = "textBox_tendn";
            this.textBox_tendn.Size = new System.Drawing.Size(157, 27);
            this.textBox_tendn.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Họ tên";
            // 
            // textBox_hoten
            // 
            this.textBox_hoten.Location = new System.Drawing.Point(115, 38);
            this.textBox_hoten.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_hoten.Name = "textBox_hoten";
            this.textBox_hoten.Size = new System.Drawing.Size(157, 27);
            this.textBox_hoten.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(299, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 25);
            this.label8.TabIndex = 19;
            this.label8.Text = "Chỉnh sửa thông tin";
            // 
            // FormEditCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditCustomer";
            this.Text = "Chỉnh sửa thông tin người dùng";
            this.Load += new System.EventHandler(this.FormEditCustomer_Load);
            this.Shown += new System.EventHandler(this.FormEditCustomer_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_chucvi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton_nu;
        private System.Windows.Forms.RadioButton radioButton_nam;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_mk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_diachi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_sdt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_tendn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_hoten;
        private System.Windows.Forms.Label label8;
    }
}