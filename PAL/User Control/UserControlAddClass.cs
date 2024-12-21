using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TeacherPortal.PAL.User_Control
{
    public partial class UserControlAddClass : UserControl
    {
        // SQLite connection string
        private string sql = @"Data Source=D:\VSdeveloptment\C#\TeacherPortal\Database.db;Version=3;";

        private string CID = ""; // Class ID for update and delete operations

        public UserControlAddClass()
        {
            InitializeComponent();
        }

        private void IntegerType(KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }
        }

        public void ClearTextBox()
        {
            textBoxName.Clear();
            textBoxHmStudent.Clear();
            textBoxMale.Clear();
            textBoxFemale.Clear();
            tabControlAddClass.SelectedTab = tabPageAddClass;
        }

        private void ClearTextBox1()
        {
            textBoxName1.Clear();
            textBoxHmStudent1.Clear();
            textBoxMale1.Clear();
            textBoxFemale1.Clear();
            CID = "";
        }

        private void UserControlAddClass_Load(object sender, EventArgs e)
        {
        }

        private void pictureBoxSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(pictureBoxSearch, "Search");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Trim() == string.Empty ||
                textBoxHmStudent.Text.Trim() == string.Empty ||
                textBoxMale.Text.Trim() == string.Empty ||
                textBoxFemale.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill up all fields", "Required All Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    using (var connection = new SQLiteConnection(sql))
                    {
                        connection.Open();
                        string query = "INSERT INTO Class_Table (Class_Name, Class_Total, Class_Male, Class_Female) VALUES (@Name, @Total, @Male, @Female)";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", textBoxName.Text.Trim());
                            command.Parameters.AddWithValue("@Total", int.Parse(textBoxHmStudent.Text.Trim()));
                            command.Parameters.AddWithValue("@Male", int.Parse(textBoxMale.Text.Trim()));
                            command.Parameters.AddWithValue("@Female", int.Parse(textBoxFemale.Text.Trim()));
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Class added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTextBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabPageUpClass_Leave(object sender, EventArgs e)
        {
            ClearTextBox1();
        }

        private void tabPageSearchClass_Enter(object sender, EventArgs e)
        {
            textBoxSearch.Clear();
            LoadClassData();
        }

        private void tabControlAddClass_Enter(object sender, EventArgs e)
        {
            ClearTextBox1();
        }

        private void tabControlAddClass_Leave(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private void textBoxHmStudent_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxMale_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxFemale_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxHmStudent1_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxMale1_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxFemale1_KeyPress(object sender, KeyPressEventArgs e)
        {
            IntegerType(e);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            LoadClassData(textBoxSearch.Text.Trim());
        }

        private void LoadClassData(string search = "")
        {
            try
            {
                using (var connection = new SQLiteConnection(sql))
                {
                    connection.Open();
                    string query = "SELECT * FROM Class_Table";
                    if (!string.IsNullOrEmpty(search))
                    {
                        query += " WHERE Class_Name LIKE @Search";
                    }
                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        if (!string.IsNullOrEmpty(search))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + search + "%");
                        }
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewClass.DataSource = dt;
                        dataGridViewClass.Columns[0].Visible = false;
                        labelTotalClass.Text = dt.Rows.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewClass.Rows[e.RowIndex];
                CID = row.Cells[0].Value.ToString();
                textBoxName1.Text = row.Cells[1].Value.ToString();
                textBoxHmStudent1.Text = row.Cells[2].Value.ToString();
                textBoxMale1.Text = row.Cells[3].Value.ToString();
                textBoxFemale1.Text = row.Cells[4].Value.ToString();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CID))
            {
                if (textBoxName1.Text.Trim() == string.Empty ||
                    textBoxHmStudent1.Text.Trim() == string.Empty ||
                    textBoxMale1.Text.Trim() == string.Empty ||
                    textBoxFemale1.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please fill up all fields", "Required All Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        using (var connection = new SQLiteConnection(sql))
                        {
                            connection.Open();
                            string query = "UPDATE Class_Table SET Class_Name = @Name, Class_Total = @Total, Class_Male = @Male, Class_Female = @Female WHERE Class_ID = @CID";
                            using (var command = new SQLiteCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Name", textBoxName1.Text.Trim());
                                command.Parameters.AddWithValue("@Total", int.Parse(textBoxHmStudent1.Text.Trim()));
                                command.Parameters.AddWithValue("@Male", int.Parse(textBoxMale1.Text.Trim()));
                                command.Parameters.AddWithValue("@Female", int.Parse(textBoxFemale1.Text.Trim()));
                                command.Parameters.AddWithValue("@CID", CID);
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Class updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextBox1();
                        LoadClassData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a class in the row to update", "Select Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CID))
            {
                var dialogResult = MessageBox.Show("Are you sure you want to delete this class?", "Delete Class", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = new SQLiteConnection(sql))
                        {
                            connection.Open();
                            string query = "DELETE FROM Class_Table WHERE Class_ID = @CID";
                            using (var command = new SQLiteCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@CID", CID);
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Class deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextBox1();
                        LoadClassData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a class in the row to delete", "Select Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
