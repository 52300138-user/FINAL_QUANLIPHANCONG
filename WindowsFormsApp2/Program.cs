using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormsApp2.Forms.Auth;
using WindowsFormsApp2.Forms.Main;

namespace WindowsFormsApp2
{
    public static class Program
    {
        public static int CurrentUserId = 0;
        public static HashSet<string> CurrentPerms = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        START_APP:
            // ==========================================
            // B1: ĐỌC CHUỖI KẾT NỐI
            // ==========================================
            string connStr;

            try
            {
                var cs = ConfigurationManager.ConnectionStrings["MyDB"];
                if (cs == null || string.IsNullOrWhiteSpace(cs.ConnectionString))
                    throw new Exception("Không tìm thấy hoặc chuỗi kết nối 'MyDB' rỗng.");

                connStr = cs.ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run(new FormConfigSQL());
                // Sau khi FormConfigSQL restart → chạy lại Program
                return;
            }

            // ==========================================
            // B2: TEST CONNECT SQL
            // ==========================================
            string error;
            if (!DbChecker.TestConnection(connStr, out error))
            {
                MessageBox.Show(
                    "Không thể kết nối SQL Server:\n\n" + error,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Application.Run(new FormConfigSQL());
                return;
            }

        // ==========================================
        // B3: VÒNG LẶP LOGIN – LOGOUT
        // ==========================================
        LOGIN_AGAIN:
            int loginStatus = RunLoginScreen();

            if (loginStatus == -1)
                return; // người dùng đóng form login

            // B4: MỞ FORM CHÍNH THEO ROLE
            Form mainForm;

            if (CurrentPerms.Contains("TBM"))
                mainForm = new FormMainMenu();
            else if (CurrentPerms.Contains("GV"))
                mainForm = new FormMainMenu_GV();
            else
            {
                MessageBox.Show("Tài khoản không có quyền.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = RunMainForm(mainForm);

            if (result == DialogResult.Retry)  // LOGOUT
                goto LOGIN_AGAIN;

            // Nếu form muốn restart app (ví dụ đổi config SQL)
            if (result == DialogResult.Abort)
                goto START_APP;

            return;
        }

        // ==========================================
        // FUNCTION: CHẠY FORM LOGIN
        // ==========================================
        private static int RunLoginScreen()
        {
            using (var login = new Dangnhap())
            {
                var dr = login.ShowDialog();

                if (dr != DialogResult.OK)
                    return -1;

                CurrentUserId = login.LoggedUserId;

                CurrentPerms = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    login.LoggedRole
                };

                return 1;
            }
        }

        // ==========================================
        // FUNCTION: CHẠY FORM CHÍNH
        // ==========================================
        private static DialogResult RunMainForm(Form f)
        {
            f.ShowDialog();
            return f.DialogResult;
        }
    }
}
