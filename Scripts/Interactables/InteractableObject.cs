using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracting;

    public List<Interaction> interactions = new List<Interaction>();

    public Interaction current;

    public void Awake()
    {
        foreach(Interaction i in interactions)
        {
            i.parent = this;
        }
    }

    public void Interact(Interaction action)
    {
        current = action;
        action.Interact();
        isInteracting = true;
    }

    public void Interupt()
    {
        if(isInteracting)
        {
            current.Interupt();
            current = null;
            isInteracting = false;
        }
    }
}
