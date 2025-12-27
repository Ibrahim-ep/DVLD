using DVLD_Business;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.Users
{
    public partial class frmAdd_Edit_User : Form
    {

        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        private clsUser _User;
        private int _UserID;
        public frmAdd_Edit_User()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAdd_Edit_User(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
           
        }

        private void _ResetDefultValues()
        {
            if (_Mode == enMode.AddNew)
            { 
                lblTitle.Text = "Add New User";
                _User = new clsUser();
            }
            else
            {
                lblTitle.Text = "Update User";
            }

            lblUserID.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
           
        }

        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            ctrPerson_Info_WithFilter1.FilterEnabled = false;

            if (_User != null)
            {
                ctrPerson_Info_WithFilter1.LoadPersonInfo(_User.Person.ID);
                ctrPerson_Info_WithFilter1.FilterEnabled = false;
                lblUserID.Text = _UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                chbIsActive.Checked = _User.isActive;
            }
            else
            {
                MessageBox.Show("User was not found");
                this.Close();
                return;
            }    
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["tbLoginInfo"];
                return;
                
            }
            
            if (ctrPerson_Info_WithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistsForPeraon(ctrPerson_Info_WithFilter1.PersonID))
                {
                    MessageBox.Show("User Already Exists");
                   
                }

                else
                {
                    tabControl1.SelectedTab = tabControl1.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please select a person");
               
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.isUserExists(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }

            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.isUserExists(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    ;
                }
            }
            
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password not matched");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.UserName = txtUserName.Text;
            
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                _User.Password = txtPassword.Text;
            }
            else
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.isActive = chbIsActive.Checked;

            _User.Person = ctrPerson_Info_WithFilter1.SelectedPersonInfo;

            if (_User.Save())
            {
                MessageBox.Show("User Saved Successfully");
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
            }
            else
            {
                MessageBox.Show("Failed to save the User", "Error");
            }

            
        }

        private void frmAdd_Edit_User_Load_1(object sender, EventArgs e)
        {
            _ResetDefultValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password mustn't be empty");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }
    }
}
