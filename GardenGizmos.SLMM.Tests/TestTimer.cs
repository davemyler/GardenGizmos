using GardenGizmos.SLMM.WorkSimulation;
using System.Threading;

namespace GardenGizmos.SLMM.Tests
{
    public class TestTimer : IWorkTimer
    {
        public static TestTimer Instance { get; } = new TestTimer();

        private Semaphore _sempahore = new Semaphore(0, 1);
        private int _waitTime = 0;

        public void DoWorkFor(int seconds)
        {
            _waitTime = seconds;
            _sempahore.WaitOne();
        }

        public static void ClockTick()
        {
            Instance.InternalClockTick();
        }

        private void InternalClockTick()
        {
            _waitTime -= 1;
            if (_waitTime == 0)
            {
                _sempahore.Release();
            }
        }
    }
}