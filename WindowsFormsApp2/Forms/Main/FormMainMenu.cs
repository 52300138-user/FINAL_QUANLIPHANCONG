using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.Forms.FormCustomer;

namespace WindowsFormsApp2.Forms.Main
{
    public partial class FormMainMenu : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private bool isExisting = false;

        // Nếu true: khi click sẽ dùng fixedPalette color; nếu false: dùng random
        private bool useFixedColors = true;

        // Mapping button.Name -> hex color
        private Dictionary<string, string> fixedPalette;

        public FormMainMenu()
        {
            InitializeComponent();

            random = new Random();
            btnCloseChildForm.Visible = false;

            InitFixedPalette();
        }

        // Khởi tạo bảng màu cố định. btnOrders được giữ màu xanh lá (#4CAF50).
        private void InitFixedPalette()
        {
            fixedPalette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "btnProducts",     "#2A9D8F" }, // Quản Lí Môn Học (teal)
                { "btnOrders",       "#4CAF50" }, // Tiến Độ Công Việc (xanh lá) <- yêu cầu
                { "btnCustomer",     "#E63946" }, // Quản lý người dùng (đỏ)
                { "btnReporting",    "#F4A261" }, // Quản Lý Kế Hoạch (cam)
                { "btnNotifications","#8A2BE2" }, // Quản Lý Công Việc (tím)
                { "btnBaoCao",       "#E9C46A" }, // Báo Cáo (vàng/gold)
                { "btn_setting",     "#264653" }  // Cài Đặt (xanh đậm)
            };
        }

        // Chọn màu random cũ (nếu cần dùng)
        private Color SelectThemeColor()
        {
            if (ThemeColor.ColorList == null || ThemeColor.ColorList.Count == 0)
                return Color.FromArgb(0, 150, 136);

            int index = random.Next(ThemeColor.ColorList.Count);
            while (index == tempIndex)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        // Trả về màu chữ phù hợp (đen hoặc trắng) dựa trên luminance
        private Color GetReadableTextColor(Color bg)
        {
            double luminance = (0.299 * bg.R + 0.587 * bg.G + 0.114 * bg.B);
            return luminance > 186 ? Color.FromArgb(17, 17, 17) : Color.White;
        }

        // Khi 1 button được active (chỉ button đó đổi màu, các button khác vẫn xám)
        private void ActivateButton(object btnSender)
        {
            if (btnSender == null) return;
            var clicked = btnSender as Button;
            if (clicked == null) return;

            // Nếu click lại cùng nút đang active -> không làm gì (hoặc toggle nếu cần)
            if (currentButton == clicked) return;

            // Reset màu của nút trước (đưa về xám)
            if (currentButton != null)
            {
                // đặt nút trước về màu xám (giữ nguyên text/font mặc định)
                currentButton.UseVisualStyleBackColor = false;
                currentButton.BackColor = Color.FromArgb(51, 51, 76);
                currentButton.ForeColor = Color.Gainsboro;
                currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            }

            // Chọn màu cho nút vừa click
            Color color;
            if (useFixedColors && fixedPalette != null && fixedPalette.TryGetValue(clicked.Name, out var hex))
            {
                try
                {
                    color = ColorTranslator.FromHtml(hex);
                }
                catch
                {
                    color = SelectThemeColor();
                }
            }
            else
            {
                color = SelectThemeColor();
            }

            // Áp màu cho nút được click
            currentButton = clicked;
            currentButton.UseVisualStyleBackColor = false;
            currentButton.BackColor = color;
            currentButton.ForeColor = GetReadableTextColor(color);
            currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));

            // Cập nhật titlebar & logo theo màu của nút active
            panelTitleBar.BackColor = color;
            panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3f);
            ThemeColor.PrimaryColor = color;
            ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3f);

            btnCloseChildForm.Visible = true;
        }

        // Đưa tất cả nút về trạng thái xám (mặc định) — sẽ được gọi khi load và reset
        private void ResetAllButtonsToGray()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button b)
                {
                    b.UseVisualStyleBackColor = false;
                    b.BackColor = Color.FromArgb(51, 51, 76);
                    b.ForeColor = Color.Gainsboro;
                    b.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatAppearance.MouseOverBackColor = ControlPaint.Light(b.BackColor);
                    b.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(b.BackColor);
                }
            }
            currentButton = null;
        }

        // Mở child form
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        // ===== Event handlers cho các nút menu =====

        // orders (Tiến Độ Công Việc)
        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormQLCV.FormOrders(), sender);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormMonHoc.FormMonHoc(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCustomer.FormCustomers(), sender);
        }

        private void btnReporting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormPlan.FormPlan(), sender);
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormBangDiem.FormBangDiem(), sender);
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSetting.FormSetting(), sender);
        }



        // Đóng child form hiện tại và reset giao diện
        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        // Reset về HOME: titlebar/logo về default, các nút về xám
        private void Reset()
        {
            chart7.BringToFront();
            chart7.Visible = true;
            chart7.Invalidate();
            chart7.Update();
            ResetAllButtonsToGray();
            LoadAllCharts();
            lblTitle.Text = "Trang chủ";

            var defaultPrimary = Color.FromArgb(0, 150, 136); // xanh ngọc (titlebar mặc định)
            var defaultSecondary = Color.FromArgb(39, 39, 58);

            panelTitleBar.BackColor = defaultPrimary;
            panelLogo.BackColor = defaultSecondary;

            ThemeColor.PrimaryColor = defaultPrimary;
            ThemeColor.SecondaryColor = defaultSecondary;

            currentButton = null;
            btnCloseChildForm.Visible = false;
            activeForm = null;
        }

        // Các event trống (Designer giữ)


        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            chart7.BringToFront();
            chart7.Visible = true;
            chart7.Invalidate();
            chart7.Update();

            SetGreetingLabel();

            // Mặc định khi load: giữ tất cả nút màu xám (không áp palette)
            ResetAllButtonsToGray();
            LoadAllCharts();
            // Nếu muốn, có thể thiết lập titlebar theo ThemeColor hiện tại
            if (ThemeColor.PrimaryColor != Color.Empty)
            {
                panelTitleBar.BackColor = ThemeColor.PrimaryColor;
                panelLogo.BackColor = ThemeColor.SecondaryColor;
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Ép chắc chắn kích thước client area (lần cuối sau tất cả layout)
            this.ClientSize = new Size(809, 503);
            this.CenterToScreen();
            this.Refresh();
        }


        private void FormMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đã có DialogResult hợp lệ thif cho phép thoát chạy Program.cs nhận kết quả
            if (this.DialogResult == DialogResult.Retry || this.DialogResult == DialogResult.OK)
            {
                return;
            }

            // Chặn đóng form mặc định
            e.Cancel = true;

            var result = MessageBox.Show(
                "Bạn muốn Đăng xuất và Đăng nhập lại?",
                "Xác nhận",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Đăng xuất => Program.cs sẽ lặp lại login
                this.DialogResult = DialogResult.Retry;
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
            {
                // Thoát hẳn toàn bộ ứng dụng
                this.DialogResult = DialogResult.OK;
                Application.ExitThread();
                e.Cancel = false;
            }
            // Cancel => Giữ nguyên form
        }

        private void btn_MyJob_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCVGV.FormMyJob(), sender);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn Đăng xuất và trở về màn hình Đăng nhập không?",
                "Xác nhận Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            
            if (confirm == DialogResult.Yes)
            {
                // 1. Đặt cờ cho Program.cs
                this.DialogResult = DialogResult.Retry;

                // 2. Đóng form hiện tại (kích hoạt Application.Run trong Program.cs kết thúc)
                this.Close();
            }
        }

        private void LoadDashboardChart()
        {
            try
            {
                DataTable dt = CongViecBUS.GetAllCongViec();
                if (dt == null || dt.Rows.Count == 0) return;

                var statusData = dt.AsEnumerable()
                    .Select(row =>
                    {
                        string trangThai = row.Field<string>("TrangThai");
                        DateTime? hanChot = row.Field<DateTime?>("HanChot");

                        if (trangThai != "HOAN_THANH"
                            && hanChot.HasValue
                            && hanChot.Value.Date < DateTime.Now.Date)
                        {
                            return "Trễ";
                        }
                        if (trangThai == "MOI") return "Mới";
                        if (trangThai == "DANG_LAM") return "Đang làm";
                        if (trangThai == "HOAN_THANH") return "Đã hoàn thành";
                        return "Khác";
                    })
                    .GroupBy(st => st)
                    .Select(g => new { Status = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                int total = statusData.Sum(x => x.Count);
                if (total == 0) return;

                chartDashboard.Series.Clear();
                chartDashboard.Titles.Clear();

                var series = new Series("DashboardSeries")
                {
                    ChartType = SeriesChartType.Doughnut,
                    IsValueShownAsLabel = true,
                    Font = new Font("Arial", 8, FontStyle.Bold)
                };

                foreach (var item in statusData)
                {
                    double percent = (double)item.Count / total * 100;
                    int idx = series.Points.AddXY(item.Status, percent);
                    var point = series.Points[idx];

                    point.Label = percent.ToString("0") + "%";
                    point.LegendText = item.Status;

                    if (item.Status == "Trễ") point.Color = Color.Red;
                    else if (item.Status == "Đã hoàn thành") point.Color = Color.SeaGreen;
                    else if (item.Status == "Đang làm") point.Color = Color.DodgerBlue;
                    else point.Color = Color.Gray;
                }

                chartDashboard.Titles.Add("Tổng quan trạng thái công việc");
                chartDashboard.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ: " + ex.Message);
            }
        }
        private void LoadUserStatusChart()
        {
            try
            {
                DataTable dt = UsersBUS.GetAllUsers();
                if (dt == null || dt.Rows.Count == 0) return;

                // Gom nhóm
                int locked = dt.AsEnumerable().Count(r => r.Field<bool>("IsLocked") == true);
                int unlocked = dt.AsEnumerable().Count(r => r.Field<bool>("IsLocked") == false);

                chart6.Series.Clear();
                chart6.Titles.Clear();
                chart6.Legends.Clear();

                // Legend
                chart6.Legends.Add("Legend");
                chart6.Legends["Legend"].Docking = Docking.Right;

                // Series
                var series = new Series("UserStatus")
                {
                    ChartType = SeriesChartType.Doughnut,
                    IsValueShownAsLabel = true,
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };

                // Thêm dữ liệu
                int total = locked + unlocked;
                if (total == 0) return;

                double percentLocked = (double)locked / total * 100;
                double percentUnlocked = (double)unlocked / total * 100;

                int idx1 = series.Points.AddXY("Đang khóa", percentLocked);
                series.Points[idx1].Color = Color.Red;
                series.Points[idx1].LegendText = "Đang khóa";
                series.Points[idx1].Label = percentLocked.ToString("0") + "%";

                int idx2 = series.Points.AddXY("Không khóa", percentUnlocked);
                series.Points[idx2].Color = Color.SeaGreen;
                series.Points[idx2].LegendText = "Không khóa";
                series.Points[idx2].Label = percentUnlocked.ToString("0") + "%";

                chart6.Titles.Add("Tình trạng Người dùng");
                chart6.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi biểu đồ người dùng: " + ex.Message);
            }
        }
        private void LoadJobStatusForCurrentUser()
        {
            try
            {
                int userId = Program.CurrentUserId;

                DataTable dt = PhanCongBUS.GetPhanCongByUser(userId);
                if (dt == null || dt.Rows.Count == 0) return;

                int tre = 0;
                int moi = 0;
                int dangLam = 0;
                int hoanThanh = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string trangThai = row["TrangThaiGV"].ToString();
                    DateTime? hanChot = row["HanChot"] == DBNull.Value
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(row["HanChot"]);

                    // Trễ
                    if (trangThai != "HOAN_THANH"
                        && hanChot.HasValue
                        && hanChot.Value.Date < DateTime.Now.Date)
                    {
                        tre++;
                    }
                    else if (trangThai == "MOI")
                    {
                        moi++;
                    }
                    else if (trangThai == "DANG_LAM")
                    {
                        dangLam++;
                    }
                    else if (trangThai == "HOAN_THANH")
                    {
                        hoanThanh++;
                    }
                }

                int total = tre + moi + dangLam + hoanThanh;
                if (total == 0) return;

                chart7.Series.Clear();
                chart7.Titles.Clear();
                chart7.Legends.Clear();

                chart7.Legends.Add("Legend");
                chart7.Legends["Legend"].Docking = Docking.Right;

                var series = new Series("UserJobStatus")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = false
                };
                series["PieLabelStyle"] = "Disabled";

                void AddPoint(string name, int count, Color color)
                {
                    if (count <= 0) return;

                    double percent = (double)count / total * 100;
                    int idx = series.Points.AddXY(name, percent);
                    series.Points[idx].Color = color;
                    series.Points[idx].LegendText = name;
                }

                AddPoint("Mới", moi, Color.Orange);
                AddPoint("Đang làm", dangLam, Color.DodgerBlue);
                AddPoint("Hoàn thành", hoanThanh, Color.SeaGreen);
                AddPoint("Trễ", tre, Color.Red);

                chart7.Titles.Add("Tiến độ công việc của bạn");
                chart7.Series.Add(series);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chart7: " + ex.Message);
            }
        }

        private void LoadMonHocChart()
        {
            try
            {
                DataTable dt = MonHocBUS.GetAllMonHoc();
                if (dt == null || dt.Rows.Count == 0) return;

                int hk1 = 0;
                int hk2 = 0;

                foreach (DataRow row in dt.Rows)
                {
                    string hk = row["HocKy"].ToString().Trim();

                    if (hk == "HocKyI") hk1++;
                    else if (hk == "HocKyII") hk2++;
                }

                int total = hk1 + hk2;
                if (total == 0) return;

                // Reset chart
                chart4.Series.Clear();
                chart4.Titles.Clear();
                chart4.Legends.Clear();

                chart4.Legends.Add("Legend");
                chart4.Legends["Legend"].Docking = Docking.Right;

                Series series = new Series("MonHocTheoHocKy")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };

                void AddPoint(string label, int value, Color color)
                {
                    if (value <= 0) return;

                    int idx = series.Points.AddXY(label, value);
                    series.Points[idx].LegendText = $"{label}: {value}";
                    series.Points[idx].Label = value.ToString();
                    series.Points[idx].Color = color;
                }

                AddPoint("Học kỳ 1", hk1, Color.DodgerBlue);
                AddPoint("Học kỳ 2", hk2, Color.Orange);

                chart4.Series.Add(series);
                chart4.Titles.Add("Số lượng môn học theo học kỳ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chart môn học: " + ex.Message);
            }
        }



        private void LoadAllCharts()
        {
            LoadDashboardChart();
            LoadUserStatusChart();
            LoadJobStatusForCurrentUser();
            LoadMonHocChart();
            LoadChartGiangVien();
            LoadChart_TongCVTheoGV();
        }

        private void SetGreetingLabel()
        {
            int userid = Program.CurrentUserId;
            string username = UsersBUS.GetUserById(userid).UserName;
            int hour = DateTime.Now.Hour;

            string timeGreeting;

            if (hour < 12)
                timeGreeting = "buổi sáng";
            else if (hour < 18)
                timeGreeting = "buổi chiều";
            else
                timeGreeting = "buổi tối";

            label1.Text = $"Xin chào {username}, chúc bạn {timeGreeting} tốt lành!";
        }
        private void LoadChartGiangVien()
        {
            DataTable dt = PhanCongBUS.GetSoLuongCongViecTheoGV();
            if (dt == null || dt.Rows.Count == 0) return;

            System.Windows.Forms.DataVisualization.Charting.Chart chart = chart2;

            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Area1"));

            chart.Titles.Add("Tổng Công việc Chưa Hoàn thành theo Giảng viên");

            // Formatting trục X-Y
            var area = chart.ChartAreas["Area1"];
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Giảng viên";
            area.AxisY.Title = "Tổng số CV (Mới + Đang làm)";

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            System.Windows.Forms.DataVisualization.Charting.Series sTong = new System.Windows.Forms.DataVisualization.Charting.Series("Mới và Đang làm")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
                Color = System.Drawing.Color.SteelBlue, // Màu xanh dương thể hiện workload
                BorderWidth = 1,
                IsValueShownAsLabel = true,
                IsVisibleInLegend = true // Đảm bảo series này được hiển thị trong chú thích
            };

            foreach (DataRow row in dt.Rows)
            {
                string ten = row["FullName"].ToString();
                int moi = Convert.ToInt32(row["Moi"]);
                int dangLam = Convert.ToInt32(row["DangLam"]);

                // Tính tổng: CV cần xác nhận + CV đang làm
                int tongCongViec = moi + dangLam;

                // Chỉ thêm vào biểu đồ nếu có công việc
                if (tongCongViec > 0)
                {
                    sTong.Points.AddXY(ten, tongCongViec);

                    // Tùy chọn: Thêm tooltip chi tiết (nếu cần)
                    sTong.Points.Last().ToolTip = $"Mới: {moi}, Đang làm: {dangLam}";
                }
            }

            // Chỉ thêm Series nếu nó có data
            if (sTong.Points.Count > 0)
            {
                chart.Series.Add(sTong);
                chart.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("Legend1"));
            }
        }

        private void LoadChart_TongCVTheoGV()
        {
            DataTable dt = PhanCongBUS.GetAllAssignmentsForChart();
            if (dt == null || dt.Rows.Count == 0) return;

            var chart = chart3;

            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(new ChartArea("Area1"));

            chart.Titles.Add("Tổng Công việc theo Giảng viên");

            var area = chart.ChartAreas["Area1"];
            area.AxisX.Interval = 1;
            area.AxisX.Title = "Giảng viên";
            area.AxisY.Title = "Tổng số công việc";
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            Series sTong = new Series("Tổng công việc")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue,
                BorderWidth = 1,
                IsValueShownAsLabel = true,
                IsVisibleInLegend = true
            };

            // Gom nhóm theo GV
            var groups = dt.AsEnumerable()
                           .GroupBy(r => r.Field<string>("FullName"));

            foreach (var gv in groups)
            {
                int moi = 0, dangLam = 0, hoanThanh = 0, choDuyet = 0, tre = 0;

                foreach (var row in gv)
                {
                    string st = row.Field<string>("TrangThaiGV");
                    DateTime? hanChot = row["HanChot"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["HanChot"]);


                    // Tính TRỄ
                    if (st != "HOAN_THANH" && hanChot.HasValue && hanChot.Value.Date < DateTime.Now.Date)
                    {
                        tre++;
                        continue;
                    }

                    switch (st)
                    {
                        case "MOI": moi++; break;
                        case "DANG_LAM": dangLam++; break;
                        case "HOAN_THANH": hoanThanh++; break;
                        case "CHO_DUYET": choDuyet++; break;
                    }
                }

                int tong = moi + dangLam + hoanThanh + choDuyet + tre;

                if (tong > 0)
                {
                    int index = sTong.Points.AddXY(gv.Key, tong);
                    sTong.Points[index].ToolTip =
                        $"Mới: {moi}\nĐang làm: {dangLam}\nHoàn thành: {hoanThanh}\nChờ duyệt: {choDuyet}\nTrễ: {tre}";
                }
            }

            if (sTong.Points.Count > 0)
            {
                chart.Series.Add(sTong);
                chart.Legends.Add(new Legend("Legend"));
            }
        }


        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormMonHoc.FormMonHoc(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormQLCV.FormOrders(), sender);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCustomer.FormCustomers(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCVGV.FormMyJob(), sender);
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new WindowsFormsApp2.Forms.FormBaoCao.FormBaoCaoTongHop(), sender);
        }
    }
}
