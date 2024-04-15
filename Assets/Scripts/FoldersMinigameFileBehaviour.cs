using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(BoxCollider2D))]
public class FoldersMinigameFileBehaviour : MonoBehaviour, IDragHandler
{
    private Color _color;
    public bool ShowsTrueColor { get; set; }
    public AudioSource audioSource;
    
    public Color Color
    {
        set
        {
            _color = value;
            gameObject.GetComponent<Image>().color = value;
        }
        get
        {
            if (ShowsTrueColor)
            {
                return _color;
            }
            else
            {
                return FoldersBehaviour.SupportedColors[UnityEngine.Random.Range(0, FoldersBehaviour.SupportedColors.Count)];
            }
        }
    }

    public void Start()
    {
        gameObject.GetComponent<Image>().color = Color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag: " + eventData.delta.x + " " + eventData.delta.y);
        // todo: do not let it out of the container
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        var parentRectTransform = transform.parent.GetComponent<RectTransform>();
        var myRectTransform = transform.GetComponent<RectTransform>();
        var minX = -1 * parentRectTransform.rect.width / 2 + myRectTransform.rect.width;
        var maxX = parentRectTransform.rect.width / 2 - myRectTransform.rect.width;
        var minY = -1 * parentRectTransform.rect.height / 2 + myRectTransform.rect.height;
        var maxY = parentRectTransform.rect.height / 2 - myRectTransform.rect.height;
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, minX, maxX),
            Mathf.Clamp(transform.localPosition.y, minY, maxY),
            transform.localPosition.z
        );
        if (CollidingWithFolderOfTheSameColor())
        {
            transform.GetComponentInParent<FoldersBehaviour>().OnFileInFolder();
            gameObject.SetActive(false);
            audioSource.Play();
        }
    }

    public bool CollidingWithFolderOfTheSameColor()
    {
        if (!ShowsTrueColor) { return false; }
        var collided = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), collided);
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
