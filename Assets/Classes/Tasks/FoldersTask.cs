using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Classes.Tasks
{
    public class FoldersTask : Task
    {
        public int FilesToGenerate { get; set; }

        private readonly string[] _hints = {
            "Hi, I need your immediate assistance with an urgent task related to one of our projects. We've got a crucial client presentation coming up, and it's essential that we ensure everything is meticulously organized. We need to move files into two specific folders, and here's the catch: the files' colors must match the folders' colors for consistency and clarity. I know it sounds a bit meticulous, but it's vital for maintaining our professionalism and ensuring a smooth presentation. Could you please prioritize this task and ensure it's completed accurately and promptly? Your attention to detail and efficiency will be greatly appreciated as we work to impress our client and deliver exceptional results.",
            "Hi, I'm reaching out to you about an urgent task regarding our document management system. We've identified an issue where files are not consistently organized according to our color-coded system. To maintain clarity and efficiency, I need your help in moving files into designated folders where the file colors align with the folders' colors. Your attention to detail in executing this task promptly will contribute to the smooth operation of our document management system and facilitate seamless collaboration among team members. Thank you for your prompt action on this matter.",
            "Hey, I need your assistance with a time-sensitive task concerning our client deliverables. As part of our quality assurance process, we need to organize files for a project review meeting later today. The files must be sorted into two folders, and it's crucial that the colors of the files match the colors of the respective folders. Your meticulousness in executing this task will demonstrate our commitment to excellence and enhance our client's confidence in our work. Please prioritize this task to ensure we meet our deadline and maintain our high standards of performance.",
            "Hello, I have an urgent task related to organizing our project files that requires your immediate attention. We've recently restructured our file system, and it's essential to ensure consistency in file naming and organization. Specifically, I need you to move files into designated folders where the file colors match the folders' colors. This meticulous attention to detail will streamline our workflow and ensure easy access to important documents. Your prompt action on this matter will contribute to the efficiency and professionalism of our team."
        };
        
        public FoldersTask(int filesToGenerate = 2) : base("Folders", true)
        {
            FilesToGenerate = filesToGenerate;
        }

        public override GameObject Prefab()
        {
            return Resources.Load("Prefab/Minigames/Folders", typeof(GameObject)) as GameObject;
        }

        public override string HintText()
        {
            return _hints[Random.Range(0, _hints.Length)];
        }
    }
}
