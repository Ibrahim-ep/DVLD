using DVLD.Applications.Release_Detained_License;
using DVLD.Licenses;
using DVLD.Licenses.Detained_Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtAllDetaindLicenses = new DataTable();
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _dtAllDetaindLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtAllDetaindLicenses;
            lblTotalRecords.Text = _dtAllDetaindLicenses.Rows.Count.ToString();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
               
                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string Filter = string.Empty;

            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    Filter = "DetainID";
                    break;
                case "License ID":
                    Filter = "LicenseID";
                    break;
                case "Is Released":
                    Filter = "IsReleased";
                    break;
                case "National No.":
                    Filter = "NationalNo";
                    break;
                case "Full Name":
                    Filter = "FullName";
                    break;
                case "Release Application ID":
                    Filter = "ReleaseApplicationID";
                    break;
                default:
                    Filter = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == string.Empty || Filter == "None")
            {
               _dtAllDetaindLicenses.DefaultView.RowFilter = string.Empty;
                lblTotalRecords.Text = _dtAllDetaindLicenses.Rows.Count.ToString();
                return;
            }

            if (Filter == "ReleaseApplicationID" || Filter == "DetainID")
                _dtAllDetaindLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", Filter, txtFilterValue.Text.Trim());
            else

                _dtAllDetaindLicenses.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", Filter, txtFilterValue.Text.Trim());

            lblTotalRecords.Text = _dtAllDetaindLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;
                cbIsReleased.Focus();
            }
            else
            {
                txtFilterValue.Visible = cbFilterBy.Text != "None";
                cbIsReleased.Visible = false;

                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;
            string FilterValue = cbIsReleased.Text;

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
            {
                _dtAllDetaindLicenses.DefaultView.RowFilter = string.Empty;
                lblTotalRecords.Text = _dtAllDetaindLicenses.Rows.Count.ToString();
            }
            else
            {
                _dtAllDetaindLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn ,FilterValue);
            }

            lblTotalRecords.Text = _dtAllDetaindLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo((dgvDetainedLicenses.CurrentRow.Cells[6].Value).ToString());
            frm.Show();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmShowLicenseInfo frm = new frmShowLicenseInfo(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            frm.Show();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.Find((dgvDetainedLicenses.CurrentRow.Cells[6].Value).ToString());
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(person.ID);
            frm.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            releaseDetainToolStripMenuItem.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
                
           
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.Show();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.Show();
            frmListDetainedLicenses_Load(null, null);
        }

        private void releaseDetainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[0].Value));
            frm.Show();
            frmListDetainedLicenses_Load(null, null);
        }
    }
}
