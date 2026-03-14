using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance { get; private set; }

    [SerializeField] private PlayerEffectController effectController;

    private readonly List<IAttack> attacks = new();
    private readonly Dictionary<IAttack, float> timers = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //AttackBase ‚©‚çŒÄ‚Î‚ê‚é“o˜^—pƒƒ\ƒbƒh
    public void RegisterAttack(IAttack attack)
    {
        if (attacks.Contains(attack))
            return;

        attacks.Add(attack);
        timers[attack] = 0f;
    }


    //UŒ‚ˆ—
    void Update()
    {
        if (LevelUpManager.Instance != null &&
            LevelUpManager.Instance.IsPaused)
            return;

        foreach (var attack in attacks)
        {
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
