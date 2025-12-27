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
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void _Refresh()
        {
            dgvApplicationTypesList.DataSource = clsApplicationTypes.GetAllApplicaionTypes();
            lblNumberOfRecords.Text = dgvApplicationTypesList.RowCount.ToString();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationTypes frm = new frmUpdateApplicationTypes(Convert.ToInt32(dgvApplicationTypesList.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }
    }
}
