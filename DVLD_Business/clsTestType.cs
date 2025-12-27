using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        public enum enTestTypes { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        public clsTestType.enTestTypes TestTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Fees { get; set; }

        public clsTestType()

        {
            this.TestTypeID = clsTestType.enTestTypes.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            _Mode = enMode.AddNew;

        }

        private clsTestType(clsTestType.enTestTypes testTypeID, string title, string description ,int fees)
        {
            TestTypeID = testTypeID;
            Title = title;
            Description = description;
            Fees = fees;

            _Mode = enMode.Update;
        }
        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.TestTypeID = (clsTestType.enTestTypes)clsTestTypesDataAccess.AddNewTestType(this.Title, this.Description, this.Fees);

            return (this.Title != string.Empty);
        }
        private bool _Update()
        {
            return clsTestTypesDataAccess.UpdateTestTypes((int)this.TestTypeID, this.Title, this.Description , this.Fees);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        _Mode = enMode.Update;
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

        public static clsTestType Find(clsTestType.enTestTypes TestTypeID)
        {
            string Title = string.Empty, Description = string.Empty;
            int Fees = 0;

            if (clsTestTypesDataAccess.GetTestTypeInfoByID((int)TestTypeID, ref Title, ref Description , ref Fees))
            {
                return new clsTestType(TestTypeID, Title, Description , Fees);
            }
            else
                return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }
    }
}
