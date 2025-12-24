using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] PlayerEffectController effectController;

    private readonly List<IAttack> attacks = new();
    private readonly Dictionary<IAttack, float> timers = new();

    void Start()
    {
        // 最初に全部集める
        foreach (var attack in FindObjectsByType<AttakBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            attacks.Add(attack);
            timers[attack] = 0f;
        }

        //effectController.SetEffect(EffectType.AreaAttack, true);
    }

    void Update()
    {
        //// デバッグ
        //foreach (var attack in attacks)
        //{
        //    bool active = effectController.GetEffectActive(attack.Type);

        //    if (!active) continue;

        //    timers[attack] += Time.deltaTime;

        //    if (timers[attack] >= attack.CoolTime)
        //    {
        //        Debug.Log("Calling Attack()");
        //        timers[attack] = 0f;
        //        attack.Attack();
        //    }
        //}

        // 攻撃処理
        foreach (var attack in attacks)
        {
            // Effect が ON の時だけ攻撃
            Debug.Log($"{attack.Type} active = {effectController.GetEffectActive(attack.Type)}");
            if (!effectController.GetEffectActive(attack.Type))
                continue;

            timers[attack] += Time.deltaTime;

            if (timers[attack] >= attack.CoolTime)
            {
                timers[attack] = 0f;
                attack.Attack();
            }
        }
    }
}
