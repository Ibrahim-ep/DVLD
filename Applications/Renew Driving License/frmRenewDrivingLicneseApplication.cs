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

namespace DVLD.Applications.Renew_Driving_License
{
    public partial class frmRenewDrivingLicneseApplication : Form
    {
        private int _LicenseID = -1;
        public frmRenewDrivingLicneseApplication()
        {
            InitializeComponent();
        }

        private void _LoadDefualtValues()
        {

            lblApplicationID.Text = "???";
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            
            lblRenewedLicenseID.Text = "???";
            lblOldLicenseID.Text = "???";
           
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
           
        }

        private void ctrlDriverLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            if (_LicenseID != -1)
            {
                clsLicense License = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo;
                int ApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID,
                    clsApplication.enApplicationType.RenewDrivingLicense, License.LicenseClassID);

                if (ApplicationID != -1)
                {
                    MessageBox.Show("There is already an active renewal application for the selected license.");
                    btnRenewLicense.Enabled = false;
                    return;
                }


                if (!License.IsLicenseExpired())
                {
                    MessageBox.Show("The selected license is not expired yet.");
                    btnRenewLicense.Enabled = false;
                    return;
                }

                if (License.IsActive == false)
                {
                    MessageBox.Show("The selected license is not active.");
                    btnRenewLicense.Enabled = false;
                    return;
                }

                if (License != null)
                {
                    btnRenewLicense.Enabled = true;
                    lblOldLicenseID.Text = License.LicenseID.ToString();
                    llShowLicenseHistory.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error loading license information.");
                    return;
                }

                byte DefualtValidatyLength = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;

                lblExpirationDate.Text = DateTime.Now.AddYears(DefualtValidatyLength).ToShortDateString();
                lblLicenseFees.Text = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
                txtNotes.Text = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.Notes;
            }
            else
            {
                MessageBox.Show("Please select a valid license.");
                return;
            }
        }

        private void frmRenewDrivingLicneseApplication_Load(object sender, EventArgs e)
        {
            _LoadDefualtValues();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense = clsLicense.RenewLiense(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID, clsGlobal.CurrentUser.UserID
                , ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo, txtNotes.Text);

            if (NewLicense != null)
            {
                MessageBox.Show("Driving license renewed successfully.");
                lblApplicationID.Text = NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                _LicenseID = NewLicense.LicenseID;

                ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
                btnRenewLicense.Enabled = false;
                llShowLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error renewing driving license.");
                return;
            }

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);
            frm.Show();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.Show();
        }
    }
}
