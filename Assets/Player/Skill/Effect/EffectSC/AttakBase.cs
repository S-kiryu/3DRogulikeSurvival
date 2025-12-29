using UnityEngine;

public abstract class AttakBase : MonoBehaviour, IAttack
{
    public abstract EffectType Type { get; }

    public abstract float CoolTime { get; }

    public abstract void Attack();
}
