using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _text;
    [SerializeField] private Text _desc;

    private Skill _skill;
    private System.Action<Skill> _onSelected; // コールバック

    // スキルとコールバックを設定する
    public void SetSkill(Skill skill, System.Action<Skill> onSelected)
    {
        Debug.Log(skill);

        _skill = skill;
        _onSelected = onSelected;

        _icon.sprite = skill.icon;
        _text.text = skill.skillName;
        _desc.text = skill.skillDescription;
    }
    // ボタンがクリックされたときに呼ばれる
    public void OnClick()
    {
        // スキルが選択されたことを通知
        _onSelected?.Invoke(_skill);
    }
}
