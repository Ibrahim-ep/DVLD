using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public int ID { get; set; }
        public string NationalNO { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public int NationalityCounrtyID { get; set; }
        public string ImagePath { get; set; }

        public clsCountry CountryInfo;
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}", FirstName, SecondName, ThirdName, LastName);
            }
        }

        public clsPerson()
        {
            ID = 0;
            NationalNO = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gender = 0;
            Address = string.Empty;
            NationalityCounrtyID = 0;
            ImagePath = string.Empty;

            _Mode = enMode.AddNew;
        }

        private clsPerson(int id, string nationalNO, string firstName, string secondName, string thirdName, string lastName,
            string phone, string email, DateTime dateOfBirth, byte gender, string address, int nationalityCounrtyID, string imagePath)
        {
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            ID = id;
            NationalNO = nationalNO;
            Phone = phone;
            Email = email;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            NationalityCounrtyID = nationalityCounrtyID;
            ImagePath = imagePath;
            CountryInfo = clsCountry.Find(nationalityCounrtyID);

            _Mode = enMode.Update;
        }

        private bool AddNew()
        {
            this.ID = PersonDataAccess.AddNewPerson(this.NationalNO, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.Phone, this.Email, this.DateOfBirth, this.Gender, this.Address, this.NationalityCounrtyID, this.ImagePath);

            return this.ID != -1;
        }

        private bool Update()
        {
            return PersonDataAccess.UpdatePerson(this.ID, this.NationalNO, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.Phone, this.Email, this.DateOfBirth, this.Gender, this.Address, this.NationalityCounrtyID, this.ImagePath);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.Update;
                        AddNew();
                        return true;
                    }
                case enMode.Update:
                    {
                        return Update();
                    }
            }

            return false;
        }

        public static clsPerson Find(int ID)
        {
            string FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty, LastName = string.Empty,
                NationalNO = string.Empty, Phone = string.Empty, Email = string.Empty, Address = string.Empty, ImagePath = string.Empty;
            DateTime DateOfBirth = DateTime.MinValue;
            byte Gender = 0;
            int NationalityCounrtyID = 0;

            if (PersonDataAccess.GetPersonInfoByID(ID, ref NationalNO, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Phone, ref Email,
                        ref DateOfBirth, ref Gender, ref Address, ref NationalityCounrtyID, ref ImagePath))
            {
                return new clsPerson(ID, NationalNO, FirstName, SecondName, ThirdName, LastName,
               Phone, Email, DateOfBirth, Gender, Address, NationalityCounrtyID, ImagePath);
            }
            else   
            {
                return null;
            }
        }

        public static clsPerson Find(string NationalNO)
        {
            string FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty, LastName = string.Empty,
                Phone = string.Empty, Email = string.Empty, Address = string.Empty, ImagePath = string.Empty;
            DateTime DateOfBirth = DateTime.MinValue;
            byte Gender = 0;
            int NationalityCounrtyID = 0, ID = -1;

            if (PersonDataAccess.GetPersonInfoByNationalNO(ref ID, NationalNO, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Phone, ref Email,
                        ref DateOfBirth, ref Gender, ref Address, ref NationalityCounrtyID, ref ImagePath))
            {
                return new clsPerson(ID, NationalNO, FirstName, SecondName, ThirdName, LastName,
               Phone, Email, DateOfBirth, Gender, Address, NationalityCounrtyID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllPeople()
        {
            return PersonDataAccess.GetAllPeople();
        }

        public static bool isPersonExists(string NationalNO)
        {
            return PersonDataAccess.isPersonExistsByNationalNO(NationalNO);
        }

        public static bool isPersonExists(int ID)
        {
            return PersonDataAccess.isPersonExistsByPersonID(ID);
        }

        public static bool DeletePerson(int ID)
        {
            return PersonDataAccess.DeletePerson(ID);
        }                                               
    }
}