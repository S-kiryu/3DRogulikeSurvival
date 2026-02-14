using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _text;
    [SerializeField] private Text _desc;
    [SerializeField] private Button _button;

    private Skill _skill;
    private System.Action<Skill> _onSelected;

    private void Awake()
    {
        // Buttonコンポーネントを取得
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }

        // Imageコンポーネントも取得
        if (_icon == null)
        {
            _icon = GetComponent<Image>();
        }
    }

    // スキルと選択時のコールバックを設定
    public void SetSkill(Skill skill, System.Action<Skill> onSelected)
    {
        _skill = skill;
        _onSelected = onSelected;

        Debug.Log($"Icon is null: {skill.icon == null}");
        Debug.Log($"_icon component is null: {_icon == null}");

        _icon.sprite = skill.icon;

        _skill = skill;
        _onSelected = onSelected;
        _icon.sprite = skill.icon;
        _text.text = skill.skillName;
        _desc.text = skill.skillDescription;

        // 古いリスナーを削除して新しく追加
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);

    }

    // ボタンがクリックされたときに呼ばれる
    public void OnClick()
    {

        if (_onSelected != null && _skill != null)
        {
            _onSelected.Invoke(_skill);
        }
        else
        {
            Debug.LogError(_onSelected);
            Debug.LogError(_skill);
            Debug.LogError($"[SkillButton] Cannot invoke callback!");
        }
    }
}