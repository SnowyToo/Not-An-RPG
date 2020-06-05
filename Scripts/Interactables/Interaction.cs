using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Interaction : MonoBehaviour
{
    public string actionName;
    public bool requiresMovement;
    [HideInInspector]
    public InteractableObject parent;

    public virtual void Interact()
    {
        //Do Interaction
        StartCoroutine(MoveAndAct());
    }

    public IEnumerator MoveAndAct()
    {
        //Move
        if(requiresMovement)
        {
            GameManager.playerMovement.GetPath(parent.transform.position);
            while (!GameManager.playerMovement.IsNextToObject(parent) && parent.isInteracting) //Walk up to the spot
            {
                yield return null;
            }
        }
        if(parent.isInteracting)
        {
            //And Act
            Act();
        }
    }

    public virtual void Act()
    {
        //Action
    }

    public virtual void Interupt()
    {
        //Stop Interaction
        StopCoroutine(MoveAndAct());
    }

    public virtual void SetParent(InteractableObject _parent)
    {
        parent = _parent;
    }
}
