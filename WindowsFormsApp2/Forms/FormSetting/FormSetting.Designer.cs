namespace WindowsFormsApp2.Forms.FormSetting
{
    partial class FormSetting
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
            this.btn_DangXuat = new System.Windows.Forms.Button();
            this.btn_HoTro = new System.Windows.Forms.Button();
            this.btn_CapNhat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_DangXuat);
            this.panel1.Controls.Add(this.btn_HoTro);
            this.panel1.Controls.Add(this.btn_CapNhat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 482);
            this.panel1.TabIndex = 36;
            // 
            // btn_DangXuat
            // 
            this.btn_DangXuat.BackColor = System.Drawing.Color.Salmon;
            this.btn_DangXuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DangXuat.Location = new System.Drawing.Point(0, 246);
            this.btn_DangXuat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_DangXuat.Name = "btn_DangXuat";
            this.btn_DangXuat.Size = new System.Drawing.Size(769, 123);
            this.btn_DangXuat.TabIndex = 38;
            this.btn_DangXuat.Text = "Đăng Xuất";
            this.btn_DangXuat.UseVisualStyleBackColor = false;
            this.btn_DangXuat.Click += new System.EventHandler(this.btn_DangXuat_Click);
            // 
            // btn_HoTro
            // 
            this.btn_HoTro.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_HoTro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_HoTro.Location = new System.Drawing.Point(0, 123);
            this.btn_HoTro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_HoTro.Name = "btn_HoTro";
            this.btn_HoTro.Size = new System.Drawing.Size(769, 123);
            this.btn_HoTro.TabIndex = 37;
            this.btn_HoTro.Text = "Hỗ Trợ";
            this.btn_HoTro.UseVisualStyleBackColor = true;
            this.btn_HoTro.Click += new System.EventHandler(this.btn_HoTro_Click);
            // 
            // btn_CapNhat
            // 
            this.btn_CapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_CapNhat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_CapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CapNhat.Location = new System.Drawing.Point(0, 0);
            this.btn_CapNhat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CapNhat.Name = "btn_CapNhat";
            this.btn_CapNhat.Size = new System.Drawing.Size(769, 123);
            this.btn_CapNhat.TabIndex = 36;
            this.btn_CapNhat.Text = "Cập Nhật Thông Tin";
            this.btn_CapNhat.UseVisualStyleBackColor = false;
            this.btn_CapNhat.Click += new System.EventHandler(this.btn_CapNhat_Click);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 482);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_DangXuat;
        private System.Windows.Forms.Button btn_HoTro;
        private System.Windows.Forms.Button btn_CapNhat;
    }
}