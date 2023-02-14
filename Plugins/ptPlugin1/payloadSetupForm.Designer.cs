
namespace ptPlugin1
{
    partial class payloadSetupForm
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
            this.payloadsetup1 = new MissionPlanner.Controls.payloadsetup();
            this.SuspendLayout();
            // 
            // payloadsetup1
            // 
            this.payloadsetup1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.payloadsetup1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.payloadsetup1.Location = new System.Drawing.Point(-1, 1);
            this.payloadsetup1.Name = "payloadsetup1";
            this.payloadsetup1.Size = new System.Drawing.Size(431, 437);
            this.payloadsetup1.TabIndex = 0;
            this.payloadsetup1.updateClicked += new System.EventHandler(this.payloadsetup1_updateClicked);
            // 
            // payloadSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 442);
            this.Controls.Add(this.payloadsetup1);
            this.Name = "payloadSetupForm";
            this.Text = "payloadSetupForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private MissionPlanner.Controls.payloadsetup payloadsetup1;
    }
}