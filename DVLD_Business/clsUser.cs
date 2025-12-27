using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        
        public clsPerson Person { get; set; }

        public clsUser()
        {
            Person = new clsPerson();

            UserID = -1;
            PersonID = -1;
            UserName = string.Empty;
            Password = string.Empty;
            isActive = true;
            _Mode = enMode.AddNew;
        }

        private clsUser(int Userid, int Personid,
            string userName, string password, bool IsActive)
        {
            UserID = Userid;
            PersonID = Personid;
            Person = clsPerson.Find(Personid);
            UserName = userName;
            Password = password;
            isActive = IsActive;

            _Mode = enMode.Update;
        }

        private bool AddNew()
        {
            this.UserID = clsUserDataAccess.AddNewUser(this.Person.ID, this.UserName, this.Password, this.isActive);

            return this.UserID != -1;
        }

        private bool Update()
        {
            return clsUserDataAccess.UpdateUser(this.UserID, this.Person.NationalNO, this.Person.FirstName, this.Person.SecondName, this.Person.ThirdName, this.Person.LastName,
                this.Person.Phone, this.Person.Email, this.Person.DateOfBirth, this.Person.Gender, this.Person.Address, this.Person.NationalityCounrtyID, this.Person.ImagePath,
                this.UserName, this.Password, this.isActive);
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

        public static clsUser Find(int UserID)
        {
            
            string UserName =string.Empty, Password = string.Empty;
            int PersonID = 0;
            bool IsActive = false;

            if (clsUserDataAccess.GetUserInfoByID(UserID,ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser Find(string UserName, string Password)
        {
            int PersonID = 0, UserID = 0;
            bool IsActive = false;

            if (clsUserDataAccess.GetUserInfoByUsernameAndPassword(UserName,Password, ref UserID, ref PersonID ,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

        public static bool isUserExists(string UserName)
        {
            return clsUserDataAccess.isUserExistsByUserName(UserName);
        }

        public static bool isUserExists(int ID)
        {
            return clsUserDataAccess.isUserExistsByUserID(ID);
        }

        public static bool DeleteUser(int ID)
        {
            return clsUserDataAccess.DeleteUser(ID);
        }

        public static bool isUserExistsForPeraon(int PersonID)
        {
            return clsUserDataAccess.IsUserExistForPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserDataAccess.ChangePassword(UserID,NewPassword);
        }
    }
}
