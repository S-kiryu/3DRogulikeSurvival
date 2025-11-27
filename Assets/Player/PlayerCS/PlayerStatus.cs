using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private StatusSettings _settings;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        // ScriptableObject‚©‚ç‰Šú’l“Ç‚ÝŽæ‚è
        CurrentHealth = _settings.MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        Debug.Log($"Player HP: {CurrentHealth}");
    }

    public bool IsAlive => CurrentHealth > 0;
}