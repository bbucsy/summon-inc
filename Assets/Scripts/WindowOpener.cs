using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class OpenWindow : MonoBehaviour
{

    public Canvas canvas;
    public GameObject window;

    public void CreateWindow(GameObject content)
    {
        var component = Instantiate(window, canvas.transform);
        var x = Random.value * 300;
        var y = Random.value * 300;
        component.transform.position = new Vector3(
            x,
            y
        );
        foreach (Transform child in component.transform)
        {
            if (!child.CompareTag("WindowContent"))
            {
                continue;
            }
            var textObject = Instantiate(new GameObject(), child, false);
            textObject.AddComponent<TextMeshProUGUI>();
            textObject.GetComponent<TextMeshProUGUI>().text = "Test" + Math.Round(Random.value * 10000);
        }
        
    
    }
    
}
