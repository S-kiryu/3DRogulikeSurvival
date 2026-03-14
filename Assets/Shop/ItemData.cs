using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [Tooltip("アイテムの説明")]
    [SerializeField] private string _description;
    [Tooltip("アイテムのイメージ")]
    [SerializeField] private Sprite _icon;
    [Tooltip("アイテムの値段")]
    [SerializeField] private int _price;
    [Tooltip("アイテムのMaxレベル")]
    [SerializeField] private int _maxItemLv;
    [Header("アクション設定")]
    [Tooltip("このアイテムがクリックされた時に実行されるスクリプト")]
    [SerializeField] private List<ItemAction> itemActions = new List<ItemAction>();

    public UnityEvent<bool> OnPurchaseAttempt = new UnityEvent<bool>();

    // ゲッターメソッド
    public string GetItemName()
    {
        return _itemName != null ? _itemName : "名前なし";
    }

    public string GetDescription()
    {
        return _description != null ? _description : "説明なし";
    }

    public Sprite GetIcon()
    {
        return _icon;
    }

    public int GetPrice()
    {
        return _price;
    }

    public int GetItemLv()
    {
        return _maxItemLv;
    }

    // 金額チェック付きで購入を試みる
    public bool TryPurchaseAndExecute()
    {
        if (PlayerStatusManager.Instance == null)
        {
            Debug.LogError("PlayerStatusManagerが見つかりません");
            OnPurchaseAttempt.Invoke(false);
            return false;
        }

        // 購入可能か確認
        if (!PlayerStatusManager.Instance.CanPurchase(_price))
        {
            Debug.LogWarning($"所持金不足: {_itemName}の購入に失敗しました。必要: {_price}、所持: {PlayerStatusManager.Instance.GetMoney()}");
            OnPurchaseAttempt.Invoke(false);
            return false;
        }

        // 金額を消費
        if (PlayerStatusManager.Instance.TrySpendMoney(_price))
        {
            ExecuteAction();
            OnPurchaseAttempt.Invoke(true);
            Debug.Log($"{_itemName}を購入しました。残り所持金: {PlayerStatusManager.Instance.GetMoney()}");
            return true;
        }

        OnPurchaseAttempt.Invoke(false);
        return false;
    }

    public void ExecuteAction()
    {
        if (itemActions != null && itemActions.Count > 0)
        {
            foreach (var action in itemActions)
            {
                if (action != null)
                {
                    action.Execute(this);
                }
            }
        }
    }
}