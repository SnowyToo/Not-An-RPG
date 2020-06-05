using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gather : Interaction
{
    // Start is called before the first frame update
    private void Start()
    {
        actionName = "Gather";
        requiresMovement = true;
    }

    public override void Act()
    {
        Debug.Log("Gathered");
    }
}
