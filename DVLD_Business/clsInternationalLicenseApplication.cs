using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsInternationalLicenseApplication : clsApplication
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalLicenseApplication() : base()
        {
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;

            _Mode = enMode.AddNew;
        }

        private clsInternationalLicenseApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID,  int DriverID,
             int IssuedUsingLocalLicenseID, DateTime IssueDate,
             DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.Find(this.DriverID);

            _Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseApplicationDataAccess.AddNewInternationalLicenseApplication(this.ApplicationID, this.DriverID
                , this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return this.InternationalLicenseID != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseApplicationDataAccess.UpdateInternationalLicenseApplication(this.InternationalLicenseID, this.ApplicationID, this.DriverID
                ,this.IssuedUsingLocalLicenseID, this.IssueDate,this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)_Mode;

            if (!base.Save())
                return false;

            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewInternationalLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                    case enMode.Update:
                    {
                        return _UpdateInternationalLicense();
                    }
            }

            return false;
        }

        public static clsInternationalLicenseApplication Find(int InternetationalLicenseID)
        {
            int DriverID = -1, IssuedUsingLocalLicenseID = -1, ApplicationID = -1, CreatedByUserID = -1;

            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue;
            
            bool IsActive = false;

            bool IsFound = clsInternationalLicenseApplicationDataAccess.GetInternationalLicenseApplicationInfoByID(InternetationalLicenseID,
                ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsInternationalLicenseApplication(ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees,
                    Application.CreatedByUserID, InternetationalLicenseID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;
        }

        public bool DeleteInternationalLicense()
        {
            bool IsInternationalLicenseApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsInternationalLicenseApplicationDeleted = 
                clsInternationalLicenseApplicationDataAccess.DeleteInternationalLicenseApplication(this.InternationalLicenseID);

            if (!IsInternationalLicenseApplicationDeleted)
                return false;

            IsBaseApplicationDeleted = base.Delete();

            return IsBaseApplicationDeleted;
        }

        public static bool IsInternetationalLicenseApplicationExists(int InternationalLicenseID)
        {
            return clsInternationalLicenseApplicationDataAccess.IsInternationalLicenseExists(InternationalLicenseID);
        }

        public static DataTable GetAllInternationalLicneses()
        {
            return clsInternationalLicenseApplicationDataAccess.GetAllInternationalLicenses();
        }

        public static DataTable GetAllDriverInternationalLicneses(int DriverID)
        {
            return clsInternationalLicenseApplicationDataAccess.GetAllDriverInternationalLicneses(DriverID);
        }

        public static int GetActiveInternationaLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseApplicationDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
    }
}
