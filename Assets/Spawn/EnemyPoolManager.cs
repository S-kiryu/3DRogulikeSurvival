using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    [System.Serializable]
    public class PoolEntry
    {
        public EnemyType type;
        public EnemyPool pool;
    }

    [SerializeField] private PoolEntry[] pools;

    private Dictionary<EnemyType, EnemyPool> poolDict;

    private void Awake()
    {
        Instance = this;

        poolDict = new Dictionary<EnemyType, EnemyPool>();
        foreach (var entry in pools)
        {
            poolDict[entry.type] = entry.pool;
        }
    }

    public GameObject Get(EnemyType type, Vector3 position)
    {
        if (!poolDict.TryGetValue(type, out var pool))
        {
            Debug.LogError($"EnemyPool ‚ª–¢“o˜^‚Å‚·: {type}");
            return null;
        }

        return pool.Get(position);
    }

    public void Return(EnemyType type, GameObject obj)
    {
        if (!poolDict.TryGetValue(type, out var pool))
        {
            Debug.LogError($"EnemyPool ‚ª–¢“o˜^‚Å‚·: {type}");
            return;
        }

        pool.Return(obj);
    }
}
