using DVLD.Global;
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

namespace DVLD.Tests.Vision_Test
{
    public partial class frmTakeTest : Form
    {
        private clsTestType.enTestTypes _TestType;
        private int _TestAppointmentID;

        private int _TestID;
        private clsTests _Test;

        public frmTakeTest(int TestAppointmentID , clsTestType.enTestTypes TestType)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;

            _TestType = TestType;
        }

        private void _LoadData()
        {
            ctrlSchduledTests1.TestType = _TestType;
            ctrlSchduledTests1.LoadInfo(_TestAppointmentID);

            if(ctrlSchduledTests1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int _TestID = ctrlSchduledTests1.TestID;

            {
                if (_TestID != -1)
                {
                    _Test = clsTests.Find(_TestID);

                    if (_Test.TestResult)
                        rbPass.Checked = true;
                    else
                        rbFail.Checked = true;

                    txtNotes.Text = _Test.Notes;

                    rbPass.Enabled = false;
                    rbFail.Enabled = false;
                }
                else
                    _Test = new clsTests();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            if (rbPass.Checked)
            {
                _Test.TestResult = true;
            }
            else
            {
                _Test.TestResult = false;
            }

            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.TestAppointmentID = _TestAppointmentID;
            

            if (_Test.Save())
            {
                MessageBox.Show("Test results saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error saving test results.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
