using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

namespace DefaultNamespace
{
    public class FoldersBehaviour : MonoBehaviour, IMinigame
    {
        public Task Task { get; set; }
        public event EventHandler OnFinish;

        public List<GameObject> folders = new();
        public List<FoldersMinigameFileBehaviour> files = new();
        public FoldersMinigameFileBehaviour fileGameObject;
        public int filesToGenerate = 10;
        private int _filesInFolders = 0;

        public void Start()
        {
            FoldersTask foldersTask = (FoldersTask)Task;
            List<float> colors = new List<float>();
            while (foldersTask.FolderColors.Count < folders.Count)
            {
                colors.Add(UnityEngine.Random.Range(0f, 1f));
                colors.Add(UnityEngine.Random.Range(0f, 1f));
                colors.Add(UnityEngine.Random.Range(0f, 1f));
                foldersTask.FolderColors.Add(
                    new Color(
                        colors[foldersTask.FolderColors.Count * 3],
                        colors[foldersTask.FolderColors.Count * 3 + 1],
                        colors[foldersTask.FolderColors.Count * 3 + 2]
                    )
                );
            }
            
            foreach (var foldersTaskFolderColor in foldersTask.FolderColors)
            {
                colors.Add(foldersTaskFolderColor.r);
                colors.Add(foldersTaskFolderColor.g);
                colors.Add(foldersTaskFolderColor.b);
            }

            for (int i = 0; i < folders.Count; i++)
            {
                folders[i].GetComponent<FoldersMinigameFolderBehaviour>().Color = new Color(
                    colors[i * 3],
                    colors[i * 3 + 1],
                    colors[i * 3 + 2]
                );
            }

            while (foldersTask.FilePositions.Count < filesToGenerate)
            {
                foldersTask.FilePositions.Add(
                    new Vector3(
                        UnityEngine.Random.Range(-225f, 225f),
                        UnityEngine.Random.Range(-100f, 100f), 
                        0
                    )
                );
                foldersTask.FoldersOfFiles.Add(
                    UnityEngine.Random.Range(0, foldersTask.FolderColors.Count)
                );
            }

            for (int i = 0; i < foldersTask.FilePositions.Count; i++)
            {
                var filePosition = foldersTask.FilePositions[i];
                var fileColor = foldersTask.FolderColors[foldersTask.FoldersOfFiles[i]];
                var file = Instantiate(fileGameObject);
                file.gameObject.SetActive(true);
                file.transform.SetParent(transform);
                file.transform.localPosition = new Vector3(filePosition.x, filePosition.y, 0);
                file.Color = fileColor;
                files.Add(file.GetComponent<FoldersMinigameFileBehaviour>());
            }
            
            _filesInFolders = 0;
        }

        public void OnWindowClosed()
        {
            TaskList.Instance.TaskWindowClosed();
            for (int i = 0; i < files.Count; i++)
            {
                var pos = files[i].transform.position;
                var foldersTask = (FoldersTask)Task;
                var relativePos = files[i].transform.parent.InverseTransformPoint(files[i].transform.position);
                foldersTask.FilePositions[i] = new Vector3(relativePos.x, relativePos.y, 0);
            }
            Destroy(this.gameObject);
        }

        public void OnFileInFolder()
        {
            _filesInFolders++;
            if (_filesInFolders == filesToGenerate)
            {
                TaskList.Instance.TaskFinished(Task);
                Destroy(this.gameObject);
            }
        }
    }
}