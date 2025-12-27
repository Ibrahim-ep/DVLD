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

namespace DVLD.People
{
    public partial class frmPersonInfo : Form
    {

        public frmPersonInfo(int ID)
        {
            InitializeComponent();

            ctrPerson_Info1.LoadPersonInfo(ID);
        }

        public frmPersonInfo(string NationalNo)
        {
            InitializeComponent();

            ctrPerson_Info1.LoadPersonInfo(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
