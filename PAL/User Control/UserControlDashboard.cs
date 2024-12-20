using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeacherPortal.PAL.User_Control
{
    public partial class UserControlDashboard : UserControl
    {
        private string sql = @"Data Source = D:\VSdeveloptment\C#\TeacherPortal\Database.db;";

        public UserControlDashboard()
        {
            InitializeComponent();
        }

        public void Count()
        {  
        }

        private void UserControlDashboard_Load(object sender, EventArgs e)
        {
            Count();
        }
    }
}
