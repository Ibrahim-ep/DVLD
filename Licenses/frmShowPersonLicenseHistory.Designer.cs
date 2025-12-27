namespace DVLD.Licenses
{
    partial class frmShowPersonLicenseHistory
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
            this.ctrlDriverLicenses1 = new DVLD.Licenses.Controls.ctrlDriverLicenses();
            this.ctrPerson_Info_WithFilter1 = new DVLD.People.Controls.ctrPerson_Info_WithFilter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(12, 399);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(889, 295);
            this.ctrlDriverLicenses1.TabIndex = 0;
            this.ctrlDriverLicenses1.Load += new System.EventHandler(this.ctrlDriverLicenses1_Load);
            // 
            // ctrPerson_Info_WithFilter1
            // 
            this.ctrPerson_Info_WithFilter1.FilterEnabled = true;
            this.ctrPerson_Info_WithFilter1.Location = new System.Drawing.Point(191, 1);
            this.ctrPerson_Info_WithFilter1.Name = "ctrPerson_Info_WithFilter1";
            this.ctrPerson_Info_WithFilter1.ShowAddNewPersonButton = true;
            this.ctrPerson_Info_WithFilter1.Size = new System.Drawing.Size(714, 392);
            this.ctrPerson_Info_WithFilter1.TabIndex = 1;
            this.ctrPerson_Info_WithFilter1.OnPersonSelected += new System.Action<int>(this.ctrPerson_Info_WithFilter1_OnPersonSelected);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD.Properties.Resources.PersonLicenseHistory_512;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 107);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 197);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frmShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 706);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ctrPerson_Info_WithFilter1);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowPersonLicenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Person License History";
            this.Load += new System.EventHandler(this.frmShowPersonLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlDriverLicenses ctrlDriverLicenses1;
        private People.Controls.ctrPerson_Info_WithFilter ctrPerson_Info_WithFilter1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}