using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        CurrentHealth = MaxHealth;
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
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth + "のダメージを受けた");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // スコア加算
        if (_scoreManager != null)
        {
            _scoreManager.ScoreUP(_scoreReward);
        }

        // コイン加算
        if (PlayerStatusManager.Instance != null)
        {
            PlayerStatusManager.Instance.AddMoney(_coinReward);
            Debug.Log($"{_coinReward}コイン獲得！");
        }
        else
        {
            Debug.LogError("PlayerStatusManager.Instance が null です");
        }

        // XPドロップ
        _dropXp.Drop();

        Debug.Log("敵が倒された！");

        OnDead?.Invoke();
        Destroy(gameObject);
    }
}