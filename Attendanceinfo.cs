using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Attendanceinfo : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        public Attendanceinfo()
        {
            InitializeComponent();
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        void fillGrid(string searchTerm = "")
        {
            connection();

            string query;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = "SELECT * FROM attendance WHERE program LIKE '%" + searchTerm + "%'";
            }
            else
            {
                query = "SELECT * FROM attendance";
            }

            da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Attendance at = new Attendance(this);
            at.ShowDialog();
        }

        private void Attendanceinfo_Load(object sender, EventArgs e)
        {
            fillGrid(); 
        }

       

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            fillGrid(textBox6.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Main back = new Main();
            back.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void Attendanceinfo_Load_1(object sender, EventArgs e)
        {
            fillGrid();
        }
    }
}
