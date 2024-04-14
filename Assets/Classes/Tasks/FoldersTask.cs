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
           return "We recieved a new task from our client. They don't like the current coloring of the file system and want to do it in another way.";
        }
    }
}
