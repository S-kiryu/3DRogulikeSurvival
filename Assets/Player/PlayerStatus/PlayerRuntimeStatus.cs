[System.Serializable]
public class PlayerRuntimeStatus
{
    public int Level = 1;
    public int CurrentExp = 0;

    public int MaxHealth;
    public int CurrentHealth;
    public float AttackPower;

    public PlayerRuntimeStatus(StatusSettings settings)
    {
        MaxHealth = settings.MaxHealth;
        CurrentHealth = settings.MaxHealth;
        AttackPower = settings.AttackPower;
    }
}
