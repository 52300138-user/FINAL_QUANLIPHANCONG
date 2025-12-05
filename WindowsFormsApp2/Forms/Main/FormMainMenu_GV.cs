using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;

namespace WindowsFormsApp2.Forms.Main
{
    public partial class FormMainMenu_GV : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        private bool useFixedColors = true;
        private Dictionary<string, string> fixedPalette;

        public FormMainMenu_GV()
        {
            InitializeComponent();

            random = new Random();
            btnCloseChildForm.Visible = false;
            InitFixedPalette();
        }

        private void InitFixedPalette()
        {
            fixedPalette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "btnProducts",     "#2A9D8F" },
                { "btnOrders",       "#4CAF50" },
                { "btnCustomer",     "#E63946" },
                { "btnReporting",    "#F4A261" },
                { "btnNotifications","#8A2BE2" },
                { "btnBaoCao",       "#E9C46A" },
                { "btn_setting",     "#264653" }
            };
        }

        private Color SelectThemeColor()
        {
            if (ThemeColor.ColorList == null || ThemeColor.ColorList.Count == 0)
                return Color.FromArgb(0, 150, 136);

            int index = random.Next(ThemeColor.ColorList.Count);
            while (index == tempIndex)
                index = random.Next(ThemeColor.ColorList.Count);

            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private Color GetReadableTextColor(Color bg)
        {
            double luminance = (0.299 * bg.R + 0.587 * bg.G + 0.114 * bg.B);
            return luminance > 186 ? Color.Black : Color.White;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender == null) return;
            var clicked = btnSender as Button;
            if (clicked == null) return;

            if (currentButton != clicked)
            {
                if (currentButton != null)
                {
                    currentButton.BackColor = Color.FromArgb(51, 51, 76);
                    currentButton.ForeColor = Color.Gainsboro;
                    currentButton.Font = new Font("Microsoft Sans Serif", 9F);
                }

                Color color = useFixedColors && fixedPalette.TryGetValue(clicked.Name, out var hex)
                    ? ColorTranslator.FromHtml(hex)
                    : SelectThemeColor();

                currentButton = clicked;
                currentButton.BackColor = color;
                currentButton.ForeColor = GetReadableTextColor(color);
                currentButton.Font = new Font("Microsoft Sans Serif", 11F);

                panelTitleBar.BackColor = color;
                panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3f);

                ThemeColor.PrimaryColor = color;
                ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3f);

                btnCloseChildForm.Visible = true;
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktopPanel.Controls.Add(childForm);
            panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void Reset()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = Color.FromArgb(51, 51, 76);
                    b.ForeColor = Color.Gainsboro;
                    b.Font = new Font("Microsoft Sans Serif", 9F);
                }
            }

            lblTitle.Text = "Trang chủ";
            var primary = Color.FromArgb(0, 150, 136);
            var secondary = Color.FromArgb(39, 39, 58);

            panelTitleBar.BackColor = primary;
            panelLogo.BackColor = secondary;

            ThemeColor.PrimaryColor = primary;
            ThemeColor.SecondaryColor = secondary;

            currentButton = null;
            activeForm = null;
            btnCloseChildForm.Visible = false;
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        private void FormMainMenu_GV_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            SetGreetingLabel();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.CenterToScreen();
        }

        // EVENTS
        private void btnNotifications_Click(object sender, EventArgs e) =>
            OpenChildForm(new Forms.FormCVGV.FormMyJob(), sender);



        private void btnBaoCao_Click(object sender, EventArgs e)
        {

            OpenChildForm(new Forms.FormMHGV.FormMonHoc_GV(), sender);
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

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormMainMenu_GV_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu đã có DialogResult hợp lệ => cho phép thoát chạy Program.cs nhận kết quả
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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSetting.FormSetting(), sender);
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
    }
}
