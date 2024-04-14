using System;
using UnityEngine;

namespace Classes.Tasks
{
    public class OsUpdateTask : Task
    {
        public int TimeToFinish { get; set; }
        public DateTime? StartTime { get; set; }
        public OsUpdateTask(int timeToFinish = 5) : base("Os Update")
        {
            TimeToFinish = timeToFinish;
            StartTime = null;
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Os_update", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            return "PC needs an OS update, find the update button in the settings.";
        }
    }
}