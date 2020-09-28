using Premy.Chatovatko.Client.Libs.ClientCommunication;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Premy.Chatovatko.Client.Libs.Sync
{
    public class Synchronizer
    {
        public Queue<IAction> Queue { get; set; }
        public List<WeakReference<IUpdatable>> Updatable { get; }

        private volatile int delay = 1000;
        
        public int GetDelay()
        {
            return delay;
        }

        public void SetDelay(int delay)
        {
            this.delay = delay;
        }

        public Synchronizer(Func<Connection> getConnection, Action reconnect, Logger logger, SettingsCapsula settings)
        {
            Queue = new Queue<IAction>();
            Queue.Enqueue(new PullAction(getConnection, reconnect, logger, settings));
            Queue.Enqueue(new PushAction(getConnection, reconnect, logger, settings));
            Queue.Enqueue(new DelayAction(GetDelay));
            Updatable = new List<WeakReference<IUpdatable>>();
        }

        public void Run()
        {
            Task.Run(() =>
            {
                while (Queue.Count != 0)
                {
                    IAction action = Queue.Dequeue();
                    if (action.Run())
                    {
                        DoUpdate();
                    }
                    Queue.Enqueue(action.GetNext());
                }
            });
        }

        public void DoUpdate()
        {
            foreach(var updatableRef in Updatable)
            {
                if (updatableRef.TryGetTarget(out IUpdatable updatable))
                {
                    try
                    { 
                        updatable.Update();
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void AddAction(Action action)
        {
            Queue.Enqueue(new SingleAction(action));
        }
    }
}
