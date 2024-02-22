using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    public static class TaskManager
    {
        static List<Task> tasks;

        static TaskManager()
        {
            tasks = new List<Task>();
        }

        public static void Add(Task task) {
            tasks.Add(task);
        }

        public static void Stop(string name)
        {
            var task = GetTaskByName(name);
            if (task != null)
                task.Stop();
        }

        public static void Start(string name)
        {
            var task = GetTaskByName(name);
            if (task != null)
                task.Start();
        }

        public static void StopAll()
        {
            foreach(var task in tasks)
            {
                task.Stop();
            }
        }

        public static void StartAll()
        {
            foreach (var task in tasks)
            {
                task.Start();
            }
        }

        public static bool IsRunning(string name)
        {
            var task = GetTaskByName(name);
            return task == null ? false : task.IsRunning; 
        }

        private static Task GetTaskByName(string name) => tasks.FirstOrDefault(t => t.Name == name);
    }

    public class TaskFactory
    {
        public static Task Create(Action action, int delay, string name) => new Task(action, delay, name);
    }

    public class Task
    {
        Action action;
        int delay;
        
        public string Name { get; private set; }
        public bool IsRunning { get; private set; } 

        public Task(Action action, int delay, string name)
        {
            this.action = action;
            this.delay = delay;
            this.Name = name;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Start()
        {
            IsRunning = true;

            System.Threading.Tasks.Task.Run(async() =>
            {
                while (IsRunning)
                {
                    action();
                    if (delay > 0)
                        await System.Threading.Tasks.Task.Delay(delay);
                }
            });
        }
    }
}
