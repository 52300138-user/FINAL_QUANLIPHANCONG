namespace WindowsFormsApp2.Forms.FormPlan.ChildPlan
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
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbb_Loai = new System.Windows.Forms.ComboBox();
            this.NgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.NgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.btn_Them = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_TenKeHoach = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(293, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(186, 25);
            this.label8.TabIndex = 22;
            this.label8.Text = "Chỉnh sửa kế hoạch";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbb_Loai);
            this.panel1.Controls.Add(this.NgayKetThuc);
            this.panel1.Controls.Add(this.NgayBatDau);
            this.panel1.Controls.Add(this.btn_Them);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_TenKeHoach);
            this.panel1.ForeColor = System.Drawing.Color.Maroon;
            this.panel1.Location = new System.Drawing.Point(47, 98);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 289);
            this.panel1.TabIndex = 21;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cbb_Loai
            // 
            this.cbb_Loai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cbb_Loai.FormattingEnabled = true;
            this.cbb_Loai.Location = new System.Drawing.Point(155, 127);
            this.cbb_Loai.Margin = new System.Windows.Forms.Padding(4);
            this.cbb_Loai.Name = "cbb_Loai";
            this.cbb_Loai.Size = new System.Drawing.Size(157, 26);
            this.cbb_Loai.TabIndex = 18;
            // 
            // NgayKetThuc
            // 
            this.NgayKetThuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.NgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NgayKetThuc.Location = new System.Drawing.Point(500, 124);
            this.NgayKetThuc.Margin = new System.Windows.Forms.Padding(4);
            this.NgayKetThuc.Name = "NgayKetThuc";
            this.NgayKetThuc.Size = new System.Drawing.Size(174, 24);
            this.NgayKetThuc.TabIndex = 17;
            // 
            // NgayBatDau
            // 
            this.NgayBatDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.NgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NgayBatDau.Location = new System.Drawing.Point(500, 67);
            this.NgayBatDau.Margin = new System.Windows.Forms.Padding(4);
            this.NgayBatDau.Name = "NgayBatDau";
            this.NgayBatDau.Size = new System.Drawing.Size(174, 24);
            this.NgayBatDau.TabIndex = 16;
            // 
            // btn_Them
            // 
            this.btn_Them.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Them.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Them.Location = new System.Drawing.Point(305, 223);
            this.btn_Them.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Them.Name = "btn_Them";
            this.btn_Them.Size = new System.Drawing.Size(95, 39);
            this.btn_Them.TabIndex = 15;
            this.btn_Them.Text = "Xác nhận";
            this.btn_Them.UseVisualStyleBackColor = false;
            this.btn_Them.Click += new System.EventHandler(this.btn_Them_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(377, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ngày kết thúc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(377, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ngày bắt đầu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(47, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Loại";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(47, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên Kế Hoạch";
            // 
            // textBox_TenKeHoach
            // 
            this.textBox_TenKeHoach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox_TenKeHoach.Location = new System.Drawing.Point(155, 66);
            this.textBox_TenKeHoach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_TenKeHoach.Name = "textBox_TenKeHoach";
            this.textBox_TenKeHoach.Size = new System.Drawing.Size(157, 24);
            this.textBox_TenKeHoach.TabIndex = 0;
            // 
            // FormEditPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditPlan";
            this.Text = "Chỉnh sửa kế hoạch";
            this.Load += new System.EventHandler(this.FormEditPlan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbb_Loai;
        private System.Windows.Forms.DateTimePicker NgayKetThuc;
        private System.Windows.Forms.DateTimePicker NgayBatDau;
        private System.Windows.Forms.Button btn_Them;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_TenKeHoach;
    }
}