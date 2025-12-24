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