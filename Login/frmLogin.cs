using DVLD.Global;
using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = string.Empty , Password = string.Empty ;

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                ckbRemeberMe.Checked = true;
            }
            else
                ckbRemeberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsGlobal.CurrentUser = clsUser.Find(txtUserName.Text,txtPassword.Text);

            if (clsGlobal.CurrentUser != null)
            {
                if (ckbRemeberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!clsGlobal.CurrentUser.isActive)
                {
                    MessageBox.Show("Your account is not active");
                    return;
                }

                this.Hide();
                Main frm = new Main(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("User Not Found", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
