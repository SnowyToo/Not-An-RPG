using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Pickup : Interaction
{
    public Item item;

    private void Start()
    {
        actionName = "Pick Up";
        requiresMovement = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && parent.current == this)
        {
            parent.Interupt();
        }
    }

    public override void Act()
    {
        GameManager.inv.AddItem(item, 1);
        Destroy(parent.gameObject);
        parent.Interupt();
    }

    public override void SetParent(InteractableObject _parent)
    {
        base.SetParent(_parent);
        item = _parent as Item;
    }

    public override void Interupt()
    {
        base.Interupt();
        GameManager.dialogueManager.StopDialogue();
    }
}
