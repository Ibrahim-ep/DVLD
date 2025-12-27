using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.IO;

namespace DVLD.People.Controls
{
    public partial class ctrPerson_Info : UserControl
    {
        private int _PersonID;
        private clsPerson _Person;

        public int PersonID
        {
            get { return _PersonID; }
        }
        
        public clsPerson SelectedPerson
        {
            get { return _Person; }
        }


        public ctrPerson_Info()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            _PersonID = PersonID;
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            _PersonID = _Person.ID;

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {
            lblEditPersonInfo.Visible = true;

            _Person.ID = _PersonID;

            lblPersonID.Text = _Person.ID.ToString();
            lblNationalNo.Text = _Person.NationalNO;
            lblName.Text = _Person.FullName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblCountry.Text = _Person.NationalityCounrtyID.ToString();
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblName.Text = "[????]";
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pictureBox1.Image = Resources.Male_512;

        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }
    }
}
