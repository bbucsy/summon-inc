using System;

namespace Classes.Tasks
{
    public class FoldersTask : Task
    {
    
        public FoldersTask(DateTime deadline) : base("Folders", TaskType.Folders, deadline)
        {
        }
    }
}
