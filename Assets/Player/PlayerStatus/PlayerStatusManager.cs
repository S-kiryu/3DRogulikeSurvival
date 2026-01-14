using UnityEngine;
using UnityEngine.Events;

public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance;

    [SerializeField] private StatusSettings _defaultSettings;
    public PlayerRuntimeStatus Status;

    private int _money = 0;

    public UnityEvent<int> OnMoneyChanged = new UnityEvent<int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Status = new PlayerRuntimeStatus(_defaultSettings);
    }

    //次に必要なexpを変える
    public int ExpToNextLevel => Status.Level * 10;

    // 所持金を取得
    public int GetMoney()
    {
        return _money;
    }

    // 所持金を設定
    public void SetMoney(int amount)
    {
        _money = Mathf.Max(0, amount); // 負の値は0に設定
        OnMoneyChanged.Invoke(_money);
    }

    // 所持金を追加
    public void AddMoney(int amount)
    {
        SetMoney(_money + amount);
    }

    // 所持金を減らす（成功したかどうかを返す）
    public bool TrySpendMoney(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("消費金額が負の値です");
            return false;
        }

        if (_money >= amount)
        {
            SetMoney(_money - amount);
            return true;
        }

        Debug.LogWarning($"所持金不足：必要 {amount}、所持 {_money}");
        return false;
    }

    // 購入が可能か確認
    public bool CanPurchase(int price)
    {
        return _money >= price;
    }
}
