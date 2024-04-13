using System;
using UnityEngine;

namespace Classes
{
    public class Task
    {
        public string Name { get; set; }
        public bool Completed { get; set; }
        public DateTime Deadline { get; set; }
        public bool HintReceived { get; set; }
        public TaskType Type { get; set; }
        public Task(string name, TaskType type, DateTime deadline)
        {
            Name = name;
            Deadline = deadline;
            Type = type;
            Completed = false;
            HintReceived = false;
        }
    }
}
