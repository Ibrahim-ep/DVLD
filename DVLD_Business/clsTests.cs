using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTests
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointment { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
       

        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsTests(int testID, int testAppointmentID, bool testResult,
            string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestAppointment = clsTestAppointment.Find(testAppointmentID);
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;

            _Mode = enMode.Update;
        }
        private bool _AddNew()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return this.TestID != -1;
        }

        private bool _Update()
        {
            return clsTestsDataAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
               if ( _AddNew())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _Update();
            }
        }

        public static bool Delete(int TestID)
        {
            return clsTestsDataAccess.DeleteTest(TestID);
        }

        public static clsTests Find(int TestID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = string.Empty;
            int createdByUserID = -1;
            bool isFound = clsTestsDataAccess.GetTestInfoByID(TestID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID);
            if (isFound)
            {
                return new clsTests(TestID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsTests FindByLastTestPerPersonAndLicenseClass (int PersonID, int LicenseClassID, clsTestType.enTestTypes TestType)
        {
            int testID = -1, TestAppointmentID = -1;
            bool testResult = false;
            string notes = string.Empty;
            int createdByUserID = -1;

            bool isFound = clsTestsDataAccess.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, Convert.ToInt32(TestType), ref testID, ref TestAppointmentID , ref testResult, ref notes, ref createdByUserID);

            if (isFound)
            {
                return new clsTests(testID, TestAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool IsTestExist(int TestID)
        {
            return clsTestsDataAccess.IsTestExist(TestID);
        }

        public static DataTable GetAllTests()
        {
            return clsTestsDataAccess.GetAllTests();
        }

        public static byte GetPassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetNumberOfPassedTests(LocalDrivingLicenseApplicationID);
        }

        public static bool DoesPersonPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetNumberOfPassedTests(LocalDrivingLicenseApplicationID) == 3;
        }
    }

}
