using System.Collections;
using System.Collections.Generic;
using Classes;
using DefaultNamespace;
using UnityEngine;

public class FoldersComputerBehaviour : MonoBehaviour
{

    private bool _playerInRadius;
    private bool _minigameShowing;
    private bool _minigameAvailable;
    
    public Transform keyBoardIconSprite;
    public Transform keyboardIconSpritePosition;
    public float lerpSpeed = 2f;
    public Canvas Canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInRadius = false;
        _minigameShowing = false;
        _minigameAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {if (_playerInRadius && _minigameAvailable)
        {
            keyBoardIconSprite.position = Vector2.Lerp( keyBoardIconSprite.position, keyboardIconSpritePosition.position, lerpSpeed * Time.deltaTime);
           
        }
        else
        {
            keyBoardIconSprite.position = Vector3.zero;
        }
        
        if (Input.GetKeyDown("e") && _playerInRadius)
        {
            Debug.unityLogger.Log("E key was pressed");
            if (!_minigameShowing)
            {
                _minigameShowing = true;
                var task = TasksManagerBehaviour.Instance.TasksOfComputers[this.gameObject];
                var miniGamePrefab = task.Type.Prefab();
                var minigame = Instantiate(miniGamePrefab,Canvas.transform);
                var minigameScript = minigame.GetComponent<IMinigame>();
                minigameScript.Task = task;
                TasksManagerBehaviour.Instance.OnTaskFinished += (sender, finishedTask) =>
                {
                    if (finishedTask == task)
                    {
                        // Debug.Log("TASK FINISHED: " + finishedTask);
                        this._minigameAvailable = false;
                    }
                };
                TasksManagerBehaviour.Instance.OnTaskWindowClosed += (sender, args) =>
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
    }
    
}
