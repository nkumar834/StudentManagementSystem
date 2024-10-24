using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class StudentDetails : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        private Studentinfo _studentinfo;

        public StudentDetails(Studentinfo studentInfo)
        {
            InitializeComponent();
            _studentinfo = studentInfo;
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _studentinfo.Show();
        }

        private void StudentDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a STUDENT ID.");
                return;
            }

            int studentId;
            if (!int.TryParse(textBox1.Text, out studentId))
            {
                MessageBox.Show("Please enter a valid STUDENT ID.");
                return;
            }

            connection();

            string query = "SELECT name, dob, gender, phone, email, program, admi_date, nation, f_name, f_contact, f_occupaction " +
                           "FROM studentab WHERE Id = " + studentId;

            cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                textBox2.Text = reader["name"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(reader["dob"]);
                string gender = reader["gender"].ToString();
                if (gender == "Male")
                    radioButtonMale.Checked = true;
                else if (gender == "Female")
                    radioButtonFemale.Checked = true;

                textBox4.Text = reader["phone"].ToString();
                textBox5.Text = reader["email"].ToString();
                comboBox1.SelectedItem = reader["program"].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(reader["admi_date"]);
                comboBox2.SelectedItem = reader["nation"].ToString();
                textBox6.Text = reader["f_name"].ToString();
                textBox7.Text = reader["f_contact"].ToString();
                textBox8.Text = reader["f_occupaction"].ToString();
            }
            else
            {
                MessageBox.Show("No student found with the provided STUDENT ID.");
            }

            reader.Close();
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a STUDENT ID to save changes.");
                return;
            }

            int studentId;
            if (!int.TryParse(textBox1.Text, out studentId))
            {
                MessageBox.Show("Please enter a valid STUDENT ID.");
                return;
            }

            string gender = radioButtonMale.Checked ? "Male" : radioButtonFemale.Checked ? "Female" : "";

            connection();

            string query = "UPDATE studentab SET "
                            + "name = '" + textBox2.Text + "', "
                            + "dob = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', "
                            + "gender = '" + gender + "', "
                            + "phone = '" + textBox4.Text + "', "
                            + "email = '" + textBox5.Text + "', "
                            + "program = '" + comboBox1.SelectedItem.ToString() + "', "
                            + "admi_date = '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', "
                            + "nation = '" + comboBox2.SelectedItem.ToString() + "', "
                            + "f_name = '" + textBox6.Text + "', "
                            + "f_contact = '" + textBox7.Text + "', "
                            + "f_occupaction = '" + textBox8.Text + "' "
                            + "WHERE Id = " + studentId;

            cmd = new SqlCommand(query, con);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Data updated successfully.");
            }
            else
            {
                MessageBox.Show("No data was updated. Please check the STUDENT ID.");
            }

            con.Close();
        }

        private void btnNew_Click_1(object sender, EventArgs e)
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
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            _studentinfo.Show();
        }
    }
}
