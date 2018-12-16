using GardenGizmos.SLMM.Model;

namespace GardenGizmos.SLMM.Navigation
{
    public class TurnAntiClockwise : INavigationCommand
    {
        public void Execute(MowingMachine mowingMachine, Lawn lawn)
        {
            mowingMachine.TurnAntiClockwise();
        }
    }
}
