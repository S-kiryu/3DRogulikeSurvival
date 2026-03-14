using UnityEngine;
using TMPro;

public class MoneyUi : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    private PlayerStatusManager _playerStatusManager;

    private void Awake()
    {
        _playerStatusManager = PlayerStatusManager.Instance;
    }

    private void OnEnable()
    {
        _playerStatusManager.OnMoneyChanged.AddListener(UpdateMoneyUI);
        UpdateMoneyUI(_playerStatusManager.GetMoney());
    }

    private void OnDisable()
    {
        _playerStatusManager.OnMoneyChanged.RemoveListener(UpdateMoneyUI);
    }

    private void UpdateMoneyUI(int money)
    {
        moneyText.text = $"Money : {money}";
    }
}
