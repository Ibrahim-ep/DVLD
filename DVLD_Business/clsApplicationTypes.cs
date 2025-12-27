using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string Title { get; set; }
        public int Fees { get; set; }

        public clsApplicationTypes()

        {
            this.ApplicationTypeID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        private clsApplicationTypes(int applicationTypeID ,string title, int fees)
        {
            ApplicationTypeID = applicationTypeID;
            Title = title;
            Fees = fees;

            Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplicationTypeDataAccess.AddNewApplicationType(this.Title, this.Fees);


            return (this.ApplicationTypeID != -1);
        }

        private bool _Update()
        {
            return clsApplicationTypeDataAccess.UpdateApplicationTypes(this.ApplicationTypeID, this.Title, this.Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
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

        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string Title = string.Empty;
            int Fees = 0;

            if (clsApplicationTypeDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref Title, ref Fees))
            {
                return new clsApplicationTypes(ApplicationTypeID, Title, Fees);
            }
            else 
                return null;
        }

        public static DataTable GetAllApplicaionTypes()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes();
        }
    }
}
