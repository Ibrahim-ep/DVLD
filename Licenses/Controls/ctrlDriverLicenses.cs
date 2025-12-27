using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;

        private DataTable _dtLocalDrivingLicenseHistory;
        private DataTable _dtInternationalLicenseHistory;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {
            _dtLocalDrivingLicenseHistory = clsDriver.GetDriverLicense(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtLocalDrivingLicenseHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.RowCount.ToString();
        }

        private void _LoadInternationalLicenseInfo()
        {
            _dtInternationalLicenseHistory = clsInternationalLicenseApplication.GetAllDriverInternationalLicneses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtInternationalLicenseHistory;
            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.RowCount.ToString();
        }

        public void LoadData(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.Find(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver");
                return;
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void LoadDataByPersonID(int PersonID)
        {
           
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver");
                return;
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void Clear()
        {
            _dtInternationalLicenseHistory.Clear();
            _dtLocalDrivingLicenseHistory.Clear();
        }
    }
}

