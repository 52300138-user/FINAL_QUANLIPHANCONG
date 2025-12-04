using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.DTO;

namespace WindowsFormsApp2.Forms.FormBangDiem.ChildBangDiem
{
    public partial class FormDetailBD : Form
    {
        private int currentCongViecId;
        private CongViecDTO currentCV;
        public FormDetailBD(int congViecId)
        {
            InitializeComponent();
            this.currentCongViecId = congViecId;
        }

        private void FormPhanCong_Load(object sender, EventArgs e)
        {
            //// ===== CÀI ĐẶT DATETIMEPICKER =====
            //dtp_HanChot.Format = DateTimePickerFormat.Custom;
            //dtp_HanChot.CustomFormat = "dd/MM/yyyy";
            //dtp_HanChot.Value = currentCV.HanChot ?? DateTime.Today;

            //// ===== Bước 1 =====
            //try { LoadGiangVienComboBox(); }
            //catch (Exception ex) { MessageBox.Show("Lỗi danh sách GV: " + ex.Message); }

            //SetupDataGridView();
            //LoadDanhSachDaPhanCong();

            //// ===== Bước 2: Lấy chi tiết công việc =====
            //this.currentCV = CongViecBUS.GetCongViecById(this.currentCongViecId);
            //if (currentCV == null)
            //{
            //    MessageBox.Show("Không tìm thấy công việc!");
            //    Close();
            //    return;
            //}

            //lbl_TenCV.Text = currentCV.TenCongViec;
            //lbl_LoaiCV.Text = currentCV.Loai;

            //// ===== HẠN CHÓT: GÁN VALUE CHO DATETIMEPICKER, KHÔNG GÁN .TEXT =====
            //if (currentCV.HanChot.HasValue)
            //{
            //    dtp_HanChot.Checked = true;
            //    dtp_HanChot.Value = currentCV.HanChot.Value;
            //}
            //else
            //{
            //    dtp_HanChot.Checked = false;
            //}

            //// ===== NGƯỜI GIAO =====
            //try
            //{
            //    var nguoiGiao = UsersBUS.GetUserById(Program.CurrentUserId);
            //    lbl_NguoiGiao.Text = nguoiGiao?.FullName ?? "(Không rõ)";
            //}
            //catch { lbl_NguoiGiao.Text = "(Lỗi tải User)"; }

            //// ===== HỌC KỲ =====
            //if (currentCV.KeHoachID.HasValue && currentCV.KeHoachID > 0)
            //{
            //    try
            //    {
            //        var plan = PlansBUS.GetPlanById(currentCV.KeHoachID.Value);
            //        lbl_HocKy.Text = plan?.TenKeHoach ?? "(Kế hoạch bị xoá)";
            //    }
            //    catch
            //    {
            //        lbl_HocKy.Text = "(Lỗi kế hoạch)";
            //    }
            //}
            //else
            //{
            //    lbl_HocKy.Text = "(Không thuộc kế hoạch nào)";
            //}
        }

        private void LoadGiangVienComboBox()
        {
            //try
            //{
            //    DataTable dt = PhanCongBUS.GetGiangVienList();
            //    cbb_ChonGV.DataSource = dt;
            //    cbb_ChonGV.DisplayMember = "FullName";
            //    cbb_ChonGV.ValueMember = "UserID";
            //}
            //catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách GV: " + ex.Message); }
        }

        private void SetupDataGridView()
        {
            //dgv_DaPhanCong.Columns.Clear();
            //dgv_DaPhanCong.AutoGenerateColumns = false;
            //dgv_DaPhanCong.AllowUserToAddRows = false;
            //dgv_DaPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgv_DaPhanCong.ReadOnly = true;
            //dgv_DaPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //dgv_DaPhanCong.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "PC_ID",
            //    Name = "colPC_ID",
            //    Visible = false
            //});
            //dgv_DaPhanCong.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "UserID",
            //    Name = "colUserID",
            //    Visible = false
            //});
            //dgv_DaPhanCong.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "TenGiangVien",
            //    HeaderText = "Tên Giảng viên",
            //    FillWeight = 200
            //});
            //dgv_DaPhanCong.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "EmailGV",
            //    HeaderText = "Email",
            //    FillWeight = 150
            //});
            //dgv_DaPhanCong.Columns.Add(new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = "VaiTro",
            //    HeaderText = "Vai Trò",
            //    FillWeight = 70
            //});

            //var colXoa = new DataGridViewButtonColumn
            //{
            //    Name = "colXoa",
            //    HeaderText = "Hủy gán",
            //    Text = "Xóa",
            //    UseColumnTextForButtonValue = true,
            //    Width = 60
            //};
            //dgv_DaPhanCong.Columns.Add(colXoa);
        }

        private void LoadDanhSachDaPhanCong()
        {
        //    try
        //    {
        //        DataTable dt = PhanCongBUS.GetPhanCongByCongViecID(this.currentCongViecId);
        //        dgv_DaPhanCong.DataSource = dt;
        //    }
        //    catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách phân công: " + ex.Message); }
        //}

        //private void btn_ThemGV_Click(object sender, EventArgs e)
        //{
        //    var dto = new PhanCongDTO
        //    {
        //        CongViecID = this.currentCongViecId,
        //        UserID = (int)cbb_ChonGV.SelectedValue,
        //        VaiTro = rb_ChuTri.Checked ? "ChuTri" : "HoTro",
        //        NgayGiao = DateTime.Today,
        //        GhiChu_TBM = txt_GhiChuTBM.Text
        //    };

        //    try
        //    {
        //        if (PhanCongBUS.AddPhanCong(dto))
        //        {
        //            LoadDanhSachDaPhanCong();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi hệ thống khi gán việc: " + ex.Message);
        //    }
        }

        private void dgv_DaPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //    if (e.RowIndex < 0 || e.ColumnIndex != dgv_DaPhanCong.Columns["colXoa"].Index)
            //    {
            //        return;
            //    }

            //    int pc_ID = Convert.ToInt32(dgv_DaPhanCong.Rows[e.RowIndex].Cells["colPC_ID"].Value);
            //    string tenGV = dgv_DaPhanCong.Rows[e.RowIndex].Cells["TenGiangVien"].Value.ToString();

            //    var confirm = MessageBox.Show($"Bạn có chắc muốn hủy phân công cho: \n{tenGV}?",
            //                                  "Xác nhận hủy",
            //                                  MessageBoxButtons.YesNo,
            //                                  MessageBoxIcon.Warning);

            //    if (confirm != DialogResult.Yes) return;

            //    try
            //    {
            //        if (PhanCongBUS.DeletePhanCong(pc_ID))
            //        {
            //            LoadDanhSachDaPhanCong(); // Tải lại GridView
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi hệ thống khi hủy gán việc: " + ex.Message);
            //    }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
