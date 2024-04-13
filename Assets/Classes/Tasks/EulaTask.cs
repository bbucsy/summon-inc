using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class EulaTask : Task
{
    public EulaTask(DateTime deadline) : base("EULA", TaskType.Eula, deadline)
    {
        
    }
}
