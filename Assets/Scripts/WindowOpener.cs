using System;
using Classes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OpenWindow : MonoBehaviour
{
    public Canvas canvas;
    public GameObject window;

    public GameObject CreateWindow(GameObject content)
    {
        var component = Instantiate(window, canvas.transform);
        component.GetComponent<RectTransform>().localScale = Vector3.one;
        component.SetActive(true);
        var exitButton = component.GetComponentInChildren<Button>();
        exitButton.onClick.AddListener(() =>
            {
                TaskList.Instance.TaskWindowClosed();
                Destroy(component);
            }
        );
        foreach (Transform child in component.transform)
        {
            if (!child.CompareTag("WindowContent"))
            {
                continue;
            }

            return Instantiate(content, child, false);
        }

        return null;
    }
}