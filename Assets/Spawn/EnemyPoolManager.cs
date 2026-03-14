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

    /// 指定した EnemyType のオブジェクトをプールから取得
    public GameObject Get(EnemyType type, Vector3 position)
    {
        if (!poolDict.TryGetValue(type, out var pool))
        {
            Debug.LogError($"{type}が未登録です");
            return null;
        }
        return pool.Get(position);
    }

    // 指定した EnemyType のオブジェクトをプールに返却
    public void Return(EnemyType type, GameObject obj)
    {
        if (!poolDict.TryGetValue(type, out var pool))
        {
            Debug.LogError($"{type}が未登録");
            return;
        }
        pool.Return(obj);
    }
}