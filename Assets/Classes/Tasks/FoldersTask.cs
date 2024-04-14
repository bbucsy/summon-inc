using System;

namespace Classes.Tasks
{
    public class FoldersTask : Task
    {
        public int FilesToGenerate { get; set; }
        
        public FoldersTask(int filesToGenerate = 2) : base("Folders", TaskType.Folders)
        {
            FilesToGenerate = filesToGenerate;
        }
    }
}
