using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Forms.FormCVGV.ChildFormCVGV;

namespace WindowsFormsApp2.Forms.FormCVGV
{
    public partial class FormMyJob : Form
    {
        public FormMyJob()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FormWorded addForm = new FormWorded();
            addForm.ShowDialog();
        }

        private void button_them_Click(object sender, EventArgs e)
        {
            FormListNewJob addForm = new FormListNewJob();
            addForm.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormWorking addForm = new FormWorking();
            addForm.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FormGiveBack addForm = new FormGiveBack();
            addForm.ShowDialog();
        }

        private void FormMyJob_Load(object sender, EventArgs e)
        {
            if (Program.CurrentPerms.Contains("GV"))
            {
                btn_TraLai.Visible = false;
            }
        }
    }
}
