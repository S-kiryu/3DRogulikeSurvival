using System.Collections.Generic;
using UnityEngine;

public class AreaMove : AttakBase
{
    [SerializeField] private float coolTime = 1f;
    public override float CoolTime => coolTime;
    public override EffectType Type => EffectType.AreaAttack;

    [SerializeField] float attackRadius = 2f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int attackPower = 10;

    private readonly HashSet<EnemyStatus> hitEnemies = new();

    public override void Attack()
    {
        Debug.Log("Area Attack!");

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRadius   // Å© LayerMask ÇàÍíUäOÇ∑
        );

        Debug.Log($"Hits count = {hits.Length}");

        foreach (var hit in hits)
        {
            Debug.Log($"Hit object: {hit.name}, layer={hit.gameObject.layer}");

            var enemy = hit.GetComponentInParent<EnemyStatus>();
            if (enemy == null)
            {
                Debug.Log("EnemyStatus not found in parent");
                continue;
            }

            Debug.Log("EnemyStatus FOUND");
            enemy.TakeDamage(attackPower);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
#endif
}
