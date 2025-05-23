using System.Diagnostics;
using System;

namespace Sorting.utils
{
    public class UtilCountingTime 
    {
        private Stopwatch stopwatch = new Stopwatch(); 

        public void Init()
        {
            stopwatch.Restart(); 
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public long GetElapsedTime()
        {
            return stopwatch.ElapsedMilliseconds;
        }
    }
}