using System;
using UnityEngine;
using UnityEngine.UI;

public class FoldersMinigameFolderBehaviour : MonoBehaviour
{
    private Color _color;

    public Color Color
    {
        set
        {
            _color = value;
            gameObject.GetComponent<Image>().color = value;
        }
        get => _color;
    }

    public void Start()
    {
        gameObject.GetComponent<Image>().color = _color;
    }
}