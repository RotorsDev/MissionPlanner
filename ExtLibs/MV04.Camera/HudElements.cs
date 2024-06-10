namespace MV04.Camera
{
    public enum CrosshairsType
    {
        Plus,
        HorizontalDivisions
    }

    public class HudElements
    {
        public string Time { get; set; }
        public string AGL { get; set; }
        public string Velocity { get; set; }
        public string TGD { get; set; }
        public string Battery { get; set; }
        public string SignalStrengths { get; set; }
        public string FromOperator { get; set; }
        public string ToWaypoint { get; set; }
        public string Camera { get; set; }
        public string DroneGps { get; set; }
        public string TargetGps { get; set; }

        public CrosshairsType Crosshairs { get; set; }
        public int LineSpacing { get; set; }
        public int LineDistance { get; set; }
    }
}
