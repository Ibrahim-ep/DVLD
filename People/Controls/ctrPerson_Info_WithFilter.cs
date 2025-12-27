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

namespace DVLD.People.Controls
{
    public partial class ctrPerson_Info_WithFilter : UserControl
    {
       
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> Handler = OnPersonSelected;
            if (Handler != null)
            {
                Handler(PersonID);
            }
        }

        private bool _ShowAddNewPersonButton = true;
        public bool ShowAddNewPersonButton
        {
            get { return _ShowAddNewPersonButton; }
            set
            {
                _ShowAddNewPersonButton = value;
                btnAddNewPerson.Visible = _ShowAddNewPersonButton;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        private int _PersonID;
        public int PersonID
        {
            get { return ctrPerson_Info1.PersonID; }
        }

        private clsPerson _SelectedPersonInfo;
        public clsPerson SelectedPersonInfo
        {
            get { return ctrPerson_Info1.SelectedPerson; }
        }
        public void FilterFocus()
        {
            txtFilterBy.Focus();
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Text = PersonID.ToString();
            _SearchBy();
        }

        private void _SearchBy()
        {
            string SearchString = txtFilterBy.Text.Trim();
            switch (cbFilterBy.Text.Trim())
            {

                case "PersonID":
                    {

                        if (int.TryParse(SearchString, out int ID))
                        {
                            ctrPerson_Info1.LoadPersonInfo(ID);
                        }
                       
                        break;
                    }
                case "National No":
                    {

                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            ctrPerson_Info1.LoadPersonInfo(SearchString);
                        }

                        break;
                    }
                default:
                    {
                        
                        break;

                    }

            }

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrPerson_Info1.PersonID);
        }

        public ctrPerson_Info_WithFilter()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct the errors before searching.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SearchBy();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = PersonID.ToString();
            ctrPerson_Info1.LoadPersonInfo(PersonID);
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }

            if (cbFilterBy.Text == "ID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }
    }
}
