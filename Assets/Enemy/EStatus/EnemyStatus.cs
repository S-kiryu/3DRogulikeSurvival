using System;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private StatusSettings _statusSettings;
    [SerializeField] private DropXp _dropXp;
    [SerializeField] private int _coinReward = 1;
    [SerializeField] private int _scoreReward = 100;

    private ScoreManager _scoreManager;
    private EnemyIdentity _identity;
    private bool _isDead = false;

    public float CurrentHealth { get; private set; }
    public int MaxHealth => _statusSettings.MaxHealth;
    public float AttackPower => _statusSettings.AttackPower;
    public bool IsAlive => !_isDead;

    public event Action OnDead;

    private void Awake()
    {
        _identity = GetComponent<EnemyIdentity>();
        _scoreManager = FindFirstObjectByType<ScoreManager>();
        ResetEnemy();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        Debug.Log("敵にダメージ");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("死んだー");

        _scoreManager.ScoreUP(_scoreReward);
        _isDead = true;

        // XPドロップ
        if (_dropXp != null)
        {
            Debug.Log("XPをドロップ");
            _dropXp.Drop();
        }

        // イベント発火
        OnDead?.Invoke();
        OnDead = null;

        // プール返却
        if (EnemyPoolManager.Instance == null)
        {
            Debug.LogError("EnemyPoolManager.Instance が null");
            return;
        }

        if (_identity == null)
        {
            Debug.LogError("EnemyIdentity が null");
            return;
        }

        EnemyPoolManager.Instance.Return(_identity.Type, gameObject);
    }

    public void ResetEnemy()
    {
        CurrentHealth = MaxHealth;
        _isDead = false;
        OnDead = null;
    }
}