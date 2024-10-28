using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Attendance : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\source\repos\StudentManagementSystem\StudentManagementSystem\school_db.mdf;Integrated Security=True";

        private Attendanceinfo _attendanceinfo;

        public Attendance(Attendanceinfo attendanceinfo)
        {
            InitializeComponent();
            _attendanceinfo = attendanceinfo;
        }

        void connection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _attendanceinfo.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string program = comboBoxprogram.SelectedItem.ToString();
            string semester = comboBoxsemester.SelectedItem.ToString();
            string division = comboBoxdivision.SelectedItem.ToString();

            string attendanceStatus = string.Join(",", checkedListBoxrollno.CheckedItems.Cast<string>());
            byte[] attendanceBytes = Encoding.UTF8.GetBytes(attendanceStatus);
            string query = "INSERT INTO attendance (date, program, semester, division, attendance) VALUES ('"
                           + dateTimePickerdate.Value.ToString("yyyy-MM-dd") + "', '"
                           + program + "', '"
                           + semester + "', '"
                           + division + "', CONVERT(varbinary, '"
                           + Convert.ToBase64String(attendanceBytes) + "'))"; 

            connection(); 
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery(); 
            MessageBox.Show("Data Inserted successfully");
            con.Close(); 
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            dateTimePickerdate.Value = DateTime.Now;
            comboBoxprogram.SelectedIndex = -1;
            comboBoxsemester.SelectedIndex = -1;
            comboBoxdivision.SelectedIndex = -1;

            for (int i = 0; i < checkedListBoxrollno.Items.Count; i++)
            {
                checkedListBoxrollno.SetItemChecked(i, false);
            }
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            _attendanceinfo.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            _attendanceinfo.Show();
        }

        private void Attendance_Load_1(object sender, EventArgs e)
        {

        }
    }
}
