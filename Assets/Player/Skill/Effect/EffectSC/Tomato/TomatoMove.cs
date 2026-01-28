using UnityEngine;

public class Tomatoshot : AttakBase
{
    [SerializeField] private float _coolTime = 0.5f;
    public override float CoolTime => _coolTime;
    public override EffectType Type => EffectType.Tomato;

    public override void Attack() 
    {

        TomatoPool.Instance.Get(transform.position);

    }
}
