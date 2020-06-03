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
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Gathered");
    }
}
