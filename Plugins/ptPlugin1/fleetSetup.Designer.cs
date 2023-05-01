namespace ptPlugin1
{
    partial class fleetSetup
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
            this.lConnectedUAV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbEnable1 = new System.Windows.Forms.CheckBox();
            this.cbEnable2 = new System.Windows.Forms.CheckBox();
            this.cbEnable3 = new System.Windows.Forms.CheckBox();
            this.tTail1 = new System.Windows.Forms.TextBox();
            this.tTail2 = new System.Windows.Forms.TextBox();
            this.tTail3 = new System.Windows.Forms.TextBox();
            this.tCallSign1 = new System.Windows.Forms.TextBox();
            this.tCallSign2 = new System.Windows.Forms.TextBox();
            this.tCallSign3 = new System.Windows.Forms.TextBox();
            this.bOK = new MissionPlanner.Controls.MyButton();
            this.bCancel = new MissionPlanner.Controls.MyButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lConnectedUAV
            // 
            this.lConnectedUAV.AutoSize = true;
            this.lConnectedUAV.Location = new System.Drawing.Point(12, 9);
            this.lConnectedUAV.Name = "lConnectedUAV";
            this.lConnectedUAV.Size = new System.Drawing.Size(102, 13);
            this.lConnectedUAV.TabIndex = 0;
            this.lConnectedUAV.Text = "Connected UAV id\'s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command Sots";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Slot #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Plane TAIL # (sysid)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Callsign";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tCallSign3, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tCallSign2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tTail1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbEnable1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tCallSign1, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 74);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(574, 114);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "2";
            // 
            // cbEnable1
            // 
            this.cbEnable1.AutoSize = true;
            this.cbEnable1.Location = new System.Drawing.Point(60, 31);
            this.cbEnable1.Name = "cbEnable1";
            this.cbEnable1.Size = new System.Drawing.Size(15, 14);
            this.cbEnable1.TabIndex = 1;
            this.cbEnable1.UseVisualStyleBackColor = true;
            // 
            // cbEnable2
            // 
            this.cbEnable2.AutoSize = true;
            this.cbEnable2.Location = new System.Drawing.Point(60, 59);
            this.cbEnable2.Name = "cbEnable2";
            this.cbEnable2.Size = new System.Drawing.Size(15, 14);
            this.cbEnable2.TabIndex = 4;
            this.cbEnable2.UseVisualStyleBackColor = true;
            // 
            // cbEnable3
            // 
            this.cbEnable3.AutoSize = true;
            this.cbEnable3.Location = new System.Drawing.Point(60, 87);
            this.cbEnable3.Name = "cbEnable3";
            this.cbEnable3.Size = new System.Drawing.Size(15, 14);
            this.cbEnable3.TabIndex = 7;
            this.cbEnable3.UseVisualStyleBackColor = true;
            // 
            // tTail1
            // 
            this.tTail1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tTail1.Location = new System.Drawing.Point(232, 31);
            this.tTail1.Name = "tTail1";
            this.tTail1.Size = new System.Drawing.Size(100, 20);
            this.tTail1.TabIndex = 2;
            // 
            // tTail2
            // 
            this.tTail2.Location = new System.Drawing.Point(232, 59);
            this.tTail2.Name = "tTail2";
            this.tTail2.Size = new System.Drawing.Size(100, 20);
            this.tTail2.TabIndex = 5;
            // 
            // tTail3
            // 
            this.tTail3.Location = new System.Drawing.Point(232, 87);
            this.tTail3.Name = "tTail3";
            this.tTail3.Size = new System.Drawing.Size(100, 20);
            this.tTail3.TabIndex = 8;
            // 
            // tCallSign1
            // 
            this.tCallSign1.Location = new System.Drawing.Point(409, 29);
            this.tCallSign1.Name = "tCallSign1";
            this.tCallSign1.Size = new System.Drawing.Size(100, 20);
            this.tCallSign1.TabIndex = 3;
            // 
            // tCallSign2
            // 
            this.tCallSign2.Location = new System.Drawing.Point(409, 61);
            this.tCallSign2.Name = "tCallSign2";
            this.tCallSign2.Size = new System.Drawing.Size(100, 20);
            this.tCallSign2.TabIndex = 6;
            // 
            // tCallSign3
            // 
            this.tCallSign3.Location = new System.Drawing.Point(410, 89);
            this.tCallSign3.Name = "tCallSign3";
            this.tCallSign3.Size = new System.Drawing.Size(100, 20);
            this.tCallSign3.TabIndex = 9;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(186, 214);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 10;
            this.bOK.Text = "Update";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(348, 214);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // fleetSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 272);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lConnectedUAV);
            this.Name = "fleetSetup";
            this.Text = "fleetSetup";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lConnectedUAV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tCallSign3;
        private System.Windows.Forms.TextBox tTail3;
        private System.Windows.Forms.TextBox tCallSign2;
        private System.Windows.Forms.CheckBox cbEnable3;
        private System.Windows.Forms.TextBox tTail2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tTail1;
        private System.Windows.Forms.CheckBox cbEnable2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbEnable1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tCallSign1;
        private MissionPlanner.Controls.MyButton bOK;
        private MissionPlanner.Controls.MyButton bCancel;
    }
}