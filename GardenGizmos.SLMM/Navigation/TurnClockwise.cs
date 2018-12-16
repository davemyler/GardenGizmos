using GardenGizmos.SLMM.Model;
using System;

namespace GardenGizmos.SLMM.Navigation
{
    public class TurnClockwise : INavigationCommand
    {
        public void Execute(MowingMachine mowingMachine, Lawn lawn)
        {
            mowingMachine.TurnClockwise();
        }
    }
}
