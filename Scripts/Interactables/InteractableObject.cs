﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracting;

    public List<Interaction> interactions = new List<Interaction>();

    public Interaction current;

    public virtual void Awake()
    {
        foreach (Interaction i in interactions)
        {
            i.SetParent(this);
        }
    }

    public void Start()
    {
        GameManager.tilemapManager.SetObjectAtPosition(gameObject.transform.position, this);
    }

    public void Interact(Interaction action)
    {
        isInteracting = true;
        current = action;
        action.Interact();
    }

    public void Interupt()
    {
        if(isInteracting)
        {
            isInteracting = false;
            current.Interupt();
            current = null;
        }
    }
}
