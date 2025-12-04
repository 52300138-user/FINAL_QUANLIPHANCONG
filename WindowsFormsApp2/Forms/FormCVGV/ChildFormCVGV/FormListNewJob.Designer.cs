namespace WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV
{
    partial class FormListNewJob
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button_Them = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_TenCongViec = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Tim = new Guna.UI2.WinForms.Guna2Button();
            this.cbb_HocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.button_Them);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 398);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 52);
            this.panel4.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.Location = new System.Drawing.Point(595, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "Từ chối";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Them
            // 
            this.button_Them.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button_Them.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button_Them.Location = new System.Drawing.Point(51, 2);
            this.button_Them.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Them.Name = "button_Them";
            this.button_Them.Size = new System.Drawing.Size(101, 39);
            this.button_Them.TabIndex = 1;
            this.button_Them.Text = "Đồng ý";
            this.button_Them.UseVisualStyleBackColor = false;
            this.button_Them.Click += new System.EventHandler(this.button_Them_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 142);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 292);
            this.panel3.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(800, 292);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.txt_TenCongViec);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btn_Tim);
            this.panel2.Controls.Add(this.cbb_HocKy);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 142);
            this.panel2.TabIndex = 0;
            // 
            // txt_TenCongViec
            // 
            this.txt_TenCongViec.Location = new System.Drawing.Point(187, 86);
            this.txt_TenCongViec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_TenCongViec.Name = "txt_TenCongViec";
            this.txt_TenCongViec.Size = new System.Drawing.Size(456, 22);
            this.txt_TenCongViec.TabIndex = 14;
            this.txt_TenCongViec.TextChanged += new System.EventHandler(this.txt_TenCongViec_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(61, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tên hoặc Mã";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btn_Tim
            // 
            this.btn_Tim.BorderRadius = 8;
            this.btn_Tim.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Tim.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Tim.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Tim.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Tim.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Tim.ForeColor = System.Drawing.Color.White;
            this.btn_Tim.Location = new System.Drawing.Point(651, 86);
            this.btn_Tim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Tim.Name = "btn_Tim";
            this.btn_Tim.Size = new System.Drawing.Size(89, 34);
            this.btn_Tim.TabIndex = 12;
            this.btn_Tim.Text = "Tìm";
            this.btn_Tim.Click += new System.EventHandler(this.btn_Tim_Click);
            // 
            // cbb_HocKy
            // 
            this.cbb_HocKy.BackColor = System.Drawing.Color.Transparent;
            this.cbb_HocKy.BorderRadius = 8;
            this.cbb_HocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbb_HocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_HocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbb_HocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbb_HocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbb_HocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbb_HocKy.ItemHeight = 30;
            this.cbb_HocKy.Location = new System.Drawing.Point(187, 14);
            this.cbb_HocKy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbb_HocKy.Name = "cbb_HocKy";
            this.cbb_HocKy.Size = new System.Drawing.Size(456, 36);
            this.cbb_HocKy.TabIndex = 11;
            this.cbb_HocKy.SelectedIndexChanged += new System.EventHandler(this.cbb_HocKy_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(60, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Học Kỳ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormListNewJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormListNewJob";
            this.Text = "Danh sách công việc mới";
            this.Load += new System.EventHandler(this.FormListNewJob_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_Them;
        private System.Windows.Forms.TextBox txt_TenCongViec;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Button btn_Tim;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_HocKy;
        private System.Windows.Forms.Label label1;
    }
}