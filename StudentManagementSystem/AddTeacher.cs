using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class AddTeacher : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        private Teacher _teacher;

        public AddTeacher(Teacher teacher)
        {
            InitializeComponent();
            _teacher = teacher;
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        void fillGrid()
        {
            da = new SqlDataAdapter("SELECT * FROM teachertab", con);
            ds = new DataSet();
            da.Fill(ds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _teacher.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 ||
                (!radioButtonMale.Checked && !radioButtonFemale.Checked) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                comboBox2.SelectedIndex == -1 )
                
            {
                MessageBox.Show("Error!! Empty form is not allowed", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string gender = radioButtonMale.Checked ? "Male" : "Female";
                connection();
                string query = "INSERT INTO teachertab (fname, dob, gender, phone, email, qualification, joining_date, subject) VALUES ('"
                    + textBox2.Text + "', '"
                    + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '"
                    + gender + "', '"
                    + textBox4.Text + "', '"
                    + textBox5.Text + "', '"
                    + comboBox2.SelectedItem.ToString() + "', '"
                    + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', '"
                    + comboBox1.SelectedItem.ToString() + "')";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Inserted successfully");

                fillGrid();
                con.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker2.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
            radioButtonMale.Checked = false;
            radioButtonFemale.Checked = false;
        }
        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void AddTeacher_Load_1(object sender, EventArgs e)
        {
            connection();

            cmd = new SqlCommand("SELECT ISNULL(MAX(TEACHID), 0) FROM teachertab", con);
            object result = cmd.ExecuteScalar();
            Int64 abc = Convert.ToInt64(result);

            label14.Text = (abc + 1).ToString();
            fillGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            _teacher.Show();
        }
    }
}
