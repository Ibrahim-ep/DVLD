using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clsApplication;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsApplicationTypes ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { set; get; }
        public string ApplicationStatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo;

        public clsApplication()

        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsApplication(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)

        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationDataAccess.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            //call DataAccess Layer 

            return clsApplicationDataAccess.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public static clsApplication Find(int ApplicationID)
        {
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            int PersonID = -1, UserID = -1, ApplicationTypeID = -1;
            float PaidFees = 0;
            short status = 0;

            if (clsApplicationDataAccess.GeApplicationByID(ApplicationID, ref PersonID, ref ApplicationDate, ref ApplicationTypeID, ref
                status, ref LastStatusDate, ref PaidFees, ref UserID))
            {
                return new clsApplication(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)status, LastStatusDate,
                    PaidFees, UserID);
            }
            else
            {
                return null;
            }
        }

        public bool Delete()
        {
            return clsApplicationDataAccess.DeleteApplication(this.ApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        _AddNewApplication();
                        Mode = enMode.Update;
                        return true;
                    }

                case enMode.Update:
                    {
                        return _UpdateApplication();
                    }
            }
            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationDataAccess.GetAllApplications();
        }

        public static bool ChangeStatus(int AppID, short NewStatus)
        {
            return clsApplicationDataAccess.ChangeApplicationStatus(AppID, NewStatus);
        }

        public bool Cancel()
        {
            return clsApplicationDataAccess.ChangeApplicationStatus(this.ApplicationID, 2);
        }

        public bool Compete()
        {
            return clsApplicationDataAccess.ChangeApplicationStatus(this.ApplicationID, 3);
        }

        public static bool isApplicationExists(int AppID)
        {
            return clsApplicationDataAccess.isApplicationExists(AppID);
        }

        public static bool DoesPersonHaveAnActiveApplication(int ApplicationTypeID, int PersonID)
        {
            return clsApplicationDataAccess.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DeosPersonHaveAnActiveApplication(int applicationTypeID)
        {
            return clsApplicationDataAccess.DoesPersonHaveActiveApplication(this.ApplicantPersonID, applicationTypeID);
        }

        public static int GetActiveApplicationID(int ApplicationID, clsApplication.enApplicationType ApplicationType)
        {
            return clsApplicationDataAccess.GetActiveApplicationID(ApplicationID, (int)ApplicationType);
        }

        public int GetApplicationID(clsApplication.enApplicationType ApplcationType)
        {
            return clsApplicationDataAccess.GetActiveApplicationID(this.ApplicantPersonID, (int)ApplcationType);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationType, int LicensClassID)
        {
            return clsApplicationDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationType, LicensClassID);
        }
    }
}
