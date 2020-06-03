using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueHelper
{
    private static bool Compare(int n1, int n2, string compare)
    {
        switch(compare)
        {
            case "<=":
                return n1 <= n2;
            case "==":
                return n1 == n2;
            case ">=":
                return n1 >= n2;
            case "!=":
                return n1 != n2;
        }
        return false;
    }

    public static bool CheckCondition(string c)
    {
        string compare = string.Empty;
        if (c.Contains("="))
        {
            compare = c.Substring(c.LastIndexOf("=") - 1, 2);
        }

        if (c.Contains("Quest"))
        {
            Quest q = GetQuestFromCommand(c);
            //What to do with quest
            if (c.Contains(".state") || c.Contains(".progress"))
            {
                string stateNum = c.Substring(c.LastIndexOf("=") + 1);
                if (Int32.TryParse(stateNum, out int result))
                {
                    return Compare(q.curState, result, compare);
                }
            }
            else if(c.Contains("canStart"))
            {
                return GameManager.questManager.HasRequirements(q);
            }
        }
        return false;
    }

    public static Quest GetQuestFromCommand(string c)
    {
        string questName = c.Substring(c.IndexOf('(') + 1, c.IndexOf(')') - c.IndexOf('(') - 1);
        return GameManager.questManager.FindQuest(questName);
    }
}
