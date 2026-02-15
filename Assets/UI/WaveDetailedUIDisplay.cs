using UnityEngine;
using TMPro;

public class WaveDetailedUIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveNumberText;  // "Wave 3 / 5"
    [SerializeField] private TextMeshProUGUI _waveStatusText;   // "進行中" or "完了" など
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private bool _autoFindSpawner = true;

    private void Start()
    {
        if (_waveNumberText == null)
        {
            Debug.LogError("WaveDetailedUIDisplay: _waveNumberText が割り当てられていません");
            return;
        }

        if (_autoFindSpawner && _enemySpawner == null)
        {
            _enemySpawner = FindFirstObjectByType<EnemySpawner>();
        }

        if (_enemySpawner == null)
        {
            Debug.LogError("WaveDetailedUIDisplay: EnemySpawner が見つかりません");
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
        bool isActive = _enemySpawner.IsGameActive;

        // ウェーブ数を表示
        _waveNumberText.text = $"Wave {currentWave} / {totalWaves}";

        // ステータスを表示（オプション）
        if (_waveStatusText != null)
        {
            if (!isActive)
            {
                _waveStatusText.text = "ゲームクリア！";
                _waveStatusText.color = Color.green;
            }
            else if (currentWave <= totalWaves)
            {
                _waveStatusText.text = "進行中...";
                _waveStatusText.color = Color.white;
            }
        }
    }
}