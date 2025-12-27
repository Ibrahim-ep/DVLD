using DVLD.Applications.Detained_License;
using DVLD.Applications.International_License_Application;
using DVLD.Applications.LocalDrivingLiceseApplication;
using DVLD.Applications.Release_Detained_License;
using DVLD.Applications.Renew_Driving_License;
using DVLD.Applications.Replace_for_lost_or_damaged_license;
using DVLD.ApplicationTypes;
using DVLD.Drivers;
using DVLD.Global;
using DVLD.Licenses.Detained_Licenses;
using DVLD.People;
using DVLD.Tests;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    
    public partial class Main : Form
    {
        frmLogin _LoginForm;

        public Main(frmLogin Frm)
        {
            InitializeComponent();
            _LoginForm = Frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmManagePeople people = new frmManagePeople();
            people.MdiParent = this;
            people.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFindPerson findPerson = new frmFindPerson();
            findPerson.MdiParent = this;
            findPerson.Show();
        }

        private void poepleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople managePeople = new frmManagePeople();
            managePeople.MdiParent = this;
            managePeople.Show();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void drivingLicensesSevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.MdiParent = this;
            frm.BringToFront();
            frm.Show();
           
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            this.Close();
        }

        private void ToolMenuStripManageApplictionTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ToolMenuStripManageTestTypes_Click(object sender, EventArgs e)
        {
            //frmTestTypesList frm = new frmTestTypesList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLiceseApplicationsList frm = new frmLocalDrivingLiceseApplicationsList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewDrivingLicenseApplication frm = new frmAddNewDrivingLicenseApplication();
            frm.MdiParent = this;
            frm.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LoginForm.Close();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.MdiParent = this;
            frm.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIusseInternationalLicnese frm = new frmIusseInternationalLicnese();
            frm.MdiParent = this;
            frm.Show();
        }

        private void internationalLiceseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.MdiParent = this;
            frm.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewDrivingLicneseApplication frm = new frmRenewDrivingLicneseApplication();
            frm.MdiParent = this;
            frm.Show();
        }

        private void replacementForDamagedOrLostLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceForLostOrDamagedLicense frm = new frmReplaceForLostOrDamagedLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLiceseApplicationsList frm = new frmLocalDrivingLiceseApplicationsList();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
