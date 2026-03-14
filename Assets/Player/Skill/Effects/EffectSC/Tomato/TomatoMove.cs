using UnityEngine;

public class Tomatoshot : AttackBase
{
    [SerializeField] private float _coolTime = 0.5f;

    [Header("永続ステータス")]
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 10f;

    public override float CoolTime => _coolTime;
    public override EffectType Type => EffectType.Tomato;

    // ステータスアップ系メソッド
    public void AddDamage(float amount)
    {
        _damage += amount;
        Debug.Log("とまとのダメージアップ" + _damage);
    }

    public void AddSpeed(float amount)
    {
        _speed += amount;
    }

    public void AddLifeTime(float amount)
    {
        _lifeTime += amount;
    }

    // 攻撃処理
    public override void Attack()
    {
        Quaternion rot = Quaternion.LookRotation(transform.forward);
        GameObject obj = TomatoPool.Instance.Get(transform.position, rot);

        var tomato = obj.GetComponent<TomatoAttack>();
        tomato.Initialize(_damage, _speed, _lifeTime);
    }
}
