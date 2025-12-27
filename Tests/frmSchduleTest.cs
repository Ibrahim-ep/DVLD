using DVLD.Tests.Controls;
using DVLD.Tests.Vision_Test;
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

namespace DVLD.Tests
{
    public partial class frmSchduleTest : Form
    {
       
        private clsTestType.enTestTypes _TestType;
        private int _AppointmentID = -1;
        private int _LocalDrivingLiceseApplicationID;
        public frmSchduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestTypes TestType, int AppointmentID = -1)
        {
            InitializeComponent();

            _LocalDrivingLiceseApplicationID = LocalDrivingLicenseApplicationID;
            _TestType = TestType;
            _AppointmentID = AppointmentID;
        }

        private void frmSchduleTest_Load(object sender, EventArgs e)
        {
           
        }

        private void ctrlSchduleTest1_Load(object sender, EventArgs e)
        {
            ctrlSchduleTest1.TestTypeID = _TestType;
            ctrlSchduleTest1.LoadInfo(_LocalDrivingLiceseApplicationID, _AppointmentID);
        }
    }
}
