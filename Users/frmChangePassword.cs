using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show("Didn't find the user");
                this.Close();
                return;
            }
            ctrUser_Info1.LoadUserData(_UserID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid");
                return;
            }

            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password changed successfully.");
            }
            else
            {
                MessageBox.Show("Failed to change password.");
            }
            
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password is requirade");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (_User.Password != txtCurrentPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password if is not matched");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }    


        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "New Password is requirade");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password is requirade");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }

            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel= true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password does not match New Password");
                return;
            }
            else
            {
                errorProvider1.SetError (txtConfirmPassword, null);
            }
        }
    }
}
