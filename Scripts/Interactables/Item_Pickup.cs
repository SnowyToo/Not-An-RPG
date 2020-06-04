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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && parent.current == this)
        {
            parent.Interupt();
        }
    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(WalkAndTalk());
    }

    public override void SetParent(InteractableObject _parent)
    {
        base.SetParent(_parent);
        item = _parent as Item;
    }

    //Walk to NPC and talk to it
    private IEnumerator WalkAndTalk()
    {
        GameManager.playerMovement.GetPath(parent.transform.position);
        while (!GameManager.playerMovement.MoveToInteract(parent) && parent.isInteracting) //Walk up to the spot
        {
            yield return null;
        }
        if (parent.isInteracting)
        {
            GameManager.inv.AddItem(item, 1);
            Destroy(parent.gameObject);
        }
    }

    public override void Interupt()
    {
        base.Interupt();
        StopCoroutine(WalkAndTalk());
        GameManager.dialogueManager.StopDialogue();
    }
}
