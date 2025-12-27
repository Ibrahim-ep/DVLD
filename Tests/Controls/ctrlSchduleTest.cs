using DVLD.Global;
using DVLD.Properties;
using DVLD.Tests.Vision_Test;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Controls
{
    public partial class ctrlSchduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1}
        private enMode _Mode;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }
        private enCreationMode _CreationMode;

        private clsTestType.enTestTypes _TestTypeID;
        public clsTestType.enTestTypes TestTypeID
        {
            get { return _TestTypeID; }

            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestTypes.VisionTest:

                        pbTestType.BackgroundImage = Resources.Vision_512;
                        gbTestType.Text = "Vision Test";
                        break;

                    case clsTestType.enTestTypes.WrittenTest:

                        pbTestType.BackgroundImage = Resources.Written_Test_512;
                        gbTestType.Text = "Written Test";
                        break;

                    case clsTestType.enTestTypes.StreetTest:

                        pbTestType.BackgroundImage = Resources.driving_test_512;
                        gbTestType.Text = "Street Test";
                        break;
                }
            }
        }

        private int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }

        private int _TestAppointmentID;
        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        private clsTestAppointment _TestAppointment;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public ctrlSchduleTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //decide if the createion mode is retake test or not based if the person attended this test before
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))

                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeTestFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gbRetakeTestInfo.Enabled = true;
                
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
              
                lblRetakeTestFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            //this will show the trials for this test before  
            lblTrials.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpSchduleTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;
            }


            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstrain())
                return;

            if (!_HandleAppointmentLockedConstrain())
                return;

            if (!_HandlePreviousTestConstrain())
                return;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpSchduleTestDate.MinDate = DateTime.Now;
            else
                dtpSchduleTestDate.MinDate = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeTestFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeTestFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }

            return true;
        }

        private bool _HandleActiveTestAppointmentConstrain()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.isThereIsAnActiveSchduleTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                lblTestLockedMessage.Enabled = true;
                lblTestLockedMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpSchduleTestDate.Enabled = false;
                return false;
            }

            return true;
        }

        private bool _HandleAppointmentLockedConstrain()
        {
            if (_TestAppointment.IsLocked)
            {
                lblTestLockedMessage.Enabled = true;
                lblTestLockedMessage.Text = "Person already sat for the test, appointment loacked.";
                btnSave.Enabled = false;
                dtpSchduleTestDate.Enabled = false;
                return false;
            }

            return true;
        }

        private bool _HandlePreviousTestConstrain()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestTypes.VisionTest:
                    {
                        lblTestLockedMessage.Enabled = false;
                        return true;
                    }
                case clsTestType.enTestTypes.WrittenTest:
                    {
                        if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestTypes.VisionTest))
                        {
                            lblTestLockedMessage.Enabled = true;
                            lblTestLockedMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                            btnSave.Enabled = false;
                            dtpSchduleTestDate.Enabled = false;

                            return false;
                        }
                        else
                        {
                            lblTestLockedMessage.Enabled = false;
                            btnSave.Enabled = true;
                            dtpSchduleTestDate.Enabled = true;

                        }
                        return true;
                    }
                case clsTestType.enTestTypes.StreetTest:
                    {
                        if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestTypes.WrittenTest))
                        {
                            lblTestLockedMessage.Enabled = true;
                            lblTestLockedMessage.Text = "Cannot Sechule, Written Test should be passed first";
                            btnSave.Enabled = false;
                            dtpSchduleTestDate.Enabled= false;

                            return false;
                        }
                        else
                        {
                            lblTestLockedMessage.Enabled = false;
                            btnSave.Enabled = true;
                            dtpSchduleTestDate.Enabled = true;
                        }
                        return true;
                    }
            }
            return true;

        }

        private bool _HandleRetakeApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpSchduleTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
