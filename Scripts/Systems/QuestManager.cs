using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public List<QuestPair> questList = new List<QuestPair>();

    public void Start()
    {
        //Setup quests
        foreach (QuestPair q in questList)
        {
            q.key = q.value.referal;
            q.id = q.value.ID;
        }
    }

    //Finds a quest by its name.
    public Quest FindQuest(string name)
    {
        foreach(QuestPair q in questList)
        {
            if (q.key == name)
                return q.value;
        }
        return null;
    }

    public void AdvanceQuest(Quest q)
    {
        if(q.curState == 0)
        {
            StartQuest(q);
        }
        q.curState++;
    }

    public bool HasRequirements(Quest q)
    {
        foreach (Skill s in q.requirements)
        {
            if (GameManager.stats.GetLevel(s) < s.Level)
                return false;
        }
        return true;
    }

    public void StartQuest(Quest q)
    {
        //Start quest logic
    }

    public void FinishQuest(Quest q)
    {
        GainRewards(q);
        //Finish Quest Logic
        q.finished = true;
    }

    public void GainRewards(Quest q)
    {
        GameManager.stats.questPoints += q.questPoints;
        foreach (Skill s in q.expRewards)
        {
            foreach (Skill t in GameManager.stats.skills)
            {
                if (s.Equals(t))
                {
                    t.Exp += s.Exp;
                }
            }
        }
    }
}

[System.Serializable]
public class QuestPair
{
    public int id;
    public string key;
    public Quest value;

    public QuestPair(int _id, string _key, Quest _value)
    {
        id = _id;
        key = _key;
        value = _value;
    }
}
