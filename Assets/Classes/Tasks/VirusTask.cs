using UnityEngine;

namespace Classes.Tasks
{
    public class VirusTask : Task
    {

        private readonly string[] _hints = {
            "Hey, urgent task ahead: one of our clients recently fell victim to a malicious virus attack, and they're understandably anxious about their cybersecurity. We need to act fast to install top-of-the-line antivirus software on their systems to fortify their defenses. Your expertise in this area is crucial to ensuring the security of our client's data and restoring their peace of mind. Let's prioritize this task and demonstrate our commitment to safeguarding our clients' interests.",
            "Hey, I need your help with something urgent regarding one of our clients. It seems their system got compromised in a recent hack, and they're understandably concerned about their cybersecurity. We need to act fast and ensure their systems are protected. Could you please prioritize installing the latest anti-virus software on their PCs? I'll provide you with all the necessary resources and support to get this done efficiently. Let's make sure we reassure the client of our commitment to their security and regain their trust.",
            "Hi, I need your immediate assistance regarding a critical cybersecurity issue. One of our clients has experienced a significant breach due to a virus attack, and they're counting on us to help them recover and strengthen their defenses. We need to deploy antivirus software across their network promptly to mitigate further risks and protect their sensitive information. Your swift action in coordinating this effort will play a vital role in restoring our client's confidence in our services and maintaining our reputation for reliability.",
            "Hello, I'm reaching out to you regarding a pressing matter concerning one of our clients' cybersecurity. Unfortunately, they've fallen victim to a severe virus attack, and the situation demands our immediate attention. We must act swiftly to install robust antivirus software on their systems to prevent any further breaches and secure their sensitive data. Your expertise in cybersecurity will be invaluable in addressing this urgent issue and helping our client navigate through this challenging time. Let's prioritize this task and demonstrate our unwavering commitment to protecting our clients' interests."
        };
        
        public VirusTask() : base("Virus", true)
        {
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Virus", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            return _hints[Random.Range(0, _hints.Length)];
        }
    }
}