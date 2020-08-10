using System;
using System.Threading;

namespace CommonTasksLib.UnSorted
{
    public static class ThreadingExtensions
    {
        public static bool Completed = false;
        public static void RunAsynchronously<T>(this T obj, Action method, Action callback = null)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    Completed = false;
                    method();
                    Completed = true;
                }
                catch { }

                callback?.Invoke();
            });
        }
    }
}
