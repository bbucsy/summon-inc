using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Classes.Tasks;
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

        private static readonly List<Color32> supportedColors = new(
            new[]
            {
                new Color32(155, 93, 229, 255),
                new Color32(241, 91, 181, 255),
                new Color32(254, 228, 64, 255),
                new Color32(0, 187, 249, 255),
                new Color32(0, 245, 212, 255)
            }
        );

        private Vector3 RandomPosition()
        {
            return new Vector3(
                UnityEngine.Random.Range(-225f, 225f),
                UnityEngine.Random.Range(-100f, 100f),
                0
            );
        }

        public void Start()
        {
            List<Color> colors = new List<Color>();

            for (int i = 0; i < folders.Count; i++)
            {
                var randomColor = supportedColors[UnityEngine.Random.Range(0, supportedColors.Count)];
                while (colors.Contains(randomColor))
                {
                    randomColor = supportedColors[UnityEngine.Random.Range(0, supportedColors.Count)];
                }
                colors.Add(randomColor);
                folders[i].GetComponent<FoldersMinigameFolderBehaviour>().Color = colors[i];
            }

            for (int i = 0; i < filesToGenerate; i++)
            {
                var filePosition = RandomPosition();
                var fileColor = colors[UnityEngine.Random.Range(0, colors.Count)];
                var file = Instantiate(fileGameObject);
                file.gameObject.SetActive(true);
                file.transform.SetParent(transform);
                var fileBehaviour = file.GetComponent<FoldersMinigameFileBehaviour>();
                file.transform.localPosition = new Vector3(filePosition.x, filePosition.y, 0);
                file.Color = fileColor;
                files.Add(fileBehaviour);
            }
            
            _filesInFolders = 0;
        }

        public void OnWindowClosed()
        {
            TasksManagerBehaviour.Instance.TaskWindowClosed();
            Destroy(this.gameObject);
        }

        public void OnFileInFolder()
        {
            _filesInFolders++;
            if (_filesInFolders == filesToGenerate)
            {
                TasksManagerBehaviour.Instance.TaskFinished(Task);
                Destroy(this.gameObject);
            }
        }
    }
}