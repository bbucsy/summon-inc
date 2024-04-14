using System;
using UnityEngine;

namespace Classes
{
    public abstract class Task
    {
        public string Name { get; set; }
        public bool Completed { get; set; }
        // public DateTime Deadline { get; set; }
        public bool HintReceived { get; set; }
        public Task(string name)
        {
            Name = name;
            Completed = false;
            HintReceived = false;
        }

        public abstract GameObject Prefab();

        public abstract string HintText();

    }
}
