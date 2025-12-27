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
using static DVLD_Business.clsApplication;

namespace DVLD.Licenses.Detained_Licenses
{
    public partial class frmDetainLicense : Form
    {
        private int _DetainedID = -1;
        private int _LicenseID = -1;
       
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            if (_LicenseID != -1)
            {
                lblLicenseID.Text = _LicenseID.ToString();
                llShowLicenseHistory.Enabled = _LicenseID != -1;

                if (ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.IsDetained)
                {
                    MessageBox.Show("The selected license is already detained.");
                    btnDetain.Enabled = false;
                    return;
                }

                btnDetain.Enabled = true;
                txtFineFees.Focus();
            }
            else
            {
                MessageBox.Show("Please select a valid license.");
                return;
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            _DetainedID = ctrlDriverLicenseInfoWithFillter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if (_DetainedID != -1)
            {
                MessageBox.Show("License detained successfully.");
                btnDetain.Enabled = false;
                txtFineFees.Enabled = false;
                ctrlDriverLicenseInfoWithFillter1.FilterEnabled = false;
                
                lblDetainID.Text = _DetainedID.ToString();


            }
            else
            {
                MessageBox.Show("Error detaining license.");
            }
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
            ;


            if (!clsValidation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
            ;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
