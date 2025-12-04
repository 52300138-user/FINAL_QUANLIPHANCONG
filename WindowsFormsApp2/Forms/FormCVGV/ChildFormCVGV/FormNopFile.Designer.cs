namespace WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV
{
    partial class FormNopFile
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
            this.lbl_File = new System.Windows.Forms.Label();
            this.btnChonFile = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNopKetQua = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_File
            // 
            this.lbl_File.AutoSize = true;
            this.lbl_File.Font = new System.Drawing.Font("Times New Roman", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_File.Location = new System.Drawing.Point(71, 82);
            this.lbl_File.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_File.Name = "lbl_File";
            this.lbl_File.Size = new System.Drawing.Size(526, 53);
            this.lbl_File.TabIndex = 0;
            this.lbl_File.Text = "VUI LÒNG CHỌN FILE !";
            // 
            // btnChonFile
            // 
            this.btnChonFile.Location = new System.Drawing.Point(16, 208);
            this.btnChonFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(100, 28);
            this.btnChonFile.TabIndex = 1;
            this.btnChonFile.Text = "Chọn File";
            this.btnChonFile.UseVisualStyleBackColor = true;
            this.btnChonFile.Click += new System.EventHandler(this.btnChonFile_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(525, 208);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNopKetQua
            // 
            this.btnNopKetQua.Location = new System.Drawing.Point(271, 208);
            this.btnNopKetQua.Margin = new System.Windows.Forms.Padding(4);
            this.btnNopKetQua.Name = "btnNopKetQua";
            this.btnNopKetQua.Size = new System.Drawing.Size(100, 28);
            this.btnNopKetQua.TabIndex = 2;
            this.btnNopKetQua.Text = "Nộp";
            this.btnNopKetQua.UseVisualStyleBackColor = true;
            this.btnNopKetQua.Click += new System.EventHandler(this.btnNopKetQua_Click);
            // 
            // FormNopFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(641, 251);
            this.Controls.Add(this.btnNopKetQua);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChonFile);
            this.Controls.Add(this.lbl_File);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormNopFile";
            this.Text = "Nộp File Báo Cáo";
            this.Load += new System.EventHandler(this.FormNopFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_File;
        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNopKetQua;
    }
}