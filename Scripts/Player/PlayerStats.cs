using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    //Combat Skill
    public List<Skill> skills;

    //Misc
    public int questPoints;
    public int gold;

    public PlayerStats()
    {
        skills = new List<Skill>();

        skills.Add(new Skill(Skill.SkillType.HITPOINTS, 10));
        skills.Add(new Skill(Skill.SkillType.MELEE));
        skills.Add(new Skill(Skill.SkillType.MAGIC));
        skills.Add(new Skill(Skill.SkillType.DEFENSE));
        skills.Add(new Skill(Skill.SkillType.ARCHERY));
        skills.Add(new Skill(Skill.SkillType.MORTIS));

        skills.Add(new Skill(Skill.SkillType.ESSENTIA));

        questPoints = 0;
        gold = 0;
    }

    public int GetLevel(Skill toCompare)
    {
        foreach(Skill s in skills)
        {
            if (s.Equals(toCompare))
                return s.Level;
        }
        throw new Exception("No such skill found!");
    }

}

[Serializable]
public class Skill
{
    public enum SkillType { HITPOINTS, MELEE, MAGIC, DEFENSE, ARCHERY, MORTIS, ESSENTIA }

    [SerializeField]
    private SkillType type;
    public SkillType Type
    {
        get { return type; }
    }

    [SerializeField]
    private int level;
    public int Level
    {
        get { return level; }
    }

    [SerializeField]
    private int exp;
    public int Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            while (exp >= GetExpForLevel(level+1))
            {
                level++;
                if(GameManager.uiManager != null)
                    GameManager.uiManager.UpdateStats();
            }
            UpdateExpTilNextLevel();
        }
    }

    private int expTilNextLevel;
    public int ExpLeft
    {
        get { return expTilNextLevel; }
    }

    public Skill()
    {
        Exp = 0;
    }

    public Skill(SkillType _name)
    {
        type = _name;
        Exp = 0;
    }

    public Skill(SkillType _name, int _level)
    {
        type = _name;
        level = _level;
        exp = GetExpForLevel(_level);
    }

    public void UpdateExpTilNextLevel()
    {
        expTilNextLevel = exp - GetExpForLevel(Level+1);
    }

    public int GetExpForLevel(int level)
    {
        return (int)(75 * (Math.Pow(2, (level - 1) * 0.12) - 1) / (1 - Math.Pow(2, -0.12)));
    }

    public bool Equals(Skill s)
    {
        return (s.type == type);
    }
}
