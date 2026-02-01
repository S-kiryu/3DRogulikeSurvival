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

    public enum EnemyType
    {
        Slime,
        Bat,
        Goblin
    }


    private void Start()
    {
        Debug.Log("EnemySpawner Start");

        if (_waves == null)
        {
            Debug.LogError("_waves が null です");
            return;
        }

        Debug.Log($"Wave数: {_waves.Length}");

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

            Debug.Log(
                $"Wave[{_currentWaveIndex}] : " +
                $"wave is null = {wave == null}"
            );

            Debug.Log(
                $"Enemies null = {wave.Enemies == null}, " +
                $"Length = {(wave.Enemies == null ? -1 : wave.Enemies.Length)}"
            );

            Debug.Log($"Wave {_currentWaveIndex + 1} 開始");

            //Wave が終わるまで待つ
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
        if (wave == null)
        {
            Debug.LogError("PlayWave: wave が null");
            yield break;
        }

        if (wave.Enemies == null || wave.Enemies.Length == 0)
        {
            Debug.LogError("PlayWave: Enemies が null または空です");
            yield break;
        }

        Debug.Log($"PlayWave 開始: Enemies数 = {wave.Enemies.Length}");


        var spawnList = new List<EnemySpawnData>();

        foreach (var enemyData in wave.Enemies)
        {
            Debug.Log(
                $"EnemyData: Type={enemyData.Type}, " +
                $"Count={enemyData.SpawnCount}"
            );

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
        Debug.Log($"SpawnEnemy: {enemyData.Type}");

        if (EnemyPoolManager.Instance == null)
        {
            Debug.LogError("EnemyPoolManager.Instance が null");
            return;
        }

        Transform spawnPoint = GetRandomSpawnPoint();

        GameObject enemy = EnemyPoolManager.Instance
    .Get(enemyData.Type, spawnPoint.position);


        _aliveEnemyCount++;

        var status = enemy.GetComponent<EnemyStatus>();
        if (status != null)
        {
            status.OnDead -= OnEnemyDead;
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
