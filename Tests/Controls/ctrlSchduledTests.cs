using DVLD.Global;
using DVLD.Properties;
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

namespace DVLD.Tests.Controls
{
    public partial class ctrlSchduledTests : UserControl
    {
        clsTestType.enTestTypes _TestType;
        public clsTestType.enTestTypes TestType
        {
            get
            {
                return _TestType;
            }
            
            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestType.enTestTypes.VisionTest:
                        {
                            groupBox1.Text = "Vision Test";
                            pictureBox1.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestType.enTestTypes.WrittenTest:
                        {
                            groupBox1.Text = "Written Test";
                            pictureBox1.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestTypes.StreetTest:
                        {
                            groupBox1.Text = "Street Test";
                            pictureBox1.Image = Resources.driving_test_512;
                            break;
                        }
                }
                
            }
        }

        private int _TestID = -1;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;

        public ctrlSchduledTests()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {

            _TestAppointmentID = TestAppointmentID;


            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            //incase we did not find any appointment .
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _LocalDrivingLicenseApplication.PersonFullName;


            //this will show the trials for this test before 
            lblTrials.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestType).ToString();



            lblDate.Text = clsFormat.DateFormat(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();



        }
    }
}
