using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private LevelUpManager _levelUpManager;
    public PlayerEffectController effectController;

    private PlayerRuntimeStatus status;

    private void Awake()
    {
        status = PlayerStatusManager.Instance.Status;
    }

    public int Level => status.Level;
    public int CurrentExp => status.CurrentExp;
    public int CurrentHealth => status.CurrentHealth;
    public float AttackPower => status.AttackPower;

    public void TakeDamage(int amount)
    {
        status.CurrentHealth -= amount;
        status.CurrentHealth = Mathf.Max(status.CurrentHealth, 0);

        Debug.Log($"Player HP: {status.CurrentHealth}");
    }

    public void AddAttackPower(float amount)
    {
        status.AttackPower += amount;
        Debug.Log($"Œ»Ý‚ÌUŒ‚—ÍF{status.AttackPower}");
    }

    public void AddExp(int amount)
    {
        status.CurrentExp += amount;

        if (status.CurrentExp >= PlayerStatusManager.Instance.ExpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        status.CurrentExp -= PlayerStatusManager.Instance.ExpToNextLevel;
        status.Level++;

        Debug.Log($"Level Up!!! Œ»Ý‚ÌƒŒƒxƒ‹: {status.Level}");

        _levelUpManager?.OnLevelUp();
    }

    public bool IsAlive => status.CurrentHealth > 0;
}
