using System;
using UnityEngine;

namespace Classes.Tasks
{
    public class OsUpdateTask : Task
    {
        public int TimeToFinish { get; set; }
        public DateTime? StartTime { get; set; }
        public OsUpdateTask(int timeToFinish = 5) : base("Os Update", false)
        {
            TimeToFinish = timeToFinish;
            HintReceived = true;
            StartTime = null;
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Os_update", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            // Does not need hints
            return "";
        }
    }
}