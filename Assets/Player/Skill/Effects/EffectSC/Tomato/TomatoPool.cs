using System.Collections.Generic;
using UnityEngine;

public class TomatoPool : MonoBehaviour
{
    public static TomatoPool Instance { get; private set; }

    [SerializeField] private GameObject _tomatoPrefab;
    [SerializeField] private int _poolSize = 100;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    // シングルトンの初期化とプールの準備
    private void Awake()
    {
        Instance = this;

        // プールの初期化
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject tomato = Instantiate(_tomatoPrefab);
            tomato.SetActive(false);
            _pool.Enqueue(tomato);
        }
    }

    // とまとオブジェクトをプールから取得
    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject tomato;

        if (_pool.Count > 0)
        {
            tomato = _pool.Dequeue();
        }
        else
        {
            tomato = Instantiate(_tomatoPrefab);
        }

        tomato.transform.SetPositionAndRotation(position, rotation);
        tomato.SetActive(true);

        return tomato;
    }

    // とまとオブジェクトをプールに戻す（★ここが変更点）
    public void Return(GameObject tomato)
    {
        // 🔽 状態リセット（Pool再利用の安全装置）
        var attack = tomato.GetComponent<TomatoAttack>();
        if (attack != null)
        {
            attack.ResetState();
        }

        tomato.SetActive(false);
        _pool.Enqueue(tomato);
    }
}
