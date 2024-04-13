using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBehaviour : MonoBehaviour
{

    public event EventHandler OnDialogClose;

    public TextMeshProUGUI NameHolder;

    public TextMeshProUGUI TextHolder;
    


    public void SetDialogContent(string Name, string Content)
    {
        if (NameHolder != null)
        {
            NameHolder.text = Name;
        }

        if (TextHolder != null)
        {
            TextHolder.text = Content;
        }
    }
    public void CloseDialogBox()
    {
        Debug.unityLogger.Log("Close dialog pushed");
        OnDialogClose?.Invoke(this,null);
        Destroy(gameObject);
    }
}
