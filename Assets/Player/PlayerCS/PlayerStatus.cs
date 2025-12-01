using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public StatusSettings _settings;

    public int CurrentHealth { get; private set; }

    // š ’Ç‰ÁFŒ»Ý‚ÌUŒ‚—ÍiƒXƒLƒ‹‚Å‘‚¦‚éj
    public float AttackPower { get; private set; }

    private void Awake()
    {
        CurrentHealth = _settings.MaxHealth;

        // š ‰ŠúUŒ‚—Í‚ð ScriptableObject ‚©‚ç“Ç‚ÝŽæ‚é
        AttackPower = _settings.AttackPower;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public void AddAttackPower(float amount)
    {
        AttackPower += amount;
        Debug.Log($"UŒ‚—Í‚ª {amount} ‘‰ÁI Œ»Ý‚ÌUŒ‚—ÍF{AttackPower}");
    }

    public bool IsAlive => CurrentHealth > 0;
}
