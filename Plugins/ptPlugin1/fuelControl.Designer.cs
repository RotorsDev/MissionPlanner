
namespace ptPlugin1
{
    partial class fuelControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fuelControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lConsumed = new System.Windows.Forms.Label();
            this.lLoaded = new System.Windows.Forms.Label();
            this.IRaw = new System.Windows.Forms.Label();
            this.gaugeFuel = new AGaugeApp.AGauge();
            this.bSetLoadedFuel = new MissionPlanner.Controls.MyButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gaugeFuel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bSetLoadedFuel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lConsumed, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lLoaded, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.IRaw, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(481, 571);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lConsumed
            // 
            this.lConsumed.AutoSize = true;
            this.lConsumed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lConsumed.Location = new System.Drawing.Point(243, 531);
            this.lConsumed.Name = "lConsumed";
            this.lConsumed.Size = new System.Drawing.Size(235, 40);
            this.lConsumed.TabIndex = 3;
            this.lConsumed.Text = "Consumed Fuel: 000 l";
            // 
            // lLoaded
            // 
            this.lLoaded.AutoSize = true;
            this.lLoaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lLoaded.Location = new System.Drawing.Point(3, 531);
            this.lLoaded.Name = "lLoaded";
            this.lLoaded.Size = new System.Drawing.Size(234, 40);
            this.lLoaded.TabIndex = 4;
            this.lLoaded.Text = "Loaded Fuel : 29 liters";
            // 
            // IRaw
            // 
            this.IRaw.AutoSize = true;
            this.IRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IRaw.Location = new System.Drawing.Point(243, 501);
            this.IRaw.Name = "IRaw";
            this.IRaw.Size = new System.Drawing.Size(235, 30);
            this.IRaw.TabIndex = 5;
            this.IRaw.Text = "Raw Level : 000";
            // 
            // gaugeFuel
            // 
            this.gaugeFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gaugeFuel.BackColor = System.Drawing.Color.Transparent;
            this.gaugeFuel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeFuel.BackgroundImage")));
            this.gaugeFuel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeFuel.BaseArcColor = System.Drawing.Color.White;
            this.gaugeFuel.BaseArcRadius = 60;
            this.gaugeFuel.BaseArcStart = 135;
            this.gaugeFuel.BaseArcSweep = 270;
            this.gaugeFuel.BaseArcWidth = 2;
            this.gaugeFuel.Cap_Idx = ((byte)(0));
            this.gaugeFuel.CapColor = System.Drawing.Color.White;
            this.gaugeFuel.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeFuel.CapPosition = new System.Drawing.Point(60, 115);
            this.gaugeFuel.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(60, 115),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeFuel.CapsText = new string[] {
        "00.00L",
        "Remaining fuel",
        "",
        "",
        ""};
            this.gaugeFuel.CapText = "00.00L";
            this.gaugeFuel.Center = new System.Drawing.Point(75, 75);
            this.tableLayoutPanel1.SetColumnSpan(this.gaugeFuel, 2);
            this.gaugeFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeFuel.Location = new System.Drawing.Point(0, 0);
            this.gaugeFuel.Margin = new System.Windows.Forms.Padding(0);
            this.gaugeFuel.MaxValue = 30F;
            this.gaugeFuel.MinValue = 0F;
            this.gaugeFuel.Name = "gaugeFuel";
            this.gaugeFuel.Need_Idx = ((byte)(3));
            this.gaugeFuel.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeFuel.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeFuel.NeedleEnabled = false;
            this.gaugeFuel.NeedleRadius = 80;
            this.gaugeFuel.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeFuel.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeFuel.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeFuel.NeedlesRadius = new int[] {
        50,
        80,
        80,
        80};
            this.gaugeFuel.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeFuel.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeFuel.NeedleType = 0;
            this.gaugeFuel.NeedleWidth = 2;
            this.gaugeFuel.Range_Idx = ((byte)(2));
            this.gaugeFuel.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gaugeFuel.RangeEnabled = true;
            this.gaugeFuel.RangeEndValue = 30F;
            this.gaugeFuel.RangeInnerRadius = 50;
            this.gaugeFuel.RangeOuterRadius = 60;
            this.gaugeFuel.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Crimson,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0))))),
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gaugeFuel.RangesEnabled = new bool[] {
        true,
        true,
        true,
        false,
        false};
            this.gaugeFuel.RangesEndValue = new float[] {
        5F,
        8F,
        30F,
        0F,
        0F};
            this.gaugeFuel.RangesInnerRadius = new int[] {
        50,
        50,
        50,
        70,
        70};
            this.gaugeFuel.RangesOuterRadius = new int[] {
        60,
        60,
        60,
        80,
        80};
            this.gaugeFuel.RangesStartValue = new float[] {
        0F,
        5F,
        8F,
        0F,
        0F};
            this.gaugeFuel.RangeStartValue = 8F;
            this.gaugeFuel.ScaleLinesInterColor = System.Drawing.Color.Transparent;
            this.gaugeFuel.ScaleLinesInterInnerRadius = 53;
            this.gaugeFuel.ScaleLinesInterOuterRadius = 60;
            this.gaugeFuel.ScaleLinesInterWidth = 1;
            this.gaugeFuel.ScaleLinesMajorColor = System.Drawing.Color.Transparent;
            this.gaugeFuel.ScaleLinesMajorInnerRadius = 50;
            this.gaugeFuel.ScaleLinesMajorOuterRadius = 60;
            this.gaugeFuel.ScaleLinesMajorStepValue = 5F;
            this.gaugeFuel.ScaleLinesMajorWidth = 2;
            this.gaugeFuel.ScaleLinesMinorColor = System.Drawing.Color.Transparent;
            this.gaugeFuel.ScaleLinesMinorInnerRadius = 55;
            this.gaugeFuel.ScaleLinesMinorNumOf = 9;
            this.gaugeFuel.ScaleLinesMinorOuterRadius = 60;
            this.gaugeFuel.ScaleLinesMinorWidth = 1;
            this.gaugeFuel.ScaleNumbersColor = System.Drawing.Color.White;
            this.gaugeFuel.ScaleNumbersFormat = null;
            this.gaugeFuel.ScaleNumbersRadius = 40;
            this.gaugeFuel.ScaleNumbersRotation = 0;
            this.gaugeFuel.ScaleNumbersStartScaleLine = 0;
            this.gaugeFuel.ScaleNumbersStepScaleLines = 1;
            this.gaugeFuel.Size = new System.Drawing.Size(489, 489);
            this.gaugeFuel.TabIndex = 0;
            this.gaugeFuel.Value = 0F;
            this.gaugeFuel.Value0 = 0F;
            this.gaugeFuel.Value1 = 0F;
            this.gaugeFuel.Value2 = 0F;
            this.gaugeFuel.Value3 = 0F;
            // 
            // bSetLoadedFuel
            // 
            this.bSetLoadedFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSetLoadedFuel.Location = new System.Drawing.Point(0, 501);
            this.bSetLoadedFuel.Margin = new System.Windows.Forms.Padding(0);
            this.bSetLoadedFuel.Name = "bSetLoadedFuel";
            this.bSetLoadedFuel.Size = new System.Drawing.Size(240, 30);
            this.bSetLoadedFuel.TabIndex = 2;
            this.bSetLoadedFuel.Text = "SET Loaded Fuel";
            this.bSetLoadedFuel.UseVisualStyleBackColor = true;
            this.bSetLoadedFuel.Click += new System.EventHandler(this.bSetLoadedFuel_Click);
            // 
            // fuelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fuelControl";
            this.Size = new System.Drawing.Size(481, 571);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AGaugeApp.AGauge gaugeFuel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MissionPlanner.Controls.MyButton bSetLoadedFuel;
        private System.Windows.Forms.Label lConsumed;
        private System.Windows.Forms.Label lLoaded;
        private System.Windows.Forms.Label IRaw;
    }
}
