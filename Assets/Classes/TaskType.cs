using System;
using UnityEngine;

namespace Classes
{
    public enum TaskType
    {
        Eula,
        Folders
    }

    public static class TaskTypeExtensions
    {
        public static GameObject Prefab(this TaskType taskType)
        {
            switch (taskType)
            {
                case TaskType.Eula:
                    return Resources.Load("Prefab/Minigames/EULA", typeof(GameObject)) as GameObject;
                case TaskType.Folders:
                    return Resources.Load("Prefab/Minigames/Folders", typeof(GameObject)) as GameObject;
                default:
                    return null;
            }
        }
    }
}
