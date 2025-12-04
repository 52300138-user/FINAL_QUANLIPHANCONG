namespace WindowsFormsApp2.Forms.Main
{
    partial class FormMainMenu_GV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnCloseChildForm = new System.Windows.Forms.Button();
            this.guna2ControlBox4 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDesktopPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.session1 = new DevExpress.Xpo.Session(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.gunaAreaDataset1 = new Guna.Charts.WinForms.GunaAreaDataset();
            this.gunaAreaDataset2 = new Guna.Charts.WinForms.GunaAreaDataset();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panelDesktopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.button1);
            this.panelMenu.Controls.Add(this.btnBaoCao);
            this.panelMenu.Controls.Add(this.btnNotifications);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 626);
            this.panelMenu.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button1.ForeColor = System.Drawing.Color.Gainsboro;
            this.button1.Image = global::WindowsFormsApp2.Properties.Resources.alarm__1_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 232);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(220, 63);
            this.button1.TabIndex = 3;
            this.button1.Text = " Môn Học";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnBaoCao.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCao.Image = global::WindowsFormsApp2.Properties.Resources.settings;
            this.btnBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.Location = new System.Drawing.Point(0, 162);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnBaoCao.Size = new System.Drawing.Size(220, 70);
            this.btnBaoCao.TabIndex = 2;
            this.btnBaoCao.Text = " Cài Đặt";
            this.btnBaoCao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnNotifications
            // 
            this.btnNotifications.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNotifications.FlatAppearance.BorderSize = 0;
            this.btnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnNotifications.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNotifications.Image = global::WindowsFormsApp2.Properties.Resources.alarm__1_;
            this.btnNotifications.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNotifications.Location = new System.Drawing.Point(0, 99);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNotifications.Size = new System.Drawing.Size(220, 63);
            this.btnNotifications.TabIndex = 1;
            this.btnNotifications.Text = " Công Việc Của Tôi";
            this.btnNotifications.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelLogo.Controls.Add(this.label10);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 99);
            this.panelLogo.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Italic);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(52, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 35);
            this.label10.TabIndex = 0;
            this.label10.Text = "ETMs";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panelTitleBar.Controls.Add(this.guna2ControlBox3);
            this.panelTitleBar.Controls.Add(this.btnCloseChildForm);
            this.panelTitleBar.Controls.Add(this.guna2ControlBox4);
            this.panelTitleBar.Controls.Add(this.guna2ControlBox2);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(787, 97);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTitleBar_Paint);
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(680, 3);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox3.TabIndex = 0;
            // 
            // btnCloseChildForm
            // 
            this.btnCloseChildForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCloseChildForm.FlatAppearance.BorderSize = 0;
            this.btnCloseChildForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseChildForm.Image = global::WindowsFormsApp2.Properties.Resources.cross_out__2_;
            this.btnCloseChildForm.Location = new System.Drawing.Point(0, 0);
            this.btnCloseChildForm.Name = "btnCloseChildForm";
            this.btnCloseChildForm.Size = new System.Drawing.Size(75, 97);
            this.btnCloseChildForm.TabIndex = 1;
            this.btnCloseChildForm.Click += new System.EventHandler(this.btnCloseChildForm_Click);
            // 
            // guna2ControlBox4
            // 
            this.guna2ControlBox4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2ControlBox4.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox4.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox4.Location = new System.Drawing.Point(755, 3);
            this.guna2ControlBox4.Name = "guna2ControlBox4";
            this.guna2ControlBox4.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox4.TabIndex = 2;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(717, 3);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox2.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(349, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(159, 34);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "TRANG CHỦ";
            // 
            // panelDesktopPanel
            // 
            this.panelDesktopPanel.Controls.Add(this.panel2);
            this.panelDesktopPanel.Controls.Add(this.pictureBox1);
            this.panelDesktopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPanel.Location = new System.Drawing.Point(220, 97);
            this.panelDesktopPanel.Name = "panelDesktopPanel";
            this.panelDesktopPanel.Size = new System.Drawing.Size(787, 529);
            this.panelDesktopPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::WindowsFormsApp2.Properties.Resources.ChatGPT_Image_Nov_16__2025__10_58_14_PM;
            this.pictureBox1.Location = new System.Drawing.Point(95, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(614, 347);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl2.UseTransparentDrag = true;
            // 
            // gunaAreaDataset1
            // 
            this.gunaAreaDataset1.BorderColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset1.FillColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset1.Label = "Area1";
            // 
            // gunaAreaDataset2
            // 
            this.gunaAreaDataset2.BorderColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset2.FillColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset2.Label = "Area2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 41);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xin chào!";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(664, 2);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 34);
            this.button5.TabIndex = 0;
            this.button5.Text = "Đăng xuất";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FormMainMenu_GV
            // 
            this.ClientSize = new System.Drawing.Size(1007, 626);
            this.Controls.Add(this.panelDesktopPanel);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainMenu_GV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMainMenu_GV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainMenu_GV_FormClosing);
            this.Load += new System.EventHandler(this.FormMainMenu_GV_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelDesktopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnNotifications;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelDesktopPanel;
        private System.Windows.Forms.Button btnCloseChildForm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private DevExpress.Xpo.Session session1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox4;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.Charts.WinForms.GunaAreaDataset gunaAreaDataset1;
        private System.Windows.Forms.Button btnBaoCao;
        private Guna.Charts.WinForms.GunaAreaDataset gunaAreaDataset2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
    }
}
