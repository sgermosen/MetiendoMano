using MassTransit.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient.Services
{
    public class TaskManager
    {
        private readonly List<Task> _taskList;
        private readonly TaskFactory _factory;

        public TaskManager()
        {
            // Create a scheduler that uses two threads.
            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
            _taskList = new List<Task>();

            // Create a TaskFactory and pass it our custom scheduler.
            _factory = new TaskFactory(taskScheduler);
        }

        public void Run(Action action, CancellationToken token = default)
        {
            var task = _factory.StartNew(action, token)
                .ContinueWith(TaskCompleted, token);

            _taskList.Add(task);

            // wait until all task are completed
            Task.WaitAll(_taskList.ToArray());
        }

        private void TaskCompleted(Task completedTask)
        {
            _taskList.Remove(completedTask);
        }
    }
}
