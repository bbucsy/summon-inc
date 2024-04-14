using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class FoldersMinigameFileBehaviour : MonoBehaviour, IDragHandler
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

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag: " + eventData.delta.x + " " + eventData.delta.y);
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        if (CollidingWithFolderOfTheSameColor())
        {
            transform.GetComponentInParent<FoldersBehaviour>().OnFileInFolder();
            gameObject.SetActive(false);
        }
    }

    public bool CollidingWithFolderOfTheSameColor()
    {
        var collided = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), collided);
        // Debug.Log("Collided: " + collided.Count);
        foreach (var other in collided)
        {
            var folder = other.gameObject.GetComponent<FoldersMinigameFolderBehaviour>();
            if (folder == null) continue;
            if (!folder.Color.Equals(Color)) continue;
            return true;
        }
        return false;
    }
    
}
