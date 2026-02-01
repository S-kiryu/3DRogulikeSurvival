using UnityEngine;

public class KatanaMove : AttakBase
{
    [SerializeField] private float _coolTime = 1f;
    public override float CoolTime => _coolTime;

    public override EffectType Type => EffectType.Katana;

    public override void Attack() 
    {

    }
}
