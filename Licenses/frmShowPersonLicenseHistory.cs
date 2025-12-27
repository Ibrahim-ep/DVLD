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

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID;
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlDriverLicenses1.LoadDataByPersonID(_PersonID);
                ctrPerson_Info_WithFilter1.LoadPersonInfo(_PersonID);
                ctrPerson_Info_WithFilter1.FilterEnabled = false;
            }
            else
            {
                ctrPerson_Info_WithFilter1.FilterEnabled = true;
                ctrPerson_Info_WithFilter1.FilterFocus();
            }
        }

        private void ctrPerson_Info_WithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else
            {
                ctrlDriverLicenses1.LoadDataByPersonID(_PersonID);
            }

        }

        private void ctrlDriverLicenses1_Load(object sender, EventArgs e)
        {

        }
    }
}
