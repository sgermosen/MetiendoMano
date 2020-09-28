using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Premy.Chatovatko.Client.Libs.Sync
{
    public class DelayAction : IAction
    {
        public int Miliseconds { get;}
        private DateTime timeToRun;
        private readonly Func<int> getDelay;

        public DelayAction(Func<int> getDelay)
        {
            this.getDelay = getDelay;
            Miliseconds = getDelay();
            this.timeToRun = DateTime.Now;
            timeToRun.AddMilliseconds(Miliseconds);
        }

        public IAction GetNext()
        {
            return new DelayAction(getDelay);
        }

        public bool Run()
        {
            DateTime now = DateTime.Now;
            if (timeToRun > now)
            { 
                Thread.Sleep((timeToRun - now).Milliseconds);
            }
            return false;
        }

    }
}
