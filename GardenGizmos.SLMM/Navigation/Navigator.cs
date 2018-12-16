using GardenGizmos.SLMM.Model;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GardenGizmos.SLMM.Navigation
{
    public class Navigator
    {
        private MowingMachine _mowingMachine;
        private Lawn _lawn;

        public Navigator(MowingMachine mowingMachine, Lawn lawn)
        {
            _mowingMachine = mowingMachine;
            _lawn = lawn;
        }

        private BlockingCollection<INavigationCommand> _navigationCommands = new BlockingCollection<INavigationCommand>();

        public void AddNavigationCommand(INavigationCommand navigationCommand)
        {
            _navigationCommands.Add(navigationCommand);
        }

        public MowingMachine MachinePosition()
        {
            return _mowingMachine;
        }

        public void StartNavigation()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var navigationCommand = _navigationCommands.Take();
                    navigationCommand.Execute(_mowingMachine, _lawn);
                }
            });
        }
    }
}
