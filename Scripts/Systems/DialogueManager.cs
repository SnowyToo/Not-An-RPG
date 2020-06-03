using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public CanvasGroup dialogueBox;
    [SerializeField]
    private TextMeshProUGUI characterBox;
    [SerializeField]
    private TextMeshProUGUI textBox;

    [SerializeField]
    private string[] text;
    private int curLine;
    private Dictionary<string, int> keyLines;
    private bool isTalking;
    private bool isAwaitingInput;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isTalking && !isAwaitingInput)
        {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(TextAsset file)
    {
        GameManager.uiManager.EnableGroup(dialogueBox);
        isTalking = true;
        keyLines = new Dictionary<string, int>();
        SetUpPile(file);
        curLine = -1;
        AdvanceDialogue();
    }

    public void AdvanceDialogue()
    {
        curLine++;
        if (curLine >= text.Length)
        {
            return;
        }
        string line = text[curLine];
        //DO LOGIC
        if (line.Contains("["))
        {
            string comm = line.Replace("[", "").Replace("]", "").Replace("\r", "");
            InterpretCommand(comm);
            AdvanceDialogue();
        }
        else if(line.Replace("\r", "") == string.Empty || line.Contains("---") || line.StartsWith("//")) //Skip lines
        {
            AdvanceDialogue();
        }
        else //Print line
        {
            textBox.text = line;
        }
    }

    private void InterpretCommand(string command)
    {
        
        if (command.StartsWith("=>")) //Go to tab
        {
            command = command.Replace("=>", "").Replace(" ","");
            curLine = keyLines[command];
        }
        else if(command.StartsWith("if")) //Conditional
        {
            string[] parts = command.Split(','); //Splits condition from reaction
            string condition = parts[0].Replace("if ", "").Replace(" ", ""); //Condition part
            string action = parts[1].Replace("do ", "").Replace("then ", "").Replace(" ", ""); //Reaction part
            string[] conds = condition.Split('&');

            //If all conditions are met, you the action will complete
            foreach(string c in conds)
            {
                if (!DialogueHelper.CheckCondition(c))
                {
                    return;
                }
            }
            InterpretCommand(action);
        }
        else if(command.StartsWith("Quest")) //QuestLogic
        {
            Quest q = DialogueHelper.GetQuestFromCommand(command);
            if(command.Contains(".Finish"))
            {
                GameManager.questManager.FinishQuest(q);
            }
            else if(command.Contains(".Advance") || command.Contains(".Start"))
            {
                GameManager.questManager.AdvanceQuest(q);
            }
        }
    }

    public void StopDialogue()
    {
        GameManager.uiManager.DisableGroup(dialogueBox);
        isTalking = false;
    }

    public void SetUpPile(TextAsset file)
    {
        text = file.ToString().Replace("\t", "").Split('\n');

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].Contains("---"))
            {
                keyLines.Add(text[i].Replace("-", "").Replace(" ", "").Replace("\r", ""), i);
            }
        }
    }
}
