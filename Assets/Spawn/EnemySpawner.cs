using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("ウェーブ設定")]
    [SerializeField,Tooltip("ウェーブのデータがはいってる")] private WaveData[] _waves;
    [SerializeField,Tooltip("1ウェーブの長さ")] private float _waveDuration = 60f;
    [SerializeField,Tooltip("１ウェーブ内のスポーンする感覚")] private float _spawnInterval = 10f;

    [Header("スポーン設定")]
    [SerializeField,Tooltip("スポーンする場所")] private Transform[] _spawnPoints;

    private int _currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    /// <summary>
    /// ウェーブのループ処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveLoop()
    {
        while (_currentWaveIndex < _waves.Length)
        {
            WaveData currentWave = _waves[_currentWaveIndex];
            float elapsedTime = 0f;

            while (elapsedTime < _waveDuration)
            {
                SpawnWaveEnemies(currentWave);
                yield return new WaitForSeconds(_spawnInterval);
                elapsedTime += _spawnInterval;
            }

            _currentWaveIndex++;
        }
    }

    /// <summary>
    /// スポーン処理
    /// </summary>
    /// <param name="wave"></param>
    private void SpawnWaveEnemies(WaveData wave)
    {
        foreach (var enemyData in wave.Enemies)
        {
            for (int i = 0; i < enemyData.SpawnCount; i++)
            {
                Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                Instantiate(enemyData.EnemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
