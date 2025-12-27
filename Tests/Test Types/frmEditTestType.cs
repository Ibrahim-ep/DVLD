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

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        
        private clsTestType.enTestTypes _TestTypeID;
        clsTestType _TestType;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();

            if (TestTypeID != 0)
            {
                _TestTypeID = (clsTestType.enTestTypes)TestTypeID;
                _TestType = clsTestType.Find(_TestTypeID);
               
            }
        }

        private void _LoadData()
        {
            if (_TestType == null)
            {
                MessageBox.Show("Did not find test Type");
                _ResetDefualValues();
                return;
            }

            txtDescription.Text = _TestType.Description;
            txtTitle.Text = _TestType.Title;
            txtFees.Text = _TestType.Fees.ToString();

            lblTestTypeID.Text = _TestTypeID.ToString();
            
        }

        private void _ResetDefualValues()
        {
            txtDescription.Text = string.Empty;
            txtFees.Text = "???";
            txtTitle.Text = "???";
            lblTestTypeID.Text = "???";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.Title = txtTitle.Text.Trim();
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToInt32(txtFees.Text.Trim());

            if(_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
