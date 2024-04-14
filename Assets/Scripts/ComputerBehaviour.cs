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
    
    
    public Transform keyBoardIconSprite;
    public Transform keyboardIconSpritePosition;
    public float lerpSpeed = 2f;
    public Canvas Canvas;
    private TasksManagerBehaviour _tasksManagerBehaviour;
    private GameObject _createdMinigame;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInRadius = false;
        _minigameShowing = false;
        _minigameAvailable = true;
        _tasksManagerBehaviour = FindFirstObjectByType<TasksManagerBehaviour>();
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
                var task = _tasksManagerBehaviour.TasksOfComputers[this.gameObject];
                var miniGamePrefab = task.Prefab();
                var minigame = Instantiate(miniGamePrefab,Canvas.transform);
                _createdMinigame = minigame;
                var minigameScript = minigame.GetComponent<IMinigame>();
                minigameScript.Task = task;
                _tasksManagerBehaviour.OnTaskFinished += (sender, finishedTask) =>
                {
                    if (finishedTask == task)
                    {
                        Debug.Log("TASK FINISHED: " + finishedTask);
                        this._minigameAvailable = false;
                        keyBoardIconSprite.localPosition = Vector2.zero;
                    }
                };
                _tasksManagerBehaviour.OnTaskWindowClosed += (sender, args) =>
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
        _tasksManagerBehaviour.TaskWindowClosed();
        Destroy(_createdMinigame);
    }
 
}
