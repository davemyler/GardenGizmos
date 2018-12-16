using System;

namespace GardenGizmos.SLMM.Model
{
    public class Lawn
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public bool PositionWithinBounds(Position position)
        {
            if (position.Width < 0 || position.Width >= Width) return false;
            if (position.Length < 0 || position.Length >= Length) return false;
            return true;
        }
    }
}
