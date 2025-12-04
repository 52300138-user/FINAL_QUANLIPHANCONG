namespace WindowsFormsApp2.Forms.FormQLCV.ChildPlans
{
    partial class FormDetailPlan
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_XuatExcel = new System.Windows.Forms.Button();
            this.btn_Thoat = new System.Windows.Forms.Button();
            this.btn_XuatPdf = new System.Windows.Forms.Button();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NhiemVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThaiGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGianNop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_MucUuTien = new System.Windows.Forms.TextBox();
            this.aa = new System.Windows.Forms.Label();
            this.txt_GhiChu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_NguoiGiao = new System.Windows.Forms.TextBox();
            this.dtp_NgayNop = new System.Windows.Forms.DateTimePicker();
            this.dtp_NgayGiao = new System.Windows.Forms.DateTimePicker();
            this.txt_TenCV = new System.Windows.Forms.TextBox();
            this.txt_LoaiKeHoach = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.is_BoMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_NhiemVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_HoTenGV = new Guna.UI2.WinForms.Guna2TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_GioiTinh = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox6 = new Guna.UI2.WinForms.Guna2TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_SDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_EmailGV = new Guna.UI2.WinForms.Guna2TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.dgvChiTiet);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(524, 373);
            this.panel4.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_XuatExcel);
            this.panel6.Controls.Add(this.btn_Thoat);
            this.panel6.Controls.Add(this.btn_XuatPdf);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 290);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(524, 83);
            this.panel6.TabIndex = 1;
            // 
            // btn_XuatExcel
            // 
            this.btn_XuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_XuatExcel.Location = new System.Drawing.Point(108, 44);
            this.btn_XuatExcel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_XuatExcel.Name = "btn_XuatExcel";
            this.btn_XuatExcel.Size = new System.Drawing.Size(101, 29);
            this.btn_XuatExcel.TabIndex = 2;
            this.btn_XuatExcel.Text = "Xuất File Excel";
            this.btn_XuatExcel.UseVisualStyleBackColor = false;
            this.btn_XuatExcel.Click += new System.EventHandler(this.btn_XuatExcel_Click);
            // 
            // btn_Thoat
            // 
            this.btn_Thoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_Thoat.Location = new System.Drawing.Point(428, 44);
            this.btn_Thoat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Thoat.Name = "btn_Thoat";
            this.btn_Thoat.Size = new System.Drawing.Size(81, 29);
            this.btn_Thoat.TabIndex = 1;
            this.btn_Thoat.Text = "Thoát";
            this.btn_Thoat.UseVisualStyleBackColor = false;
            this.btn_Thoat.Click += new System.EventHandler(this.btn_Thoat_Click);
            // 
            // btn_XuatPdf
            // 
            this.btn_XuatPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_XuatPdf.Location = new System.Drawing.Point(10, 44);
            this.btn_XuatPdf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_XuatPdf.Name = "btn_XuatPdf";
            this.btn_XuatPdf.Size = new System.Drawing.Size(83, 29);
            this.btn_XuatPdf.TabIndex = 0;
            this.btn_XuatPdf.Text = "Xuất File PDF";
            this.btn_XuatPdf.UseVisualStyleBackColor = false;
            this.btn_XuatPdf.Click += new System.EventHandler(this.btn_XuatPdf_Click);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.HoTen,
            this.ChucVu,
            this.NhiemVu,
            this.NgayBatDau,
            this.NgayKetThuc,
            this.TrangThaiGV,
            this.Column9,
            this.Column10,
            this.ThoiGianNop});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 0);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(524, 373);
            this.dgvChiTiet.TabIndex = 0;
            this.dgvChiTiet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellClick);
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // HoTen
            // 
            this.HoTen.HeaderText = "Họ và tên";
            this.HoTen.MinimumWidth = 6;
            this.HoTen.Name = "HoTen";
            this.HoTen.ReadOnly = true;
            // 
            // ChucVu
            // 
            this.ChucVu.HeaderText = "Chức vụ";
            this.ChucVu.MinimumWidth = 6;
            this.ChucVu.Name = "ChucVu";
            this.ChucVu.ReadOnly = true;
            // 
            // NhiemVu
            // 
            this.NhiemVu.HeaderText = "Nhiệm vụ";
            this.NhiemVu.MinimumWidth = 6;
            this.NhiemVu.Name = "NhiemVu";
            this.NhiemVu.ReadOnly = true;
            // 
            // NgayBatDau
            // 
            this.NgayBatDau.HeaderText = "Ngày bắt đầu";
            this.NgayBatDau.MinimumWidth = 6;
            this.NgayBatDau.Name = "NgayBatDau";
            this.NgayBatDau.ReadOnly = true;
            // 
            // NgayKetThuc
            // 
            this.NgayKetThuc.HeaderText = "Ngày kết thúc";
            this.NgayKetThuc.MinimumWidth = 6;
            this.NgayKetThuc.Name = "NgayKetThuc";
            this.NgayKetThuc.ReadOnly = true;
            // 
            // TrangThaiGV
            // 
            this.TrangThaiGV.HeaderText = "Trạng thái";
            this.TrangThaiGV.MinimumWidth = 6;
            this.TrangThaiGV.Name = "TrangThaiGV";
            this.TrangThaiGV.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Ghi chú";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Số lần cập nhật";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            // 
            // ThoiGianNop
            // 
            this.ThoiGianNop.HeaderText = "Thời gian nộp";
            this.ThoiGianNop.MinimumWidth = 6;
            this.ThoiGianNop.Name = "ThoiGianNop";
            this.ThoiGianNop.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.txt_MucUuTien);
            this.panel2.Controls.Add(this.aa);
            this.panel2.Controls.Add(this.txt_GhiChu);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txt_NguoiGiao);
            this.panel2.Controls.Add(this.dtp_NgayNop);
            this.panel2.Controls.Add(this.dtp_NgayGiao);
            this.panel2.Controls.Add(this.txt_TenCV);
            this.panel2.Controls.Add(this.txt_LoaiKeHoach);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(810, 118);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // txt_MucUuTien
            // 
            this.txt_MucUuTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_MucUuTien.Location = new System.Drawing.Point(372, 82);
            this.txt_MucUuTien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_MucUuTien.Name = "txt_MucUuTien";
            this.txt_MucUuTien.ReadOnly = true;
            this.txt_MucUuTien.Size = new System.Drawing.Size(101, 21);
            this.txt_MucUuTien.TabIndex = 19;
            // 
            // aa
            // 
            this.aa.AutoSize = true;
            this.aa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.aa.Location = new System.Drawing.Point(281, 84);
            this.aa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.aa.Name = "aa";
            this.aa.Size = new System.Drawing.Size(88, 15);
            this.aa.TabIndex = 18;
            this.aa.Text = "Mức Ưu Tiên";
            // 
            // txt_GhiChu
            // 
            this.txt_GhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_GhiChu.Location = new System.Drawing.Point(539, 9);
            this.txt_GhiChu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_GhiChu.Multiline = true;
            this.txt_GhiChu.Name = "txt_GhiChu";
            this.txt_GhiChu.ReadOnly = true;
            this.txt_GhiChu.Size = new System.Drawing.Size(252, 98);
            this.txt_GhiChu.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(485, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Ghi chú";
            // 
            // txt_NguoiGiao
            // 
            this.txt_NguoiGiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_NguoiGiao.Location = new System.Drawing.Point(98, 82);
            this.txt_NguoiGiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_NguoiGiao.Name = "txt_NguoiGiao";
            this.txt_NguoiGiao.ReadOnly = true;
            this.txt_NguoiGiao.Size = new System.Drawing.Size(176, 21);
            this.txt_NguoiGiao.TabIndex = 14;
            // 
            // dtp_NgayNop
            // 
            this.dtp_NgayNop.Enabled = false;
            this.dtp_NgayNop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtp_NgayNop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_NgayNop.Location = new System.Drawing.Point(374, 46);
            this.dtp_NgayNop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_NgayNop.Name = "dtp_NgayNop";
            this.dtp_NgayNop.Size = new System.Drawing.Size(101, 21);
            this.dtp_NgayNop.TabIndex = 13;
            // 
            // dtp_NgayGiao
            // 
            this.dtp_NgayGiao.CustomFormat = "";
            this.dtp_NgayGiao.Enabled = false;
            this.dtp_NgayGiao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtp_NgayGiao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_NgayGiao.Location = new System.Drawing.Point(374, 11);
            this.dtp_NgayGiao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_NgayGiao.Name = "dtp_NgayGiao";
            this.dtp_NgayGiao.Size = new System.Drawing.Size(101, 21);
            this.dtp_NgayGiao.TabIndex = 12;
            // 
            // txt_TenCV
            // 
            this.txt_TenCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_TenCV.Location = new System.Drawing.Point(40, 48);
            this.txt_TenCV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_TenCV.Name = "txt_TenCV";
            this.txt_TenCV.ReadOnly = true;
            this.txt_TenCV.Size = new System.Drawing.Size(235, 21);
            this.txt_TenCV.TabIndex = 11;
            // 
            // txt_LoaiKeHoach
            // 
            this.txt_LoaiKeHoach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txt_LoaiKeHoach.Location = new System.Drawing.Point(40, 13);
            this.txt_LoaiKeHoach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_LoaiKeHoach.Name = "txt_LoaiKeHoach";
            this.txt_LoaiKeHoach.ReadOnly = true;
            this.txt_LoaiKeHoach.Size = new System.Drawing.Size(235, 21);
            this.txt_LoaiKeHoach.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(296, 46);
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
            this.label5.Location = new System.Drawing.Point(296, 14);
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
            this.label4.Location = new System.Drawing.Point(9, 82);
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
            this.label3.Location = new System.Drawing.Point(8, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loại";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 132);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(810, 373);
            this.panel3.TabIndex = 18;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(524, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(286, 373);
            this.panel5.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.is_BoMon);
            this.panel7.Controls.Add(this.txt_NhiemVu);
            this.panel7.Controls.Add(this.txt_HoTenGV);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.txt_GioiTinh);
            this.panel7.Controls.Add(this.guna2TextBox6);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.txt_SDT);
            this.panel7.Controls.Add(this.txt_EmailGV);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Location = new System.Drawing.Point(2, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(282, 373);
            this.panel7.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 214);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 76;
            this.label12.Text = "Hoạt động";
            // 
            // is_BoMon
            // 
            this.is_BoMon.BorderRadius = 6;
            this.is_BoMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.is_BoMon.DefaultText = "";
            this.is_BoMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.is_BoMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.is_BoMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.is_BoMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.is_BoMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.is_BoMon.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.is_BoMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.is_BoMon.Location = new System.Drawing.Point(98, 214);
            this.is_BoMon.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.is_BoMon.Name = "is_BoMon";
            this.is_BoMon.PlaceholderText = "";
            this.is_BoMon.ReadOnly = true;
            this.is_BoMon.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.is_BoMon.SelectedText = "";
            this.is_BoMon.Size = new System.Drawing.Size(167, 19);
            this.is_BoMon.TabIndex = 75;
            // 
            // txt_NhiemVu
            // 
            this.txt_NhiemVu.BorderRadius = 6;
            this.txt_NhiemVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_NhiemVu.DefaultText = "";
            this.txt_NhiemVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_NhiemVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_NhiemVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_NhiemVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_NhiemVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_NhiemVu.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.txt_NhiemVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_NhiemVu.Location = new System.Drawing.Point(94, 156);
            this.txt_NhiemVu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_NhiemVu.Name = "txt_NhiemVu";
            this.txt_NhiemVu.PlaceholderText = "";
            this.txt_NhiemVu.ReadOnly = true;
            this.txt_NhiemVu.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_NhiemVu.SelectedText = "";
            this.txt_NhiemVu.Size = new System.Drawing.Size(170, 19);
            this.txt_NhiemVu.TabIndex = 74;
            // 
            // txt_HoTenGV
            // 
            this.txt_HoTenGV.BorderRadius = 6;
            this.txt_HoTenGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_HoTenGV.DefaultText = "";
            this.txt_HoTenGV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_HoTenGV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_HoTenGV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_HoTenGV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_HoTenGV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_HoTenGV.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.txt_HoTenGV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_HoTenGV.Location = new System.Drawing.Point(94, 63);
            this.txt_HoTenGV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_HoTenGV.Name = "txt_HoTenGV";
            this.txt_HoTenGV.PlaceholderText = "";
            this.txt_HoTenGV.ReadOnly = true;
            this.txt_HoTenGV.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_HoTenGV.SelectedText = "";
            this.txt_HoTenGV.Size = new System.Drawing.Size(170, 19);
            this.txt_HoTenGV.TabIndex = 73;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 189);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 72;
            this.label8.Text = "Giới Tính";
            // 
            // txt_GioiTinh
            // 
            this.txt_GioiTinh.BorderRadius = 6;
            this.txt_GioiTinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_GioiTinh.DefaultText = "";
            this.txt_GioiTinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_GioiTinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_GioiTinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_GioiTinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_GioiTinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_GioiTinh.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.txt_GioiTinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_GioiTinh.Location = new System.Drawing.Point(98, 184);
            this.txt_GioiTinh.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_GioiTinh.Name = "txt_GioiTinh";
            this.txt_GioiTinh.PlaceholderText = "";
            this.txt_GioiTinh.ReadOnly = true;
            this.txt_GioiTinh.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_GioiTinh.SelectedText = "";
            this.txt_GioiTinh.Size = new System.Drawing.Size(170, 19);
            this.txt_GioiTinh.TabIndex = 71;
            // 
            // guna2TextBox6
            // 
            this.guna2TextBox6.BorderRadius = 6;
            this.guna2TextBox6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guna2TextBox6.DefaultText = "";
            this.guna2TextBox6.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.guna2TextBox6.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.guna2TextBox6.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox6.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.guna2TextBox6.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox6.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.guna2TextBox6.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2TextBox6.Location = new System.Drawing.Point(94, 248);
            this.guna2TextBox6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.guna2TextBox6.Multiline = true;
            this.guna2TextBox6.Name = "guna2TextBox6";
            this.guna2TextBox6.PlaceholderText = "";
            this.guna2TextBox6.ReadOnly = true;
            this.guna2TextBox6.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.guna2TextBox6.SelectedText = "";
            this.guna2TextBox6.Size = new System.Drawing.Size(170, 68);
            this.guna2TextBox6.TabIndex = 69;
            this.guna2TextBox6.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(28, 248);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "Ghi chú";
            this.label15.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 131);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 67;
            this.label10.Text = "SĐT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 98);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 66;
            this.label9.Text = "Email";
            // 
            // txt_SDT
            // 
            this.txt_SDT.BorderRadius = 6;
            this.txt_SDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_SDT.DefaultText = "";
            this.txt_SDT.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_SDT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_SDT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_SDT.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_SDT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_SDT.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.txt_SDT.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_SDT.Location = new System.Drawing.Point(94, 125);
            this.txt_SDT.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_SDT.Name = "txt_SDT";
            this.txt_SDT.PlaceholderText = "";
            this.txt_SDT.ReadOnly = true;
            this.txt_SDT.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_SDT.SelectedText = "";
            this.txt_SDT.Size = new System.Drawing.Size(170, 19);
            this.txt_SDT.TabIndex = 62;
            // 
            // txt_EmailGV
            // 
            this.txt_EmailGV.BorderRadius = 6;
            this.txt_EmailGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_EmailGV.DefaultText = "";
            this.txt_EmailGV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_EmailGV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_EmailGV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_EmailGV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_EmailGV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_EmailGV.Font = new System.Drawing.Font("Microsoft Tai Le", 9F);
            this.txt_EmailGV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_EmailGV.Location = new System.Drawing.Point(94, 92);
            this.txt_EmailGV.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_EmailGV.Name = "txt_EmailGV";
            this.txt_EmailGV.PlaceholderText = "";
            this.txt_EmailGV.ReadOnly = true;
            this.txt_EmailGV.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_EmailGV.SelectedText = "";
            this.txt_EmailGV.Size = new System.Drawing.Size(170, 19);
            this.txt_EmailGV.TabIndex = 61;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 68);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 59;
            this.label14.Text = "Họ và tên";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 160);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "Chức Vụ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(82, 15);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 17);
            this.label13.TabIndex = 55;
            this.label13.Text = "Thông tin giảng viên";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 505);
            this.panel1.TabIndex = 2;
            // 
            // FormDetailPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 505);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetailPlan";
            this.Text = "FormDetailPlan";
            this.Load += new System.EventHandler(this.FormDetailPlan_Load);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_Thoat;
        private System.Windows.Forms.Button btn_XuatPdf;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_GhiChu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_NguoiGiao;
        private System.Windows.Forms.DateTimePicker dtp_NgayNop;
        private System.Windows.Forms.DateTimePicker dtp_NgayGiao;
        private System.Windows.Forms.TextBox txt_TenCV;
        private System.Windows.Forms.TextBox txt_LoaiKeHoach;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_MucUuTien;
        private System.Windows.Forms.Label aa;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label12;
        private Guna.UI2.WinForms.Guna2TextBox is_BoMon;
        private Guna.UI2.WinForms.Guna2TextBox txt_NhiemVu;
        private Guna.UI2.WinForms.Guna2TextBox txt_HoTenGV;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2TextBox txt_GioiTinh;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2TextBox txt_SDT;
        private Guna.UI2.WinForms.Guna2TextBox txt_EmailGV;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_XuatExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn NhiemVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayBatDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayKetThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThaiGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianNop;
    }
}