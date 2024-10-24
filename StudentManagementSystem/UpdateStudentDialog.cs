using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class UpdateStudentDialog : Form
    {
        public UpdateStudentDialog()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtFullName.Text;
            string dob = dateTimePickerDOB.Text;
            string mobile = txtPhone.Text;
            string gender = "";
            bool isChecked = radioButtonMale.Checked;
            if (isChecked)
            {
                gender = radioButtonMale.Text;
            }
            else
            {
                gender = radioButtonFemale.Text;
            }
            string contact = txtContact.Text;
            string email = txtEmail.Text;
            string program = comboBox1.Text;
           

        }

        private void UpdateStudentDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
