namespace WindowsFormsApp2.Forms.Auth
{
    partial class FormConfigSQL
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDatabase;

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGuide;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGuide = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblServer
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(30, 30);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(109, 20);
            this.lblServer.Text = "Server / Instance:";

            // txtServer
            this.txtServer.Location = new System.Drawing.Point(160, 27);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(260, 27);

            // lblDatabase
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(30, 85);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(74, 20);
            this.lblDatabase.Text = "Database:";

            // txtDatabase
            this.txtDatabase.Location = new System.Drawing.Point(160, 82);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(260, 27);
            this.txtDatabase.Text = "QuanLiPhanCong";

            // btnGuide
            this.btnGuide.Location = new System.Drawing.Point(30, 140);
            this.btnGuide.Name = "btnGuide";
            this.btnGuide.Size = new System.Drawing.Size(110, 35);
            this.btnGuide.Text = "Hướng dẫn";
            this.btnGuide.UseVisualStyleBackColor = true;
            this.btnGuide.Click += new System.EventHandler(this.btnGuide_Click);

            // btnTest
            this.btnTest.Location = new System.Drawing.Point(160, 140);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(120, 35);
            this.btnTest.Text = "Test kết nối";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(300, 140);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "Lưu && Restart";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // FormConfigSQL
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(460, 210);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnGuide);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình SQL Server";
            this.Load += new System.EventHandler(this.FormConfigSQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
