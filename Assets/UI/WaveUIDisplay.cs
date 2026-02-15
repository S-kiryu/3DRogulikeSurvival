using UnityEngine;
using TMPro;

public class WaveUIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private bool _autoFindSpawner = true;

    private void Start()
    {
        if (_waveText == null)
        {
            Debug.LogError("WaveUIDisplay: _waveText が割り当てられていません");
            return;
        }

        if (_autoFindSpawner && _enemySpawner == null)
        {
            _enemySpawner = FindFirstObjectByType<EnemySpawner>();
        }

        if (_enemySpawner == null)
        {
            Debug.LogError("WaveUIDisplay: EnemySpawner が見つかりません");
            return;
        }

        UpdateWaveDisplay();
    }

    private void Update()
    {
        if (_enemySpawner != null)
        {
            UpdateWaveDisplay();
        }
    }

    // ウェーブ情報をテキストに反映する
    private void UpdateWaveDisplay()
    {
        int currentWave = _enemySpawner.CurrentWaveNumber;
        int totalWaves = _enemySpawner.TotalWaveCount;

        _waveText.text = $"Wave {currentWave} / {totalWaves}";
    }
}