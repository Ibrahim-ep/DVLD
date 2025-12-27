using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDriver
    {
        enum enMode { AddNew = 1, Update = 2}
        enMode _Mode;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
           
            DriverID = driverID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;

            _Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID);

            return this.DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            return clsDriversDataAccess.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDriver())
                        {
                            _Mode = enMode.Update;
                            return true;
                            
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return _UpdateDriver();
                    }
            }

            return false;
        }

        public static clsDriver Find(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverInfoByPersonID(ref DriverID, PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDrivers();
        }

        public static DataTable GetDriverLicense(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }

        //public static DataTable GetInternationalLicenses(int DriverID)
        //{
        //    return clsLicense.GetInternationalLicenses(DriverID);
        //}
    }
}
