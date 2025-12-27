using DVLD.Global;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
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

namespace DVLD.Applications.International_License_Application
{
    public partial class frmIusseInternationalLicnese : Form
    {
        private int _LicenseID;
       
        clsInternationalLicenseApplication _InternationalLicense;

        public frmIusseInternationalLicnese()
        {
            InitializeComponent();
        }

        private void _LoadDefualtValues()
        {
          

            _InternationalLicense = new clsInternationalLicenseApplication();

           
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = 0.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToShortDateString();
            lblLocalLicenseID.Text = _LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            llShowLicenseHistory.Enabled = true;
        }

        private void frmIusseInternationalLicnese_Load(object sender, EventArgs e)
        {
            _LoadDefualtValues();
        }

        private void ctrlDriverLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            if (_LicenseID != -1)
            {
                btnIssueLicense.Enabled = true;
            }
            else
            {
                btnIssueLicense.Enabled = false;
            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID, clsApplication.enApplicationType.NewInternationalLicense,
                ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("There is already an active International License application for this person and license class.");
                return;
            }


            if (ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a local license first.");
                return;
            }

            if (!ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.IsActive)
            {
                               MessageBox.Show("The selected local license is not active.");
                return;
            }

            if (ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("The selected local license is expired.");
                return;
            }

            if (_LicenseID == -1)
            {
                MessageBox.Show("Please select a local license first.");
                return;
            }

            _InternationalLicense.IssuedUsingLocalLicenseID = _LicenseID;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(10);
            _InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _InternationalLicense.IsActive = true;
            _InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverID;

            _InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID;
            _InternationalLicense.ApplicationDate = DateTime.Now;
            _InternationalLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            _InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _InternationalLicense.LastStatusDate = DateTime.Now;
            _InternationalLicense.PaidFees = Convert.ToSingle(lblFees.Text);
           

            if (_InternationalLicense.Save())
            {
                MessageBox.Show("International License issued successfully.");
                
                lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
                lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
                llShowLicenseInfo.Enabled = true;
                btnIssueLicense.Enabled = false;
                ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Error issuing International License.");
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverInternationalLicenseInfo frm = new frmDriverInternationalLicenseInfo(_InternationalLicense.InternationalLicenseID);
            frm.Show();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(_InternationalLicense.DriverID);
            frm.Show();
        }
    }
}
