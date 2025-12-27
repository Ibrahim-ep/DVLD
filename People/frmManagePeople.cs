using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmManagePeople : Form
    {
       

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            dgvListPeople.DataSource = clsPerson.GetAllPeople();
            lblNumberOfRecords.Text = dgvListPeople.RowCount.ToString();
        }

        private void _SearchBy()
        {
            DataTable DT = clsPerson.GetAllPeople();
            string SearchString = txtFilterBy.Text.Trim();
            switch (cbFilterBy.Text.Trim())
            {
                
                case "ID":
                    {
                       
                        if (int.TryParse(SearchString, out int ID))
                        {
                            DT.DefaultView.RowFilter = $"PersonID = {ID}";
                        }
                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "National No":
                {
                       
                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"NationalNo = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                }
                case "First Name":
                    {
                       
                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"FirstName = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Second Name":
                    {
                       
                       if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"SecondName = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Third Name":
                    {
                        
                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"ThirdName = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Last Name":
                    {
                       if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"LastName = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Nationality":
                    {

                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"NationalityCountryID = {SearchString}";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Phone":
                    {

                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"Phone = '{SearchString}'";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Email":
                    {
                       
                       if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"Email = {SearchString}";
                        }

                        dgvListPeople.DataSource = DT;
                        break;
                    }
                case "Gender":
                    {

                        if (string.IsNullOrEmpty(SearchString) == false)
                        {
                            DT.DefaultView.RowFilter = $"Gendor = {SearchString}";
                        }
                            dgvListPeople.DataSource = DT;
                        break;
                    }
                default:
                    {
                        DT.DefaultView.RowFilter = "";
                        dgvListPeople.DataSource = DT;
                        break;

                    }

            }
        }
        

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(Convert.ToInt32(dgvListPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(Convert.ToInt32(dgvListPeople.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Person deleted successfully.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete person.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                    _RefreshPeopleList();
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvListPeople.CurrentRow.Cells[0].Value);

            frmPersonInfo frm = new frmPersonInfo(PersonID);

            frm.Show();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            _SearchBy();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = true;
        }
    }
}
