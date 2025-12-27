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

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueLicenseForTheFirstTime : Form
    {
        int _LocalDrivingAppID;
       
        clsLocalDrivingLicenseApplication _LocalDrivingApp;
        

        public frmIssueLicenseForTheFirstTime(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();

            _LocalDrivingAppID = LocalDrivingLicenseAppID;
            
        }

        private bool _CheckData()
        {       
            _LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingAppID);

            if (_LocalDrivingApp == null)
            {
                MessageBox.Show("Didn't Find the application");
                this.Close();
                return false;
            }

            if(!_LocalDrivingApp.PassedAllTests())
            {
                MessageBox.Show("Person Should pass all tests first");
                this.Close();
                return false;
            }

            int LicenseID = _LocalDrivingApp.GetActiveLicenseID();

            if(LicenseID != -1)
            {
                MessageBox.Show("Person already has a license");
                this.Close();
                return false;
            }

            return true;
        }

        private void frmIssueLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            
            if (_CheckData())
            {
                ctrlLocalDrivingLicenseAppInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingAppID);
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingApp.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully");

                this.Close();
            }
            else
            {
                MessageBox.Show("License was not issued");
            }
        }
    }
}
