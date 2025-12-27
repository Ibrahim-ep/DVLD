using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLiceseApplication
{
    
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _LocalDrivingLicenseID;
        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseApplicationID;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseAppInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseID);
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load_1(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseAppInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseID);
        }
    }
}
