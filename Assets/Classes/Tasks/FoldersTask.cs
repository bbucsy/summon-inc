using System;
using UnityEngine;

namespace Classes.Tasks
{
    public class FoldersTask : Task
    {
        public int FilesToGenerate { get; set; }
        
        public FoldersTask(int filesToGenerate = 2) : base("Folders")
        {
            FilesToGenerate = filesToGenerate;
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Folders", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
           return "Hi, I need your immediate assistance with an urgent task related to one of our projects. We've got a crucial client presentation coming up, and it's essential that we ensure everything is meticulously organized. We need to move files into two specific folders, and here's the catch: the files' colors must match the folders' colors for consistency and clarity. I know it sounds a bit meticulous, but it's vital for maintaining our professionalism and ensuring a smooth presentation. Could you please prioritize this task and ensure it's completed accurately and promptly? Your attention to detail and efficiency will be greatly appreciated as we work to impress our client and deliver exceptional results.";
        }
    }
}
