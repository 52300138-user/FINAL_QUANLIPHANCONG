using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.Forms.FormQLCV.ChildPlans;

namespace WindowsFormsApp2.Forms.FormQLCV
{
    public partial class FormOrders : Form
    {
        // Biến lưu để lọc
        private DateTime filterTuNgay = new DateTime(1900, 1, 1);
        private DateTime filterDenNgay = new DateTime(9999, 12, 31);

        public FormOrders()
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
                    
                }
            }
        }
        // --- LOGIC VẼ CHART ---
        private void LoadStatusChart(DataTable dtCongViec)
        {
            var statusData = dtCongViec.AsEnumerable()
                .Select(row =>
                {
                    string trangThai = row.Field<string>("TrangThai");
                    DateTime? hanChot = row.Field<DateTime?>("HanChot");

                    // Xử lý logic TRỄ: Nếu chưa Hoàn thành VÀ đã qua Hạn Chót
                    if (trangThai != "HOAN_THANH" && hanChot.HasValue && hanChot.Value.Date < DateTime.Now.Date)
                    {
                        return "Trễ";
                    }

                    // Chuyển đổi mã trạng thái sang tên hiển thị
                    switch (trangThai)
                    {
                        case "MOI": return "Mới";
                        case "DANG_LAM": return "Đang làm";
                        case "HOAN_THANH": return "Đã hoàn thành";
                        default: return "Khác";
                    }
                })
                .GroupBy(status => status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .OrderBy(g => g.Status)
                .ToList();

            chart2.Series.Clear();
            chart2.Titles.Clear();

            // Tính tổng số công việc
            int total = statusData.Sum(x => x.Count);

            var series = new Series("StatusSeries")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Font = new Font("Arial", 8, FontStyle.Bold)
            };

            foreach (var item in statusData)
            {
                double percent = (double)item.Count / total * 100.0;

                int index = series.Points.AddY(percent);
                DataPoint point = series.Points[index];

                point.LegendText = item.Status;
                point.Label = $"{percent:0}%";

                switch (item.Status)
                {
                    case "Trễ":
                        point.Color = Color.Red;
                        break;
                    case "Đã hoàn thành":
                        point.Color = Color.Green;
                        break;
                    case "Mới":
                        point.Color = Color.LightSkyBlue;
                        break;
                    case "Đang làm":
                        point.Color = Color.DodgerBlue;
                        break;
                    default:
                        point.Color = Color.Gray;
                        break;
                }
            }

            chart2.Titles.Add("Tỷ Lệ Trạng Thái Công Việc");
            chart2.Series.Add(series);

        }
        private void LoadLoaiChart(DataTable dtCongViec)
        {
            var loaiData = dtCongViec.AsEnumerable()
                .Select(row =>
                {
                    string loai = row.Field<string>("Loai") ?? "";

                    switch (loai)
                    {
                        case "GiangDay": return "Giảng dạy";
                        case "SuKien": return "Sự kiện";
                        case "HanhChinh": return "Hành chính";
                        case "NghienCuu": return "Nghiên Cứu";
                        default: return "Khác";
                    }
                })
                .GroupBy(loai => loai)
                .Select(g => new { Loai = g.Key, Count = g.Count() })
                .OrderBy(g => g.Loai)
                .ToList();
            int total = loaiData.Sum(x => x.Count);

            chart3.Series.Clear();
            chart3.Titles.Clear();

            var series = new Series("LoaiSeries")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Font = new Font("Arial", 8, FontStyle.Bold)
            };

            foreach (var item in loaiData)
            {
                double percent = (double)item.Count / total * 100;

                int index = series.Points.AddY(percent);
                DataPoint point = series.Points[index];

                point.LegendText = item.Loai;
                point.Label = $"{percent:0}%";

                switch (item.Loai)
                {
                    case "Giảng dạy":
                        point.Color = Color.SteelBlue; break;
                    case "Sự kiện":
                        point.Color = Color.Orange; break;
                    case "Hành chính":
                        point.Color = Color.MediumSeaGreen; break;
                    case "Sự Kiện":
                        point.Color = Color.Aquamarine; break;
                    default:
                        point.Color = Color.Gray; break;
                }
            }

            chart3.Titles.Add("Tỷ lệ Loại Công Việc");
            chart3.Series.Add(series);

        }

        // --- HÀM LOAD TỔNG HỢP  ---
        private void LoadAllData(DataTable dtSource)
        {
            // Tắt sự kiện SelectionChanged để tránh lỗi lặp khi gán DataSource
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            if (dtSource == null) return;
            MapTrangThaiForGrid(dtSource);
            if (label4 != null)
                label4.Text = dtSource.Rows.Count.ToString();

            dataGridView1.DataSource = dtSource;
            LoadStatusChart(dtSource);
            LoadLoaiChart(dtSource);
            DataTable dtCongViec = CongViecBUS.GetLoaiKeHoachThongKe();
            LoadLoaiKeHoachChart(dtCongViec);

            // Bật lại sự kiện
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            // Kích hoạt ẩn/hiện nút thủ công (vì sự kiện SelectionChanged có thể chưa chạy)
            dataGridView1_SelectionChanged(null, null);
        }
        private void LoadKeHoachCombo()
        {
            try
            {
                DataTable dt = PlansBUS.GetAllKeHoach();

                cbb_KeHoach.DataSource = dt;
                cbb_KeHoach.DisplayMember = "TenKeHoach";
                cbb_KeHoach.ValueMember = "KeHoachID";

                // Dòng này rất quan trọng: Cho phép bỏ lọc theo kế hoạch
                cbb_KeHoach.SelectedIndex = -1;
                cbb_KeHoach.Text = "— Chọn kế hoạch —";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách Kế hoạch: " + ex.Message);
            }
        }

        private void LoadCongViec()
        {
            try
            {
                // Range mặc định load hết dữ liệu
                filterTuNgay = new DateTime(1900, 1, 1);
                filterDenNgay = new DateTime(9999, 12, 31);

                // Load toàn bộ công việc (không lọc từ khóa & kế hoạch)
                DataTable dt = CongViecBUS.GetCongViecFiltered(
                    filterTuNgay,
                    filterDenNgay,
                    null,       // không từ khóa
                    null        // không lọc kế hoạch
                );

                LoadAllData(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Công việc: " + ex.Message,
                    "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- Định nghĩa các cột ---
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                Name = "colID",
                DataPropertyName = "CongViecID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên Công Việc",
                Name = "colTen",
                DataPropertyName = "TenCongViec",
                FillWeight = 300
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Loại",
                Name = "colLoai",
                DataPropertyName = "Loai",
                FillWeight = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng Thái",
                Name = "colTrangThai",
                DataPropertyName = "TrangThaiHienThi",
                FillWeight = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ưu Tiên",
                Name = "colUuTien",
                DataPropertyName = "MucUuTien",
                FillWeight = 70
            });

            var colHanChot = new DataGridViewTextBoxColumn
            {
                HeaderText = "Hạn Chót",
                Name = "colHanChot",
                DataPropertyName = "HanChot",
                FillWeight = 100
            };
            colHanChot.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colHanChot);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Kế Hoạch",
                Name = "colKeHoach",
                DataPropertyName = "TenKeHoach",
                FillWeight = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Người Đảm Nhiệm",
                Name = "colNguoiDamNhiem",
                DataPropertyName = "NguoiDamNhiem",
                FillWeight = 120
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Người Giao",
                Name = "colNguoiGiao",
                DataPropertyName = "NguoiGiao",
                FillWeight = 120
            });
            dtp_TuNgay.Value = new DateTime(2020, 1, 1); // Ngày bắt đầu an toàn hơn
            dtp_DenNgay.Value = DateTime.Today;
            LoadTheme();
            LoadCongViec();
            LoadKeHoachCombo();
            // (Giả sử tên nút là button_them, button_sua, button_xoa)
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
        }

        // Lọc
        private void button1_Click(object sender, EventArgs e)
        {
            ApplyAllFilters();
        }

        // Phân công
        private void button6_Click(object sender, EventArgs e)
        {
            // Logic lấy ID Công việc (giống nút Sửa/Xóa)
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Công việc để phân công.", "Cảnh báo");
                return;
            }

            try
            {   
                int congViecId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);

                if (congViecId == 0) return;

                FormAssignTask formAssign = new FormAssignTask(congViecId);
                formAssign.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ID Công việc: " + ex.Message, "Lỗi");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một công việc trước.");
                    return;
                }

                var cell = dataGridView1.CurrentRow.Cells["colID"].Value;
                if (cell == null || cell == DBNull.Value)
                {
                    MessageBox.Show("Không lấy được ID công việc.");
                    return;
                }

                int congViecId = Convert.ToInt32(cell);
                FormEditPlan formEdit = new FormEditPlan(congViecId);

                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    LoadCongViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Form: " + ex.Message);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một công việc.", "Thông báo");
                    return;
                }

                var cell = dataGridView1.CurrentRow.Cells["colID"].Value;
                if (cell == null || cell == DBNull.Value)
                {
                    MessageBox.Show("Không lấy được ID công việc.", "Lỗi");
                    return;
                }

                int congViecId = Convert.ToInt32(cell);
                var formDetail = new FormDetailPlan(congViecId);
                formDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết công việc: " + ex.Message);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            try
            {
                int congViecId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colID"].Value);
                string tenCV = dataGridView1.SelectedRows[0].Cells["colTen"].Value.ToString();

                if (congViecId == 0) return;

                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa công việc: \n{tenCV}?\n(Toàn bộ phân công liên quan cũng sẽ bị xóa)",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    if (CongViecBUS.DeleteCongViec(congViecId))
                    {
                        MessageBox.Show("Xóa công việc thành công!");
                        LoadCongViec(); // Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa công việc: " + ex.Message);
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool isRowSelected = dataGridView1.SelectedRows.Count > 0;
            button3.Visible = isRowSelected;
            button5.Visible = isRowSelected;
            button6.Visible = isRowSelected;
        }
        private void ApplyAllFilters()
        {
            string keyword = txt_Loc.Text.Trim();
            filterTuNgay = dtp_TuNgay.Value.Date;
            filterDenNgay = dtp_DenNgay.Value.Date;

            if (filterTuNgay > filterDenNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn Ngày kết thúc.", "Lỗi Validation");
                return;
            }

            int? keHoachId = (cbb_KeHoach.SelectedIndex >= 0)
                ? Convert.ToInt32(cbb_KeHoach.SelectedValue)
                : (int?)null;

            try
            {
                DataTable dtFiltered = CongViecBUS.GetCongViecFiltered(
                    filterTuNgay, filterDenNgay, keyword, keHoachId
                );

                LoadAllData(dtFiltered);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi áp dụng bộ lọc: " + ex.Message);
            }
        }


        private void MapTrangThaiForGrid(DataTable dt)
        {
            if (dt == null) return;

            // Tạo cột hiển thị nếu chưa có
            if (!dt.Columns.Contains("TrangThaiHienThi"))
                dt.Columns.Add("TrangThaiHienThi", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string trangThai = row.Field<string>("TrangThai");
                DateTime? hanChot = row.Field<DateTime?>("HanChot");

                string display;

                // Logic TRỄ: giống chart
                if (trangThai != "HOAN_THANH" &&
                    hanChot.HasValue &&
                    hanChot.Value.Date < DateTime.Now.Date)
                {
                    display = "Trễ";
                }
                else
                {
                    switch (trangThai)
                    {
                        case "MOI": display = "Mới"; break;
                        case "DANG_LAM": display = "Đang làm"; break;
                        case "HOAN_THANH": display = "Đã hoàn thành"; break;
                        default: display = "Khác"; break;
                    }
                }

                row["TrangThaiHienThi"] = display;
            }
        }

        private void dtp_TuNgay_ValueChanged(object sender, EventArgs e)
        {
            ApplyAllFilters();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ApplyAllFilters();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void cbb_KeHoach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_KeHoach.SelectedIndex < 0)
            {
                // Reset ngày
                dtp_TuNgay.Value = new DateTime(2020, 1, 1);
                dtp_DenNgay.Value = DateTime.Today;
                ApplyAllFilters();
                return;
            }

            int keHoachId = Convert.ToInt32(cbb_KeHoach.SelectedValue);
            var kh = PlansBUS.GetPlanById(keHoachId);

            if (kh != null)
            {
                // Nếu DB trả về giá trị không hợp lệ (MinValue), đặt fallback
                dtp_TuNgay.Value = (kh.NgayBatDau <= DateTime.MinValue.AddDays(1))
                    ? new DateTime(2020, 1, 1)
                    : kh.NgayBatDau;

                dtp_DenNgay.Value = (kh.NgayKetThuc <= DateTime.MinValue.AddDays(1))
                    ? DateTime.Today
                    : kh.NgayKetThuc;

                // Đảm bảo không thể "kết thúc < bắt đầu"
                if (dtp_DenNgay.Value < dtp_TuNgay.Value)
                    dtp_DenNgay.Value = dtp_TuNgay.Value;
            }


            ApplyAllFilters();
        }

        private void btn_HuyTim_Click(object sender, EventArgs e)
        {
            txt_Loc.Clear();
            cbb_KeHoach.SelectedIndex = -1;
            cbb_KeHoach.Text = "— Chọn kế hoạch —";
            dtp_TuNgay.Value = new DateTime(2020, 1, 1);
            dtp_DenNgay.Value = DateTime.Today;

            ApplyAllFilters();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                FormAddCongViec formAdd = new FormAddCongViec();
                formAdd.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Lỗi tải thông tin");
            }
        }

        private void cbb_KeHoach_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadLoaiKeHoachChart(DataTable dtCongViec)
        {
            if (dtCongViec == null || dtCongViec.Rows.Count == 0)
                return;

            // ====== Bảng map KEY → Label tiếng Việt ======
            Dictionary<string, string> labelMap = new Dictionary<string, string>
            {
                { "HocKyI",  "Kế hoạch học kỳ I" },
                { "HocKyII", "Kế hoạch học kỳ II" },
                { "HocKyHe", "Kế hoạch học kỳ Hè" },
                { "NamHoc",  "Kế hoạch năm học" },
                { "DeTai",   "Đề tài nghiên cứu" },
                { "SuKien",  "Sự kiện học thuật" },
                { "Khac",    "Khác" }
            };

            // ====== Màu sắc ======
            Dictionary<string, Color> colorMap = new Dictionary<string, Color>
            {
                { "HocKyI", Color.DodgerBlue },
                { "HocKyII", Color.Orange },
                { "HocKyHe", Color.Red },
                { "NamHoc", Color.MediumSeaGreen },
                { "DeTai", Color.SlateBlue },
                { "SuKien", Color.Crimson },
                { "Khac", Color.Gray }
            };

            // ====== Gom nhóm loại kế hoạch từ KeHoach table ======
            var keHoachData =
                dtCongViec.AsEnumerable()
                .Select(row =>
                {
                    string loai = row["Loai"]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(loai)) loai = "Khac";
                    return loai;
                })
                .GroupBy(x => x)
                .Select(g => new { Key = g.Key, Count = g.Count() })
                .ToList();

            if (keHoachData.Count == 0)
                return;

            int total = keHoachData.Sum(x => x.Count);
            if (total == 0) return;

            // ====== Reset chart ======
            chart4.Series.Clear();
            chart4.Titles.Clear();
            chart4.Legends.Clear();

            chart4.Legends.Add("Legend");
            chart4.Legends["Legend"].Docking = Docking.Right;

            var series = new Series("LoaiKeHoachSeries")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            // ====== Add điểm vào Pie Chart ======
            foreach (var item in keHoachData)
            {
                double percent = (double)item.Count / total * 100;

                int idx = series.Points.AddY(percent);
                var point = series.Points[idx];

                // Lấy label tiếng Việt
                string vietLabel = labelMap.ContainsKey(item.Key) ? labelMap[item.Key] : item.Key;

                point.Label = percent.ToString("0") + "%";     // chỉ phần trăm
                point.LegendText = vietLabel;                  // legend tiếng Việt
                point.Color = colorMap.ContainsKey(item.Key)
                                ? colorMap[item.Key]
                                : Color.Gray;
            }

            chart4.Titles.Add("Loại kế hoạch");
            chart4.Series.Add(series);
        }


        private void chart4_Click(object sender, EventArgs e)
        {

        }
    }
}
