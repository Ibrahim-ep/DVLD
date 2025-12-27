namespace DVLD.Applications.LocalDrivingLiceseApplication
{
    partial class frmLocalDrivingLiceseApplicationsList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.lblNumberOfRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvListLocalDrivingLicensAppliaction = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canceApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechdualTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schduleVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issueDrivingLicensFirstTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicensHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNewDrivingLiceseApplication = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingLicensAppliaction)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.Location = new System.Drawing.Point(241, 181);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(148, 20);
            this.txtFilterBy.TabIndex = 27;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            // 
            // lblNumberOfRecords
            // 
            this.lblNumberOfRecords.AutoSize = true;
            this.lblNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecords.Location = new System.Drawing.Point(95, 435);
            this.lblNumberOfRecords.Name = "lblNumberOfRecords";
            this.lblNumberOfRecords.Size = new System.Drawing.Size(14, 16);
            this.lblNumberOfRecords.TabIndex = 26;
            this.lblNumberOfRecords.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "# Records :";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L AppID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(83, 181);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(152, 21);
            this.cbFilterBy.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Filter By : ";
            // 
            // dgvListLocalDrivingLicensAppliaction
            // 
            this.dgvListLocalDrivingLicensAppliaction.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvListLocalDrivingLicensAppliaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListLocalDrivingLicensAppliaction.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvListLocalDrivingLicensAppliaction.Location = new System.Drawing.Point(14, 205);
            this.dgvListLocalDrivingLicensAppliaction.Name = "dgvListLocalDrivingLicensAppliaction";
            this.dgvListLocalDrivingLicensAppliaction.Size = new System.Drawing.Size(776, 206);
            this.dgvListLocalDrivingLicensAppliaction.TabIndex = 21;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.editApplicationToolStripMenuItem,
            this.deleteApplicationToolStripMenuItem,
            this.canceApplicationToolStripMenuItem,
            this.sechdualTestsToolStripMenuItem,
            this.issueDrivingLicensFirstTimeToolStripMenuItem,
            this.showLicensToolStripMenuItem,
            this.showPersonLicensHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(233, 202);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.showApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.showApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // editApplicationToolStripMenuItem
            // 
            this.editApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editApplicationToolStripMenuItem.Name = "editApplicationToolStripMenuItem";
            this.editApplicationToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.editApplicationToolStripMenuItem.Text = "Edit Application";
            this.editApplicationToolStripMenuItem.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // deleteApplicationToolStripMenuItem
            // 
            this.deleteApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.deleteApplicationToolStripMenuItem.Name = "deleteApplicationToolStripMenuItem";
            this.deleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.deleteApplicationToolStripMenuItem.Text = "Delete Application";
            this.deleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // canceApplicationToolStripMenuItem
            // 
            this.canceApplicationToolStripMenuItem.Image = global::DVLD.Properties.Resources.Delete_32;
            this.canceApplicationToolStripMenuItem.Name = "canceApplicationToolStripMenuItem";
            this.canceApplicationToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.canceApplicationToolStripMenuItem.Text = "Cancle Application";
            this.canceApplicationToolStripMenuItem.Click += new System.EventHandler(this.canceApplicationToolStripMenuItem_Click);
            // 
            // sechdualTestsToolStripMenuItem
            // 
            this.sechdualTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schduleVisionTestToolStripMenuItem,
            this.scheduleWrittenTestToolStripMenuItem,
            this.scheduleStreetTestToolStripMenuItem});
            this.sechdualTestsToolStripMenuItem.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.sechdualTestsToolStripMenuItem.Name = "sechdualTestsToolStripMenuItem";
            this.sechdualTestsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.sechdualTestsToolStripMenuItem.Text = "Sechdual Tests";
            this.sechdualTestsToolStripMenuItem.Click += new System.EventHandler(this.sechdualTestsToolStripMenuItem_Click);
            // 
            // schduleVisionTestToolStripMenuItem
            // 
            this.schduleVisionTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.schduleVisionTestToolStripMenuItem.Name = "schduleVisionTestToolStripMenuItem";
            this.schduleVisionTestToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.schduleVisionTestToolStripMenuItem.Text = "Schdule vision test";
            this.schduleVisionTestToolStripMenuItem.Click += new System.EventHandler(this.schduleVisionTestToolStripMenuItem_Click);
            // 
            // scheduleWrittenTestToolStripMenuItem
            // 
            this.scheduleWrittenTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Written_Test_32_Sechdule;
            this.scheduleWrittenTestToolStripMenuItem.Name = "scheduleWrittenTestToolStripMenuItem";
            this.scheduleWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.scheduleWrittenTestToolStripMenuItem.Text = "Schedule Written test";
            this.scheduleWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.scheduleWrittenTestToolStripMenuItem_Click);
            // 
            // scheduleStreetTestToolStripMenuItem
            // 
            this.scheduleStreetTestToolStripMenuItem.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.scheduleStreetTestToolStripMenuItem.Name = "scheduleStreetTestToolStripMenuItem";
            this.scheduleStreetTestToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.scheduleStreetTestToolStripMenuItem.Text = "Schedule street test";
            this.scheduleStreetTestToolStripMenuItem.Click += new System.EventHandler(this.scheduleStreetTestToolStripMenuItem_Click);
            // 
            // issueDrivingLicensFirstTimeToolStripMenuItem
            // 
            this.issueDrivingLicensFirstTimeToolStripMenuItem.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.issueDrivingLicensFirstTimeToolStripMenuItem.Name = "issueDrivingLicensFirstTimeToolStripMenuItem";
            this.issueDrivingLicensFirstTimeToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.issueDrivingLicensFirstTimeToolStripMenuItem.Text = "Issue Driving Licens First Time";
            this.issueDrivingLicensFirstTimeToolStripMenuItem.Click += new System.EventHandler(this.issueDrivingLicensFirstTimeToolStripMenuItem_Click);
            // 
            // showLicensToolStripMenuItem
            // 
            this.showLicensToolStripMenuItem.Image = global::DVLD.Properties.Resources.License_View_32;
            this.showLicensToolStripMenuItem.Name = "showLicensToolStripMenuItem";
            this.showLicensToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.showLicensToolStripMenuItem.Text = "Show Licens";
            this.showLicensToolStripMenuItem.Click += new System.EventHandler(this.showLicensToolStripMenuItem_Click);
            // 
            // showPersonLicensHistoryToolStripMenuItem
            // 
            this.showPersonLicensHistoryToolStripMenuItem.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicensHistoryToolStripMenuItem.Name = "showPersonLicensHistoryToolStripMenuItem";
            this.showPersonLicensHistoryToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.showPersonLicensHistoryToolStripMenuItem.Text = "Show Person Licens History";
            this.showPersonLicensHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicensHistoryToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(193, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 33);
            this.label1.TabIndex = 20;
            this.label1.Text = "Manage Local Driving License List";
            // 
            // btnAddNewDrivingLiceseApplication
            // 
            this.btnAddNewDrivingLiceseApplication.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAddNewDrivingLiceseApplication.BackgroundImage = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewDrivingLiceseApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewDrivingLiceseApplication.Location = new System.Drawing.Point(726, 159);
            this.btnAddNewDrivingLiceseApplication.Name = "btnAddNewDrivingLiceseApplication";
            this.btnAddNewDrivingLiceseApplication.Size = new System.Drawing.Size(64, 39);
            this.btnAddNewDrivingLiceseApplication.TabIndex = 24;
            this.btnAddNewDrivingLiceseApplication.UseVisualStyleBackColor = false;
            this.btnAddNewDrivingLiceseApplication.Click += new System.EventHandler(this.btnAddNewDrivingLiceseApplication_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD.Properties.Resources.Manage_Applications_64;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(345, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 113);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // frmLocalDrivingLiceseApplicationsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.lblNumberOfRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddNewDrivingLiceseApplication);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvListLocalDrivingLicensAppliaction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLiceseApplicationsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving Licese ApplicationsList";
            this.Load += new System.EventHandler(this.frmLocalDrivingLiceseApplicationsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLocalDrivingLicensAppliaction)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.Label lblNumberOfRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNewDrivingLiceseApplication;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvListLocalDrivingLicensAppliaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem canceApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechdualTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicensFirstTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicensHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schduleVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStreetTestToolStripMenuItem;
    }
}