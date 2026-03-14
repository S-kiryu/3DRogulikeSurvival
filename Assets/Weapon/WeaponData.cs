using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public EffectType effectType;

    [System.Serializable]
    public struct LevelData
    {
        public float coolTime;
        public int power;
        public float radius;
    }

    [SerializeField] private LevelData[] levels;

    public int MaxLevel => levels.Length;

    public LevelData GetLevelData(int level)
    {
        level = Mathf.Clamp(level, 1, MaxLevel);
        return levels[level - 1];
    }
}
