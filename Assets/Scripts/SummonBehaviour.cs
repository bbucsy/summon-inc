using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonBehaviour : MonoBehaviour
{
    
    private float counter = 0f;

    public TextMeshProUGUI summonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetText(string Str)
    {
        if (summonText != null)
        {
            summonText.text = Str;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        counter+= Time.deltaTime;
        if (counter >= 3)
        {
            Destroy(gameObject);
        }
    }
}
