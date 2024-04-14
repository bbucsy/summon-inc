using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Classes.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class OsUpdateBehaviour : MonoBehaviour, IMinigame
{
    public Task Task { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        var task = ((OsUpdateTask)Task);
        var now = DateTime.Now;
        var timer = GetComponentInChildren<Timer>();
        timer.seconds = task.TimeToFinish;
        if (task.StartTime == null) { return; }
        showFinishButton();
        timer.StartTimer();
        timer.timeRemaining = (now - task.StartTime.Value).TotalMilliseconds / 1000.0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer()
    {
        showFinishButton();
        var task = ((OsUpdateTask)Task);
        var timer = GetComponentInChildren<Timer>();
        timer.StartTimer();
        task.StartTime = DateTime.Now;
    }

    private void showFinishButton()
    {
        var buttons = GetComponentsInChildren<Button>(includeInactive: true);
        var startButton = buttons.First(b => b.gameObject.name == "StartButton");
        startButton.gameObject.SetActive(false);
        var finishButton = buttons.First(b => b.gameObject.name == "FinishButton");
        finishButton.gameObject.SetActive(true);
        finishButton.enabled = false;
    }

    public void TimerFinished()
    {
        var buttons = GetComponentsInChildren<Button>();
        var finishButton = buttons.First(b => b.gameObject.name == "FinishButton");
        finishButton.enabled = true;
    }

    public void OnWindowClosed()
    {
        TasksManagerBehaviour.Instance.TaskWindowClosed();
        Destroy(this.gameObject);
    }
    
    public void OnOsUpdateFinished()
    {
        TasksManagerBehaviour.Instance.TaskFinished(Task);
        Destroy(this.gameObject);
    }
}
