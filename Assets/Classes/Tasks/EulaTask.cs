using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class EulaTask : Task
{
    public EulaTask() : base("EULA")
    {
        this.HintReceived = true;
    }

    public override GameObject Prefab()
    {
        return Resources.Load("Prefab/Minigames/EULA", typeof(GameObject)) as GameObject;
    }

    public override string HintText()
    {
        // Does not need hints
        return "";
    }
}
