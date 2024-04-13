using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(
            Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * speed * Time.deltaTime
        );
    }
}
