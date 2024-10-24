using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Teacher : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        public Teacher()
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
                query = "SELECT * FROM teachertab WHERE fname LIKE '%" + searchTerm + "%'";
            }
            else
            {
                query = "SELECT * FROM teachertab";
            }

            da = new SqlDataAdapter(query, con);

            ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTeacher ad = new AddTeacher(this);
            ad.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Main back = new Main();
            back.Show();
        }

        private void Teacher_Load_1(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            fillGrid(textBox1.Text);
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to permanently delete this record?",
                                                            "Delete Confirmation",
                                                            MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.OK)
                {
                    connection();
                    try
                    {
                        string query = "DELETE FROM teachertab WHERE TEACHID = " + textBox2.Text;

                        SqlCommand cmd = new SqlCommand(query, con);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                            fillGrid(); 
                        }
                        else
                        {
                            MessageBox.Show("No record found with this ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Deletion canceled.");
                }
            }
            else
            {
                MessageBox.Show("Please enter an ID to delete.");
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Teacherform tf = new Teacherform(this);
            tf.ShowDialog();
        }
    }
}
