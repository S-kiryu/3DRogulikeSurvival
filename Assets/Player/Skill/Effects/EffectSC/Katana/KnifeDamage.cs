using UnityEngine;

public class KnifeDamage : AttackBase
{
    [SerializeField] private float _coolTime = 0.1f;
    public override float CoolTime => _coolTime;
    [SerializeField] private float _knifeDamage = 15f;
    public override EffectType Type => EffectType.KnifeDamage;

    //強化用メソッド
    public void AddKnifeDamageUp(float value)
    {
        _knifeDamage += value;
    }

    public override void Attack()
    {
        Debug.Log("ナイフダメージ！！！");
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            1f
        );
        //範囲内にいる敵にダメージを与える
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponentInParent<EnemyStatus>();
            if (enemy == null)
            {
                // 敵ではない場合はスキップ
                continue;
            }
            enemy.TakeDamage(_playerStatus.AttackPower + _knifeDamage);
            Debug.Log(_playerStatus.AttackPower + _knifeDamage + "のナイフダメージ攻撃");
        }
    }
}
