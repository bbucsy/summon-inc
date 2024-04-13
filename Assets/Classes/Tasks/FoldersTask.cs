using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class FoldersTask : Task
{
    private int FolderCount { get; set; }
    private int FilesCount { get; set; }
    private List<int> FoldersOfFiles { get; set; }
    
    public FoldersTask(DateTime deadline) : base("Folders", TaskType.Folders, deadline)
    {
        FolderCount = 5;
        FilesCount = 10;
        FoldersOfFiles = new List<int>();
        for (int i = 0; i < FilesCount; i++)
        {
            FoldersOfFiles.Add(UnityEngine.Random.Range(0, FolderCount));
        }
    }
}
