using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users.Controls
{
    public partial class ctrUser_Info : UserControl
    {
        private int _UserID;
        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }
        public clsUser User
        {
            get { return _User; }
        }
        public ctrUser_Info()
        {
            InitializeComponent();
        }

        public void LoadUserData(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(_UserID);

            if (_User != null)
            {
                _FillUserInfo();
            }
            else
            {
                MessageBox.Show("No User Found");
            }
        }

        private void _FillUserInfo()
        {
            ctrPerson_Info1.LoadPersonInfo(_User.Person.ID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.isActive ? "Yes" : "No";
        }

    }
}
