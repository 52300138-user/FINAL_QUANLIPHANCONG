namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    partial class FormEditPlan
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbb_Loai = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pnl_GiangDay = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbb_LopPhuTrach = new System.Windows.Forms.ComboBox();
            this.num_SoTiet = new System.Windows.Forms.NumericUpDown();
            this.cbb_MonHoc = new System.Windows.Forms.ComboBox();
            this.pnl_SuKien = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DiaDiem = new System.Windows.Forms.TextBox();
            this.txt_LyDoSua = new System.Windows.Forms.TextBox();
            this.btn_XacNhan = new System.Windows.Forms.Button();
            this.pnl_NghienCuu = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_MaDeTai = new System.Windows.Forms.TextBox();
            this.cbb_MucUuTien = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_NguoiGiao = new System.Windows.Forms.TextBox();
            this.dtp_NgayNop = new System.Windows.Forms.DateTimePicker();
            this.dtp_NgayGiao = new System.Windows.Forms.DateTimePicker();
            this.txt_TenCongViec = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_GiangDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SoTiet)).BeginInit();
            this.pnl_SuKien.SuspendLayout();
            this.pnl_NghienCuu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 483);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.cbb_Loai);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.pnl_GiangDay);
            this.panel2.Controls.Add(this.pnl_SuKien);
            this.panel2.Controls.Add(this.txt_LyDoSua);
            this.panel2.Controls.Add(this.btn_XacNhan);
            this.panel2.Controls.Add(this.pnl_NghienCuu);
            this.panel2.Controls.Add(this.cbb_MucUuTien);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txt_NguoiGiao);
            this.panel2.Controls.Add(this.dtp_NgayNop);
            this.panel2.Controls.Add(this.dtp_NgayGiao);
            this.panel2.Controls.Add(this.txt_TenCongViec);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(406, 483);
            this.panel2.TabIndex = 0;
            // 
            // cbb_Loai
            // 
            this.cbb_Loai.FormattingEnabled = true;
            this.cbb_Loai.Location = new System.Drawing.Point(128, 27);
            this.cbb_Loai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbb_Loai.Name = "cbb_Loai";
            this.cbb_Loai.Size = new System.Drawing.Size(234, 21);
            this.cbb_Loai.TabIndex = 32;
            this.cbb_Loai.SelectedIndexChanged += new System.EventHandler(this.cbb_Loai_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(20, 345);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 15);
            this.label13.TabIndex = 31;
            this.label13.Text = "Lý Do Sửa";
            // 
            // pnl_GiangDay
            // 
            this.pnl_GiangDay.Controls.Add(this.label10);
            this.pnl_GiangDay.Controls.Add(this.label9);
            this.pnl_GiangDay.Controls.Add(this.label8);
            this.pnl_GiangDay.Controls.Add(this.cbb_LopPhuTrach);
            this.pnl_GiangDay.Controls.Add(this.num_SoTiet);
            this.pnl_GiangDay.Controls.Add(this.cbb_MonHoc);
            this.pnl_GiangDay.Location = new System.Drawing.Point(40, 165);
            this.pnl_GiangDay.Name = "pnl_GiangDay";
            this.pnl_GiangDay.Size = new System.Drawing.Size(265, 100);
            this.pnl_GiangDay.TabIndex = 26;
            this.pnl_GiangDay.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(-2, 38);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 15);
            this.label10.TabIndex = 31;
            this.label10.Text = "Lớp ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(-1, 80);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 30;
            this.label9.Text = "Số Tiết";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(-1, 4);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 15);
            this.label8.TabIndex = 29;
            this.label8.Text = "Môn Học";
            // 
            // cbb_LopPhuTrach
            // 
            this.cbb_LopPhuTrach.FormattingEnabled = true;
            this.cbb_LopPhuTrach.Location = new System.Drawing.Point(101, 38);
            this.cbb_LopPhuTrach.Name = "cbb_LopPhuTrach";
            this.cbb_LopPhuTrach.Size = new System.Drawing.Size(121, 21);
            this.cbb_LopPhuTrach.TabIndex = 28;
            this.cbb_LopPhuTrach.SelectedIndexChanged += new System.EventHandler(this.cbb_LopPhuTrach_SelectedIndexChanged);
            // 
            // num_SoTiet
            // 
            this.num_SoTiet.Location = new System.Drawing.Point(101, 80);
            this.num_SoTiet.Name = "num_SoTiet";
            this.num_SoTiet.Size = new System.Drawing.Size(120, 20);
            this.num_SoTiet.TabIndex = 26;
            // 
            // cbb_MonHoc
            // 
            this.cbb_MonHoc.FormattingEnabled = true;
            this.cbb_MonHoc.Location = new System.Drawing.Point(87, 3);
            this.cbb_MonHoc.Name = "cbb_MonHoc";
            this.cbb_MonHoc.Size = new System.Drawing.Size(176, 21);
            this.cbb_MonHoc.TabIndex = 24;
            this.cbb_MonHoc.SelectedIndexChanged += new System.EventHandler(this.cbb_MonHoc_SelectedIndexChanged);
            // 
            // pnl_SuKien
            // 
            this.pnl_SuKien.AutoSize = true;
            this.pnl_SuKien.Controls.Add(this.label1);
            this.pnl_SuKien.Controls.Add(this.txt_DiaDiem);
            this.pnl_SuKien.Location = new System.Drawing.Point(40, 165);
            this.pnl_SuKien.Name = "pnl_SuKien";
            this.pnl_SuKien.Size = new System.Drawing.Size(265, 100);
            this.pnl_SuKien.TabIndex = 26;
            this.pnl_SuKien.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(-1, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Địa Điểm";
            // 
            // txt_DiaDiem
            // 
            this.txt_DiaDiem.Location = new System.Drawing.Point(101, 38);
            this.txt_DiaDiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_DiaDiem.Name = "txt_DiaDiem";
            this.txt_DiaDiem.Size = new System.Drawing.Size(162, 20);
            this.txt_DiaDiem.TabIndex = 24;
            // 
            // txt_LyDoSua
            // 
            this.txt_LyDoSua.Location = new System.Drawing.Point(101, 345);
            this.txt_LyDoSua.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_LyDoSua.Multiline = true;
            this.txt_LyDoSua.Name = "txt_LyDoSua";
            this.txt_LyDoSua.Size = new System.Drawing.Size(263, 88);
            this.txt_LyDoSua.TabIndex = 29;
            // 
            // btn_XacNhan
            // 
            this.btn_XacNhan.BackColor = System.Drawing.Color.DimGray;
            this.btn_XacNhan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_XacNhan.Location = new System.Drawing.Point(173, 452);
            this.btn_XacNhan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_XacNhan.Name = "btn_XacNhan";
            this.btn_XacNhan.Size = new System.Drawing.Size(88, 32);
            this.btn_XacNhan.TabIndex = 28;
            this.btn_XacNhan.Text = "Xác nhận";
            this.btn_XacNhan.UseVisualStyleBackColor = false;
            this.btn_XacNhan.Click += new System.EventHandler(this.btn_XacNhan_Click);
            // 
            // pnl_NghienCuu
            // 
            this.pnl_NghienCuu.Controls.Add(this.label12);
            this.pnl_NghienCuu.Controls.Add(this.txt_MaDeTai);
            this.pnl_NghienCuu.Location = new System.Drawing.Point(40, 165);
            this.pnl_NghienCuu.Name = "pnl_NghienCuu";
            this.pnl_NghienCuu.Size = new System.Drawing.Size(265, 100);
            this.pnl_NghienCuu.TabIndex = 25;
            this.pnl_NghienCuu.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(-1, 40);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 15);
            this.label12.TabIndex = 26;
            this.label12.Text = "Mã Đề Tài";
            // 
            // txt_MaDeTai
            // 
            this.txt_MaDeTai.Location = new System.Drawing.Point(86, 40);
            this.txt_MaDeTai.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_MaDeTai.Name = "txt_MaDeTai";
            this.txt_MaDeTai.Size = new System.Drawing.Size(177, 20);
            this.txt_MaDeTai.TabIndex = 24;
            // 
            // cbb_MucUuTien
            // 
            this.cbb_MucUuTien.FormattingEnabled = true;
            this.cbb_MucUuTien.Location = new System.Drawing.Point(130, 130);
            this.cbb_MucUuTien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbb_MucUuTien.Name = "cbb_MucUuTien";
            this.cbb_MucUuTien.Size = new System.Drawing.Size(234, 21);
            this.cbb_MucUuTien.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(20, 130);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Mức Ưu Tiên";
            // 
            // txt_NguoiGiao
            // 
            this.txt_NguoiGiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_NguoiGiao.Location = new System.Drawing.Point(129, 97);
            this.txt_NguoiGiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_NguoiGiao.Name = "txt_NguoiGiao";
            this.txt_NguoiGiao.Size = new System.Drawing.Size(235, 21);
            this.txt_NguoiGiao.TabIndex = 14;
            // 
            // dtp_NgayNop
            // 
            this.dtp_NgayNop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtp_NgayNop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_NgayNop.Location = new System.Drawing.Point(124, 308);
            this.dtp_NgayNop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_NgayNop.Name = "dtp_NgayNop";
            this.dtp_NgayNop.Size = new System.Drawing.Size(97, 21);
            this.dtp_NgayNop.TabIndex = 13;
            // 
            // dtp_NgayGiao
            // 
            this.dtp_NgayGiao.CustomFormat = "";
            this.dtp_NgayGiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtp_NgayGiao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_NgayGiao.Location = new System.Drawing.Point(124, 277);
            this.dtp_NgayGiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_NgayGiao.Name = "dtp_NgayGiao";
            this.dtp_NgayGiao.Size = new System.Drawing.Size(97, 21);
            this.dtp_NgayGiao.TabIndex = 12;
            // 
            // txt_TenCongViec
            // 
            this.txt_TenCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_TenCongViec.Location = new System.Drawing.Point(128, 63);
            this.txt_TenCongViec.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_TenCongViec.Name = "txt_TenCongViec";
            this.txt_TenCongViec.Size = new System.Drawing.Size(236, 21);
            this.txt_TenCongViec.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(22, 316);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ngày nộp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(20, 281);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày giao";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(20, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Người giao";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(20, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Công Việc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(20, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loại kế hoạch";
            // 
            // FormEditPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 483);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormEditPlan";
            this.Text = "FormAddPlan";
            this.Load += new System.EventHandler(this.FormEditPlan_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnl_GiangDay.ResumeLayout(false);
            this.pnl_GiangDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SoTiet)).EndInit();
            this.pnl_SuKien.ResumeLayout(false);
            this.pnl_SuKien.PerformLayout();
            this.pnl_NghienCuu.ResumeLayout(false);
            this.pnl_NghienCuu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_NguoiGiao;
        private System.Windows.Forms.DateTimePicker dtp_NgayNop;
        private System.Windows.Forms.DateTimePicker dtp_NgayGiao;
        private System.Windows.Forms.TextBox txt_TenCongViec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_MucUuTien;
        private System.Windows.Forms.Panel pnl_NghienCuu;
        private System.Windows.Forms.TextBox txt_MaDeTai;
        private System.Windows.Forms.Panel pnl_SuKien;
        private System.Windows.Forms.TextBox txt_DiaDiem;
        private System.Windows.Forms.Panel pnl_GiangDay;
        private System.Windows.Forms.ComboBox cbb_LopPhuTrach;
        private System.Windows.Forms.NumericUpDown num_SoTiet;
        private System.Windows.Forms.ComboBox cbb_MonHoc;
        private System.Windows.Forms.TextBox txt_LyDoSua;
        private System.Windows.Forms.Button btn_XacNhan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbb_Loai;
    }
}