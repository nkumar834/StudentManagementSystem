using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True"))
            {
                con.Open();
                string username = txtUsername.Text;
                string password = txtPassword.Text;


                SqlCommand cnn = new SqlCommand("SELECT Username, Password FROM login WHERE Username = '" + username + "' AND Password = '" + password + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Main mn = new Main();
                    mn.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username and password");
                }

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
