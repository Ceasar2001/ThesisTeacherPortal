using System;
using System.Data.SQLite;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace TeacherPortal.PAL.Forms
{
    public partial class FormForgotPassword : Form
    {
        private string connectionString = @"Data Source=D:\VSdeveloptment\C#\TeacherPortal\Database.db;";

        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxClose, "Close");
        }

        private void textBoxEmail_Enter(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "example@gmail.com")
            {
                textBoxEmail.Clear();
                textBoxEmail.ForeColor = Color.Black;
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.Text = "example@gmail.com";
                textBoxEmail.ForeColor = Color.DarkGray;
            }

            if (!IsValidEmail(textBoxEmail.Text) || textBoxEmail.Text == "example@gmail.com")
            {
                pictureBoxError.Show();
            }
            else
            {
                pictureBoxError.Hide();
            }
        }

        private void pictureBoxError_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxError, "Invalid Email");
        }

        private void VerifyBtn_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();

            if (!IsValidEmail(email) || email == "example@gmail.com")
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT User_Name, User_Password FROM User_Table WHERE User_Email = @Email";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userName = reader["User_Name"].ToString();
                                string password = reader["User_Password"].ToString();

                                MessageBox.Show($"Your username is '{userName}' and your password is '{password}'.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Close the form after the user clicks OK
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Email not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormForgotPassword_Load(object sender, EventArgs e)
        {
            pictureBoxError.Hide();
        }
    }
}
