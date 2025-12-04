using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp2.BUS;
using WindowsFormsApp2.Common;
using WindowsFormsApp2.Forms.ChildCustomer;

namespace WindowsFormsApp2.Forms.FormCustomer
{
    public partial class FormCustomers : Form
    {
        public FormCustomers()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
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

        private void LoadUsers()
        {
            try
            {
                DataTable dt = UsersBUS.GetUsersTable();
                // Tạm thời tắt MessageBox này đi cho đỡ phiền khi load lại
                // MessageBox.Show($"Số dòng lấy được: {dt.Rows.Count}");

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", Name = "columnID", DataPropertyName = "ID" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ tên", Name = "columnName", DataPropertyName = "FullName" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên đăng nhập", Name = "columnUserName", DataPropertyName = "UserName" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", Name = "columnEmail", DataPropertyName = "Email" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Địa chỉ", Name = "columnAddress", DataPropertyName = "Address" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới tính", Name = "columnGender", DataPropertyName = "GioiTinh" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chức vụ", Name = "columnRole", DataPropertyName = "ChucVu" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày tạo", Name = "columnCreatedAt", DataPropertyName = "CreatedAt" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SĐT", Name = "columnSDT", DataPropertyName = "SDT" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng thái", Name = "columnStatus", DataPropertyName = "TrangThai" });

            // Ẩn các button chức năng ban đầu
            button_sua.Visible = false;
            button_khoa.Visible = false;
            button_mokhoa.Visible = false;
            button_xoa.Visible = false;

            LoadTheme();
            LoadUsers();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool isRowSelected = dataGridView1.SelectedRows.Count > 0;

            button_sua.Visible = isRowSelected;
            button_khoa.Visible = isRowSelected;
            button_mokhoa.Visible = isRowSelected;
            button_xoa.Visible = isRowSelected; // 
        }

        private void label5_Click(object sender, EventArgs e)
        {
            FormAddCustomer addForm = new FormAddCustomer();
            addForm.FormClosed += (s, args) => LoadUsers();
            addForm.ShowDialog();
        }

      

        
       
       

        private void button_them_Click(object sender, EventArgs e)
        {
            FormAddCustomer formAdd = new FormAddCustomer();
            formAdd.ShowDialog();
            LoadUsers();
        }

        private void button_sua_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedUserId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["columnID"].Value);
            FormEditCustomer editForm = new FormEditCustomer(selectedUserId);
            editForm.FormClosed += (s, args) => LoadUsers();
            editForm.ShowDialog();
        }

        private void button_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["columnID"].Value);

            // Cảnh báo mạnh hơn vì đây là thao tác xóa
            DialogResult result = MessageBox.Show("Bạn có chắc muốn XÓA VĨNH VIỄN tài khoản này không?\nThao tác này không thể hoàn tác!",
                                                  "Xác nhận XÓA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool ok = UsersBUS.DeleteUser(selectedUserId);
                    if (ok)
                    {
                        MessageBox.Show("Đã xóa tài khoản thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Không xóa được tài khoản (không có dòng nào bị ảnh hưởng).",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Có thể có lỗi (ví dụ: xóa người dùng công việc - FK constraint)
                    MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_khoa_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["columnID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc muốn khóa tài khoản này không?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool ok = UsersBUS.SetLock(selectedUserId, true);
                    if (ok)
                    {
                        MessageBox.Show("Đã khóa tài khoản thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Không khóa được tài khoản (không có dòng nào bị ảnh hưởng).",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi khóa tài khoản: " + ex.Message, "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button_mokhoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần mở khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["columnID"].Value);

            try
            {
                bool ok = UsersBUS.SetLock(selectedUserId, false);
                if (ok)
                {
                    MessageBox.Show("Đã mở khóa tài khoản thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Không mở khóa được tài khoản (không có dòng nào bị ảnh hưởng).",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở khóa tài khoản: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}