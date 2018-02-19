using System;
using System.Threading;

namespace TestarwKaiGoustarw.Threads
{
    public class DailySchedule
    {
        private Timer timer;

        public void SetUpTimer(TimeSpan startTime)
        {
            //DateTime current = DateTime.Now;
            //TimeSpan timeToGo = startTime - current.TimeOfDay;
            //if (timeToGo < TimeSpan.Zero)
            //{
            //    return;//time already passed
            //}

            this.timer = new Timer(x =>
            {
                this.SomeMethodRunsAt1600();
            }, null, startTime, Timeout.InfiniteTimeSpan);
        }

        private void SomeMethodRunsAt1600()
        {
            //this runs at 16:00:00
            Console.WriteLine("2 seconds have passed");
            SetUpTimer(TimeSpan.FromSeconds(2));
        }
    }
}

