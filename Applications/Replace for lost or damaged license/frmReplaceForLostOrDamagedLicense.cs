using DVLD.Global;
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

namespace DVLD.Applications.Replace_for_lost_or_damaged_license
{
    public partial class frmReplaceForLostOrDamagedLicense : Form
    {
        private int _LicenseID = -1;
        clsApplication.enApplicationType _ApplicationType;

        public frmReplaceForLostOrDamagedLicense()
        {
            InitializeComponent();
        }

        private void _LoadDefualtValues()
        {

            lblApplicationID.Text = "???";
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
          

            lblReplacedLicenseID.Text = "???";
            lblOldLicenseID.Text = "???";

            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

            rbDamaged.Checked = true;

        }

        private void ctrlDriverLicenseInfoWithFillter1_Load(object sender, EventArgs e)
        {

        }

        private void frmReplaceForLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            _LoadDefualtValues();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense = clsLicense.ReplaceLostOrDamagedLicense(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID, clsGlobal.CurrentUser.UserID
                , ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo, txtNotes.Text, _ApplicationType);

            if (NewLicense != null)
            {
                MessageBox.Show("Driving license replaced successfully.");
                lblApplicationID.Text = NewLicense.ApplicationID.ToString();
                lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                _LicenseID = NewLicense.LicenseID;

                ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
                btnIssueReplacement.Enabled = false;
                llShowLicenseInfo.Enabled = true;
                groupBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error replacing driving license.");
                return;
            }
        }

        private void ctrlDriverLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;


            if (_LicenseID != -1)
            {
                clsLicense License = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo;
                int ApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID,
                   _ApplicationType, License.LicenseClassID);

                if (ApplicationID != -1)
                {
                    MessageBox.Show("There is already an active renewal application for the selected license.");
                    btnIssueReplacement.Enabled = false;
                    return;
                }

                if (License.IsActive == false)
                {
                    MessageBox.Show("The selected license is not active.");
                    btnIssueReplacement.Enabled = false;
                    return;
                }

                if (License != null)
                {
                    btnIssueReplacement.Enabled = true;
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

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement For Damaged Driving License";
            _ApplicationType = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            lblApplicationFees.Text = clsApplicationTypes.Find((int)_ApplicationType).Fees.ToString();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            _ApplicationType = clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            lblTitle.Text = "Replacement For Lost Driving License";
            lblApplicationFees.Text = clsApplicationTypes.Find((int)_ApplicationType).Fees.ToString();
        }
    }
}
