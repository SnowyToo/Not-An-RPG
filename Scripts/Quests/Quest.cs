using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string referal;
    public int ID;

    public bool finished;

    public List<Skill> requirements;

    public List<Skill> expRewards;
    public int questPoints;

    public int curState;
}
