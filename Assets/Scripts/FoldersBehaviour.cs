using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Classes.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class FoldersBehaviour : MonoBehaviour, IMinigame
{
    public Task Task { get; set; }

    public List<GameObject> folders = new();
    public List<FoldersMinigameFileBehaviour> files = new();
    public FoldersMinigameFileBehaviour fileGameObject;
    private int _filesToGenerate;
    private int _filesInFolders;
    public int hintAfterSeconds = 5;
    private List<BossBehaviour> bossBehaviours;

    public static readonly List<Color32> SupportedColors = new(
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
        _filesToGenerate = ((FoldersTask)Task).FilesToGenerate;
        for (int i = 0; i < folders.Count; i++)
        {
            var randomColor = SupportedColors[UnityEngine.Random.Range(0, SupportedColors.Count)];
            while (colors.Contains(randomColor))
            {
                randomColor = SupportedColors[UnityEngine.Random.Range(0, SupportedColors.Count)];
            }
            colors.Add(randomColor);
            folders[i].GetComponent<FoldersMinigameFolderBehaviour>().Color = colors[i];
        }

        for (int i = 0; i < _filesToGenerate; i++)
        {
            var filePosition = RandomPosition();
            var fileColor = colors[UnityEngine.Random.Range(0, colors.Count)];
            var file = Instantiate(fileGameObject, transform, false);
            file.gameObject.SetActive(true);
            var fileBehaviour = file.GetComponent<FoldersMinigameFileBehaviour>();
            file.transform.localPosition = new Vector3(filePosition.x, filePosition.y, 0);
            file.Color = fileColor;
            file.ShowsTrueColor = Task.HintReceived;
            files.Add(fileBehaviour);
        }
            
        _filesInFolders = 0;
    }

    public void OnWindowClosed()
    {
        FindFirstObjectByType<TasksManagerBehaviour>().TaskWindowClosed();
        Destroy(this.gameObject);
    }

    public void OnFileInFolder()
    {
        _filesInFolders++;
        if (_filesInFolders == _filesToGenerate)
        {
            FindFirstObjectByType<TasksManagerBehaviour>().TaskFinished(Task);
            Destroy(this.gameObject);
        }
    }
}