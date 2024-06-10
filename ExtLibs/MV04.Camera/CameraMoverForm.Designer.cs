namespace MV04.Camera
{
    partial class CameraMoverForm
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
            this.button_Up = new System.Windows.Forms.Button();
            this.button_Center = new System.Windows.Forms.Button();
            this.button_Down = new System.Windows.Forms.Button();
            this.button_Right = new System.Windows.Forms.Button();
            this.button_Left = new System.Windows.Forms.Button();
            this.button_ZoomIn = new System.Windows.Forms.Button();
            this.button_ZoomOut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Up
            // 
            this.button_Up.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Up.Location = new System.Drawing.Point(43, 12);
            this.button_Up.Name = "button_Up";
            this.button_Up.Size = new System.Drawing.Size(25, 25);
            this.button_Up.TabIndex = 0;
            this.button_Up.Text = "▲";
            this.button_Up.UseVisualStyleBackColor = true;
            this.button_Up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_Up_MouseDown);
            this.button_Up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_Up_MouseUp);
            // 
            // button_Center
            // 
            this.button_Center.Location = new System.Drawing.Point(43, 43);
            this.button_Center.Name = "button_Center";
            this.button_Center.Size = new System.Drawing.Size(25, 25);
            this.button_Center.TabIndex = 1;
            this.button_Center.Text = "⚪";
            this.button_Center.UseVisualStyleBackColor = true;
            this.button_Center.Visible = false;
            this.button_Center.Click += new System.EventHandler(this.button_Center_Click);
            // 
            // button_Down
            // 
            this.button_Down.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Down.Location = new System.Drawing.Point(43, 74);
            this.button_Down.Name = "button_Down";
            this.button_Down.Size = new System.Drawing.Size(25, 25);
            this.button_Down.TabIndex = 2;
            this.button_Down.Text = "▼";
            this.button_Down.UseVisualStyleBackColor = true;
            this.button_Down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_Down_MouseDown);
            this.button_Down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_Up_MouseUp);
            // 
            // button_Right
            // 
            this.button_Right.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Right.Location = new System.Drawing.Point(74, 43);
            this.button_Right.Name = "button_Right";
            this.button_Right.Size = new System.Drawing.Size(25, 25);
            this.button_Right.TabIndex = 3;
            this.button_Right.Text = "►";
            this.button_Right.UseVisualStyleBackColor = true;
            this.button_Right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_Right_MouseDown);
            this.button_Right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_Right_MouseUp);
            // 
            // button_Left
            // 
            this.button_Left.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Left.Location = new System.Drawing.Point(12, 43);
            this.button_Left.Name = "button_Left";
            this.button_Left.Size = new System.Drawing.Size(25, 25);
            this.button_Left.TabIndex = 4;
            this.button_Left.Text = "◄";
            this.button_Left.UseVisualStyleBackColor = true;
            this.button_Left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_Left_MouseDown);
            this.button_Left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_Right_MouseUp);
            // 
            // button_ZoomIn
            // 
            this.button_ZoomIn.Location = new System.Drawing.Point(74, 12);
            this.button_ZoomIn.Name = "button_ZoomIn";
            this.button_ZoomIn.Size = new System.Drawing.Size(25, 25);
            this.button_ZoomIn.TabIndex = 5;
            this.button_ZoomIn.Text = "+";
            this.button_ZoomIn.UseVisualStyleBackColor = true;
            this.button_ZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ZoomIn_MouseDown);
            this.button_ZoomIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ZoomIn_MouseUp);
            // 
            // button_ZoomOut
            // 
            this.button_ZoomOut.Location = new System.Drawing.Point(74, 74);
            this.button_ZoomOut.Name = "button_ZoomOut";
            this.button_ZoomOut.Size = new System.Drawing.Size(25, 25);
            this.button_ZoomOut.TabIndex = 6;
            this.button_ZoomOut.Text = "-";
            this.button_ZoomOut.UseVisualStyleBackColor = true;
            this.button_ZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ZoomOut_MouseDown);
            this.button_ZoomOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ZoomIn_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // CameraMoverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(119, 130);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_ZoomOut);
            this.Controls.Add(this.button_ZoomIn);
            this.Controls.Add(this.button_Right);
            this.Controls.Add(this.button_Down);
            this.Controls.Add(this.button_Center);
            this.Controls.Add(this.button_Left);
            this.Controls.Add(this.button_Up);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CameraMoverForm";
            this.Text = "Camera Mover";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraMoverForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Up;
        private System.Windows.Forms.Button button_Center;
        private System.Windows.Forms.Button button_Down;
        private System.Windows.Forms.Button button_Right;
        private System.Windows.Forms.Button button_Left;
        private System.Windows.Forms.Button button_ZoomIn;
        private System.Windows.Forms.Button button_ZoomOut;
        private System.Windows.Forms.Label label1;
    }
}