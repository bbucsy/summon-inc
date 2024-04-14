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
    private bool _minigameShowing;
    private bool _minigameAvailable;
    private Canvas _canvas;


    public Animator Animator;
    public Transform keyBoardIconSprite;
    public Transform keyboardIconSpritePosition;
    public float lerpSpeed = 2f;
    private static readonly int TaskFinished = Animator.StringToHash("taskFinished");


    // Start is called before the first frame update
    void Start()
    {
        _playerInRadius = false;
        _minigameShowing = false;
        _minigameAvailable = true;
        _canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInRadius && _minigameAvailable)
        {
            keyBoardIconSprite.position = Vector2.Lerp( keyBoardIconSprite.position, keyboardIconSpritePosition.position, lerpSpeed * Time.deltaTime);
           
        }
        
        if (Input.GetKeyDown("e") && _playerInRadius)
        {
            Debug.unityLogger.Log("E key was pressed");
            if (!_minigameShowing)
            {
                _minigameShowing = true;
                var task = FindFirstObjectByType<TasksManagerBehaviour>().TasksOfComputers[this.gameObject];
                var miniGamePrefab = task.Prefab();
                var minigame = Instantiate(miniGamePrefab,_canvas.transform);
                var minigameScript = minigame.GetComponent<IMinigame>();
                minigameScript.Task = task;
                var taskManager = FindFirstObjectByType<TasksManagerBehaviour>();
                taskManager.OnTaskFinished += (sender, finishedTask) =>
                {
                    if (finishedTask != task) return;
                    
                    _minigameAvailable = false;
                    keyBoardIconSprite.localPosition = Vector2.zero;
                    if (Animator != null)
                    {
                        Animator.SetTrigger(TaskFinished);
                    }
                };
                taskManager.OnTaskWindowClosed += (sender, args) =>
                {
                    // Debug.Log("TASK WINDOW CLOSED");
                    _minigameShowing = false;
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
        keyBoardIconSprite.localPosition = Vector2.zero;
    }

 
}
