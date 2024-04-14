using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Classes.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Classes
{
    public class TasksManagerBehaviour : MonoBehaviour
    {
        public static TasksManagerBehaviour Instance { get; private set; }
        public List<Task> Tasks { get; } = new();
        public Dictionary<GameObject, Task> TasksOfComputers { get; } = new Dictionary<GameObject, Task>();
        public event EventHandler<Task> OnTaskFinished;
        public event EventHandler OnTaskWindowClosed; 
        public int tasksToGenerate = 3;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.Log("Instance already exists!");
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            var allComputers = GameObject.FindGameObjectsWithTag("ComputerWithTask");
            var computers = new List<GameObject>();
            for (int i = 0; i < tasksToGenerate; i++)
            {
                var computer = allComputers[UnityEngine.Random.Range(0, allComputers.Length)];
                while (computers.Contains(computer))
                {
                    computer = allComputers[UnityEngine.Random.Range(0, allComputers.Length)];
                }
                computer.SetActive(true);
                computers.Add(computer);
            }
            foreach (var computer in allComputers.Where(c => !computers.Contains(c)))
            {
                computer.SetActive(false);
            }
            foreach (var computer in computers)
            {
                if (computer.name.Contains("Eula"))
                {
                    Tasks.Add(new EulaTask(new DateTime().AddMinutes(2)));
                }
                else
                {
                    Tasks.Add(new FoldersTask(new DateTime().AddMinutes(2)));
                }
                /*
                switch (UnityEngine.Random.value < 0.5 ? 0 : 1)
                {
                    case 0:
                        Tasks.Add(new EulaTask(new DateTime().AddMinutes(2)));
                        break;
                    case 1:
                        Tasks.Add(new FoldersTask(new DateTime().AddMinutes(2)));
                        break;
                }
                */
                TasksOfComputers.Add(computer, Tasks[^1]);
            }
            var text = GetComponent<TextMeshProUGUI>();
            text.text = "Tasks: " + Tasks.Count;
            foreach (var task in Tasks)
            {
                text.text += "\n: " + task.Name + "(" + (task.Completed ? "Completed" : "Not completed") + ")";
            }
            this.OnTaskFinished += (sender, task) =>
            {
                text.text = "Tasks: " + Tasks.Count(t => t.Completed) + " / " + Tasks.Count;
                foreach (var t in Tasks)
                {
                    text.text += "\n: " + t.Name + "(" + (t.Completed ? "Completed" : "Not completed") + ")";
                }
            };
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void TaskFinished(Task task)
        {
            task.Completed = true;
            OnTaskFinished?.Invoke(this, task);
        }
        
        public void TaskWindowClosed()
        {
            OnTaskWindowClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
