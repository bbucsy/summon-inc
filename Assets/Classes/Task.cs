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
        public GameObject TaskObject { get; set; }
        public Task(string name, GameObject taskObject, DateTime deadline)
        {
            Name = name;
            TaskObject = taskObject;
            Deadline = deadline;
            Completed = false;
            HintReceived = false;
        }
    }
}
