using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC_Talk : Interaction
{
    [SerializeField]
    private TextAsset dialogue;
    private CanvasGroup dialogueBox;

    private void Start()
    {
        actionName = "Talk";
        dialogueBox = GameManager.dialogueManager.dialogueBox;
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
            GameManager.dialogueManager.StartDialogue(dialogue);
        }
    }

    public override void Interupt()
    {
        base.Interupt();
        StopCoroutine(WalkAndTalk());
        GameManager.dialogueManager.StopDialogue();
    }
}
