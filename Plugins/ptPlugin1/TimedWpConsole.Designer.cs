namespace ptPlugin1
{
    partial class TimedWpConsole
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bExecute = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bExit = new System.Windows.Forms.Button();
            this.lRemaining = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lTarget = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lActualTime = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgV = new System.Windows.Forms.DataGridView();
            this.WP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtWP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgV)).BeginInit();
            this.SuspendLayout();
            // 
            // bExecute
            // 
            this.bExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bExecute.Location = new System.Drawing.Point(198, 237);
            this.bExecute.Name = "bExecute";
            this.bExecute.Size = new System.Drawing.Size(124, 46);
            this.bExecute.TabIndex = 1;
            this.bExecute.Text = "EXECUTE";
            this.bExecute.UseVisualStyleBackColor = true;
            this.bExecute.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtWP);
            this.panel1.Controls.Add(this.bExit);
            this.panel1.Controls.Add(this.lRemaining);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.bExecute);
            this.panel1.Controls.Add(this.lTarget);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lActualTime);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.dgV);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 417);
            this.panel1.TabIndex = 2;
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(3, 289);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(75, 23);
            this.bExit.TabIndex = 3;
            this.bExit.Text = "Exit";
            this.bExit.UseVisualStyleBackColor = true;
            // 
            // lRemaining
            // 
            this.lRemaining.AutoSize = true;
            this.lRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRemaining.Location = new System.Drawing.Point(198, 136);
            this.lRemaining.Name = "lRemaining";
            this.lRemaining.Size = new System.Drawing.Size(88, 24);
            this.lRemaining.TabIndex = 6;
            this.lRemaining.Text = "00:00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Remaining time till Execute";
            // 
            // lTarget
            // 
            this.lTarget.AutoSize = true;
            this.lTarget.Location = new System.Drawing.Point(199, 62);
            this.lTarget.Name = "lTarget";
            this.lTarget.Size = new System.Drawing.Size(98, 13);
            this.lTarget.TabIndex = 4;
            this.lTarget.Text = "WP ## Target time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Actual Time";
            // 
            // lActualTime
            // 
            this.lActualTime.AutoSize = true;
            this.lActualTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lActualTime.Location = new System.Drawing.Point(198, 27);
            this.lActualTime.Name = "lActualTime";
            this.lActualTime.Size = new System.Drawing.Size(88, 24);
            this.lActualTime.TabIndex = 2;
            this.lActualTime.Text = "00:00:00";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(198, 78);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(99, 29);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dgV
            // 
            this.dgV.AllowUserToAddRows = false;
            this.dgV.AllowUserToDeleteRows = false;
            this.dgV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WP,
            this.Time1});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgV.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgV.Location = new System.Drawing.Point(3, 3);
            this.dgV.Name = "dgV";
            this.dgV.ReadOnly = true;
            this.dgV.Size = new System.Drawing.Size(180, 280);
            this.dgV.TabIndex = 0;
            this.dgV.SelectionChanged += new System.EventHandler(this.dgV_SelectionChanged);
            // 
            // WP
            // 
            this.WP.HeaderText = "WP#";
            this.WP.Name = "WP";
            this.WP.ReadOnly = true;
            this.WP.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.WP.Width = 50;
            // 
            // Time1
            // 
            this.Time1.HeaderText = "Total Time";
            this.Time1.Name = "Time1";
            this.Time1.ReadOnly = true;
            this.Time1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Time1.Width = 50;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtWP
            // 
            this.txtWP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWP.Location = new System.Drawing.Point(198, 187);
            this.txtWP.Name = "txtWP";
            this.txtWP.Size = new System.Drawing.Size(52, 29);
            this.txtWP.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "WP to Jump";
            // 
            // TimedWpConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimedWpConsole";
            this.Text = "Timed WP Console";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bExecute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgV;
        private System.Windows.Forms.Label lTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lActualTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time1;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Label lRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWP;
    }
}