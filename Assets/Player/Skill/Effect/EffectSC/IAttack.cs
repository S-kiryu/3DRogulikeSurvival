public interface IAttack
{
    EffectType Type { get; }
    float CoolTime { get; }
    void Attack();
}
