using System;
using System.Collections.Generic;
using System.Linq;
using Classes.Tasks;
using TMPro;
using UnityEngine;
using Task = Classes.Task;

public class TasksManagerBehaviour : MonoBehaviour
{
    public List<Task> Tasks { get; } = new();
    public Dictionary<GameObject, Task> TasksOfComputers { get; } = new Dictionary<GameObject, Task>();
    public event EventHandler<Task> OnTaskFinished;
    public event EventHandler OnTaskWindowClosed; 
    public int tasksToGenerate = 5;
        
    // Start is called before the first frame update
    void Start()
    {
        var allComputers = GameObject.FindGameObjectsWithTag("ComputerWithTask");
        var computers = new List<GameObject>();
        for (int i = 0; i < tasksToGenerate; i++)
        {
            var computer = allComputers[UnityEngine.Random.Range(0, allComputers.Length)];
            while (computers.Contains(computer))
            {
                computer = allComputers[UnityEngine.Random.Range(0, allComputers.Length)];
            }
            computer.SetActive(true);
            computers.Add(computer);
        }
        foreach (var computer in allComputers.Where(c => !computers.Contains(c)))
        {
            computer.SetActive(false);
        }
        foreach (var computer in computers)
        {
            // todo: randomly set some tasks to have already received hints
            if (computer.name.Contains("Eula"))
            {
                Tasks.Add(new EulaTask());
            }
            if (computer.name.Contains("Virus"))
            {
                Tasks.Add(new VirusTask());
            }
            if (computer.name.Contains("Folders"))
            {
                Tasks.Add(new FoldersTask(
                    UnityEngine.Random.Range(2, 10)
                ));
                // temporary
                // Tasks[^1].HintReceived = true;
            }

            if (computer.name.Contains("Os_Update"))
            {
                Tasks.Add(new OsUpdateTask());
            }
            TasksOfComputers.Add(computer, Tasks[^1]);
        }
        var text = GetComponent<TextMeshProUGUI>();
        text.text = "Tasks: " + Tasks.Count;
        foreach (var task in Tasks)
        {
            text.text += "\n: " + task.Name + "(" + (task.Completed ? "Completed" : "Not completed") + ")";
        }
        this.OnTaskFinished += (sender, task) =>
        {
            text.text = "Tasks: " + Tasks.Count(t => t.Completed) + " / " + Tasks.Count;
            foreach (var t in Tasks)
            {
                text.text += "\n: " + t.Name + "(" + (t.Completed ? "Completed" : "Not completed") + ")";
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    public void TaskFinished(Task task)
    {
        task.Completed = true;
        OnTaskFinished?.Invoke(this, task);
    }
        
    public void TaskWindowClosed()
    {
        OnTaskWindowClosed?.Invoke(this, EventArgs.Empty);
    }
}