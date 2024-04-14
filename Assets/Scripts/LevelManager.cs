using System.Collections;
using System.Collections.Generic;
using Classes;
using Classes.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
                    taskManager.Tasks.Add(new FoldersTask(
                        Random.Range(2, 10)
                    ));
                    break;
                case { } a when a.Contains("Os_Update"):
                    taskManager.Tasks.Add(new OsUpdateTask(
                        Random.Range(10, 25)
                    ));
                    break;
                default:
                    taskManager.Tasks.Add(
                        // Add one at random
                        new Task[]
                        {
                            new EulaTask(), 
                            new FoldersTask(Random.Range(2, 10)),
                            new OsUpdateTask(Random.Range(10, 25))
                        }[
                            Random.Range(0, 3)
                        ]
                    );
                    break;
            }
            taskManager.TasksOfComputers.Add(computer, taskManager.Tasks[^1]);
        }

        taskManager.InitText();
    }

    public void OnStopperEnded()
    {
        // todo: move to the end scene
    }
}