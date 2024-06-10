namespace MV04.Camera
{
    partial class TrackerPosForm
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
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.button_StartTracking = new System.Windows.Forms.Button();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_StopTracking = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_X
            // 
            this.numericUpDown_X.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_X.Location = new System.Drawing.Point(44, 16);
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_X.TabIndex = 0;
            // 
            // button_StartTracking
            // 
            this.button_StartTracking.Location = new System.Drawing.Point(110, 12);
            this.button_StartTracking.Name = "button_StartTracking";
            this.button_StartTracking.Size = new System.Drawing.Size(90, 25);
            this.button_StartTracking.TabIndex = 1;
            this.button_StartTracking.Text = "Start Tracking";
            this.button_StartTracking.UseVisualStyleBackColor = true;
            this.button_StartTracking.Click += new System.EventHandler(this.button_StartTracking_Click);
            // 
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Y.Location = new System.Drawing.Point(44, 47);
            this.numericUpDown_Y.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown_Y.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "X = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y = ";
            // 
            // button_StopTracking
            // 
            this.button_StopTracking.Location = new System.Drawing.Point(110, 43);
            this.button_StopTracking.Name = "button_StopTracking";
            this.button_StopTracking.Size = new System.Drawing.Size(90, 25);
            this.button_StopTracking.TabIndex = 5;
            this.button_StopTracking.Text = "Stop Tracking";
            this.button_StopTracking.UseVisualStyleBackColor = true;
            this.button_StopTracking.Click += new System.EventHandler(this.button_StopTracking_Click);
            // 
            // TrackerPosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 83);
            this.Controls.Add(this.button_StopTracking);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_Y);
            this.Controls.Add(this.button_StartTracking);
            this.Controls.Add(this.numericUpDown_X);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrackerPosForm";
            this.Text = "TrackerPosForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_X;
        private System.Windows.Forms.Button button_StartTracking;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_StopTracking;
    }
}