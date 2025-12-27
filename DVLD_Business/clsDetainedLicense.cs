using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDetainedLicense
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public clsLicense LicenseInfo { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public clsDetainedLicense()
        {
            this.DetainDate = DateTime.Now;
            this.ReleaseDate = DateTime.MinValue;
            this.DetainID = -1;
            this.LicenseID = -1;
            this.FineFees = 0;
            this.IsReleased = false;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID ,bool IsReleased, DateTime ReleaseDate,
           int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainDate = DetainDate;
            this.ReleaseDate = ReleaseDate;
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.FineFees = FineFees;
            this.IsReleased = IsReleased;
            this.CreatedByUserID = CreatedByUserID;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            _Mode = enMode.Update;
        }

        private bool _AddNewDetain()
        {
            this.DetainID = clsDetainedLicenseDataAccess.AddNewDetain(this.LicenseID, this.DetainDate, this.FineFees,
               this.CreatedByUserID , this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);

            return this.DetainID != -1;
        }

        private bool _UpadateDetain()
        {
            return clsDetainedLicenseDataAccess.UpdateDetain(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased
                , this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDetain())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                    case enMode.Update:
                    {
                        return _UpadateDetain();
                    }
            }
            return false;
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1 , CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicenseDataAccess.FindDetainByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID ,ref IsReleased, ref
                ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicenseDataAccess.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref
                ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseDataAccess.GetAllDetainedLicenses();
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseDataAccess.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseDataAccess.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
