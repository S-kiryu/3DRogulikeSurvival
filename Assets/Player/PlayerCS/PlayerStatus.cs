using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private LevelUpManager _levelUpManager;
    [SerializeField] private StatusSettings _settings;
    [SerializeField] private GameObject _katanaObject;
    public GameObject KatanaObject => _katanaObject;
    //現在のレベル
    public int Level { get; private set; } = 1;
    //現在の経験値
    public int CurrentExp { get; private set; } = 0;
    public int ExpToNextLevel => Level * 10;

    public int CurrentHealth { get; private set; }

    //現在の攻撃力
    public float AttackPower { get; private set; }

    private void Awake()
    {
        CurrentHealth = _settings.MaxHealth;

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
        Debug.Log($"現在の攻撃力：{AttackPower}");
    }

    public void AddExp(int amount)
    {
        CurrentExp += amount;

        //レベルアップ判定
        if (CurrentExp >= ExpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        CurrentExp -= ExpToNextLevel;
        Level++;
        Debug.Log($"Level Up!!! 現在のレベル: {Level}");
        if (_levelUpManager != null)
        {
            _levelUpManager.OnLevelUp();
        }
    }

    public bool IsAlive => CurrentHealth > 0;
}
