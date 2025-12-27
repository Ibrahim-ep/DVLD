using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Business;
using DVLD.People.Controls;
using DVLD.Properties;
using DVLD.Global;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;

        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        int _PersonID = -1;
        clsPerson _Person;


        public frmAddUpdatePerson()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            cbCountry.Items.Add(clsCountry.GetAllCountries());

            _PersonID = PersonID;

            if (_PersonID != -1)
            {
                _Mode = enMode.Update;

            }
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow dr in dtCountries.Rows)
            {
                cbCountry.Items.Add(dr["CountryName"].ToString());
            }
        }

        private void _ResetDefultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Edit Person Info";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            
            lblRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

          
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Iraq");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.ID.ToString();
            
            txtNationalNo.Text = _Person.NationalNO;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }


            if (_Person.ThirdName != string.Empty)
            {
                txtThirdName.Text = _Person.ThirdName;
            }

            txtLastName.Text = _Person.LastName;
            txtPhone.Text = _Person.Phone;
            
            if (_Person.Email != string.Empty)
            {
                txtEmail.Text = _Person.Email;
            }

            dtpDateOfBirth.Value = _Person.DateOfBirth;
            
            if (_Person.ImagePath != string.Empty)
            {
               pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            lblRemoveImage.Visible = (_Person.ImagePath != string.Empty);

            cbCountry.SelectedIndex = cbCountry.FindStringExact(clsCountry.Find(_Person.NationalityCounrtyID).CountryName);
        }

        private bool _MangePersonImage()
        {
            if (pbPersonImage.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != string.Empty)
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.Trim();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Failed to copy image to project images folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void ValidateEmptyTextBox(object Sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)Sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "This field is required.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(Temp, string.Empty);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

           
            if (txtNationalNo.Text.Trim() == _Person.NationalNO && clsPerson.isPersonExists(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.BackgroundImage = Properties.Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == null)
                pbPersonImage.BackgroundImage = Properties.Resources.Female_512;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == string.Empty)
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid email format.");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
            
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (txtAddress.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Address is required.");
            }
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedImagePath = openFileDialog1.FileName;

                pbPersonImage.Load(SelectedImagePath);

                lblRemoveImage.Visible = true;
            }

        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
            {
                pbPersonImage.BackgroundImage = Properties.Resources.Male_512;
            }
            else
            {
                pbPersonImage.BackgroundImage = Properties.Resources.Female_512;
            }

            lblRemoveImage.Visible = false;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefultValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_MangePersonImage())
            {
                return;
            }

            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.NationalNO = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;

            if (txtThirdName.Text != string.Empty)
            {
                _Person.ThirdName = txtThirdName.Text;
            }

            _Person.LastName = txtLastName.Text;
            _Person.Phone = txtPhone.Text;

            if (txtEmail.Text != string.Empty)
            {
                _Person.Email = txtEmail.Text;
            }

            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCounrtyID = CountryID;
            _Person.Address = txtAddress.Text;
            _Person.Gender = rbMale.Checked ? (byte)0 : (byte)1;

            if (pbPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation; 
            }
            else
            {
                _Person.ImagePath = string.Empty;
            }

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.ID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Edit Person Info";

                MessageBox.Show("Person data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.ID);
            }
            else
            {
                MessageBox.Show("Failed to save person data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
