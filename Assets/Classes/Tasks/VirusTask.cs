using UnityEngine;

namespace Classes.Tasks
{
    public class VirusTask : Task
    {
        public VirusTask() : base("Virus", true)
        {
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Virus", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            return "Hey, I need your help with something urgent regarding one of our clients. It seems their system got compromised in a recent hack, and they're understandably concerned about their cybersecurity. We need to act fast and ensure their systems are protected. Could you please prioritize installing the latest anti-virus software on their PCs? I'll provide you with all the necessary resources and support to get this done efficiently. Let's make sure we reassure the client of our commitment to their security and regain their trust.";
        }
    }
}