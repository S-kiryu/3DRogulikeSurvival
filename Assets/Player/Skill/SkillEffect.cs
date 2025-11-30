using UnityEngine;

public abstract class SkillEffect : ScriptableObject
{
    //スキルの抽象クラス
    public abstract void Apply(PlayerStatus player);
}
