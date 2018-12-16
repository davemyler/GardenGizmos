using System;

namespace GardenGizmos.SLMM.Model
{
    public class Position
    {
        public int Length { get; set; }
        public int Width { get; set; }

        public Position NextPositionInDirection(string orientation)
        {
            var length = Length;
            var width = Width;

            if ("North".Equals(orientation)) length++;
            if ("South".Equals(orientation)) length--;
            if ("East".Equals(orientation)) width++;
            if ("West".Equals(orientation)) width--;

            return new Position
            {
                Length = length,
                Width = width
            };
        }
    }
}
