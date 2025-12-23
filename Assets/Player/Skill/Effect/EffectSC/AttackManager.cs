using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] PlayerEffectController effectController;

    private readonly List<IAttack> attacks = new();
    private readonly Dictionary<IAttack, float> timers = new();

    void Start()
    {
        // Å‰‚É‘S•”W‚ß‚é
        foreach (var attack in GetComponents<MonoBehaviour>())
        {
            if (attack is IAttack a)
            {
                attacks.Add(a);
                timers[a] = 0f;
            }
        }

        effectController.SetEffect(EffectType.AreaAttack, true);
    }

    void Update()
    {
        foreach (var attack in attacks)
        {
            bool active = effectController.GetEffectActive(attack.Type);
            Debug.Log($"{attack.Type} active={active}, timer={timers[attack]}");

            if (!active) continue;

            timers[attack] += Time.deltaTime;

            if (timers[attack] >= attack.CoolTime)
            {
                Debug.Log("Calling Attack()");
                timers[attack] = 0f;
                attack.Attack();
            }
        }

        foreach (var attack in attacks)
        {
            // Effect ‚ª ON ‚Ì‚¾‚¯UŒ‚
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
