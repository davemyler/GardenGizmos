namespace GardenGizmos.SLMM.Model
{
    public class Compass
    {
        public static string AntiClockwiseFrom(string orientation)
        {
            if ("North".Equals(orientation)) return "West";
            if ("West".Equals(orientation)) return "South";
            if ("South".Equals(orientation)) return "East";
            return "North";
        }

        public static string ClockwiseFrom(string orientation)
        {
            if ("North".Equals(orientation)) return "East";
            if ("East".Equals(orientation)) return "South";
            if ("South".Equals(orientation)) return "West";
            return "North";
        }
    }
}