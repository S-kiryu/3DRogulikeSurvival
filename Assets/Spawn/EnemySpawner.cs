using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("ウェーブ設定")]
    [SerializeField] private WaveData[] _waves;
    // ウェーブ内の敵スポーン間隔
    [SerializeField] private float _spawnInterval = 2f;
    // ウェーブ間のインターバル
    [SerializeField] private float _waveInterval = 5f;

    [Header("スポーン設定")]
    [SerializeField] private Transform[] _spawnPoints;
    

    private int _currentWaveIndex = 0;
    private int _aliveEnemyCount = 0;
    private int _lastSpawnIndex = -1;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    /// <summary>
    /// Wave を順番に再生する
    /// </summary>
    private IEnumerator WaveLoop()
    {
        while (_currentWaveIndex < _waves.Length)
        {
            WaveData wave = _waves[_currentWaveIndex];
            Debug.Log($"Wave {_currentWaveIndex + 1} 開始");

            // ★ Wave が終わるまで待つ
            yield return StartCoroutine(PlayWave(wave));


            yield return new WaitUntil(() => _aliveEnemyCount <= 0);

            Debug.Log($"Wave {_currentWaveIndex + 1} 終了");

            yield return new WaitForSeconds(_waveInterval);

            _currentWaveIndex++;
        }
    }

    /// <summary>
    /// WaveData の中身を順番・数・間隔どおりにスポーン
    /// </summary>
    private IEnumerator PlayWave(WaveData wave)
    {
        var spawnList = new List<EnemySpawnData>();

        foreach (var enemyData in wave.Enemies)
        {
            for (int i = 0; i < enemyData.SpawnCount; i++)
            {
                spawnList.Add(enemyData);
            }
        }

        while (spawnList.Count > 0)
        {
            int index = Random.Range(0, spawnList.Count);
            EnemySpawnData enemyData = spawnList[index];

            SpawnEnemy(enemyData);

            spawnList.RemoveAt(index);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }


    /// <summary>
    /// 敵を1体スポーンする
    /// </summary>
    private void SpawnEnemy(EnemySpawnData enemyData)
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        GameObject enemy = Instantiate(enemyData.EnemyPrefab,
                    spawnPoint.position,
                    Quaternion.identity);

        _aliveEnemyCount++;

        var status = enemy.GetComponent<EnemyStatus>();
        if (status != null)
        {
            status.OnDead += OnEnemyDead;
        }
    }

    private void OnEnemyDead()
    {
        _aliveEnemyCount--;
    }

    /// <summary>
    /// スポーン地点をランダム取得（連続防止）
    /// </summary>
    private Transform GetRandomSpawnPoint()
    {
        int index;
        do
        {
            index = Random.Range(0, _spawnPoints.Length);
        }
        while (index == _lastSpawnIndex && _spawnPoints.Length > 1);

        _lastSpawnIndex = index;
        return _spawnPoints[index];
    }
}
