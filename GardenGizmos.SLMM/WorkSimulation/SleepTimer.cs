using System.Threading;

namespace GardenGizmos.SLMM.WorkSimulation
{
    public class SleepTimer : IWorkTimer
    {
        public void DoWorkFor(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
