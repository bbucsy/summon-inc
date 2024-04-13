using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Classes
{
    public class TaskList : MonoBehaviour
    {

        public List<Task> tasks;
        
        // Start is called before the first frame update
        void Start()
        {
            var text = GetComponent<TextMeshProUGUI>();
            // text.SetText("Test 1");
            var allTasks = GameObject.FindGameObjectsWithTag("Task");
            text.text = "Tasks remaining: " + allTasks.Length;
            foreach (var allTask in allTasks)
            {
                var taskComponent = allTask.GetComponent<Task>();
                if (taskComponent.Completed)
                {
                    text.text += "\n" + taskComponent.Name + " - Completed";
                }
                else
                {
                    text.text += "\n" + taskComponent.Name + " - Incomplete";
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
