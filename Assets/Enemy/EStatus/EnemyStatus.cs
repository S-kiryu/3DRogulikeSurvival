using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private StatusSettings _statusSettings;
    [SerializeField] private DropXp _dropXp;

    public float CurrentHealth { get; private set; }
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
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth+"のダメージを受けた");

        if (CurrentHealth <= 0)
        {
            _dropXp.Drop();
            Debug.Log("しんだぁぁぁぁ！");
            Die();
        }
    }

    private void Die() 
    {
        Destroy(gameObject);
    }
}
