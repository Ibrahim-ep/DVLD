namespace DVLD.Tests
{
    partial class frmSchduleTest
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
            this.ctrlSchduleTest1 = new DVLD.Tests.Controls.ctrlSchduleTest();
            this.SuspendLayout();
            // 
            // ctrlSchduleTest1
            // 
            this.ctrlSchduleTest1.Location = new System.Drawing.Point(2, 12);
            this.ctrlSchduleTest1.Name = "ctrlSchduleTest1";
            this.ctrlSchduleTest1.Size = new System.Drawing.Size(422, 528);
            this.ctrlSchduleTest1.TabIndex = 0;
            this.ctrlSchduleTest1.Load += new System.EventHandler(this.ctrlSchduleTest1_Load);
            // 
            // frmSchduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 543);
            this.Controls.Add(this.ctrlSchduleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSchduleTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSchduleTest";
            this.Load += new System.EventHandler(this.frmSchduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlSchduleTest ctrlSchduleTest1;
    }
}