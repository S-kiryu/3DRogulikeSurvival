using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [System.Serializable]
    public struct EffectEntry
    {
        public EffectType type;
        public GameObject effectObject;
    }

    [SerializeField] private EffectEntry[] _effects;

    private Dictionary<EffectType, GameObject> effectDict;

    private void Awake()
    {
        effectDict = new Dictionary<EffectType, GameObject>();
        foreach (var entry in _effects)
        {
            effectDict[entry.type] = entry.effectObject;
        }
    }

    // Effect の ON/OFF を切り替えるメソッド
    public void SetEffect(EffectType type, bool active)
    {
        if (effectDict.TryGetValue(type, out var obj))
        {
            obj.SetActive(active);
        }
        else
        {
            Debug.LogWarning($"Effect {type} not found!");
        }
    }

    // Effect の ON/OFF 状態を取得するメソッド
    public bool GetEffectActive(EffectType type)
    {
        if (effectDict.TryGetValue(type, out var obj))
        {
            return obj.activeSelf;
        }
        else
        {
            return false;
        }
    }
}
public enum EffectType
{
    KnifeDamage,
    AreaAttack,
    Tomato,
}

