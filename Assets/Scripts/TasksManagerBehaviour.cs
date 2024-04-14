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
    private TextMeshProUGUI _text;
        
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        OnTaskFinished += (sender, task) => InitText();
    }

    public void InitText()
    {
        _text.text = "Tasks: " + Tasks.Count;
        foreach (var task in Tasks)
        {
            _text.text += "\n: " + task.Name + "(" + (task.Completed ? "Completed" : "Not completed") + ")";
        }
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