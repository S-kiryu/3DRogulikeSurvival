using System.Collections.Generic;
using UnityEngine;

public class AreaMove : AttakBase
{
    [SerializeField] private float _coolTime = 1f;
    public override float CoolTime => _coolTime;

    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private LayerMask enemyLayer;

    public override EffectType Type => EffectType.AreaAttack;


    //強化用メソット

    public void AddAttackDamageUp(float value) 
    {
        _attackDamage += value;
    }

    public void AddRadius() 
    {
        _attackRadius += 0.5f;
    }

    public void ReduceCoolTime(float value)
    {
        _coolTime = Mathf.Max(0.1f, _coolTime - value);
    }

    public override void Attack()
    {
        Debug.Log("Area Attack!");

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            _attackRadius
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

            enemy.TakeDamage(_playerStatus.AttackPower / 0.5f+_attackDamage);
            Debug.Log(_playerStatus.AttackPower / 0.5f + "のエリア攻撃");
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}
