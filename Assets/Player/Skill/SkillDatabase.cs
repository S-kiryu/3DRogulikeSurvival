using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillDatabase", menuName = "Game/Skill Database")]
public class SkillDatabase : ScriptableObject
{
    //スキルのリスト
    public List<Skill> skills;
}
