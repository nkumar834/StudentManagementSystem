using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Teacherform : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        private Teacher _teacher;

        public Teacherform(Teacher teacher)
        {
            InitializeComponent();
            _teacher = teacher;
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _teacher.Show();
        }


        private void Teacherform_Load(object sender, EventArgs e)
        {

        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a TEACHID.");
                return;
            }

            int teachId;
            if (!int.TryParse(textBox1.Text, out teachId))
            {
                MessageBox.Show("Please enter a valid TEACHID.");
                return;
            }

            connection();

            
            string query = "SELECT fname, dob, gender, phone, email, qualification, joining_date, subject " +
                           "FROM teachertab WHERE TEACHID = " + teachId;

            cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                textBox2.Text = reader["fname"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(reader["dob"]);
                string gender = reader["gender"].ToString();
                if (gender == "Male")
                    radioButtonMale.Checked = true;
                else if (gender == "Female")
                    radioButtonFemale.Checked = true;

                textBox4.Text = reader["phone"].ToString();
                textBox5.Text = reader["email"].ToString();
                comboBox1.SelectedItem = reader["qualification"].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(reader["joining_date"]);
                comboBox2.SelectedItem = reader["subject"].ToString();
            }
            else
            {
                MessageBox.Show("No teacher found with the provided TEACHID.");
            }

            reader.Close();
            con.Close();
        }

        private void btnNew_Click_2(object sender, EventArgs e)
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a TEACHID to save changes.");
                return;
            }

            int teachId;
            if (!int.TryParse(textBox1.Text, out teachId))
            {
                MessageBox.Show("Please enter a valid TEACHID.");
                return;
            }

            string gender = radioButtonMale.Checked ? "Male" : radioButtonFemale.Checked ? "Female" : "";

            connection();

            string query = "UPDATE teachertab SET "
                            + "fname = '" + textBox2.Text + "', "
                            + "dob = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', "
                            + "gender = '" + gender + "', "
                            + "phone = '" + textBox4.Text + "', "
                            + "email = '" + textBox5.Text + "', "
                            + "qualification = '" + comboBox1.SelectedItem.ToString() + "', "
                            + "joining_date = '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', "
                            + "subject = '" + comboBox2.SelectedItem.ToString() + "' "
                            + "WHERE TEACHID = " + teachId;
            cmd = new SqlCommand(query, con);
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data updated successfully.");
                }
                else
                {
                    MessageBox.Show("No data was updated. Please check the TEACHID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            _teacher.Show();
        }
    }
}
