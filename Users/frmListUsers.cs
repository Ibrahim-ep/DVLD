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
    public partial class frmListUsers : Form
    {
        DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _Refresh()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvListUsers.DataSource = _dtAllUsers;
            lblNumberOfRecords.Text = dgvListUsers.Rows.Count.ToString();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_User frm = new frmAdd_Edit_User();
            frm.ShowDialog();
            _Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_User frm = new frmAdd_Edit_User(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _Refresh();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.Show();
        }

        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_User frm = new frmAdd_Edit_User();
            frm.Show();
            _Refresh();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You want to delete this user ?") == DialogResult.OK)
            {
                if (clsUser.DeleteUser(Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("User deleted successfully");
                }
                else
                {
                    MessageBox.Show("The user wasn't deleted");
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterBy.Visible = false;
                cbFilterByIsActive.Visible = true;
                cbFilterByIsActive.Focus();
                cbFilterByIsActive.SelectedIndex = 0;
            }

            else
            {
                txtFilterBy.Visible = (cbFilterBy.Text != "None");
                cbFilterByIsActive.Visible = false;

                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvListUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            lblNumberOfRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void cbFilterByIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbFilterByIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblNumberOfRecords.Text = _dtAllUsers.Rows.Count.ToString();


        }
    }
    
}
