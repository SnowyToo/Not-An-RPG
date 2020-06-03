using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Examine : Interaction
{
    public string examineText;

    public void Start()
    {
        actionName = "Examine";
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log(examineText);
        parent.Interupt();
    }
}
