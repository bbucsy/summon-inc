using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class VirusBehaviour : MonoBehaviour, IMinigame
    {
        private string characters = "abcdefghijklmnopqrstuvwxyz";
        private List<char> charactersToPress = new();
        private int charactersPressed = 0;
        
        // publicly settable in the editor
        public int characterNumber = 10;
        public Task Task { get; set; }
        public TextMeshProUGUI text;
        public GameObject image;
        public AudioSource audioSource;

        public void Start()
        {
            text.text = "";
            charactersPressed = 0;
            charactersToPress.Clear();
            
            if (Task.HintReceived)
            {
                text.gameObject.SetActive(true);
                image.SetActive(false);
                for (var i = 0; i < characterNumber; i++)
                {
                    charactersToPress.Add(characters[UnityEngine.Random.Range(0, characters.Length)]);
                }
            }
            else
            {
                text.gameObject.SetActive(false);
                image.SetActive(true);
            }
        }

        public void OnWindowClosed()
        {
            FindFirstObjectByType<TasksManagerBehaviour>().WindowClosed();
            Destroy(this.gameObject);
        }
        
        public void Update()
        {
            if (charactersToPress.Count == 0)
            {
                return;
            }
            
            if (charactersPressed == characterNumber)
            {
                FindFirstObjectByType<TasksManagerBehaviour>().TaskFinished(Task);
                Destroy(this.gameObject);
                return;
            }
            text.text = $"[{charactersToPress[charactersPressed]}]";
            
            if (Input.anyKeyDown)
            {
                if (Input.inputString.Length > 0)
                {
                    if (Input.inputString[0] == charactersToPress[charactersPressed])
                    {
                        audioSource.Play();
                        charactersPressed++;
                    }
                }
            }
        }
    }
}