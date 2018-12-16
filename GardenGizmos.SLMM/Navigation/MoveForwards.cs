using GardenGizmos.SLMM.Model;
using System;

namespace GardenGizmos.SLMM.Navigation
{
    public class MoveForwards : INavigationCommand
    {
        public void Execute(MowingMachine mowingMachine, Lawn lawn)
        {
            var position = mowingMachine.NextPositionInDirection();
            if (lawn.PositionWithinBounds(position))
            {
                mowingMachine.MoveTo(position);
            }
        }
    }
}
