using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

namespace DefaultNamespace
{
    public class VirusMinigameBehaviour : MonoBehaviour, IMinigame
    {
        public int characterNumber = 5;
        public Task Task { get; set; }
        
        private List<char> charactersToPress = new();
        private int charactersPressed = 0;

        public void Start()
        {
            charactersPressed = 0;
            charactersToPress.Clear();
            for (var i = 0; i < characterNumber; i++)
            {
                charactersToPress.Add((char)UnityEngine.Random.Range(97, 123));
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