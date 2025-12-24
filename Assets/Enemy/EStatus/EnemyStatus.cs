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
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Debug.Log("しんだぁぁぁぁ！");
            Die();
        }
    }

    private void Die() 
    {
        Destroy(gameObject);
    }
}
