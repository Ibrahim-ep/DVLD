using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicense
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        public enum enIssueReason 
        {
            FirstTime = 1,
            Renew = 2,
            DamagedReplacement = 3,
            LostReplacement = 4
        }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public enIssueReason IssueReason {  get; set; }
        public string IssueReasonText
        {
            get
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";
                    case enIssueReason.Renew:
                        return "Renew";
                    case enIssueReason.DamagedReplacement:
                        return "Damaged Replacement";
                    case enIssueReason.LostReplacement:
                        return "Lost Replacement";
                    default:
                        return "Unknown";
                }
            }
        }
        public bool IsDetained
        {
            get
            {
                return IsLicenseDetained();
            }
        }
        public clsDetainedLicense DetainInfo { get; set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = string.Empty;
            PaidFees = 0.0f;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID,
            int licenseClassID, DateTime issueDate, DateTime expirationDate,
            string notes, float paidFees, bool isActive, enIssueReason issueReason,
            int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            DriverInfo = clsDriver.Find(DriverID);
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            this.DetainInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);
            _Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, Convert.ToByte(this.IssueReason), this.CreatedByUserID);

            return this.LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes,
                this.PaidFees, this.IsActive, Convert.ToByte(this.IssueReason), this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return _UpdateLicense();
                    }
            }
            return false;
        }

        public static clsLicense Find(int LicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            float paidFees = 0.0f;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserID = -1;
            bool isFound = clsLicensesDataAccess.GetLicenseInfoByLicenseID(LicenseID, ref applicationID,
                ref driverID, ref licenseClass, ref issueDate, ref expirationDate,
                ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);
            if (isFound)
            {
                return new clsLicense(LicenseID, applicationID, driverID,
                    licenseClass, issueDate, expirationDate,
                    notes, paidFees, isActive, (enIssueReason)issueReason,
                    createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicensesDataAccess.GetAllLicenses();
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicensesDataAccess.GetDriverLicenses(DriverID);
        }

        public static bool IsLicenseExistsByPersonID(int PersonID, int LicenseClass)
        {
            return GetActiveLicenseIDByPersonID(PersonID, LicenseClass) != -1;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClass)
        {
            return clsLicensesDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClass);
        }

        public Boolean IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicensesDataAccess.DeactivateLicense(this.LicenseID);
        }

        public static string GetIssueResonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "Unknown";
            }
        }

        public static clsLicense RenewLiense(int PersonID, int CurrentUserID, clsLicense OldLicense, string Notes)
        {
            clsApplication renewalApplication = new clsApplication();
            clsLicense RenewedLicense = new clsLicense();

            renewalApplication.ApplicantPersonID = PersonID;
            renewalApplication.ApplicationDate = DateTime.Now;
            renewalApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            renewalApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            renewalApplication.LastStatusDate = DateTime.Now;
            renewalApplication.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            renewalApplication.CreatedByUserID = CurrentUserID;

            if (renewalApplication.Save())
            {
                RenewedLicense.ApplicationID = renewalApplication.ApplicationID;
                RenewedLicense.DriverID = OldLicense.DriverID;
                RenewedLicense.LicenseClassID = OldLicense.LicenseClassID;
                RenewedLicense.IssueDate = DateTime.Now;
                RenewedLicense.ExpirationDate = DateTime.Now.AddYears(10);
                RenewedLicense.PaidFees = OldLicense.PaidFees;
                RenewedLicense.IsActive = true;
                RenewedLicense.IssueReason = clsLicense.enIssueReason.Renew;
                RenewedLicense.CreatedByUserID = CurrentUserID;

                RenewedLicense.Notes = Notes;



                if (RenewedLicense.Save())
                {
                    OldLicense.DeactivateCurrentLicense();
                    return RenewedLicense;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        public static clsLicense ReplaceLostOrDamagedLicense(int PersonID, int CurrentUserID, clsLicense OldLicense, string Notes, clsApplication.enApplicationType applicationType)
        {
            clsApplication ReplacementApplication = new clsApplication();
            clsLicense ReplacementLicense = new clsLicense();
            clsLicense.enIssueReason IssueReason;

            ReplacementApplication.ApplicantPersonID = PersonID;
            ReplacementApplication.ApplicationDate = DateTime.Now;
            ReplacementApplication.ApplicationTypeID = (int)applicationType;
            ReplacementApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            ReplacementApplication.LastStatusDate = DateTime.Now;
            ReplacementApplication.PaidFees = clsApplicationTypes.Find((int)applicationType).Fees;
            ReplacementApplication.CreatedByUserID = CurrentUserID;

            if (ReplacementApplication.Save())
            {
                ReplacementLicense.ApplicationID = ReplacementApplication.ApplicationID;
                ReplacementLicense.DriverID = OldLicense.DriverID;
                ReplacementLicense.LicenseClassID = OldLicense.LicenseClassID;
                ReplacementLicense.IssueDate = DateTime.Now;
                ReplacementLicense.ExpirationDate = DateTime.Now.AddYears(10);
                ReplacementLicense.PaidFees = OldLicense.PaidFees;
                ReplacementLicense.IsActive = true;

                if (applicationType == clsApplication.enApplicationType.ReplaceDamagedDrivingLicense)
                    IssueReason = clsLicense.enIssueReason.DamagedReplacement;
                else
                    IssueReason = clsLicense.enIssueReason.LostReplacement;

                ReplacementLicense.IssueReason = IssueReason;
                ReplacementLicense.CreatedByUserID = CurrentUserID;

                ReplacementLicense.Notes = Notes;



                if (ReplacementLicense.Save())
                {
                    OldLicense.DeactivateCurrentLicense();
                    return ReplacementLicense;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense Detain = new clsDetainedLicense();

            Detain.LicenseID = this.LicenseID;
            Detain.DetainDate = DateTime.Now;
            Detain.FineFees = FineFees;
            Detain.IsReleased = false;
            Detain.CreatedByUserID = CreatedByUserID;

            if (!Detain.Save())
                return -1;

            return Detain.DetainID;
        }

        public bool IsLicenseDetained()
        {
            return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleaseByUserID, ref int ApplicationID)
        {
            clsApplication ReleaseApplication = new clsApplication();
           

            ReleaseApplication.ApplicantPersonID = this.DriverInfo.PersonID;
            ReleaseApplication.ApplicationDate = DateTime.Now;
            ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;
            ReleaseApplication.CreatedByUserID = CreatedByUserID;

            if (ReleaseApplication.Save())
            {
                ApplicationID = ReleaseApplication.ApplicationID;

                return this.DetainInfo.ReleaseDetainedLicense(ReleaseByUserID, ApplicationID);
            }
            else
            {
                ApplicationID = -1;
                return false;
            }

           


        }
    }
}
