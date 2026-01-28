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
            GameObject Tomato = Instantiate(_tomatoPrefab);
            Tomato.SetActive(false);
            _pool.Enqueue(Tomato);
        }
    }

    // とまとオブジェクトをプールから取得
    public GameObject Get(Vector3 position)
    {
        GameObject Tomato;

        if (_pool.Count > 0)
        {
            Tomato = _pool.Dequeue();
        }
        else
        {
            Tomato = Instantiate(_tomatoPrefab);
        }

        Tomato.transform.position = position;
        Tomato.SetActive(true);
        return Tomato;
    }

    // とまとオブジェクトをプールに戻す
    public void Return(GameObject Tomato)
    {
        Tomato.SetActive(false);
        _pool.Enqueue(Tomato);
    }
}
