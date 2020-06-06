using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Pickup : Interaction
{
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
        //GameManager.inv.AddItem(GameManager.tilemapManager.PopObjectAtPosition(transform.position, parent) as Item, 1);
        Destroy(gameObject);
        GameManager.inv.AddItem(GameManager.tilemapManager.PopObjectAtPosition(transform.position, parent) as Item, 1);
        parent.Interupt();
    }

    public override void SetParent(InteractableObject _parent)
    {
        base.SetParent(_parent);
    }
}
