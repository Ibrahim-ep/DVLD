using DVLD.Global;
using DVLD.People;
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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;

        private int _ApplicationID = -1;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();

        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.Find(ApplicationID);
            if (_Application == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillApplicationInfo();
        }

        private void _FillApplicationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            lblBaseAppID.Text = _Application.ApplicationID.ToString();
            lblAppStatus.Text = _Application.ApplicationStatusText;
            lblAppType.Text = _Application.ApplicationTypeInfo.Title;
            lblAppFees.Text = _Application.PaidFees.ToString();
            lblApplicant.Text = _Application.ApplicantFullName;
            lblBaseAppDate.Text = clsFormat.DateFormat(_Application.ApplicationDate);
            lblBaseStatusDate.Text = clsFormat.DateFormat(_Application.LastStatusDate);
            lblCreatedBy.Text = _Application.CreatedByUserInfo.UserName;
        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblBaseAppID.Text = "[????]";
            lblAppStatus.Text = "[????]";
            lblAppType.Text = "[????]";
            lblAppFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblBaseAppDate.Text = "[????]";
            lblBaseStatusDate.Text = "[????]";
            lblCreatedBy.Text = "[????]";

        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_ApplicationID);
        }
    }
}
