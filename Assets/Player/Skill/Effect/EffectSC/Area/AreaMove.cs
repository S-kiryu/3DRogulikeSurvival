using System.Collections.Generic;
using UnityEngine;

public class AreaMove : AttakBase
{
    [SerializeField] private float _coolTime = 1f;
    public override float CoolTime => _coolTime;
    public override EffectType Type => EffectType.AreaAttack;

    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private LayerMask enemyLayer;

    private readonly HashSet<EnemyStatus> hitEnemies = new();


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
                continue;
            }

            enemy.TakeDamage(_playerStatus.AttackPower / 0.5f);
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
