namespace WindowsFormsApp2.Forms.FormCustomer
{
    partial class FormCustomers
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsLocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.button_them = new Guna.UI2.WinForms.Guna2Button();
            this.button_sua = new Guna.UI2.WinForms.Guna2Button();
            this.button_xoa = new Guna.UI2.WinForms.Guna2Button();
            this.button_khoa = new Guna.UI2.WinForms.Guna2Button();
            this.button_mokhoa = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FullName,
            this.UserName,
            this.Email,
            this.Address,
            this.Gender,
            this.Role,
            this.CreatedAt,
            this.SDT,
            this.IsLocked});
            this.dataGridView1.Location = new System.Drawing.Point(47, 74);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(572, 369);
            this.dataGridView1.TabIndex = 44;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            // 
            // FullName
            // 
            this.FullName.HeaderText = "Họ và tên";
            this.FullName.MinimumWidth = 6;
            this.FullName.Name = "FullName";
            // 
            // UserName
            // 
            this.UserName.HeaderText = "Tên đăng nhập";
            this.UserName.MinimumWidth = 6;
            this.UserName.Name = "UserName";
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            // 
            // Address
            // 
            this.Address.HeaderText = "Địa Chỉ";
            this.Address.MinimumWidth = 6;
            this.Address.Name = "Address";
            // 
            // Gender
            // 
            this.Gender.HeaderText = "Giới tính";
            this.Gender.MinimumWidth = 6;
            this.Gender.Name = "Gender";
            // 
            // Role
            // 
            this.Role.HeaderText = "Chức vị";
            this.Role.MinimumWidth = 6;
            this.Role.Name = "Role";
            // 
            // CreatedAt
            // 
            this.CreatedAt.HeaderText = "Ngày tạo";
            this.CreatedAt.MinimumWidth = 6;
            this.CreatedAt.Name = "CreatedAt";
            // 
            // SDT
            // 
            this.SDT.HeaderText = "SDT";
            this.SDT.MinimumWidth = 6;
            this.SDT.Name = "SDT";
            // 
            // IsLocked
            // 
            this.IsLocked.HeaderText = "Trạng thái";
            this.IsLocked.MinimumWidth = 6;
            this.IsLocked.Name = "IsLocked";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(218, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(266, 29);
            this.label5.TabIndex = 40;
            this.label5.Text = "Danh sách người dùng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // button_them
            // 
            this.button_them.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_them.BorderRadius = 8;
            this.button_them.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_them.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_them.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_them.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_them.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button_them.ForeColor = System.Drawing.Color.Black;
            this.button_them.Location = new System.Drawing.Point(646, 59);
            this.button_them.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_them.Name = "button_them";
            this.button_them.Size = new System.Drawing.Size(105, 40);
            this.button_them.TabIndex = 47;
            this.button_them.Text = "Thêm";
            this.button_them.Click += new System.EventHandler(this.button_them_Click);
            // 
            // button_sua
            // 
            this.button_sua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sua.BorderRadius = 8;
            this.button_sua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_sua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_sua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_sua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_sua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button_sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button_sua.ForeColor = System.Drawing.Color.Black;
            this.button_sua.Location = new System.Drawing.Point(647, 119);
            this.button_sua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sua.Name = "button_sua";
            this.button_sua.Size = new System.Drawing.Size(105, 40);
            this.button_sua.TabIndex = 48;
            this.button_sua.Text = "Sửa";
            this.button_sua.Click += new System.EventHandler(this.button_sua_Click_1);
            // 
            // button_xoa
            // 
            this.button_xoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_xoa.BorderRadius = 8;
            this.button_xoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_xoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_xoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_xoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_xoa.FillColor = System.Drawing.Color.Maroon;
            this.button_xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button_xoa.ForeColor = System.Drawing.Color.Black;
            this.button_xoa.Location = new System.Drawing.Point(647, 176);
            this.button_xoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_xoa.Name = "button_xoa";
            this.button_xoa.Size = new System.Drawing.Size(105, 40);
            this.button_xoa.TabIndex = 49;
            this.button_xoa.Text = "Xóa";
            this.button_xoa.Click += new System.EventHandler(this.button_xoa_Click);
            // 
            // button_khoa
            // 
            this.button_khoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_khoa.BorderRadius = 8;
            this.button_khoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_khoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_khoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_khoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_khoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_khoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button_khoa.ForeColor = System.Drawing.Color.Black;
            this.button_khoa.Location = new System.Drawing.Point(646, 234);
            this.button_khoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_khoa.Name = "button_khoa";
            this.button_khoa.Size = new System.Drawing.Size(105, 40);
            this.button_khoa.TabIndex = 50;
            this.button_khoa.Text = "Khóa";
            this.button_khoa.Click += new System.EventHandler(this.button_khoa_Click_1);
            // 
            // button_mokhoa
            // 
            this.button_mokhoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_mokhoa.BorderRadius = 8;
            this.button_mokhoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.button_mokhoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.button_mokhoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.button_mokhoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.button_mokhoa.FillColor = System.Drawing.Color.Green;
            this.button_mokhoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.button_mokhoa.ForeColor = System.Drawing.Color.Black;
            this.button_mokhoa.Location = new System.Drawing.Point(647, 302);
            this.button_mokhoa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_mokhoa.Name = "button_mokhoa";
            this.button_mokhoa.Size = new System.Drawing.Size(105, 40);
            this.button_mokhoa.TabIndex = 51;
            this.button_mokhoa.Text = "Mở khóa";
            this.button_mokhoa.Click += new System.EventHandler(this.button_mokhoa_Click);
            // 
            // FormCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(769, 482);
            this.Controls.Add(this.button_mokhoa);
            this.Controls.Add(this.button_khoa);
            this.Controls.Add(this.button_xoa);
            this.Controls.Add(this.button_sua);
            this.Controls.Add(this.button_them);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormCustomers";
            this.Text = "Danh sách người dùng";
            this.Load += new System.EventHandler(this.FormCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsLocked;
        private Guna.UI2.WinForms.Guna2Button button_them;
        private Guna.UI2.WinForms.Guna2Button button_sua;
        private Guna.UI2.WinForms.Guna2Button button_xoa;
        private Guna.UI2.WinForms.Guna2Button button_khoa;
        private Guna.UI2.WinForms.Guna2Button button_mokhoa;
    }
}