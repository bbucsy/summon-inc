using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using TMPro;
using UnityEngine;

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

        public void Start()
        {
            text.text = "";
            charactersPressed = 0;
            charactersToPress.Clear();
            for (var i = 0; i < characterNumber; i++)
            {
                charactersToPress.Add(characters[UnityEngine.Random.Range(0, characters.Length)]);
            }
        }

        public void OnWindowClosed()
        {
            FindFirstObjectByType<TasksManagerBehaviour>().TaskWindowClosed();
            Destroy(this.gameObject);
        }
        
        public void Update()
        {
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
                        charactersPressed++;
                    }
                }
            }
        }
    }
}