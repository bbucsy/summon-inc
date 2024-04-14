using UnityEngine;

namespace Classes.Tasks
{
    public class VirusTask : Task
    {
        public VirusTask() : base("Virus")
        {
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Virus", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            return "Virus hint text TODO";
        }
    }
}