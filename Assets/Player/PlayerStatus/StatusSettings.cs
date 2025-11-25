using UnityEngine;

[CreateAssetMenu(fileName = "StatusSettings", menuName = "Player/Status Settings")]
public class StatusSettings : ScriptableObject
{
    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth => _maxHealth;
    [SerializeField] private int _maxStamina = 50;
    public int MaxStamina => _maxStamina;
    [SerializeField] private int _maxMana = 30;
    public int MaxMana => _maxMana;
}
