using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private LevelUpManager _levelUpManager;
    private PlayerRuntimeStatus _status;
    public PlayerEffectController effectController;
    public event Action OnDead;

    private void Awake()
    {
        _status = PlayerStatusManager.Instance.Status;
    }

    public int Level => _status.Level;
    public int CurrentExp => _status.CurrentExp;
    public int CurrentHealth => _status.CurrentHealth;
    public float AttackPower => _status.AttackPower;

    //UŒ‚‚ğó‚¯‚é
    public void TakeDamage(int amount)
    {
        _status.CurrentHealth -= amount;
        _status.CurrentHealth = Mathf.Max(_status.CurrentHealth, 0);

        Debug.Log($"Player HP: {_status.CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }

    //UŒ‚—Íã¸
    public void AddAttackPower(float amount)
    {
        _status.AttackPower += amount;
        Debug.Log($"Œ»İ‚ÌUŒ‚—ÍF{_status.AttackPower}");
    }


    /// <summary>
    /// XP‚Ì‰ÁZˆ—
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(int amount)
    {
        Debug.Log("GetXp");
        _status.CurrentExp += amount;

        while(_status.CurrentExp >= PlayerStatusManager.Instance.ExpToNextLevel)
        {
            LevelUp();
        }
    }

    //ƒŒƒxƒ‹UP
    private void LevelUp()
    {
        _status.CurrentExp -= PlayerStatusManager.Instance.ExpToNextLevel;
        _status.Level++;

        Debug.Log($"Level Up!!! Œ»İ‚ÌƒŒƒxƒ‹: {_status.Level}");

        _levelUpManager?.OnLevelUp();
    }

    public bool IsAlive => _status.CurrentHealth > 0;
}
