using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmDriverInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID;
        public frmDriverInternationalLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();

            _InternationalLicenseID = internationalLicenseID;
        }

        private void frmDriverInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalLicenseID);
        }
    }
}
