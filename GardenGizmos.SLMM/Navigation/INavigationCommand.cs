using GardenGizmos.SLMM.Model;

namespace GardenGizmos.SLMM.Navigation
{
    public interface INavigationCommand
    {
        void Execute(MowingMachine mowingMachine, Lawn lawn);
    }
}