using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Tests.Vision_Test;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLiceseApplication
{
    public partial class frmLocalDrivingLiceseApplicationsList : Form
    {
       

        private DataTable _DataTable = new DataTable();
        private clsLicense _License;
        public frmLocalDrivingLiceseApplicationsList()
        {
            InitializeComponent();
        }

        private void _Refrsh()
        {
            _DataTable = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvListLocalDrivingLicensAppliaction.DataSource = _DataTable;
            lblNumberOfRecords.Text = dgvListLocalDrivingLicensAppliaction.RowCount.ToString();
        }

        private void frmLocalDrivingLiceseApplicationsList_Load(object sender, EventArgs e)
        {
            _Refrsh();
        }

        private void btnAddNewDrivingLiceseApplication_Click(object sender, EventArgs e)
        {
            frmAddNewDrivingLicenseApplication frm = new frmAddNewDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void canceApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value);

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmLocalDrivingLiceseApplicationsList_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _DataTable.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvListLocalDrivingLicensAppliaction.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "Status" && FilterColumn != "NationalNo")
                _DataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else
                _DataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(Convert.ToInt32(
                dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value));

            frm.Show();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewDrivingLicenseApplication frm = new
                frmAddNewDrivingLicenseApplication(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value));

            frm.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this application ?") == DialogResult.OK)
            {
                clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                    FindByLocalDrivingLicenseApplicationID(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value));

                if (localDrivingLicenseApplication != null)
                {
                    if (localDrivingLicenseApplication.Delete())
                    {
                        MessageBox.Show("Application deleted successfully");
                    }
                    else
                    {
                        MessageBox.Show("Application Was not deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Local driving license with this id " + dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value + " does not exists");
                }
            }
            else
            {
                return;
            }
        }

        private void schduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointment frm = new frmListTestAppointment(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value),
                clsTestType.enTestTypes.VisionTest);

            frm.Show();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointment frm = new frmListTestAppointment(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value),
                clsTestType.enTestTypes.WrittenTest);

            frm.Show();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointment frm = new frmListTestAppointment(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value),
                clsTestType.enTestTypes.StreetTest);

            frm.Show();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
           
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (clsLocalDrivingLicenseApplication.DoesHaveAnActiveLicense(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)))
            {
                sechdualTestsToolStripMenuItem.Enabled = false;
                sechdualTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = false;
                canceApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;

                showLicensToolStripMenuItem.Enabled = true;

                return;
            }

            if (!clsLocalDrivingLicenseApplication.isApplicationCancelled(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)))
            { 
                sechdualTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = false;
                canceApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;

                return;
            }

            if (clsLocalDrivingLicenseApplication.GetNumberOfPassedTestsStatic(
                Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)) < 1 
                )
            {
                schduleVisionTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;

                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = false;
                canceApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                editApplicationToolStripMenuItem.Enabled = true;

                return;
            }

            if (clsLocalDrivingLicenseApplication.GetNumberOfPassedTestsStatic(
                Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)) == 1)
            {
                schduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                scheduleStreetTestToolStripMenuItem.Enabled = false;

                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = false;
                canceApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;

                return;
            }

            if (clsLocalDrivingLicenseApplication.GetNumberOfPassedTestsStatic(
                Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)) == 2)
            {
                schduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = true;

                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = false;
                canceApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;

                return;
            }

            if (clsLocalDrivingLicenseApplication.GetNumberOfPassedTestsStatic(
                Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)) == 3)
            {
                schduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;

                canceApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;

                issueDrivingLicensFirstTimeToolStripMenuItem.Enabled = true;

                return;
            }
        }

        private void sechdualTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showLicensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value));

            frmShowLicenseInfo frm = new frmShowLicenseInfo(localDrivingLicenseApplication.GetLicenseClassID());

            frm.ShowDialog();
        }

        private void issueDrivingLicensFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueLicenseForTheFirstTime frm = new frmIssueLicenseForTheFirstTime(
                Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value));

            frm.ShowDialog();
        }

        private void showPersonLicensHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(
                clsLocalDrivingLicenseApplication.
                FindByLocalDrivingLicenseApplicationID(
                    Convert.ToInt32(dgvListLocalDrivingLicensAppliaction.CurrentRow.Cells[0].Value)).ApplicantPersonID);

            frm.ShowDialog();
        }
    }
}
