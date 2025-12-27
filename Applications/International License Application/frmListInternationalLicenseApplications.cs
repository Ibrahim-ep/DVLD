using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD.People;
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

namespace DVLD.Applications.International_License_Application
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        DataTable dtAllInternationalLicenses;

        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            dtAllInternationalLicenses = clsInternationalLicenseApplication.GetAllInternationalLicneses();

            dgvInternationalLicenses.DataSource = dtAllInternationalLicenses;

            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmIusseInternationalLicnese frm = new frmIusseInternationalLicnese();
            frm.ShowDialog();

            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;

            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    {
                        FilterColumn = "InternationalLicenseID";
                        break;
                    }
                case "ApplicationID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    }
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "LocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;

                    if (txtFilterValue.Text == "" && FilterColumn == "None")
                    {
                        dtAllInternationalLicenses.DefaultView.RowFilter = "";
                        lblInternationalLicensesRecords.Text = dtAllInternationalLicenses.Rows.Count.ToString();
                        return;
                    }

                    dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

                    lblInternationalLicensesRecords.Text = dtAllInternationalLicenses.Rows.Count.ToString();
            }
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                cbIsReleased.Visible = false;
                txtFilterValue.Visible = cbFilterBy.Text != "None";

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                {
                    txtFilterValue.Enabled = true;
                }

                txtFilterValue.Text = "";
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsReleased.Text;

            switch(FilterValue)
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
                dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblInternationalLicensesRecords.Text = dtAllInternationalLicenses.Rows.Count.ToString();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            int PersoinID = clsDriver.Find(DriverID).PersonID;

            frmPersonInfo frm = new frmPersonInfo(PersoinID);
            frm.Show();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverInternationalLicenseInfo frm = new frmDriverInternationalLicenseInfo(Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[0].Value));
            frm.Show();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            int PersoinID = clsDriver.Find(DriverID).PersonID;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersoinID);
            frm.Show();
        }
    }
    }
