
namespace ptPlugin1
{
    partial class Form1
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
            this.annunciator1 = new MissionPlanner.Controls.annunciator();
            this.SuspendLayout();
            // 
            // annunciator1
            // 
            this.annunciator1.BackColor = System.Drawing.Color.Black;
            this.annunciator1.contextMenuEnabled = false;
            this.annunciator1.Location = new System.Drawing.Point(12, 111);
            this.annunciator1.Name = "annunciator1";
            this.annunciator1.Size = new System.Drawing.Size(1300, 100);
            this.annunciator1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 450);
            this.Controls.Add(this.annunciator1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MissionPlanner.Controls.annunciator annunciator1;
    }
}