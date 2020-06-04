using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Interaction : MonoBehaviour
{
    public string actionName;
    [HideInInspector]
    public InteractableObject parent;

    public virtual void Interact()
    {
        //Start Interaction
    }

    public virtual void Interupt()
    {
        //Stop Interaction
    }

    public virtual void SetParent(InteractableObject _parent)
    {
        parent = _parent;
    }
}
