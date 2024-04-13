using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBehaviour : MonoBehaviour
{

    public GameObject missionIcon;

    public bool hasMission = false;

    public bool summonSent = false;

    public bool dialogOpen = false;
    
    public string Name = "";
    public TextMesh NameText;

    public GameObject SummonPrefab;

    public String[] CasualDialogs;
    public GameObject DialogPrefab;
    
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
            
            int randomIndex = Random.Range(0, CasualDialogs.Length);

            // Get the random string from the array
            string randomString = CasualDialogs[randomIndex];
            dialogBehaviour.SetDialogContent(Name,randomString);

            dialogOpen = true;

            dialogBehaviour.OnDialogClose += (sender, args) =>
            {
                dialogOpen = false;
                hasMission = false;
                summonSent = false;

            };

        }
        
    }

   
}
