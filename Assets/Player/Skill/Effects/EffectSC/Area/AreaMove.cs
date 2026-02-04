using System.Collections.Generic;
using UnityEngine;

public class AreaMove : AttackBase
{
    [SerializeField] private float _coolTime = 1f;
    public override float CoolTime => _coolTime;

    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _areaAttackMultiplier = 0.5f;
    [SerializeField] private GameObject _rangeView;
    [SerializeField] private float _rangeDisplayTime = 0.15f;

    public override EffectType Type => EffectType.AreaAttack;


    //強化用メソッド
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

        StartCoroutine(ShowRange());

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

            enemy.TakeDamage(_playerStatus.AttackPower * _areaAttackMultiplier + _attackDamage);
            Debug.Log(_playerStatus.AttackPower / 2f + "のエリア攻撃");
        }
    }

    private System.Collections.IEnumerator ShowRange()
    {
        if (_rangeView == null) yield break;

        _rangeView.SetActive(true);
        yield return new WaitForSeconds(_rangeDisplayTime);
        _rangeView.SetActive(false);
    }



#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}
