using System;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private StatusSettings _statusSettings;
    [SerializeField] private DropXp _dropXp;
    [SerializeField] private int _coinReward = 1;
    [SerializeField] private int _scoreReward = 100;

    private ScoreManager _scoreManager;

    public float CurrentHealth { get; private set; }
    public int MaxHealth => _statusSettings.MaxHealth;
    public float AttackPower => _statusSettings.AttackPower;

    public event Action OnDead;

    private void Awake()
    {
        ResetEnemy();
    }

    private void Start()
    {
        _scoreManager = ScoreManager.instance;

        if (_scoreManager == null)
        {
            Debug.LogError("ScoreManager.instance が null です");
        }
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log(damage + "のダメージを受けた");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // XPドロップ
        _dropXp.Drop();

        if (EnemyPoolManager.Instance == null)
        {
            Debug.LogError("EnemyPoolManager.Instance が null");
            return;
        }

        var identity = GetComponent<EnemyIdentity>();
        if (identity == null)
        {
            Debug.LogError("EnemyIdentity が null");
            return;
        }

        // ⭐ イベント発火
        OnDead?.Invoke();

        // ⭐ イベント購読を全クリア（重要！）
        OnDead = null;

        EnemyPoolManager.Instance.Return(identity.Type, gameObject);
    }

    public void ResetEnemy()
    {
        CurrentHealth = MaxHealth;

        //イベント購読をクリア
        OnDead = null;
    }
}
