using DVLD.Global;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _LiceseID;
        
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();

            _LiceseID = LicenseID;

            ctrlDriverLicenseInfoWithFillter1.LoadLicenseInfo(_LiceseID);
            ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
        }

        private void ctrlDriverLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _LiceseID = obj;

            lblLicenseID.Text = _LiceseID.ToString();
            llShowLicenseHistory.Enabled = _LiceseID != -1;

            if (_LiceseID == -1)
                return;
            if (!ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("The selected license is not detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnReleaseLicense.Enabled = true;
            lblFineFees.Text = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.PaidFees.ToString();
            lblDetainID.Text = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DetainInfo.FineFees.ToString();
            lblDetainDate.Text = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DetainInfo.DetainDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            llShowLicenseHistory.Enabled = true;
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("An error occurred while releasing the license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License Released Successfully");

            btnReleaseLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.Show();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseID);
            frm.Show();
        }
    }
}
