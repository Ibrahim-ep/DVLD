using DVLD.Properties;
using DVLD.Tests.Test_Types;
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
    public partial class frmListTestAppointment : Form
    {
       
        public clsTestType.enTestTypes _TestType;
        DataTable _dtLicenseTestAppointments = new DataTable();
        private int _LocalDrivingLicenseApplicationID;

        public frmListTestAppointment(int LocalDrivingLicenseApplicationID,clsTestType.enTestTypes testType)
        {
            InitializeComponent();

            _TestType = testType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmListTestAppointment_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            ctrlLocalDrivingLicenseAppInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvAppointmentsList.DataSource = _dtLicenseTestAppointments;

            if (dgvAppointmentsList.Rows.Count > 0)
            {
                dgvAppointmentsList.Columns[0].HeaderText = "Appointment ID";
                dgvAppointmentsList.Columns[0].Width = 150;

                dgvAppointmentsList.Columns[1].HeaderText = "Appointment Date";
                dgvAppointmentsList.Columns[1].Width = 200;

                dgvAppointmentsList.Columns[2].HeaderText = "Paid Fees";
                dgvAppointmentsList.Columns[2].Width = 150;

                dgvAppointmentsList.Columns[3].HeaderText = "Is Locked";
                dgvAppointmentsList.Columns[3].Width = 100;
            }
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.isThereIsAnActiveSchduleTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //---
            clsTests LastTest = localDrivingLicenseApplication.FindLastTestByPersonIDAndLicenseClassAndTestType(_TestType);

            if (LastTest == null)
            {
                frmSchduleTest frm1 = new frmSchduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointment_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmSchduleTest frm2 = new frmSchduleTest
                (LastTest.TestAppointment.LocalDrivingLicenseApplicationID, _TestType);
            frm2.ShowDialog();
            frmListTestAppointment_Load(null, null);
            //---
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest(Convert.ToInt32(dgvAppointmentsList.CurrentRow.Cells[0].Value), _TestType);

            frm.ShowDialog();

            frmListTestAppointment_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = Convert.ToInt32(dgvAppointmentsList.CurrentRow.Cells[0].Value);

            frmSchduleTest frm = new frmSchduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();

            frmListTestAppointment_Load(null, null);
        }

        private void _ResetDefaultValues()
        {
            switch (_TestType)
            {

                case clsTestType.enTestTypes.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestIcon.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestTypes.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestIcon.Image = Resources.Written_Test_32_Sechdule;
                        break;
                    }
                case clsTestType.enTestTypes.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        pbTestIcon.Image = Resources.Street_Test_32;
                        break;
                    }
            }
        }


    }
}
