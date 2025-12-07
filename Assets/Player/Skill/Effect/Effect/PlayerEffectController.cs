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
}
public enum EffectType
{
    Katana,
    AreaAttack,
}

