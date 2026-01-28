using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPpool : MonoBehaviour
{
    public static XPpool Instance { get; private set; }

    [SerializeField] private GameObject _xpPrefab;
    [SerializeField] private int _poolSize = 100;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    // シングルトンの初期化とプールの準備
    private void Awake()
    {
        Instance = this;

        // プールの初期化
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject xp = Instantiate(_xpPrefab);
            xp.SetActive(false);
            _pool.Enqueue(xp);
        }
    }

    // XPオブジェクトをプールから取得
    public GameObject Get(Vector3 position)
    {
        GameObject xp;

        if (_pool.Count > 0)
        {
            xp = _pool.Dequeue();
        }
        else
        {
            xp = Instantiate(_xpPrefab);
        }

        xp.transform.position = position;
        xp.SetActive(true);
        return xp;
    }

    // XPオブジェクトをプールに戻す
    public void Return(GameObject xp)
    {
        xp.SetActive(false);
        _pool.Enqueue(xp);
    }
}
