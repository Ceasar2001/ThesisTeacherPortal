using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal.PAL.User_Control
{
    public partial class UserControlDashboard : UserControl
    {
        private string sql = @"Data Source=D:\VSdeveloptment\C#\TeacherPortal\Database.db;Version=3;";

        public UserControlDashboard()
        {
            InitializeComponent();
        }

        // Method to count the total number of classes
        public void Count()
        {
            try
            {
                using (var connection = new SQLiteConnection(sql))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Class_Table";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        int totalClasses = Convert.ToInt32(command.ExecuteScalar());
                        labelTotalClasses.Text = totalClasses.ToString(); // Update the label with the total
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load event to call the Count method
        private void UserControlDashboard_Load(object sender, EventArgs e)
        {
            Count();
        }
    }
}
