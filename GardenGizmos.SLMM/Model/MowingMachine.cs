using GardenGizmos.SLMM.WorkSimulation;

namespace GardenGizmos.SLMM.Model
{
    public class MowingMachine
    {
        private IWorkTimer _workTimer;

        public MowingMachine(IWorkTimer workTimer)
        {
            _workTimer = workTimer;
        }

        public string Orientation { get; set; }
        public Position Position { get; set; }

        public void TurnAntiClockwise()
        {
            Face(Compass.AntiClockwiseFrom(Orientation));
        }

        public void TurnClockwise()
        {
            Face(Compass.ClockwiseFrom(Orientation));
        }

        public Position NextPositionInDirection()
        {
            return Position.NextPositionInDirection(Orientation);
        }

        public void MoveTo(Position position)
        {
            _workTimer.DoWorkFor(5);
            Position = position;
        }

        private void Face(string direction)
        {
            _workTimer.DoWorkFor(3);
            Orientation = direction;
        }
    }
}
