namespace WindowsFormsApp2.Forms.FormBaoCao
{
    partial class FormBaoCaoTongHop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelFilters = new System.Windows.Forms.Panel();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.cbbGiangVien = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbLoaiCongViec = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbLoaiKeHoach = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblDaDuyet = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDaNop = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTiLe = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTreHan = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHoanThanh = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblChoDuyet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDangLam = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMoi = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.panelFilters.SuspendLayout();
            this.panelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFilters
            // 
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.btnXemBaoCao);
            this.panelFilters.Controls.Add(this.cbbGiangVien);
            this.panelFilters.Controls.Add(this.label3);
            this.panelFilters.Controls.Add(this.cbbLoaiCongViec);
            this.panelFilters.Controls.Add(this.label2);
            this.panelFilters.Controls.Add(this.cbbLoaiKeHoach);
            this.panelFilters.Controls.Add(this.label1);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(980, 90);
            this.panelFilters.TabIndex = 0;
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXemBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCao.Location = new System.Drawing.Point(820, 25);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(130, 35);
            this.btnXemBaoCao.TabIndex = 6;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = false;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // cbbGiangVien
            // 
            this.cbbGiangVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGiangVien.FormattingEnabled = true;
            this.cbbGiangVien.Location = new System.Drawing.Point(580, 30);
            this.cbbGiangVien.Name = "cbbGiangVien";
            this.cbbGiangVien.Size = new System.Drawing.Size(180, 21);
            this.cbbGiangVien.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(495, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giảng viên:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbLoaiCongViec
            // 
            this.cbbLoaiCongViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLoaiCongViec.FormattingEnabled = true;
            this.cbbLoaiCongViec.Location = new System.Drawing.Point(360, 30);
            this.cbbLoaiCongViec.Name = "cbbLoaiCongViec";
            this.cbbLoaiCongViec.Size = new System.Drawing.Size(120, 21);
            this.cbbLoaiCongViec.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(260, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loại công việc:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbLoaiKeHoach
            // 
            this.cbbLoaiKeHoach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLoaiKeHoach.FormattingEnabled = true;
            this.cbbLoaiKeHoach.Location = new System.Drawing.Point(115, 30);
            this.cbbLoaiKeHoach.Name = "cbbLoaiKeHoach";
            this.cbbLoaiKeHoach.Size = new System.Drawing.Size(135, 21);
            this.cbbLoaiKeHoach.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loại kế hoạch:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelSummary
            // 
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummary.Controls.Add(this.lblDaDuyet);
            this.panelSummary.Controls.Add(this.label12);
            this.panelSummary.Controls.Add(this.lblDaNop);
            this.panelSummary.Controls.Add(this.label10);
            this.panelSummary.Controls.Add(this.lblTiLe);
            this.panelSummary.Controls.Add(this.label8);
            this.panelSummary.Controls.Add(this.lblTreHan);
            this.panelSummary.Controls.Add(this.label6);
            this.panelSummary.Controls.Add(this.lblHoanThanh);
            this.panelSummary.Controls.Add(this.label5);
            this.panelSummary.Controls.Add(this.lblChoDuyet);
            this.panelSummary.Controls.Add(this.label4);
            this.panelSummary.Controls.Add(this.lblDangLam);
            this.panelSummary.Controls.Add(this.label7);
            this.panelSummary.Controls.Add(this.lblMoi);
            this.panelSummary.Controls.Add(this.label9);
            this.panelSummary.Controls.Add(this.lblTong);
            this.panelSummary.Controls.Add(this.label11);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 90);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(980, 110);
            this.panelSummary.TabIndex = 1;
            // 
            // lblDaDuyet
            // 
            this.lblDaDuyet.AutoSize = true;
            this.lblDaDuyet.Location = new System.Drawing.Point(870, 65);
            this.lblDaDuyet.Name = "lblDaDuyet";
            this.lblDaDuyet.Size = new System.Drawing.Size(13, 13);
            this.lblDaDuyet.TabIndex = 17;
            this.lblDaDuyet.Text = "0";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(770, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 16;
            this.label12.Text = "Đã duyệt:";
            // 
            // lblDaNop
            // 
            this.lblDaNop.AutoSize = true;
            this.lblDaNop.Location = new System.Drawing.Point(870, 35);
            this.lblDaNop.Name = "lblDaNop";
            this.lblDaNop.Size = new System.Drawing.Size(13, 13);
            this.lblDaNop.TabIndex = 15;
            this.lblDaNop.Text = "0";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(770, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 14;
            this.label10.Text = "Đã nộp:";
            // 
            // lblTiLe
            // 
            this.lblTiLe.AutoSize = true;
            this.lblTiLe.Location = new System.Drawing.Point(650, 65);
            this.lblTiLe.Name = "lblTiLe";
            this.lblTiLe.Size = new System.Drawing.Size(21, 13);
            this.lblTiLe.TabIndex = 13;
            this.lblTiLe.Text = "0%";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(550, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "% Hoàn thành:";
            // 
            // lblTreHan
            // 
            this.lblTreHan.AutoSize = true;
            this.lblTreHan.Location = new System.Drawing.Point(650, 35);
            this.lblTreHan.Name = "lblTreHan";
            this.lblTreHan.Size = new System.Drawing.Size(13, 13);
            this.lblTreHan.TabIndex = 11;
            this.lblTreHan.Text = "0";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(550, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Trễ hạn:";
            // 
            // lblHoanThanh
            // 
            this.lblHoanThanh.AutoSize = true;
            this.lblHoanThanh.Location = new System.Drawing.Point(440, 65);
            this.lblHoanThanh.Name = "lblHoanThanh";
            this.lblHoanThanh.Size = new System.Drawing.Size(13, 13);
            this.lblHoanThanh.TabIndex = 9;
            this.lblHoanThanh.Text = "0";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(340, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Hoàn thành:";
            // 
            // lblChoDuyet
            // 
            this.lblChoDuyet.AutoSize = true;
            this.lblChoDuyet.Location = new System.Drawing.Point(440, 35);
            this.lblChoDuyet.Name = "lblChoDuyet";
            this.lblChoDuyet.Size = new System.Drawing.Size(13, 13);
            this.lblChoDuyet.TabIndex = 7;
            this.lblChoDuyet.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(340, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chờ duyệt:";
            // 
            // lblDangLam
            // 
            this.lblDangLam.AutoSize = true;
            this.lblDangLam.Location = new System.Drawing.Point(220, 65);
            this.lblDangLam.Name = "lblDangLam";
            this.lblDangLam.Size = new System.Drawing.Size(13, 13);
            this.lblDangLam.TabIndex = 5;
            this.lblDangLam.Text = "0";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(120, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 4;
            this.label7.Text = "Đang làm:";
            // 
            // lblMoi
            // 
            this.lblMoi.AutoSize = true;
            this.lblMoi.Location = new System.Drawing.Point(220, 35);
            this.lblMoi.Name = "lblMoi";
            this.lblMoi.Size = new System.Drawing.Size(13, 13);
            this.lblMoi.TabIndex = 3;
            this.lblMoi.Text = "0";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(120, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 2;
            this.label9.Text = "Mới:";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(70, 50);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(13, 13);
            this.lblTong.TabIndex = 1;
            this.lblTong.Text = "0";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "Tổng:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 200);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(980, 390);
            this.dgvChiTiet.TabIndex = 2;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportExcel.BackColor = System.Drawing.Color.SeaGreen;
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(320, 600);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(140, 35);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExportPdf.BackColor = System.Drawing.Color.Firebrick;
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.Location = new System.Drawing.Point(520, 600);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(140, 35);
            this.btnExportPdf.TabIndex = 4;
            this.btnExportPdf.Text = "Xuất PDF";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // FormBaoCaoTongHop
            // 
            this.ClientSize = new System.Drawing.Size(980, 650);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelFilters);
            this.Name = "FormBaoCaoTongHop";
            this.Text = "Báo cáo tổng hợp";
            this.Load += new System.EventHandler(this.FormBaoCaoTongHop_Load);
            this.panelFilters.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.ComboBox cbbGiangVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbLoaiCongViec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbLoaiKeHoach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblDaDuyet;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDaNop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTiLe;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTreHan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblHoanThanh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblChoDuyet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDangLam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMoi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
    }
}
