namespace MissionPlanner.MV04.HudCrosshairCalc
{
    partial class TestForm
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
            this.panel_CameraView = new System.Windows.Forms.Panel();
            this.button_Update = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_CrosshairType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // panel_CameraView
            // 
            this.panel_CameraView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_CameraView.BackColor = System.Drawing.Color.Black;
            this.panel_CameraView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel_CameraView.Location = new System.Drawing.Point(12, 41);
            this.panel_CameraView.Name = "panel_CameraView";
            this.panel_CameraView.Size = new System.Drawing.Size(800, 388);
            this.panel_CameraView.TabIndex = 0;
            this.panel_CameraView.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_CameraView_Paint);
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(200, 12);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 23);
            this.button_Update.TabIndex = 5;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Crosshairs";
            // 
            // comboBox_CrosshairType
            // 
            this.comboBox_CrosshairType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CrosshairType.FormattingEnabled = true;
            this.comboBox_CrosshairType.Items.AddRange(new object[] {
            "Plus",
            "HorizontalDivisions"});
            this.comboBox_CrosshairType.Location = new System.Drawing.Point(73, 14);
            this.comboBox_CrosshairType.Name = "comboBox_CrosshairType";
            this.comboBox_CrosshairType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_CrosshairType.TabIndex = 7;
            this.comboBox_CrosshairType.SelectedIndexChanged += new System.EventHandler(this.comboBox_CrosshairType_SelectedIndexChanged);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 441);
            this.Controls.Add(this.comboBox_CrosshairType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.panel_CameraView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Horizontal Crosshairs Test Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_CameraView;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_CrosshairType;
    }
}