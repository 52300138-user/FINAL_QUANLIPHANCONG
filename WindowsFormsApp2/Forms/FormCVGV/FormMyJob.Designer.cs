namespace WindowsFormsApp2.Forms.FormCVGV
{
    partial class FormMyJob
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_TraLai = new Guna.UI2.WinForms.Guna2Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_them = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_TraLai);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 300);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(800, 100);
            this.panel5.TabIndex = 4;
            // 
            // btn_TraLai
            // 
            this.btn_TraLai.BorderRadius = 8;
            this.btn_TraLai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_TraLai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_TraLai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_TraLai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_TraLai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_TraLai.FillColor = System.Drawing.Color.Navy;
            this.btn_TraLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_TraLai.ForeColor = System.Drawing.Color.White;
            this.btn_TraLai.Location = new System.Drawing.Point(0, 0);
            this.btn_TraLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_TraLai.Name = "btn_TraLai";
            this.btn_TraLai.Size = new System.Drawing.Size(800, 100);
            this.btn_TraLai.TabIndex = 5;
            this.btn_TraLai.Text = "Công việc trả lại";
            this.btn_TraLai.Click += new System.EventHandler(this.guna2Button4_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.guna2Button3);
            this.panel4.Controls.Add(this.guna2Button2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 100);
            this.panel4.TabIndex = 3;
            // 
            // guna2Button3
            // 
            this.guna2Button3.BorderRadius = 8;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Button3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.guna2Button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Location = new System.Drawing.Point(0, 0);
            this.guna2Button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(800, 100);
            this.guna2Button3.TabIndex = 4;
            this.guna2Button3.Text = "Công việc đã nộp";
            this.guna2Button3.Click += new System.EventHandler(this.guna2Button3_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 8;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2Button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Location = new System.Drawing.Point(0, 0);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(800, 100);
            this.guna2Button2.TabIndex = 3;
            this.guna2Button2.Text = "Công việc đã nộp";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.guna2Button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 100);
            this.panel3.TabIndex = 0;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 8;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.guna2Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(0, 0);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(800, 100);
            this.guna2Button1.TabIndex = 2;
            this.guna2Button1.Text = "Công việc đang làm";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_them);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 0;
            // 
            // button_them
            // 
            this.button_them.BorderRadius = 8;
            this.button_them.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_them.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_them.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_them.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_them.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_them.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button_them.ForeColor = System.Drawing.Color.White;
            this.button_them.Location = new System.Drawing.Point(0, 0);
            this.button_them.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_them.Name = "button_them";
            this.button_them.Size = new System.Drawing.Size(800, 100);
            this.button_them.TabIndex = 1;
            this.button_them.Text = "Công việc mới";
            this.button_them.Click += new System.EventHandler(this.button_them_Click);
            // 
            // FormMyJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FormMyJob";
            this.Text = "Công việc của tôi";
            this.Load += new System.EventHandler(this.FormMyJob_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button button_them;
        private System.Windows.Forms.Panel panel4;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private System.Windows.Forms.Panel panel5;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button btn_TraLai;
    }
}