using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;

namespace WindowsFormsApp2.Forms.FormMHGV
{
    public partial class FormMonHoc_GV : Form
    {
        public FormMonHoc_GV()
        {
            InitializeComponent();
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.ForeColor = Color.White;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                    label5.ForeColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void LoadMonHoc(string keyword = null, string hocKy = null)
        {
            try
            {
                DataTable dt;
                if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(hocKy))
                {
                    dt = MonHocBUS.GetMonHocTable();
                }
                else
                {
                    dt = MonHocBUS.SearchMonHoc(keyword, hocKy);
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Môn học: " + ex.Message, "Lỗi Hệ Thống");
            }
        }
        private void LoadHocKyComboBox()
        {
            // Danh sách Key-Value chuẩn
            var hocKyList = new List<object>
                {
                // Thêm mục "Tất cả" với Key là null
                new { Key = (string)null, Value = "Tất cả" },
                new { Key = "HocKyI", Value = "Học Kỳ I" },
                new { Key = "HocKyII", Value = "Học Kỳ II" },
                new { Key = "HocKyHe", Value = "Học Kỳ Hè" }
                };

            cbb_HocKy.DataSource = hocKyList;
            cbb_HocKy.DisplayMember = "Value";
            cbb_HocKy.ValueMember = "Key"; // Sẽ trả về null nếu chọn "Tất cả"
            cbb_HocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_HocKy.SelectedIndex = 0; // Chọn "Tất cả"
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string keyword = txt_TenMonHoc.Text.Trim();

            // 1. Lấy MÃ KỸ THUẬT (KEY) trực tiếp từ SelectedValue
            string hocKyKey = cbb_HocKy.SelectedValue as string;

            // Nếu SelectedValue là null (chọn "Tất cả"), hocKyKey sẽ là null.
            // Nếu là string rỗng (""), ta cũng coi là null.
            if (string.IsNullOrWhiteSpace(hocKyKey))
            {
                hocKyKey = null;
            }

            // 2. Kiểm tra điều kiện Reset/Tải toàn bộ
            if (string.IsNullOrWhiteSpace(keyword) && hocKyKey == null)
            {
                LoadMonHoc(); // LoadMonHoc() không tham số
            }
            else
            {
                LoadMonHoc(keyword, hocKyKey); // Truyền Mã Key (hoặc null)
            }
        }

        private void FormMonHoc_GV_Load(object sender, EventArgs e)
        {
            {
                try
                {
                    dataGridView1.Columns.Clear();

                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // --- Định nghĩa các cột ---
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "ID",
                        Name = "colID",
                        DataPropertyName = "MonHocID",
                        Visible = false
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Học Kỳ",
                        Name = "colHocKy",
                        DataPropertyName = "HocKy",
                        FillWeight = 250
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Mã Môn Học",
                        Name = "colMaMonHoc",
                        DataPropertyName = "MaMonHoc",
                        FillWeight = 100
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Tên Môn Học",
                        Name = "colTenMonHoc",
                        DataPropertyName = "TenMonHoc",
                        FillWeight = 250
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Tên Nhóm",
                        Name = "colTenNhom",
                        DataPropertyName = "TenNhom",
                        FillWeight = 250
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Tên Tổ",
                        Name = "colTenTo",
                        DataPropertyName = "TenTo",
                        FillWeight = 250
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Số TC",
                        Name = "colTinChi",
                        DataPropertyName = "SoTinChi",
                        FillWeight = 70
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Tiết LT",
                        Name = "colLT",
                        DataPropertyName = "SoTiet_LT",
                        FillWeight = 70
                    });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Tiết TH",
                        Name = "colTH",
                        DataPropertyName = "SoTiet_TH",
                        FillWeight = 70
                    });

                    LoadTheme();
                    LoadHocKyComboBox();
                    LoadMonHoc();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi khởi tạo Form Môn Học: " + ex.Message, "Lỗi UI");
                }
            }
        }
    }
}
