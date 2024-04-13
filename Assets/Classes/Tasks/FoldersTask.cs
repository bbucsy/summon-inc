using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class FoldersTask : Task
{
    public List<Color> FolderColors { get; set; } = new List<Color>();
    public List<Vector3> FilePositions { get; set; } = new List<Vector3>();
    public List<int> FoldersOfFiles { get; set; } = new List<int>();
    
    public FoldersTask(DateTime deadline) : base("Folders", TaskType.Folders, deadline)
    {
    }
}
