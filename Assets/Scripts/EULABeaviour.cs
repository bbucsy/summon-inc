using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EULABeaviour : MonoBehaviour, IMinigame
{
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
        this.OnFinish?.Invoke(this, null);
        Destroy(this.gameObject);
    }

    public event EventHandler OnFinish;
}
