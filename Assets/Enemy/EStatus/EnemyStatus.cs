using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private StatusSettings _statusSettings;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => _statusSettings.MaxHealth;
    public float AttackPower => _statusSettings.AttackPower;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// ƒ_ƒ[ƒW‚ğó‚¯‚é
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Debug.Log($"Enemy damaged: {damage}");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die() 
    {
        Destroy(gameObject);
    }
}
