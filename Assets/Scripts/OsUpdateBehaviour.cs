using System;
using System.Linq;
using Classes;
using Classes.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class OsUpdateBehaviour : MonoBehaviour, IMinigame
{
    public Task Task { get; set; }
    private LevelManager _levelManager;
    private AudioSource _audioSource;
    public AudioClip osUpdateFinishedSound;
    public AudioClip osUpdateStartedSound;
    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindFirstObjectByType<LevelManager>();
        _audioSource = GetComponent<AudioSource>();
        var task = ((OsUpdateTask)Task);
        var now = DateTime.Now;
        var timer = GetComponentInChildren<Timer>();
        timer.seconds = task.TimeToFinish;
        if (task.StartTime == null) { return; }
        ShowFinishButton();
        timer.StartTimer();
        timer.timeRemaining = (now - task.StartTime.Value).TotalMilliseconds / 1000.0;
    }

    public void StartTimer()
    {
        ShowFinishButton();
        var task = ((OsUpdateTask)Task);
        var timer = GetComponentInChildren<Timer>();
        timer.StartTimer();
        task.StartTime = DateTime.Now;
        _audioSource.PlayOneShot(osUpdateStartedSound);
        _levelManager.PlaySoundWithDelay(osUpdateFinishedSound, task.TimeToFinish);
    }

    private void ShowFinishButton()
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
        FindFirstObjectByType<TasksManagerBehaviour>().TaskWindowClosed();
        Destroy(this.gameObject);
    }
    
    public void OnOsUpdateFinished()
    {
        FindFirstObjectByType<TasksManagerBehaviour>().TaskFinished(Task);
        Destroy(this.gameObject);
    }
}
