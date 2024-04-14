using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class EulaTask : Task
{
    public EulaTask() : base("EULA", TaskType.Eula)
    {
        // Eula tasks do not need hints, so we set it to true
        HintReceived = true;
    }
}
