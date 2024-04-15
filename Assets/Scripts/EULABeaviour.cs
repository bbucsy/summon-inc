using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using DefaultNamespace;
using UnityEngine;

public class EULABeaviour : MonoBehaviour, IMinigame
{
    public Task Task { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnEulaAccepted()
    {
        FindFirstObjectByType<TasksManagerBehaviour>().TaskFinished(Task);
        Destroy(this.gameObject);
    }

    public void OnWindowClosed()
    {
        FindFirstObjectByType<TasksManagerBehaviour>().WindowClosed();
        Destroy(this.gameObject);
    }
}
