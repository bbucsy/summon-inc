using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Classes.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool IsGameOver { get; set; }
    public string NextLevel;

    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        var computers = GameObject.FindGameObjectsWithTag("ComputerWithTask");
        var taskManager = FindFirstObjectByType<TasksManagerBehaviour>();
        foreach (var computer in computers)
        {
            switch (computer.name)
            {
                case { } a when a.Contains("Eula"):
                    taskManager.Tasks.Add(new EulaTask());
                    break;
                case { } a when a.Contains("Folders"):
                    taskManager.Tasks.Add(new FoldersTask(10));
                    break;
                case { } a when a.Contains("Os_Update"):
                    taskManager.Tasks.Add(new OsUpdateTask(25));
                    break;
                case { } a when a.Contains("Virus"):
                    taskManager.Tasks.Add(new VirusTask());
                    break;
                default:
                    taskManager.Tasks.Add(
                        // Add one at random
                        new Task[]
                        {
                            new EulaTask(), 
                            new FoldersTask(10),
                            new OsUpdateTask(25),
                            new VirusTask()
                        }[
                            Random.Range(0, 4)
                        ]
                    );
                    break;
            }
            taskManager.TasksOfComputers.Add(computer, taskManager.Tasks[^1]);
        }
       //taskManager.Tasks.GetRange(1, taskManager.Tasks.Count - 1).ForEach(task => task.Completed = true);
        taskManager.InitText();
        taskManager.OnAllTasksFinished += (sender, args) =>
        {
            OnTasksFinished();
        };
    }

    public void OnStopperEnded()
    {
        foreach (var button in Resources.FindObjectsOfTypeAll<GameObject>()
                     .Where(b => b.CompareTag("GameOver")))
        {
            button.gameObject.SetActive(true);
        }
        IsGameOver = true;
        foreach (var computerBehaviour in FindObjectsOfType<ComputerBehaviour>())
        {
            computerBehaviour.CloseWindow();
        }
    }

    private void OnTasksFinished()
    {
        foreach (var o in Resources.FindObjectsOfTypeAll<GameObject>()
                     .Where(b => b.CompareTag("LevelFinished")))
        {
            o.gameObject.SetActive(true);
        }
        IsGameOver = true;
        foreach (var computerBehaviour in FindObjectsOfType<ComputerBehaviour>())
        {
            computerBehaviour.CloseWindow();
        }
        timer.GetComponent<Timer>().StopTimer();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }

    public void GoToNextNevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
}