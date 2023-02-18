
namespace ptPlugin1
{
    partial class pitotControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pitotControl));
            this.gaugePitotTemp = new AGaugeApp.AGauge();
            this.gaugeAmbientTemp = new AGaugeApp.AGauge();
            this.heatPower = new MissionPlanner.Controls.HorizontalProgressBar2();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.calAirspeed = new MissionPlanner.Controls.MyButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gaugePitotTemp
            // 
            this.gaugePitotTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gaugePitotTemp.BackColor = System.Drawing.Color.Transparent;
            this.gaugePitotTemp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugePitotTemp.BackgroundImage")));
            this.gaugePitotTemp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugePitotTemp.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugePitotTemp.BaseArcRadius = 60;
            this.gaugePitotTemp.BaseArcStart = 135;
            this.gaugePitotTemp.BaseArcSweep = 270;
            this.gaugePitotTemp.BaseArcWidth = 2;
            this.gaugePitotTemp.Cap_Idx = ((byte)(1));
            this.gaugePitotTemp.CapColor = System.Drawing.Color.White;
            this.gaugePitotTemp.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugePitotTemp.CapPosition = new System.Drawing.Point(43, 0);
            this.gaugePitotTemp.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(65, 120),
        new System.Drawing.Point(43, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugePitotTemp.CapsText = new string[] {
        "0 C°",
        "Pitot Temp",
        "",
        "",
        ""};
            this.gaugePitotTemp.CapText = "Pitot Temp";
            this.gaugePitotTemp.Center = new System.Drawing.Point(75, 75);
            this.gaugePitotTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugePitotTemp.Location = new System.Drawing.Point(3, 3);
            this.gaugePitotTemp.MaxValue = 200F;
            this.gaugePitotTemp.MinValue = -50F;
            this.gaugePitotTemp.Name = "gaugePitotTemp";
            this.gaugePitotTemp.Need_Idx = ((byte)(3));
            this.gaugePitotTemp.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugePitotTemp.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugePitotTemp.NeedleEnabled = false;
            this.gaugePitotTemp.NeedleRadius = 80;
            this.gaugePitotTemp.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Yellow,
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugePitotTemp.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugePitotTemp.NeedlesEnabled = new bool[] {
        true,
        true,
        false,
        false};
            this.gaugePitotTemp.NeedlesRadius = new int[] {
        50,
        50,
        80,
        80};
            this.gaugePitotTemp.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugePitotTemp.NeedlesWidth = new int[] {
        2,
        1,
        2,
        2};
            this.gaugePitotTemp.NeedleType = 0;
            this.gaugePitotTemp.NeedleWidth = 2;
            this.gaugePitotTemp.Range_Idx = ((byte)(0));
            this.gaugePitotTemp.RangeColor = System.Drawing.Color.Crimson;
            this.gaugePitotTemp.RangeEnabled = true;
            this.gaugePitotTemp.RangeEndValue = 0F;
            this.gaugePitotTemp.RangeInnerRadius = 50;
            this.gaugePitotTemp.RangeOuterRadius = 60;
            this.gaugePitotTemp.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Crimson,
        System.Drawing.Color.Green,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gaugePitotTemp.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.gaugePitotTemp.RangesEndValue = new float[] {
        0F,
        200F,
        0F,
        0F,
        0F};
            this.gaugePitotTemp.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        70,
        70};
            this.gaugePitotTemp.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        80,
        80};
            this.gaugePitotTemp.RangesStartValue = new float[] {
        -50F,
        0F,
        0F,
        0F,
        0F};
            this.gaugePitotTemp.RangeStartValue = -50F;
            this.gaugePitotTemp.ScaleLinesInterColor = System.Drawing.Color.Transparent;
            this.gaugePitotTemp.ScaleLinesInterInnerRadius = 53;
            this.gaugePitotTemp.ScaleLinesInterOuterRadius = 60;
            this.gaugePitotTemp.ScaleLinesInterWidth = 1;
            this.gaugePitotTemp.ScaleLinesMajorColor = System.Drawing.Color.Transparent;
            this.gaugePitotTemp.ScaleLinesMajorInnerRadius = 50;
            this.gaugePitotTemp.ScaleLinesMajorOuterRadius = 60;
            this.gaugePitotTemp.ScaleLinesMajorStepValue = 50F;
            this.gaugePitotTemp.ScaleLinesMajorWidth = 2;
            this.gaugePitotTemp.ScaleLinesMinorColor = System.Drawing.Color.Transparent;
            this.gaugePitotTemp.ScaleLinesMinorInnerRadius = 55;
            this.gaugePitotTemp.ScaleLinesMinorNumOf = 9;
            this.gaugePitotTemp.ScaleLinesMinorOuterRadius = 60;
            this.gaugePitotTemp.ScaleLinesMinorWidth = 1;
            this.gaugePitotTemp.ScaleNumbersColor = System.Drawing.Color.White;
            this.gaugePitotTemp.ScaleNumbersFormat = null;
            this.gaugePitotTemp.ScaleNumbersRadius = 40;
            this.gaugePitotTemp.ScaleNumbersRotation = 0;
            this.gaugePitotTemp.ScaleNumbersStartScaleLine = 0;
            this.gaugePitotTemp.ScaleNumbersStepScaleLines = 1;
            this.gaugePitotTemp.Size = new System.Drawing.Size(179, 179);
            this.gaugePitotTemp.TabIndex = 0;
            this.gaugePitotTemp.Value = 0F;
            this.gaugePitotTemp.Value0 = 0F;
            this.gaugePitotTemp.Value1 = 0F;
            this.gaugePitotTemp.Value2 = 0F;
            this.gaugePitotTemp.Value3 = 0F;
            // 
            // gaugeAmbientTemp
            // 
            this.gaugeAmbientTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.gaugeAmbientTemp.BackColor = System.Drawing.Color.Transparent;
            this.gaugeAmbientTemp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeAmbientTemp.BackgroundImage")));
            this.gaugeAmbientTemp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeAmbientTemp.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeAmbientTemp.BaseArcRadius = 60;
            this.gaugeAmbientTemp.BaseArcStart = 135;
            this.gaugeAmbientTemp.BaseArcSweep = 270;
            this.gaugeAmbientTemp.BaseArcWidth = 2;
            this.gaugeAmbientTemp.Cap_Idx = ((byte)(1));
            this.gaugeAmbientTemp.CapColor = System.Drawing.Color.White;
            this.gaugeAmbientTemp.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeAmbientTemp.CapPosition = new System.Drawing.Point(30, 0);
            this.gaugeAmbientTemp.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(65, 120),
        new System.Drawing.Point(30, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeAmbientTemp.CapsText = new string[] {
        "0 C°",
        "Ambient Temp",
        "",
        "",
        ""};
            this.gaugeAmbientTemp.CapText = "Ambient Temp";
            this.gaugeAmbientTemp.Center = new System.Drawing.Point(75, 75);
            this.gaugeAmbientTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeAmbientTemp.Location = new System.Drawing.Point(175, 3);
            this.gaugeAmbientTemp.MaxValue = 50F;
            this.gaugeAmbientTemp.MinValue = -50F;
            this.gaugeAmbientTemp.Name = "gaugeAmbientTemp";
            this.gaugeAmbientTemp.Need_Idx = ((byte)(3));
            this.gaugeAmbientTemp.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeAmbientTemp.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeAmbientTemp.NeedleEnabled = false;
            this.gaugeAmbientTemp.NeedleRadius = 80;
            this.gaugeAmbientTemp.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Yellow,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeAmbientTemp.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeAmbientTemp.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeAmbientTemp.NeedlesRadius = new int[] {
        50,
        80,
        80,
        80};
            this.gaugeAmbientTemp.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeAmbientTemp.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeAmbientTemp.NeedleType = 0;
            this.gaugeAmbientTemp.NeedleWidth = 2;
            this.gaugeAmbientTemp.Range_Idx = ((byte)(1));
            this.gaugeAmbientTemp.RangeColor = System.Drawing.Color.Green;
            this.gaugeAmbientTemp.RangeEnabled = true;
            this.gaugeAmbientTemp.RangeEndValue = 50F;
            this.gaugeAmbientTemp.RangeInnerRadius = 50;
            this.gaugeAmbientTemp.RangeOuterRadius = 60;
            this.gaugeAmbientTemp.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Crimson,
        System.Drawing.Color.Green,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.gaugeAmbientTemp.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.gaugeAmbientTemp.RangesEndValue = new float[] {
        0F,
        50F,
        0F,
        0F,
        0F};
            this.gaugeAmbientTemp.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        70,
        70};
            this.gaugeAmbientTemp.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        80,
        80};
            this.gaugeAmbientTemp.RangesStartValue = new float[] {
        -50F,
        0F,
        0F,
        0F,
        0F};
            this.gaugeAmbientTemp.RangeStartValue = 0F;
            this.gaugeAmbientTemp.ScaleLinesInterColor = System.Drawing.Color.Transparent;
            this.gaugeAmbientTemp.ScaleLinesInterInnerRadius = 53;
            this.gaugeAmbientTemp.ScaleLinesInterOuterRadius = 60;
            this.gaugeAmbientTemp.ScaleLinesInterWidth = 1;
            this.gaugeAmbientTemp.ScaleLinesMajorColor = System.Drawing.Color.Transparent;
            this.gaugeAmbientTemp.ScaleLinesMajorInnerRadius = 50;
            this.gaugeAmbientTemp.ScaleLinesMajorOuterRadius = 60;
            this.gaugeAmbientTemp.ScaleLinesMajorStepValue = 10F;
            this.gaugeAmbientTemp.ScaleLinesMajorWidth = 2;
            this.gaugeAmbientTemp.ScaleLinesMinorColor = System.Drawing.Color.Transparent;
            this.gaugeAmbientTemp.ScaleLinesMinorInnerRadius = 55;
            this.gaugeAmbientTemp.ScaleLinesMinorNumOf = 9;
            this.gaugeAmbientTemp.ScaleLinesMinorOuterRadius = 60;
            this.gaugeAmbientTemp.ScaleLinesMinorWidth = 1;
            this.gaugeAmbientTemp.ScaleNumbersColor = System.Drawing.Color.White;
            this.gaugeAmbientTemp.ScaleNumbersFormat = null;
            this.gaugeAmbientTemp.ScaleNumbersRadius = 40;
            this.gaugeAmbientTemp.ScaleNumbersRotation = 0;
            this.gaugeAmbientTemp.ScaleNumbersStartScaleLine = 0;
            this.gaugeAmbientTemp.ScaleNumbersStepScaleLines = 2;
            this.gaugeAmbientTemp.Size = new System.Drawing.Size(179, 179);
            this.gaugeAmbientTemp.TabIndex = 1;
            this.gaugeAmbientTemp.Value = 0F;
            this.gaugeAmbientTemp.Value0 = 0F;
            this.gaugeAmbientTemp.Value1 = 0F;
            this.gaugeAmbientTemp.Value2 = 0F;
            this.gaugeAmbientTemp.Value3 = 0F;
            // 
            // heatPower
            // 
            this.heatPower.BackgroundColor = System.Drawing.Color.Gray;
            this.heatPower.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel1.SetColumnSpan(this.heatPower, 2);
            this.heatPower.DisplayScale = 1F;
            this.heatPower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.heatPower.DrawLabel = false;
            this.heatPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heatPower.Label = null;
            this.heatPower.Location = new System.Drawing.Point(3, 188);
            this.heatPower.Maximum = 255;
            this.heatPower.maxline = 0;
            this.heatPower.Minimum = 0;
            this.heatPower.minline = 0;
            this.heatPower.Name = "heatPower";
            this.heatPower.Size = new System.Drawing.Size(339, 49);
            this.heatPower.TabIndex = 2;
            this.heatPower.Text = "Heater Power";
            this.heatPower.Value = 0;
            this.heatPower.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gaugePitotTemp, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.heatPower, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gaugeAmbientTemp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.calAirspeed, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 371);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // calAirspeed
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.calAirspeed, 2);
            this.calAirspeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calAirspeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calAirspeed.Location = new System.Drawing.Point(3, 298);
            this.calAirspeed.Name = "calAirspeed";
            this.calAirspeed.Size = new System.Drawing.Size(339, 49);
            this.calAirspeed.TabIndex = 3;
            this.calAirspeed.Text = "Calibrate Airspeed Sensor";
            this.calAirspeed.UseVisualStyleBackColor = true;
            this.calAirspeed.Click += new System.EventHandler(this.calAirspeed_Click);
            // 
            // pitotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "pitotControl";
            this.Size = new System.Drawing.Size(345, 371);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AGaugeApp.AGauge gaugePitotTemp;
        private AGaugeApp.AGauge gaugeAmbientTemp;
        private MissionPlanner.Controls.HorizontalProgressBar2 heatPower;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MissionPlanner.Controls.MyButton calAirspeed;
    }
}
