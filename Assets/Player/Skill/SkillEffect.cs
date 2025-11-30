using UnityEngine;

public abstract class SkillEffect : ScriptableObject
{
    public abstract void Apply(PlayerStatus player);
}
