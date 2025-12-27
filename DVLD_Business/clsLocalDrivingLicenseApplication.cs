using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }

        }

        public clsLocalDrivingLicenseApplication()

        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;


            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationDataAccess.AddNewLocalDrivingLicenseApplication
                 (
                 this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _Update()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.UpdateLocalDrivingLicenseApplication
                 (
                 this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicensClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingApplicationInfoByID(LocalDrivingLicenseApplicationID,
                ref ApplicationID, ref LicensClassID);

            if (isFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, 
                        ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID
                        , Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID
                        , LicensClassID);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicensClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingApplicationInfoByApplicationID(ref LocalDrivingLicenseApplicationID,
                ApplicationID, ref LicensClassID);

            if (isFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
                        ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID
                        , Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID
                        , LicensClassID);
            }
            else
            {
                return null;
            }
        }
        public bool DeleteLocalDrivingLicenseApplication()
        {
            bool isLocalDrivingLicenseApplicationDeleted = false;
            bool isBaseApplicationDeleted = false;

            isLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationDataAccess.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingLicenseApplicationDeleted)
                return false;

            isBaseApplicationDeleted = base.Delete();
            return isBaseApplicationDeleted;

        }
        public static bool isLocalDrivingLicensApplicationExists(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.isLocalDrivingLicenseApplicationExists(LocalDrivingLicenseApplicationID);
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetAllLocalDrivingLicenseApplications();
        }
        public bool DoesPassTestType(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesPassPreviousTest(clsTestType.enTestTypes CurrentTestType)
        {
            switch(CurrentTestType)
            {
                case clsTestType.enTestTypes.VisionTest:
                    return true;

                case clsTestType.enTestTypes.WrittenTest:
                    return this.DoesPassTestType(clsTestType.enTestTypes.VisionTest);

                case clsTestType.enTestTypes.StreetTest:
                    return this.DoesPassTestType(clsTestType.enTestTypes.WrittenTest);

                default:
                    return false;
            }
        }
        public static bool DoesPassTestType(int LocalLicensApplicationID, clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DoesPassTestType(LocalLicensApplicationID , (int)TestTypeID);
        }
        public static bool DoesAttendedTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesAttendTestType(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public bool PassedAllTests()
        {
            return GetNumberOfPassedTests() == 3;
        }
        public bool AttendedTest(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public byte TotalTrialsPerTest(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool isThereIsAnActiveSchduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool isThereIsAnActiveSchduleTest(clsTestType.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public int GetNumberOfPassedTests()
        {
            return clsTests.GetPassedTestsCount(this.LocalDrivingLicenseApplicationID);
        }
        public static int GetNumberOfPassedTestsStatic(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.GetPassedTestsCount(LocalDrivingLicenseApplicationID);
        }
        public static bool isApplicationCancelled(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.isApplicationCancelled(LocalDrivingLicenseApplicationID);
        }
        public bool isApplicationCancelled()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.isApplicationCancelled(this.LocalDrivingLicenseApplicationID);
        }
        public static bool DoesHaveAnActiveLicense(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DoesHaveAnActiveLicense(LocalDrivingLicenseApplicationID);
        }
        public bool SetComplete()
        {
            return clsApplicationDataAccess.ChangeApplicationStatus(this.ApplicationID, 3);
        }
        public static int GetLicenseClassID(int LocalDrivingLicenseApplicationID, int LicenseClass)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetLicenseClassID(LocalDrivingLicenseApplicationID, LicenseClass);
        }

        public int GetLicenseClassID()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetLicenseClassID(this.LocalDrivingLicenseApplicationID, this.LicenseClassID);
        }

        public clsTests FindLastTestByPersonIDAndLicenseClassAndTestType(clsTestType.enTestTypes TestType)
        {
            return clsTests.FindByLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestType);
        }

        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;

                if (Driver.Save())
                    DriverID = Driver.DriverID;
                else
                    return -1;
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();

            License.ApplicationID = this.ApplicationID;
            License.CreatedByUserID = CreatedByUserID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = License.IssueDate.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.LicenseClassID = this.LicenseClassID;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.Notes = Notes.Trim();
            License.DriverID = DriverID;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;

            if (License.Save())
            {
                //Update the application status to completed.

                this.SetComplete();

                return License.LicenseID;
            }
            else
            {
                return -1;
            }
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        
    }
}
