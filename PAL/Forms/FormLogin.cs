using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherPortal.PAL.Forms
{
    public partial class FormLogin : Form
    {

        private string sql = @"Data Source = D:\VSdeveloptment\C#\TeacherPortal\Database.db;";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxShow_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pictureBoxShow, "Show Password");
        }

        private void pictureBoxHide_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pictureBoxHide, "Hide Password");
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxShow.Hide();
            pictureBoxHide.Show();
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBoxShow.Show();
            pictureBoxHide.Hide();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            pictureBoxHide.Hide();
            pictureBoxError.Hide();
            labelError.Hide();
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pictureBoxClose, "Close");
        }

        private void pictureBoxMinimize_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pictureBoxMinimize, "Minimize Window");
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string Check = IsValidNamePass(sql, textBoxName.Text.Trim(), textBoxPassword.Text.Trim());

            if (!string.IsNullOrEmpty(textBoxName.Text.Trim()) && !string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(Check))
                {
                    FormMain formMain = new FormMain();
                    textBoxName.Clear();
                    textBoxPassword.Clear();
                    pictureBoxHide_Click(sender, e);
                    textBoxName.Focus();
                    pictureBoxError.Hide();
                    labelError.Hide();
                    formMain.ShowDialog();
                }
                else
                {
                    pictureBoxError.Show();
                    labelError.Show();
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }


        public static string IsValidNamePass(string connectionString, string username, string password)
        {
            string result = string.Empty;

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT User_Name FROM User_Table WHERE User_Name = @username AND User_Password = @password";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            result = reader["User_Name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return result;
        }


        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
               SelectNextControl(ActiveControl, true, true, true, true);
               e.Handled = true;
            }
        }

        private void textBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loginBtn.PerformClick();
                e.Handled = true;
            }
        }

        private void labelFP_Click(object sender, EventArgs e)
        {
            FormForgotPassword formForgotPassword = new FormForgotPassword();
            formForgotPassword.ShowDialog();
        }
    }
}
