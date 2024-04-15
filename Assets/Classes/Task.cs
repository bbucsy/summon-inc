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
        public bool HintRequired { get; }

        protected Task(string name, bool hintRequired)
        {
            Name = name;
            Completed = false;
            HintReceived = false;
            HintRequired = hintRequired;
        }

        public abstract GameObject Prefab();

        public abstract string HintText();

    }
}
