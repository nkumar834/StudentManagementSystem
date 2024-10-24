using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Student2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        private Studentinfo _studentinfo;

        public Student2(Studentinfo studentinfo)
        {
            InitializeComponent();
            _studentinfo = studentinfo;
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        void fillGrid()
        {
            da = new SqlDataAdapter("SELECT * FROM studentab", con);
            ds = new DataSet();
            da.Fill(ds);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _studentinfo.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 ||  
                (!radioButtonMale.Checked && !radioButtonFemale.Checked) ||  
                string.IsNullOrWhiteSpace(textBox4.Text) ||  
                string.IsNullOrWhiteSpace(textBox5.Text) ||  
                comboBox2.SelectedIndex == -1 ||  
                string.IsNullOrWhiteSpace(textBox6.Text) ||  
                string.IsNullOrWhiteSpace(textBox7.Text) ||  
                string.IsNullOrWhiteSpace(textBox8.Text))    
            {
                MessageBox.Show("Error!! Empty form is not allowed", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string gender = radioButtonMale.Checked ? "Male" : "Female"; 
                connection();
                string query = "INSERT INTO studentab (name, dob, gender, phone, email, program, admi_date, nation, f_name, f_contact, f_occupaction) VALUES ('" + textBox2.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + gender + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + comboBox1.SelectedItem.ToString() + "', '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "', '" + comboBox2.SelectedItem.ToString() + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "')";
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
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            radioButtonMale.Checked = false;
            radioButtonFemale.Checked = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Student2_Load(object sender, EventArgs e)
        {
            connection();

            cmd = new SqlCommand("SELECT ISNULL(MAX(ID), 0) FROM studentab", con);

            object result = cmd.ExecuteScalar();
            Int64 abc = Convert.ToInt64(result);

            label14.Text = (abc + 1).ToString();
            fillGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            _studentinfo.Show();
        }
    }
}
