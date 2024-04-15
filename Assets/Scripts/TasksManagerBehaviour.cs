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
    public event EventHandler OnAllTasksFinished;
    public AudioSource taskFinishedSound;
    public AudioSource windowClosedSound;
        
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        OnTaskFinished += (sender, task) => InitText();
        foreach (var bossBehaviour in FindObjectsOfType<BossBehaviour>())
        {
            bossBehaviour.BossTalked += (sender, behaviour) => InitText();
        }
    }

    public void InitText()
    {
        _text.text = "Tasks: " + Tasks.Count;
        foreach (var task in Tasks)
        {
            _text.text += "\n• " + (task.Completed ? "<color=green>" : "<color=red>") + task.Name + "</color>";
            if (task.HintRequired && task.HintReceived)
            {
                _text.text += "<color=yellow>*</color>";
            }
        }
    }

    public void TaskFinished(Task task)
    {
        task.Completed = true;
        OnTaskFinished?.Invoke(this, task);
        if (Tasks.All(t => t.Completed))
        {
            OnAllTasksFinished?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            taskFinishedSound.Play();
        }
    }
        
    public void TaskWindowClosed()
    {
        OnTaskWindowClosed?.Invoke(this, EventArgs.Empty);
        windowClosedSound.Play();
    }
}