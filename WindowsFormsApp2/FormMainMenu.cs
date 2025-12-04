using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            OpenChildForm(new Forms.FormCongViec.FormCongViec(), sender);
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSetting.FormSetting(), sender);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            // TODO: implement báo cáo
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
            ResetAllButtonsToGray();
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            // Không set Size ở đây nữa (đã set ở constructor)
            // this.Size = new Size(609, 403); // <-- remove this line
            this.StartPosition = FormStartPosition.CenterScreen;

            // Mặc định khi load: giữ tất cả nút màu xám (không áp palette)
            ResetAllButtonsToGray();

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



        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelDesktopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
