
namespace ptPlugin1
{
    partial class batterypanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(batterypanel));
            this.gaugeGCS = new AGaugeApp.AGauge();
            this.gaugeMain = new AGaugeApp.AGauge();
            this.gaugePayload = new AGaugeApp.AGauge();
            this.gaugeServo2 = new AGaugeApp.AGauge();
            this.gaugeServo1 = new AGaugeApp.AGauge();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gaugeGCS
            // 
            this.gaugeGCS.BackColor = System.Drawing.Color.Transparent;
            this.gaugeGCS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeGCS.BackgroundImage")));
            this.gaugeGCS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeGCS.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeGCS.BaseArcRadius = 50;
            this.gaugeGCS.BaseArcStart = 175;
            this.gaugeGCS.BaseArcSweep = 270;
            this.gaugeGCS.BaseArcWidth = 2;
            this.gaugeGCS.Cap_Idx = ((byte)(0));
            this.gaugeGCS.CapColor = System.Drawing.Color.White;
            this.gaugeGCS.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeGCS.CapPosition = new System.Drawing.Point(25, 100);
            this.gaugeGCS.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(25, 100),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeGCS.CapsText = new string[] {
        "100%",
        "GCS Batt",
        "",
        "",
        ""};
            this.gaugeGCS.CapText = "100%";
            this.gaugeGCS.Center = new System.Drawing.Point(75, 75);
            this.gaugeGCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeGCS.Location = new System.Drawing.Point(595, 3);
            this.gaugeGCS.MaxValue = 100F;
            this.gaugeGCS.MinValue = 0F;
            this.gaugeGCS.Name = "gaugeGCS";
            this.gaugeGCS.Need_Idx = ((byte)(3));
            this.gaugeGCS.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeGCS.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeGCS.NeedleEnabled = false;
            this.gaugeGCS.NeedleRadius = 80;
            this.gaugeGCS.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeGCS.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeGCS.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeGCS.NeedlesRadius = new int[] {
        60,
        80,
        80,
        80};
            this.gaugeGCS.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeGCS.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeGCS.NeedleType = 0;
            this.gaugeGCS.NeedleWidth = 2;
            this.gaugeGCS.Range_Idx = ((byte)(4));
            this.gaugeGCS.RangeColor = System.Drawing.Color.Red;
            this.gaugeGCS.RangeEnabled = false;
            this.gaugeGCS.RangeEndValue = 20F;
            this.gaugeGCS.RangeInnerRadius = 50;
            this.gaugeGCS.RangeOuterRadius = 60;
            this.gaugeGCS.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Red};
            this.gaugeGCS.RangesEnabled = new bool[] {
        true,
        true,
        false,
        true,
        false};
            this.gaugeGCS.RangesEndValue = new float[] {
        20F,
        25F,
        0F,
        100F,
        20F};
            this.gaugeGCS.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        45,
        50};
            this.gaugeGCS.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        60,
        60};
            this.gaugeGCS.RangesStartValue = new float[] {
        0F,
        20F,
        0F,
        25F,
        16.8F};
            this.gaugeGCS.RangeStartValue = 16.8F;
            this.gaugeGCS.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeGCS.ScaleLinesInterInnerRadius = 50;
            this.gaugeGCS.ScaleLinesInterOuterRadius = 60;
            this.gaugeGCS.ScaleLinesInterWidth = 1;
            this.gaugeGCS.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeGCS.ScaleLinesMajorInnerRadius = 50;
            this.gaugeGCS.ScaleLinesMajorOuterRadius = 60;
            this.gaugeGCS.ScaleLinesMajorStepValue = 100F;
            this.gaugeGCS.ScaleLinesMajorWidth = 2;
            this.gaugeGCS.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gaugeGCS.ScaleLinesMinorInnerRadius = 55;
            this.gaugeGCS.ScaleLinesMinorNumOf = 0;
            this.gaugeGCS.ScaleLinesMinorOuterRadius = 60;
            this.gaugeGCS.ScaleLinesMinorWidth = 1;
            this.gaugeGCS.ScaleNumbersColor = System.Drawing.Color.Transparent;
            this.gaugeGCS.ScaleNumbersFormat = null;
            this.gaugeGCS.ScaleNumbersRadius = 40;
            this.gaugeGCS.ScaleNumbersRotation = 0;
            this.gaugeGCS.ScaleNumbersStartScaleLine = 0;
            this.gaugeGCS.ScaleNumbersStepScaleLines = 1;
            this.gaugeGCS.Size = new System.Drawing.Size(142, 142);
            this.gaugeGCS.TabIndex = 4;
            this.gaugeGCS.Value = 5F;
            this.gaugeGCS.Value0 = 0F;
            this.gaugeGCS.Value1 = 0F;
            this.gaugeGCS.Value2 = 0F;
            this.gaugeGCS.Value3 = 5F;
            // 
            // gaugeMain
            // 
            this.gaugeMain.BackColor = System.Drawing.Color.Transparent;
            this.gaugeMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeMain.BackgroundImage")));
            this.gaugeMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeMain.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeMain.BaseArcRadius = 50;
            this.gaugeMain.BaseArcStart = 175;
            this.gaugeMain.BaseArcSweep = 270;
            this.gaugeMain.BaseArcWidth = 2;
            this.gaugeMain.Cap_Idx = ((byte)(0));
            this.gaugeMain.CapColor = System.Drawing.Color.White;
            this.gaugeMain.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeMain.CapPosition = new System.Drawing.Point(30, 100);
            this.gaugeMain.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(30, 100),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeMain.CapsText = new string[] {
        "7.6V",
        "Main",
        "",
        "",
        ""};
            this.gaugeMain.CapText = "7.6V";
            this.gaugeMain.Center = new System.Drawing.Point(75, 75);
            this.gaugeMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeMain.Location = new System.Drawing.Point(447, 3);
            this.gaugeMain.MaxValue = 20F;
            this.gaugeMain.MinValue = 10F;
            this.gaugeMain.Name = "gaugeMain";
            this.gaugeMain.Need_Idx = ((byte)(3));
            this.gaugeMain.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeMain.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeMain.NeedleEnabled = false;
            this.gaugeMain.NeedleRadius = 80;
            this.gaugeMain.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeMain.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeMain.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeMain.NeedlesRadius = new int[] {
        60,
        80,
        80,
        80};
            this.gaugeMain.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeMain.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeMain.NeedleType = 0;
            this.gaugeMain.NeedleWidth = 2;
            this.gaugeMain.Range_Idx = ((byte)(4));
            this.gaugeMain.RangeColor = System.Drawing.Color.Red;
            this.gaugeMain.RangeEnabled = true;
            this.gaugeMain.RangeEndValue = 20F;
            this.gaugeMain.RangeInnerRadius = 50;
            this.gaugeMain.RangeOuterRadius = 60;
            this.gaugeMain.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Red};
            this.gaugeMain.RangesEnabled = new bool[] {
        true,
        true,
        false,
        true,
        true};
            this.gaugeMain.RangesEndValue = new float[] {
        12F,
        13.6F,
        0F,
        16.8F,
        20F};
            this.gaugeMain.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        45,
        50};
            this.gaugeMain.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        60,
        60};
            this.gaugeMain.RangesStartValue = new float[] {
        10F,
        12F,
        0F,
        13.6F,
        16.8F};
            this.gaugeMain.RangeStartValue = 16.8F;
            this.gaugeMain.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeMain.ScaleLinesInterInnerRadius = 50;
            this.gaugeMain.ScaleLinesInterOuterRadius = 60;
            this.gaugeMain.ScaleLinesInterWidth = 1;
            this.gaugeMain.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeMain.ScaleLinesMajorInnerRadius = 50;
            this.gaugeMain.ScaleLinesMajorOuterRadius = 60;
            this.gaugeMain.ScaleLinesMajorStepValue = 20F;
            this.gaugeMain.ScaleLinesMajorWidth = 2;
            this.gaugeMain.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gaugeMain.ScaleLinesMinorInnerRadius = 55;
            this.gaugeMain.ScaleLinesMinorNumOf = 0;
            this.gaugeMain.ScaleLinesMinorOuterRadius = 60;
            this.gaugeMain.ScaleLinesMinorWidth = 1;
            this.gaugeMain.ScaleNumbersColor = System.Drawing.Color.Transparent;
            this.gaugeMain.ScaleNumbersFormat = null;
            this.gaugeMain.ScaleNumbersRadius = 40;
            this.gaugeMain.ScaleNumbersRotation = 0;
            this.gaugeMain.ScaleNumbersStartScaleLine = 0;
            this.gaugeMain.ScaleNumbersStepScaleLines = 1;
            this.gaugeMain.Size = new System.Drawing.Size(142, 142);
            this.gaugeMain.TabIndex = 3;
            this.gaugeMain.Value = 5F;
            this.gaugeMain.Value0 = 0F;
            this.gaugeMain.Value1 = 0F;
            this.gaugeMain.Value2 = 0F;
            this.gaugeMain.Value3 = 5F;
            // 
            // gaugePayload
            // 
            this.gaugePayload.BackColor = System.Drawing.Color.Transparent;
            this.gaugePayload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugePayload.BackgroundImage")));
            this.gaugePayload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugePayload.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugePayload.BaseArcRadius = 50;
            this.gaugePayload.BaseArcStart = 175;
            this.gaugePayload.BaseArcSweep = 270;
            this.gaugePayload.BaseArcWidth = 2;
            this.gaugePayload.Cap_Idx = ((byte)(0));
            this.gaugePayload.CapColor = System.Drawing.Color.White;
            this.gaugePayload.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugePayload.CapPosition = new System.Drawing.Point(30, 100);
            this.gaugePayload.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(30, 100),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugePayload.CapsText = new string[] {
        "7.6V",
        "Payload",
        "",
        "",
        ""};
            this.gaugePayload.CapText = "7.6V";
            this.gaugePayload.Center = new System.Drawing.Point(75, 75);
            this.gaugePayload.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugePayload.Location = new System.Drawing.Point(299, 3);
            this.gaugePayload.MaxValue = 15F;
            this.gaugePayload.MinValue = 7.5F;
            this.gaugePayload.Name = "gaugePayload";
            this.gaugePayload.Need_Idx = ((byte)(3));
            this.gaugePayload.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugePayload.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugePayload.NeedleEnabled = false;
            this.gaugePayload.NeedleRadius = 80;
            this.gaugePayload.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugePayload.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugePayload.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugePayload.NeedlesRadius = new int[] {
        60,
        80,
        80,
        80};
            this.gaugePayload.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugePayload.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugePayload.NeedleType = 0;
            this.gaugePayload.NeedleWidth = 2;
            this.gaugePayload.Range_Idx = ((byte)(4));
            this.gaugePayload.RangeColor = System.Drawing.Color.Red;
            this.gaugePayload.RangeEnabled = true;
            this.gaugePayload.RangeEndValue = 15F;
            this.gaugePayload.RangeInnerRadius = 50;
            this.gaugePayload.RangeOuterRadius = 60;
            this.gaugePayload.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Red};
            this.gaugePayload.RangesEnabled = new bool[] {
        true,
        true,
        false,
        true,
        true};
            this.gaugePayload.RangesEndValue = new float[] {
        9F,
        10.2F,
        0F,
        12.6F,
        15F};
            this.gaugePayload.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        45,
        50};
            this.gaugePayload.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        60,
        60};
            this.gaugePayload.RangesStartValue = new float[] {
        7.5F,
        9F,
        0F,
        10.2F,
        12.6F};
            this.gaugePayload.RangeStartValue = 12.6F;
            this.gaugePayload.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugePayload.ScaleLinesInterInnerRadius = 50;
            this.gaugePayload.ScaleLinesInterOuterRadius = 60;
            this.gaugePayload.ScaleLinesInterWidth = 1;
            this.gaugePayload.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugePayload.ScaleLinesMajorInnerRadius = 50;
            this.gaugePayload.ScaleLinesMajorOuterRadius = 60;
            this.gaugePayload.ScaleLinesMajorStepValue = 15F;
            this.gaugePayload.ScaleLinesMajorWidth = 2;
            this.gaugePayload.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gaugePayload.ScaleLinesMinorInnerRadius = 55;
            this.gaugePayload.ScaleLinesMinorNumOf = 0;
            this.gaugePayload.ScaleLinesMinorOuterRadius = 60;
            this.gaugePayload.ScaleLinesMinorWidth = 1;
            this.gaugePayload.ScaleNumbersColor = System.Drawing.Color.Transparent;
            this.gaugePayload.ScaleNumbersFormat = null;
            this.gaugePayload.ScaleNumbersRadius = 40;
            this.gaugePayload.ScaleNumbersRotation = 0;
            this.gaugePayload.ScaleNumbersStartScaleLine = 0;
            this.gaugePayload.ScaleNumbersStepScaleLines = 1;
            this.gaugePayload.Size = new System.Drawing.Size(142, 142);
            this.gaugePayload.TabIndex = 2;
            this.gaugePayload.Value = 5F;
            this.gaugePayload.Value0 = 0F;
            this.gaugePayload.Value1 = 0F;
            this.gaugePayload.Value2 = 0F;
            this.gaugePayload.Value3 = 5F;
            // 
            // gaugeServo2
            // 
            this.gaugeServo2.BackColor = System.Drawing.Color.Transparent;
            this.gaugeServo2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeServo2.BackgroundImage")));
            this.gaugeServo2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeServo2.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeServo2.BaseArcRadius = 50;
            this.gaugeServo2.BaseArcStart = 175;
            this.gaugeServo2.BaseArcSweep = 270;
            this.gaugeServo2.BaseArcWidth = 2;
            this.gaugeServo2.Cap_Idx = ((byte)(0));
            this.gaugeServo2.CapColor = System.Drawing.Color.White;
            this.gaugeServo2.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeServo2.CapPosition = new System.Drawing.Point(30, 100);
            this.gaugeServo2.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(30, 100),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeServo2.CapsText = new string[] {
        "7.6V",
        "Servo2",
        "",
        "",
        ""};
            this.gaugeServo2.CapText = "7.6V";
            this.gaugeServo2.Center = new System.Drawing.Point(75, 75);
            this.gaugeServo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeServo2.Location = new System.Drawing.Point(151, 3);
            this.gaugeServo2.MaxValue = 10F;
            this.gaugeServo2.MinValue = 5F;
            this.gaugeServo2.Name = "gaugeServo2";
            this.gaugeServo2.Need_Idx = ((byte)(3));
            this.gaugeServo2.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeServo2.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeServo2.NeedleEnabled = false;
            this.gaugeServo2.NeedleRadius = 80;
            this.gaugeServo2.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeServo2.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeServo2.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeServo2.NeedlesRadius = new int[] {
        60,
        80,
        80,
        80};
            this.gaugeServo2.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeServo2.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeServo2.NeedleType = 0;
            this.gaugeServo2.NeedleWidth = 2;
            this.gaugeServo2.Range_Idx = ((byte)(0));
            this.gaugeServo2.RangeColor = System.Drawing.Color.Red;
            this.gaugeServo2.RangeEnabled = true;
            this.gaugeServo2.RangeEndValue = 6F;
            this.gaugeServo2.RangeInnerRadius = 50;
            this.gaugeServo2.RangeOuterRadius = 60;
            this.gaugeServo2.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Red};
            this.gaugeServo2.RangesEnabled = new bool[] {
        true,
        true,
        false,
        true,
        true};
            this.gaugeServo2.RangesEndValue = new float[] {
        6F,
        6.8F,
        0F,
        8.4F,
        10F};
            this.gaugeServo2.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        45,
        50};
            this.gaugeServo2.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        60,
        60};
            this.gaugeServo2.RangesStartValue = new float[] {
        5F,
        6F,
        0F,
        6.8F,
        8.4F};
            this.gaugeServo2.RangeStartValue = 5F;
            this.gaugeServo2.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeServo2.ScaleLinesInterInnerRadius = 50;
            this.gaugeServo2.ScaleLinesInterOuterRadius = 60;
            this.gaugeServo2.ScaleLinesInterWidth = 1;
            this.gaugeServo2.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeServo2.ScaleLinesMajorInnerRadius = 50;
            this.gaugeServo2.ScaleLinesMajorOuterRadius = 60;
            this.gaugeServo2.ScaleLinesMajorStepValue = 5F;
            this.gaugeServo2.ScaleLinesMajorWidth = 2;
            this.gaugeServo2.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gaugeServo2.ScaleLinesMinorInnerRadius = 55;
            this.gaugeServo2.ScaleLinesMinorNumOf = 0;
            this.gaugeServo2.ScaleLinesMinorOuterRadius = 60;
            this.gaugeServo2.ScaleLinesMinorWidth = 1;
            this.gaugeServo2.ScaleNumbersColor = System.Drawing.Color.Transparent;
            this.gaugeServo2.ScaleNumbersFormat = null;
            this.gaugeServo2.ScaleNumbersRadius = 40;
            this.gaugeServo2.ScaleNumbersRotation = 0;
            this.gaugeServo2.ScaleNumbersStartScaleLine = 0;
            this.gaugeServo2.ScaleNumbersStepScaleLines = 1;
            this.gaugeServo2.Size = new System.Drawing.Size(142, 142);
            this.gaugeServo2.TabIndex = 1;
            this.gaugeServo2.Value = 5F;
            this.gaugeServo2.Value0 = 0F;
            this.gaugeServo2.Value1 = 0F;
            this.gaugeServo2.Value2 = 0F;
            this.gaugeServo2.Value3 = 5F;
            // 
            // gaugeServo1
            // 
            this.gaugeServo1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gaugeServo1.BackColor = System.Drawing.Color.Transparent;
            this.gaugeServo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gaugeServo1.BackgroundImage")));
            this.gaugeServo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaugeServo1.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeServo1.BaseArcRadius = 50;
            this.gaugeServo1.BaseArcStart = 175;
            this.gaugeServo1.BaseArcSweep = 270;
            this.gaugeServo1.BaseArcWidth = 2;
            this.gaugeServo1.Cap_Idx = ((byte)(0));
            this.gaugeServo1.CapColor = System.Drawing.Color.White;
            this.gaugeServo1.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.White,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.gaugeServo1.CapPosition = new System.Drawing.Point(30, 100);
            this.gaugeServo1.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(30, 100),
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10)};
            this.gaugeServo1.CapsText = new string[] {
        "7.6V",
        "Servo1",
        "",
        "",
        ""};
            this.gaugeServo1.CapText = "7.6V";
            this.gaugeServo1.Center = new System.Drawing.Point(75, 75);
            this.gaugeServo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeServo1.Location = new System.Drawing.Point(3, 3);
            this.gaugeServo1.MaxValue = 10F;
            this.gaugeServo1.MinValue = 5F;
            this.gaugeServo1.Name = "gaugeServo1";
            this.gaugeServo1.Need_Idx = ((byte)(3));
            this.gaugeServo1.NeedleColor1 = AGaugeApp.AGauge.NeedleColorEnum.Gray;
            this.gaugeServo1.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeServo1.NeedleEnabled = false;
            this.gaugeServo1.NeedleRadius = 80;
            this.gaugeServo1.NeedlesColor1 = new AGaugeApp.AGauge.NeedleColorEnum[] {
        AGaugeApp.AGauge.NeedleColorEnum.Green,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray,
        AGaugeApp.AGauge.NeedleColorEnum.Gray};
            this.gaugeServo1.NeedlesColor2 = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray,
        System.Drawing.Color.DimGray};
            this.gaugeServo1.NeedlesEnabled = new bool[] {
        true,
        false,
        false,
        false};
            this.gaugeServo1.NeedlesRadius = new int[] {
        60,
        80,
        80,
        80};
            this.gaugeServo1.NeedlesType = new int[] {
        0,
        0,
        0,
        0};
            this.gaugeServo1.NeedlesWidth = new int[] {
        2,
        2,
        2,
        2};
            this.gaugeServo1.NeedleType = 0;
            this.gaugeServo1.NeedleWidth = 2;
            this.gaugeServo1.Range_Idx = ((byte)(3));
            this.gaugeServo1.RangeColor = System.Drawing.Color.LimeGreen;
            this.gaugeServo1.RangeEnabled = true;
            this.gaugeServo1.RangeEndValue = 8.4F;
            this.gaugeServo1.RangeInnerRadius = 45;
            this.gaugeServo1.RangeOuterRadius = 60;
            this.gaugeServo1.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Orange,
        System.Drawing.SystemColors.Control,
        System.Drawing.Color.LimeGreen,
        System.Drawing.Color.Red};
            this.gaugeServo1.RangesEnabled = new bool[] {
        true,
        true,
        false,
        true,
        true};
            this.gaugeServo1.RangesEndValue = new float[] {
        6F,
        6.8F,
        0F,
        8.4F,
        10F};
            this.gaugeServo1.RangesInnerRadius = new int[] {
        50,
        50,
        70,
        45,
        50};
            this.gaugeServo1.RangesOuterRadius = new int[] {
        60,
        60,
        80,
        60,
        60};
            this.gaugeServo1.RangesStartValue = new float[] {
        5F,
        6F,
        0F,
        6.8F,
        8.4F};
            this.gaugeServo1.RangeStartValue = 6.8F;
            this.gaugeServo1.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeServo1.ScaleLinesInterInnerRadius = 50;
            this.gaugeServo1.ScaleLinesInterOuterRadius = 60;
            this.gaugeServo1.ScaleLinesInterWidth = 1;
            this.gaugeServo1.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeServo1.ScaleLinesMajorInnerRadius = 50;
            this.gaugeServo1.ScaleLinesMajorOuterRadius = 60;
            this.gaugeServo1.ScaleLinesMajorStepValue = 5F;
            this.gaugeServo1.ScaleLinesMajorWidth = 2;
            this.gaugeServo1.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.gaugeServo1.ScaleLinesMinorInnerRadius = 55;
            this.gaugeServo1.ScaleLinesMinorNumOf = 0;
            this.gaugeServo1.ScaleLinesMinorOuterRadius = 60;
            this.gaugeServo1.ScaleLinesMinorWidth = 1;
            this.gaugeServo1.ScaleNumbersColor = System.Drawing.Color.Transparent;
            this.gaugeServo1.ScaleNumbersFormat = null;
            this.gaugeServo1.ScaleNumbersRadius = 40;
            this.gaugeServo1.ScaleNumbersRotation = 0;
            this.gaugeServo1.ScaleNumbersStartScaleLine = 0;
            this.gaugeServo1.ScaleNumbersStepScaleLines = 1;
            this.gaugeServo1.Size = new System.Drawing.Size(142, 142);
            this.gaugeServo1.TabIndex = 0;
            this.gaugeServo1.Value = 0F;
            this.gaugeServo1.Value0 = 5F;
            this.gaugeServo1.Value1 = 0F;
            this.gaugeServo1.Value2 = 0F;
            this.gaugeServo1.Value3 = 0F;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.gaugeServo1);
            this.flowLayoutPanel1.Controls.Add(this.gaugeServo2);
            this.flowLayoutPanel1.Controls.Add(this.gaugePayload);
            this.flowLayoutPanel1.Controls.Add(this.gaugeMain);
            this.flowLayoutPanel1.Controls.Add(this.gaugeGCS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1097, 471);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // batterypanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "batterypanel";
            this.Size = new System.Drawing.Size(1097, 471);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AGaugeApp.AGauge gaugeGCS;
        private AGaugeApp.AGauge gaugeMain;
        private AGaugeApp.AGauge gaugePayload;
        private AGaugeApp.AGauge gaugeServo2;
        private AGaugeApp.AGauge gaugeServo1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
