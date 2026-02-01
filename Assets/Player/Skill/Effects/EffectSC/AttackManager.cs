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
        foreach (var attack in FindObjectsByType<AttakBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            attacks.Add(attack);
            timers[attack] = 0f;
        }
    }

    void Update()
    {
        if (LevelUpManager.Instance != null &&
            LevelUpManager.Instance.IsPaused)
            return;

        // UŒ‚ˆ—
        foreach (var attack in attacks)
        {
            // Effect ‚ª ON ‚Ì‚¾‚¯UŒ‚
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
