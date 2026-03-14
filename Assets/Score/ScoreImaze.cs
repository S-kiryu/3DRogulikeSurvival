using UnityEngine;
using TMPro;

public class ScoreImaze : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private  ScoreManager _scoreManager;
    [SerializeField] private bool _autoFindSpawner = true;

    private void Start()
    {
        if (_scoreText == null)
        {
            Debug.LogError("WaveUIDisplay: _waveText が割り当てられていません");
            return;
        }

        if (_autoFindSpawner && _scoreManager == null)
        {
            _scoreManager = FindFirstObjectByType<ScoreManager>();
        }

        if (_scoreManager == null)
        {
            Debug.LogError("WaveUIDisplay: ScoreManager が見つかりません");
            return;
        }

        UpdateWaveDisplay();
    }

    private void Update()
    {
        if (_scoreManager != null)
        {
            UpdateWaveDisplay();
        }
    }

    //情報をテキストに反映する
    private void UpdateWaveDisplay()
    {
        int currentWave = _scoreManager.Score;

        _scoreText.text = $"score {currentWave} ";
    }
}