using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC_Fight : Interaction
{
    public void Start()
    {
        actionName = "Fight";
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Fighting");
    }
}
