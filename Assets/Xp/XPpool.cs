using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPpool : MonoBehaviour
{
    public static XPpool Instance { get; private set; }

    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int poolSize = 100;

    private Queue<GameObject> Pool = new Queue<GameObject>();

    // シングルトンの初期化とプールの準備
    private void Awake()
    {
        Instance = this;

        // プールの初期化
        for (int i = 0; i < poolSize; i++)
        {
            GameObject xp = Instantiate(xpPrefab);
            xp.SetActive(false);
            Pool.Enqueue(xp);
        }
    }

    // XPオブジェクトをプールから取得
    public GameObject Get(Vector3 position)
    {
        GameObject xp;

        if (Pool.Count > 0)
        {
            xp = Pool.Dequeue();
        }
        else
        {
            xp = Instantiate(xpPrefab);
        }

        xp.transform.position = position;
        xp.SetActive(true);
        return xp;
    }

    // XPオブジェクトをプールに戻す
    public void Return(GameObject xp)
    {
        xp.SetActive(false);
        Pool.Enqueue(xp);
    }
}
