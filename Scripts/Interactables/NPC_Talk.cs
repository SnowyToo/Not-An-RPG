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

        GameManager.dialogueManager.StartDialogue(dialogue);
    }

    public override void Interupt()
    {
        base.Interupt();
        GameManager.dialogueManager.StopDialogue();
    }
}
