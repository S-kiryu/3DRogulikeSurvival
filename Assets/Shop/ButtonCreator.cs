using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private ShopInventory _inventory;
    [SerializeField] private Color _purchasableColor = Color.white;
    [SerializeField] private Color _notPurchasableColor = Color.gray;

    private void Start()
    {
        CreateShopButtons();
    }

    private void CreateShopButtons()
    {
        foreach (var item in _inventory.items)
        {
            // ボタンを生成
            Button newButton = Instantiate(_buttonPrefab);
            // Canvas の子にする
            newButton.transform.SetParent(_scrollRect.content, false);

            Transform nameTextTransform = newButton.transform.Find("NameText");
            Transform moneyTextTransform = newButton.transform.Find("MoneyText");
            TextMeshProUGUI nameText = nameTextTransform.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI moneyText = moneyTextTransform.GetComponent<TextMeshProUGUI>();

            nameText.text = item.GetItemName();
            moneyText.text = item.GetPrice().ToString() + "G";

            var capturedItem = item;

            // 購入ボタンの状態を設定
            UpdateButtonState(newButton, capturedItem);

            // ボタンクリック時に購入を試みる
            newButton.onClick.AddListener(() =>
            {
                if (capturedItem.TryPurchaseAndExecute())
                {
                    UpdateButtonState();
                }
            });

            // 購入成功・失敗のイベントをリッスン
            capturedItem.OnPurchaseAttempt.AddListener((success) =>
            {
                if (success)
                {
                    UpdateButtonState();
                }
            });
        }
    }

    private void UpdateButtonState()
    {
        foreach (Transform child in _scrollRect.content)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // 対応するItemDataを取得（タグやコンポーネントで管理する場合は調整が必要）
                var item = GetItemFromButton(button);
                if (item != null)
                {
                    UpdateButtonState(button, item);
                }
            }
        }
    }

    private void UpdateButtonState(Button button, ItemData item)
    {
        bool canPurchase = PlayerStatusManager.Instance.CanPurchase(item.GetPrice());

        button.interactable = canPurchase;

        var colors = button.colors;

        //買えるなら白、買えないなら灰色に変更
        colors.normalColor = canPurchase ? _purchasableColor 
                                         : _notPurchasableColor;

        colors.highlightedColor = colors.normalColor;
        colors.pressedColor = colors.normalColor * 0.9f;
        colors.disabledColor = _notPurchasableColor;

        // ボタンの色を更新
        button.colors = colors;
    }


    // ボタンから対応するItemDataを取得する補助メソッド
    private ItemData GetItemFromButton(Button button)
    {
        int buttonIndex = button.transform.GetSiblingIndex();
        if (buttonIndex >= 0 && buttonIndex < _inventory.items.Count)
        {
            return _inventory.items[buttonIndex];
        }
        return null;
    }
}