
namespace ptPlugin1
{
    partial class engineControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(engineControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pStart = new System.Windows.Forms.Panel();
            this.uEngineSTART = new MissionPlanner.Controls.utButton();
            this.lThrFuel = new System.Windows.Forms.Label();
            this.engineTempGauge = new AGaugeApp.AGauge();
            this.engineRpmGauge = new AGaugeApp.AGauge();
            this.bArm = new MissionPlanner.Controls.utButton();
            this.lEngineStatus = new System.Windows.Forms.Label();
            this.pEmergency = new System.Windows.Forms.Panel();
            this.bEngineEmergencyStop = new MissionPlanner.Controls.utButton();
            this.pStop = new System.Windows.Forms.Panel();
            this.uEngineSTOP = new MissionPlanner.Controls.utButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.pStart.SuspendLayout();
            this.pEmergency.SuspendLayout();
            this.pStop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pStart, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lThrFuel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.engineTempGauge, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.engineRpmGauge, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bArm, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lEngineStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pEmergency, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pStop, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.63158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 556);
            this.tableLayoutPanel1.TabIndex = 82;
            // 
            // pStart
            // 
            this.pStart.Controls.Add(this.uEngineSTART);
            this.pStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pStart.Location = new System.Drawing.Point(291, 292);
            this.pStart.Margin = new System.Windows.Forms.Padding(0);
            this.pStart.Name = "pStart";
            this.pStart.Padding = new System.Windows.Forms.Padding(10);
            this.pStart.Size = new System.Drawing.Size(291, 87);
            this.pStart.TabIndex = 86;
            // 
            // uEngineSTART
            // 
            this.uEngineSTART.BackColor = System.Drawing.Color.Lime;
            this.uEngineSTART.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uEngineSTART.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uEngineSTART.ForeColor = System.Drawing.Color.White;
            this.uEngineSTART.Location = new System.Drawing.Point(10, 10);
            this.uEngineSTART.Margin = new System.Windows.Forms.Padding(0);
            this.uEngineSTART.Name = "uEngineSTART";
            this.uEngineSTART.Padding = new System.Windows.Forms.Padding(10);
            this.uEngineSTART.Size = new System.Drawing.Size(271, 67);
            this.uEngineSTART.TabIndex = 83;
            this.uEngineSTART.Text = "ENGINE START";
            this.uEngineSTART.UseVisualStyleBackColor = false;
            this.uEngineSTART.Click += new System.EventHandler(this.uEngineSTART_Click);
            // 
            // lThrFuel
            // 
            this.lThrFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lThrFuel.AutoSize = true;
            this.lThrFuel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lThrFuel.ForeColor = System.Drawing.Color.White;
            this.lThrFuel.Location = new System.Drawing.Point(3, 466);
            this.lThrFuel.Name = "lThrFuel";
            this.lThrFuel.Size = new System.Drawing.Size(285, 90);
            this.lThrFuel.TabIndex = 87;
            this.lThrFuel.Text = "Throttle : 0%\r\nFuel Flow: 0.00 l/s\r\nFuel Used : 0.0 l";
            // 
            // engineTempGauge
            // 
            this.engineTempGauge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.engineTempGauge.BackColor = System.Drawing.Color.Transparent;
            this.engineTempGauge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("engineTempGauge.BackgroundImage")));
            this.engineTempGauge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.engineTempGauge.BaseArcColor = System.Drawing.Color.Transparent;
            this.engineTempGauge.BaseArcRadius = 70;
            this.engineTempGauge.BaseArcStart = 135;
            this.engineTempGauge.BaseArcSweep = 270;
            this.engineTempGauge.BaseArcWidth = 2;
            this.engineTempGauge.Cap_Idx = ((byte)(1));
            this.engineTempGauge.CapColor = System.Drawing.Color.White;
            this.engineTempGauge.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.engineTempGauge.CapPosition = new System.Drawing.Point(62, 110);
            this.engineTempGauge.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(62, 85),
        new System.Drawing.Point(62, 110),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.engineTempGauge.CapsText = new string[] {
        "EGT",
        "364 C°",
        "",
        "",
        ""};
            this.engineTempGauge.CapText = "364 C°";
            this.engineTempGauge.Center = new System.Drawing.Point(75, 75);
            this.engineTempGauge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engineTempGauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineTempGauge.Location = new System.Drawing.Point(291, 0);
            this.engineTempGauge.Margin = new System.Windows.Forms.Padding(0);
            this.engineTempGauge.MaxValue = 1000F;
            this.engineTempGauge.MinValue = 150F;
            this.engineTempGauge.Name = "engineTempGauge";
            this.engineTempGauge.Need_Idx = ((byte)(3));
            this.engineTempGauge.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.engineTempGauge.NeedleColor2 = System.Drawing.Color.Brown;
            this.engineTempGauge.NeedleEnabled = false;
            this.engineTempGauge.NeedleRadius = 70;
            this.engineTempGauge.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Blue,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.engineTempGauge.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Brown};
            this.engineTempGauge.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.engineTempGauge.NeedlesRadius = new int[] {
        60,
        50,
        70,
        70};
            this.engineTempGauge.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.engineTempGauge.NeedlesWidth = new int[] {
        2,
        1,
        2,
        2};
            this.engineTempGauge.NeedleType = 0;
            this.engineTempGauge.NeedleWidth = 2;
            this.engineTempGauge.Range_Idx = ((byte)(0));
            this.engineTempGauge.RangeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.engineTempGauge.RangeEnabled = true;
            this.engineTempGauge.RangeEndValue = 450F;
            this.engineTempGauge.RangeInnerRadius = 60;
            this.engineTempGauge.RangeOuterRadius = 70;
            this.engineTempGauge.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.Color.Lime,
        System.Drawing.Color.Orange,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.SystemColors.Control};
            this.engineTempGauge.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        false};
            this.engineTempGauge.RangesEndValue = new float[] {
        450F,
        800F,
        900F,
        1000F,
        0F};
            this.engineTempGauge.RangesInnerRadius = new int[] {
        60,
        60,
        60,
        60,
        70};
            this.engineTempGauge.RangesOuterRadius = new int[] {
        70,
        70,
        70,
        70,
        80};
            this.engineTempGauge.RangesStartValue = new float[] {
        150F,
        450F,
        800F,
        900F,
        0F};
            this.engineTempGauge.RangeStartValue = 150F;
            this.engineTempGauge.ScaleLinesInterColor = System.Drawing.Color.White;
            this.engineTempGauge.ScaleLinesInterInnerRadius = 52;
            this.engineTempGauge.ScaleLinesInterOuterRadius = 60;
            this.engineTempGauge.ScaleLinesInterWidth = 1;
            this.engineTempGauge.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.engineTempGauge.ScaleLinesMajorInnerRadius = 50;
            this.engineTempGauge.ScaleLinesMajorOuterRadius = 60;
            this.engineTempGauge.ScaleLinesMajorStepValue = 150F;
            this.engineTempGauge.ScaleLinesMajorWidth = 2;
            this.engineTempGauge.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.engineTempGauge.ScaleLinesMinorInnerRadius = 55;
            this.engineTempGauge.ScaleLinesMinorNumOf = 5;
            this.engineTempGauge.ScaleLinesMinorOuterRadius = 60;
            this.engineTempGauge.ScaleLinesMinorWidth = 1;
            this.engineTempGauge.ScaleNumbersColor = System.Drawing.Color.White;
            this.engineTempGauge.ScaleNumbersFormat = null;
            this.engineTempGauge.ScaleNumbersRadius = 42;
            this.engineTempGauge.ScaleNumbersRotation = 0;
            this.engineTempGauge.ScaleNumbersStartScaleLine = 1;
            this.engineTempGauge.ScaleNumbersStepScaleLines = 1;
            this.engineTempGauge.Size = new System.Drawing.Size(292, 292);
            this.engineTempGauge.TabIndex = 81;
            this.engineTempGauge.Value = 40F;
            this.engineTempGauge.Value0 = 150F;
            this.engineTempGauge.Value1 = 0F;
            this.engineTempGauge.Value2 = 0F;
            this.engineTempGauge.Value3 = 40F;
            // 
            // engineRpmGauge
            // 
            this.engineRpmGauge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.engineRpmGauge.BackColor = System.Drawing.Color.Transparent;
            this.engineRpmGauge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("engineRpmGauge.BackgroundImage")));
            this.engineRpmGauge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.engineRpmGauge.BaseArcColor = System.Drawing.Color.Transparent;
            this.engineRpmGauge.BaseArcRadius = 70;
            this.engineRpmGauge.BaseArcStart = 135;
            this.engineRpmGauge.BaseArcSweep = 270;
            this.engineRpmGauge.BaseArcWidth = 2;
            this.engineRpmGauge.Cap_Idx = ((byte)(1));
            this.engineRpmGauge.CapColor = System.Drawing.Color.White;
            this.engineRpmGauge.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.engineRpmGauge.CapPosition = new System.Drawing.Point(62, 110);
            this.engineRpmGauge.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(47, 85),
        new System.Drawing.Point(62, 110),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.engineRpmGauge.CapsText = new string[] {
        "RPM x1000",
        "28600",
        "",
        "",
        ""};
            this.engineRpmGauge.CapText = "28600";
            this.engineRpmGauge.Center = new System.Drawing.Point(75, 75);
            this.engineRpmGauge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engineRpmGauge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineRpmGauge.Location = new System.Drawing.Point(0, 0);
            this.engineRpmGauge.Margin = new System.Windows.Forms.Padding(0);
            this.engineRpmGauge.MaxValue = 135F;
            this.engineRpmGauge.MinValue = 0F;
            this.engineRpmGauge.Name = "engineRpmGauge";
            this.engineRpmGauge.Need_Idx = ((byte)(3));
            this.engineRpmGauge.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.engineRpmGauge.NeedleColor2 = System.Drawing.Color.Brown;
            this.engineRpmGauge.NeedleEnabled = false;
            this.engineRpmGauge.NeedleRadius = 70;
            this.engineRpmGauge.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Red,
        AGaugeApp.AGauge.NeedleColorEnum.Blue,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.engineRpmGauge.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Brown};
            this.engineRpmGauge.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.engineRpmGauge.NeedlesRadius = new int[] {
        60,
        50,
        70,
        70};
            this.engineRpmGauge.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.engineRpmGauge.NeedlesWidth = new int[] {
        2,
        1,
        2,
        2};
            this.engineRpmGauge.NeedleType = 0;
            this.engineRpmGauge.NeedleWidth = 2;
            this.engineRpmGauge.Range_Idx = ((byte)(2));
            this.engineRpmGauge.RangeColor = System.Drawing.Color.Orange;
            this.engineRpmGauge.RangeEnabled = true;
            this.engineRpmGauge.RangeEndValue = 120F;
            this.engineRpmGauge.RangeInnerRadius = 60;
            this.engineRpmGauge.RangeOuterRadius = 70;
            this.engineRpmGauge.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.Color.Lime,
        System.Drawing.Color.Orange,
        System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.SystemColors.Control};
            this.engineRpmGauge.RangesEnabled = new bool[] {
        true,
        true,
        true,
        true,
        false};
            this.engineRpmGauge.RangesEndValue = new float[] {
        20F,
        90F,
        120F,
        135F,
        0F};
            this.engineRpmGauge.RangesInnerRadius = new int[] {
        60,
        60,
        60,
        60,
        70};
            this.engineRpmGauge.RangesOuterRadius = new int[] {
        70,
        70,
        70,
        70,
        80};
            this.engineRpmGauge.RangesStartValue = new float[] {
        0F,
        20F,
        90F,
        120F,
        0F};
            this.engineRpmGauge.RangeStartValue = 90F;
            this.engineRpmGauge.ScaleLinesInterColor = System.Drawing.Color.White;
            this.engineRpmGauge.ScaleLinesInterInnerRadius = 52;
            this.engineRpmGauge.ScaleLinesInterOuterRadius = 60;
            this.engineRpmGauge.ScaleLinesInterWidth = 1;
            this.engineRpmGauge.ScaleLinesMajorColor = System.Drawing.Color.White;
            this.engineRpmGauge.ScaleLinesMajorInnerRadius = 50;
            this.engineRpmGauge.ScaleLinesMajorOuterRadius = 60;
            this.engineRpmGauge.ScaleLinesMajorStepValue = 15F;
            this.engineRpmGauge.ScaleLinesMajorWidth = 2;
            this.engineRpmGauge.ScaleLinesMinorColor = System.Drawing.Color.White;
            this.engineRpmGauge.ScaleLinesMinorInnerRadius = 55;
            this.engineRpmGauge.ScaleLinesMinorNumOf = 9;
            this.engineRpmGauge.ScaleLinesMinorOuterRadius = 60;
            this.engineRpmGauge.ScaleLinesMinorWidth = 1;
            this.engineRpmGauge.ScaleNumbersColor = System.Drawing.Color.White;
            this.engineRpmGauge.ScaleNumbersFormat = null;
            this.engineRpmGauge.ScaleNumbersRadius = 42;
            this.engineRpmGauge.ScaleNumbersRotation = 0;
            this.engineRpmGauge.ScaleNumbersStartScaleLine = 1;
            this.engineRpmGauge.ScaleNumbersStepScaleLines = 1;
            this.engineRpmGauge.Size = new System.Drawing.Size(292, 292);
            this.engineRpmGauge.TabIndex = 80;
            this.engineRpmGauge.Value = 40F;
            this.engineRpmGauge.Value0 = 0F;
            this.engineRpmGauge.Value1 = 0F;
            this.engineRpmGauge.Value2 = 0F;
            this.engineRpmGauge.Value3 = 40F;
            // 
            // bArm
            // 
            this.bArm.BackColor = System.Drawing.Color.Lime;
            this.bArm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bArm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bArm.ForeColor = System.Drawing.Color.White;
            this.bArm.Location = new System.Drawing.Point(10, 302);
            this.bArm.Margin = new System.Windows.Forms.Padding(10);
            this.bArm.Name = "bArm";
            this.bArm.Size = new System.Drawing.Size(271, 67);
            this.bArm.TabIndex = 82;
            this.bArm.Text = "ARM/DISARM";
            this.bArm.UseVisualStyleBackColor = false;
            this.bArm.Click += new System.EventHandler(this.bArm_Click);
            // 
            // lEngineStatus
            // 
            this.lEngineStatus.AutoSize = true;
            this.lEngineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lEngineStatus.ForeColor = System.Drawing.Color.White;
            this.lEngineStatus.Location = new System.Drawing.Point(3, 379);
            this.lEngineStatus.Name = "lEngineStatus";
            this.lEngineStatus.Size = new System.Drawing.Size(139, 48);
            this.lEngineStatus.TabIndex = 86;
            this.lEngineStatus.Text = "Engine Status\r\nEngine Error";
            // 
            // pEmergency
            // 
            this.pEmergency.Controls.Add(this.bEngineEmergencyStop);
            this.pEmergency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pEmergency.Location = new System.Drawing.Point(291, 466);
            this.pEmergency.Margin = new System.Windows.Forms.Padding(0);
            this.pEmergency.Name = "pEmergency";
            this.pEmergency.Padding = new System.Windows.Forms.Padding(10);
            this.pEmergency.Size = new System.Drawing.Size(291, 90);
            this.pEmergency.TabIndex = 88;
            // 
            // bEngineEmergencyStop
            // 
            this.bEngineEmergencyStop.BackColor = System.Drawing.Color.Red;
            this.bEngineEmergencyStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bEngineEmergencyStop.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.bEngineEmergencyStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEngineEmergencyStop.ForeColor = System.Drawing.Color.White;
            this.bEngineEmergencyStop.Location = new System.Drawing.Point(10, 10);
            this.bEngineEmergencyStop.Margin = new System.Windows.Forms.Padding(0);
            this.bEngineEmergencyStop.Name = "bEngineEmergencyStop";
            this.bEngineEmergencyStop.Size = new System.Drawing.Size(271, 70);
            this.bEngineEmergencyStop.TabIndex = 84;
            this.bEngineEmergencyStop.Text = "EMERGENCY STOP";
            this.bEngineEmergencyStop.UseVisualStyleBackColor = false;
            this.bEngineEmergencyStop.Click += new System.EventHandler(this.bEngineEmergencyStop_Click);
            // 
            // pStop
            // 
            this.pStop.Controls.Add(this.uEngineSTOP);
            this.pStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pStop.Location = new System.Drawing.Point(291, 379);
            this.pStop.Margin = new System.Windows.Forms.Padding(0);
            this.pStop.Name = "pStop";
            this.pStop.Padding = new System.Windows.Forms.Padding(10);
            this.pStop.Size = new System.Drawing.Size(291, 87);
            this.pStop.TabIndex = 89;
            // 
            // uEngineSTOP
            // 
            this.uEngineSTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.uEngineSTOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uEngineSTOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uEngineSTOP.ForeColor = System.Drawing.Color.White;
            this.uEngineSTOP.Location = new System.Drawing.Point(10, 10);
            this.uEngineSTOP.Margin = new System.Windows.Forms.Padding(10);
            this.uEngineSTOP.Name = "uEngineSTOP";
            this.uEngineSTOP.Size = new System.Drawing.Size(271, 67);
            this.uEngineSTOP.TabIndex = 85;
            this.uEngineSTOP.Text = "ENGINE STOP/COOLING";
            this.uEngineSTOP.UseVisualStyleBackColor = false;
            this.uEngineSTOP.Click += new System.EventHandler(this.uEngineSTOP_Click);
            // 
            // engineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "engineControl";
            this.Size = new System.Drawing.Size(582, 559);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pStart.ResumeLayout(false);
            this.pEmergency.ResumeLayout(false);
            this.pStop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AGaugeApp.AGauge engineRpmGauge;
        private AGaugeApp.AGauge engineTempGauge;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MissionPlanner.Controls.utButton bArm;
        private MissionPlanner.Controls.utButton uEngineSTOP;
        private MissionPlanner.Controls.utButton bEngineEmergencyStop;
        private MissionPlanner.Controls.utButton uEngineSTART;
        private System.Windows.Forms.Label lThrFuel;
        private System.Windows.Forms.Label lEngineStatus;
        private System.Windows.Forms.Panel pStart;
        private System.Windows.Forms.Panel pEmergency;
        private System.Windows.Forms.Panel pStop;
    }
}
