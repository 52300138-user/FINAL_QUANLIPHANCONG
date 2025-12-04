namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    partial class FormThemGV_Detail
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
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_CongViec = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_ChonGV = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_GhiChuTBM = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_UserID_Current = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbb_Role = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButton_nu = new System.Windows.Forms.RadioButton();
            this.radioButton_nam = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.cbb_ChucVu = new System.Windows.Forms.ComboBox();
            this.button_them = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(11, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 25);
            this.label5.TabIndex = 57;
            this.label5.Text = "Cập Nhật Giảng Viên";
            // 
            // lbl_CongViec
            // 
            this.lbl_CongViec.Location = new System.Drawing.Point(94, 23);
            this.lbl_CongViec.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_CongViec.Name = "lbl_CongViec";
            this.lbl_CongViec.ReadOnly = true;
            this.lbl_CongViec.Size = new System.Drawing.Size(119, 20);
            this.lbl_CongViec.TabIndex = 4;
            this.lbl_CongViec.TextChanged += new System.EventHandler(this.lbl_CongViec_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tên Công Việc";
            // 
            // cbb_ChonGV
            // 
            this.cbb_ChonGV.FormattingEnabled = true;
            this.cbb_ChonGV.Location = new System.Drawing.Point(94, 83);
            this.cbb_ChonGV.Name = "cbb_ChonGV";
            this.cbb_ChonGV.Size = new System.Drawing.Size(121, 21);
            this.cbb_ChonGV.TabIndex = 8;
            this.cbb_ChonGV.SelectedIndexChanged += new System.EventHandler(this.cbb_ChonGV_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Giảng viên";
            // 
            // txt_GhiChuTBM
            // 
            this.txt_GhiChuTBM.Location = new System.Drawing.Point(94, 231);
            this.txt_GhiChuTBM.Margin = new System.Windows.Forms.Padding(2);
            this.txt_GhiChuTBM.Multiline = true;
            this.txt_GhiChuTBM.Name = "txt_GhiChuTBM";
            this.txt_GhiChuTBM.Size = new System.Drawing.Size(472, 89);
            this.txt_GhiChuTBM.TabIndex = 13;
            this.txt_GhiChuTBM.TextChanged += new System.EventHandler(this.txt_GhiChuTBM_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 234);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ghi Chú";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_them);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cbb_ChucVu);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.radioButton_nu);
            this.panel1.Controls.Add(this.radioButton_nam);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cbb_Role);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txt_GhiChuTBM);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbb_ChonGV);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbl_UserID_Current);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_CongViec);
            this.panel1.Location = new System.Drawing.Point(-7, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 385);
            this.panel1.TabIndex = 59;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lbl_UserID_Current
            // 
            this.lbl_UserID_Current.Location = new System.Drawing.Point(447, 23);
            this.lbl_UserID_Current.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_UserID_Current.Name = "lbl_UserID_Current";
            this.lbl_UserID_Current.Size = new System.Drawing.Size(119, 20);
            this.lbl_UserID_Current.TabIndex = 6;
            this.lbl_UserID_Current.TextChanged += new System.EventHandler(this.lbl_UserID_Current_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Người Giao";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Email";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(447, 83);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 138);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Số Điện Thoại";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(94, 138);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(119, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 179);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Vai Trò";
            // 
            // cbb_Role
            // 
            this.cbb_Role.FormattingEnabled = true;
            this.cbb_Role.Location = new System.Drawing.Point(445, 176);
            this.cbb_Role.Name = "cbb_Role";
            this.cbb_Role.Size = new System.Drawing.Size(121, 21);
            this.cbb_Role.TabIndex = 19;
            this.cbb_Role.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 184);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Giới tính";
            // 
            // radioButton_nu
            // 
            this.radioButton_nu.AutoSize = true;
            this.radioButton_nu.Location = new System.Drawing.Point(174, 182);
            this.radioButton_nu.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_nu.Name = "radioButton_nu";
            this.radioButton_nu.Size = new System.Drawing.Size(39, 17);
            this.radioButton_nu.TabIndex = 22;
            this.radioButton_nu.TabStop = true;
            this.radioButton_nu.Text = "Nữ";
            this.radioButton_nu.UseVisualStyleBackColor = true;
            // 
            // radioButton_nam
            // 
            this.radioButton_nam.AutoSize = true;
            this.radioButton_nam.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButton_nam.Location = new System.Drawing.Point(112, 182);
            this.radioButton_nam.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton_nam.Name = "radioButton_nam";
            this.radioButton_nam.Size = new System.Drawing.Size(47, 17);
            this.radioButton_nam.TabIndex = 21;
            this.radioButton_nam.TabStop = true;
            this.radioButton_nam.Text = "Nam";
            this.radioButton_nam.UseVisualStyleBackColor = false;
            this.radioButton_nam.CheckedChanged += new System.EventHandler(this.radioButton_nam_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 135);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Chức vị";
            // 
            // cbb_ChucVu
            // 
            this.cbb_ChucVu.FormattingEnabled = true;
            this.cbb_ChucVu.Items.AddRange(new object[] {
            "Admin",
            "Người dùng"});
            this.cbb_ChucVu.Location = new System.Drawing.Point(447, 135);
            this.cbb_ChucVu.Margin = new System.Windows.Forms.Padding(2);
            this.cbb_ChucVu.Name = "cbb_ChucVu";
            this.cbb_ChucVu.Size = new System.Drawing.Size(119, 21);
            this.cbb_ChucVu.TabIndex = 24;
            this.cbb_ChucVu.SelectedIndexChanged += new System.EventHandler(this.comboBox_chucvi_SelectedIndexChanged);
            // 
            // button_them
            // 
            this.button_them.BackColor = System.Drawing.Color.DimGray;
            this.button_them.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_them.Location = new System.Drawing.Point(266, 342);
            this.button_them.Margin = new System.Windows.Forms.Padding(2);
            this.button_them.Name = "button_them";
            this.button_them.Size = new System.Drawing.Size(71, 32);
            this.button_them.TabIndex = 26;
            this.button_them.Text = "Thêm mới";
            this.button_them.UseVisualStyleBackColor = false;
            this.button_them.Click += new System.EventHandler(this.button_them_Click);
            // 
            // FormThemGV_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Name = "FormThemGV_Detail";
            this.Text = "FormThemGV_Detail";
            this.Load += new System.EventHandler(this.FormThemGV_Detail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lbl_CongViec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_ChonGV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_GhiChuTBM;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lbl_UserID_Current;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbb_Role;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButton_nu;
        private System.Windows.Forms.RadioButton radioButton_nam;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbb_ChucVu;
        private System.Windows.Forms.Button button_them;
    }
}