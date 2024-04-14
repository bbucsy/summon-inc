using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{

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
    
    public event EventHandler<BossBehaviour> BossWantsToTalk;
    public event EventHandler<BossBehaviour> BossTalked;
    
    // Start is called before the first frame update
    void Start()
    {
        if (NameText != null)
        {
            NameText.text = Name;
        }
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
            
            var canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                var summonObject = Instantiate(SummonPrefab,canvas.transform);
                var summonScript = summonObject.GetComponent<SummonBehaviour>();
                summonScript.SetText($"{Name} has summoned you!");
                Debug.Log("Summoning player");
                BossWantsToTalk?.Invoke(this, this);
                summonSent = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!dialogOpen && hasMission)
        {
            openDialog();
        }
    }


    private void openDialog()
    {
        var canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            var dialogObject = Instantiate(DialogPrefab, canvas.transform);
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
