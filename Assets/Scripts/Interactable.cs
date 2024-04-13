using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public GameObject player;
    public float interactionDistance = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((player.transform.position - transform.position).magnitude < interactionDistance)
        {
            var child = transform.GetChild(0);
            child.gameObject.SetActive(true);
            // Debug.Log("Interact with " + gameObject.name);
        }
        else
        {
            var child = transform.GetChild(0);
            child.gameObject.SetActive(false);
        }
    }
}
