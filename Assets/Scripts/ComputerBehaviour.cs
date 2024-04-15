using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class ComputerBehaviour : MonoBehaviour
{

    private bool _playerInRadius;

    public bool IsMinigameShowing { get; private set; }
    private bool _minigameAvailable;
    private Canvas _canvas;

    public Animator Animator;
    public GameObject keyBoardIconSprite;
    public float lerpSpeed = 2f;
    private static readonly int TaskFinished = Animator.StringToHash("taskFinished");
    
    private TasksManagerBehaviour _tasksManagerBehaviour;
    private GameObject _createdMinigame;
    private LevelManager _levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInRadius = false;
        IsMinigameShowing = false;
        _minigameAvailable = true;
        _canvas = FindObjectOfType<Canvas>();
        _tasksManagerBehaviour = FindFirstObjectByType<TasksManagerBehaviour>();
        _levelManager = FindFirstObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInRadius && _minigameAvailable && !keyBoardIconSprite.activeSelf)
        {
            keyBoardIconSprite.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && IsMinigameShowing)
        {
            CloseWindow();
            return;
        }

        if (_levelManager.IsGameOver)
        {
            return;
        }
        if (Input.GetKeyDown("e") && _playerInRadius && _minigameAvailable)
        {
            Debug.unityLogger.Log("E key was pressed");
            if (!IsMinigameShowing)
            {
                IsMinigameShowing = true;
                var task = _tasksManagerBehaviour.TasksOfComputers[this.gameObject];
                var miniGamePrefab = task.Prefab();
                var minigame = Instantiate(miniGamePrefab,_canvas.transform);
                _createdMinigame = minigame;
                var minigameScript = minigame.GetComponent<IMinigame>();
                minigameScript.Task = task;
                _tasksManagerBehaviour.OnTaskFinished += (sender, finishedTask) =>
                {
                    if (finishedTask != task) return;
                    _minigameAvailable = false;
                    keyBoardIconSprite.SetActive(false);
                    if (Animator != null)
                    {
                        Animator.SetTrigger(TaskFinished);
                    }
                    IsMinigameShowing = false;
                };
                _tasksManagerBehaviour.OnTaskWindowClosed += (sender, args) =>
                {
                    // Debug.Log("TASK WINDOW CLOSED");
                    IsMinigameShowing = false;
                };
                minigame.GetComponent<RectTransform>()?.position.Set(0,0,0);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.unityLogger.Log("Collided");
        _playerInRadius = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerInRadius = false;
        keyBoardIconSprite.SetActive(false);
        CloseWindow(false);
    }

    public void CloseWindow(bool sendEvent = true)
    {
        if (sendEvent)
        {
            _tasksManagerBehaviour.WindowClosed();
        }
        IsMinigameShowing = false;
        if (_createdMinigame)
        {
            Destroy(_createdMinigame);
            _createdMinigame = null;
        }
    }
 
}
