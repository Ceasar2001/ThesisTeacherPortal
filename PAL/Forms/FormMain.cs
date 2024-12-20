using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherPortal.PAL.Forms
{
    public partial class FormMain : Form
    {
        public string Username, Role;

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                timerDateAndTime.Stop();
                Close();
            }
            else
            {
                panelExpand.Hide();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            panelExpand.Hide();
            labelUsername.Text = Username;
            labelRole.Text = Role;

            if (Role == "User")
            {
                buttonDashboard.Hide();
                buttonAddClass.Hide();
                buttonReport.Hide();
                buttonAddStudent.Hide();
            }
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            panelExpand.Hide();
            WindowState = FormWindowState.Minimized;
        }

        private void timerDateAndTime_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            labelTime.Text = now.ToString("F");
        }

        public FormMain()
        {
            InitializeComponent();
            timerDateAndTime.Start();
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonDashboard);
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonAddStudent);
        }

        private void buttonAddClass_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonAddClass);
        }

        private void buttonAttendance_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonAttendance);
        }

        private void buttonGrade_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonGrade);
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
                MoveSidePanel(buttonReport);
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            MoveSidePanel(buttonRegister);
        }

        private void pictureBoxExpand_Click(object sender, EventArgs e)
        {
            if(panelExpand.Visible)
            {
                panelExpand.Visible = false;
            }
            else
            {
                panelExpand.Visible = true;
            }
        }

        private void MoveSidePanel(Control button)
        {
            //panelSlide.Location = new Point(button.Location.X - button.Location.X, button.Location.Y - 180);
        }
    }
}
