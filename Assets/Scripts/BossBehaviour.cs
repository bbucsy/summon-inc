using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{

    private bool _playerInRadius = false;
    
    public GameObject missionIcon;

    public bool hasMission = false;

    public bool summonSent = false;

    public bool dialogOpen = false;

    public Task NextTaskHint = null;
    
    public string Name = "";
    public TextMesh NameText;

    public GameObject SummonPrefab;

    public String[] CasualDialogs;
    public GameObject DialogPrefab;

    public GameObject KeyboardIcon;
    public event EventHandler<BossBehaviour> BossWantsToTalk;
    public event EventHandler<BossBehaviour> BossTalked;

    private Canvas _canvas;
    private CircleCollider2D _circleCollider;
    private Rigidbody2D _playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        if (NameText != null)
        {
            NameText.text = Name;
        }
        _circleCollider = GetComponent<CircleCollider2D>();
        _canvas = FindObjectOfType<Canvas>();
        _playerRb = FindFirstObjectByType<Play>().rb;
    }

    // Update is called once per frame
    void Update()
    {
        if (missionIcon != null)
        {
            missionIcon.SetActive(hasMission);
        }

        if (hasMission && !summonSent)
        {
            if (_canvas != null)
            {
                var summonObject = Instantiate(SummonPrefab,_canvas.transform);
                var summonScript = summonObject.GetComponent<SummonBehaviour>();
                summonScript.SetText($"{Name} has summoned you!");
                Debug.Log("Summoning player");
                BossWantsToTalk?.Invoke(this, this);
                summonSent = true;
                if (_playerRb.Distance(_circleCollider).distance <= _circleCollider.radius)
                {
                    _playerInRadius = true;
                    KeyboardIcon.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown("e") && _playerInRadius && !dialogOpen && hasMission)
        {
            openDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        _playerInRadius = true;
        KeyboardIcon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerInRadius = false;
        KeyboardIcon.SetActive(false);
    }


    private void openDialog()
    {
        if (_canvas != null)
        {
            var dialogObject = Instantiate(DialogPrefab, _canvas.transform);
            var dialogBehaviour = dialogObject.GetComponent<DialogBehaviour>();

            if (NextTaskHint == null)
            {
                // Get the random string from the array
                var randomIndex = Random.Range(0, CasualDialogs.Length);


                var randomString = CasualDialogs[randomIndex];
                dialogBehaviour.SetDialogContent(Name,randomString);
            }
            else
            {
                dialogBehaviour.SetDialogContent(Name,NextTaskHint.HintText());
            }
           

            dialogOpen = true;

            dialogBehaviour.OnDialogClose += (sender, args) =>
                handleDialogClose();

        }
        
    }

    private void handleDialogClose()
    {
        dialogOpen = false;
        hasMission = false;
        summonSent = false;

        if (NextTaskHint != null)
        {
            NextTaskHint.HintReceived = true;
            NextTaskHint = null;
        }
        BossTalked?.Invoke(this, this);
        
    }

   
}
