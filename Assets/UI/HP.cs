using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private Image _hpFillImage;
    [SerializeField] private PlayerStatus _playerStatus;

    void Start()
    {
        UpdateHP(
            _playerStatus.CurrentHealth,
            PlayerStatusManager.Instance.Status.MaxHealth
        );

        // イベント登録
        _playerStatus.OnHpChanged += UpdateHP;
    }

    private void OnDestroy()
    {
        // イベント解除（重要）
        _playerStatus.OnHpChanged -= UpdateHP;
    }

    private void UpdateHP(int current, int max)
    {
        _hpFillImage.fillAmount = (float)current / max;
    }
}
