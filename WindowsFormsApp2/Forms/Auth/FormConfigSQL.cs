using System;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApp2.Forms.Auth
{
    public partial class FormConfigSQL : Form
    {
        public FormConfigSQL()
        {
            InitializeComponent();
        }

        private void FormConfigSQL_Load(object sender, EventArgs e)
        {
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["MyDB"];
                if (cs != null && !string.IsNullOrWhiteSpace(cs.ConnectionString))
                {
                    var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(cs.ConnectionString);
                    txtServer.Text = builder.DataSource;
                    txtDatabase.Text = builder.InitialCatalog;
                }
            }
            catch
            {
                // bỏ mặc — không auto fill giá trị sai
            }
        }

        private string BuildConn()
        {
            string server = txtServer.Text.Trim();
            string db = txtDatabase.Text.Trim();

            if (string.IsNullOrWhiteSpace(server))
                throw new Exception("Server không được để trống.");
            if (string.IsNullOrWhiteSpace(db))
                throw new Exception("Database không được để trống.");

            // CHUẨN — không ép IntegratedSecurity
            return $"Data Source={server};Initial Catalog={db};Integrated Security=True;TrustServerCertificate=True;";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string conn = BuildConn();

                string error;
                if (DbChecker.TestConnection(conn, out error))
                    MessageBox.Show("Kết nối thành công.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Không thể kết nối SQL Server.\n\n" + error,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string newConn = BuildConn();

                string error;
                if (!DbChecker.TestConnection(newConn, out error))
                {
                    MessageBox.Show("Không thể kết nối với cấu hình này.\n\n" + error,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // CHUẨN – bắt buộc load config từ chính EXE đang chạy
                var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                if (config.ConnectionStrings.ConnectionStrings["MyDB"] == null)
                {
                    config.ConnectionStrings.ConnectionStrings.Add(
                        new ConnectionStringSettings("MyDB", newConn, "System.Data.SqlClient"));
                }
                else
                {
                    config.ConnectionStrings.ConnectionStrings["MyDB"].ConnectionString = newConn;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("Đã lưu cấu hình mới. Ứng dụng sẽ khởi động lại.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            @"Cách tìm Server SQL:
            1) SSMS → Server name
            2) services.msc → SQL Server (MSSQLSERVER) = .
                                  SQL Server (SQLEXPRESS) = .\SQLEXPRESS
            3) CMD: sqlcmd -L
            4) '.' = server mặc định",
            "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
