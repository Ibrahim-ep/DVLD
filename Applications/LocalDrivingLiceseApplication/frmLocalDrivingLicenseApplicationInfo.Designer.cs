namespace DVLD.Applications.LocalDrivingLiceseApplication
{
    partial class frmLocalDrivingLicenseApplicationInfo
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
            this.ctrlLocalDrivingLicenseAppInfo1 = new DVLD.Applications.LocalDrivingLiceseApplication.ctrlLocalDrivingLicenseAppInfo();
            this.SuspendLayout();
            // 
            // ctrlLocalDrivingLicenseAppInfo1
            // 
            this.ctrlLocalDrivingLicenseAppInfo1.Location = new System.Drawing.Point(5, 12);
            this.ctrlLocalDrivingLicenseAppInfo1.Name = "ctrlLocalDrivingLicenseAppInfo1";
            this.ctrlLocalDrivingLicenseAppInfo1.Size = new System.Drawing.Size(662, 281);
            this.ctrlLocalDrivingLicenseAppInfo1.TabIndex = 0;
            // 
            // frmLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 295);
            this.Controls.Add(this.ctrlLocalDrivingLicenseAppInfo1);
            this.Name = "frmLocalDrivingLicenseApplicationInfo";
            this.Text = "Local Driving License Application Info";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationInfo_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLocalDrivingLicenseAppInfo ctrlLocalDrivingLicenseAppInfo1;
    }
}