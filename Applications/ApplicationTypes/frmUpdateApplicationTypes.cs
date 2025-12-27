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

namespace DVLD.ApplicationTypes
{
    public partial class frmUpdateApplicationTypes : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypes _ApplicationType;
        public frmUpdateApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void _LoadInfo()
        {
            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show("Application with ID " + _ApplicationTypeID + " Not Found");
                this.Close();
                return;
            }

            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.Title;
            txtFees.Text = _ApplicationType.Fees.ToString();
        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees = Convert.ToInt32(txtFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully");
            }
            else
            {
                MessageBox.Show("Data Wasn't saved");
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title can't be empty");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees can't be zero");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "You Must enter a number");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }

        }

    }
}
        
   

