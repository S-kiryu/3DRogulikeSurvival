using System.Collections.Generic;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] private SkillButton[] _buttons;
    private System.Action<Skill> _onSelected;

    //ボタンの設定
    public void Open(List<Skill> skills, System.Action<Skill> onSelected)
    {

        _onSelected = onSelected;
        gameObject.SetActive(true);

        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i < skills.Count)
            {
                _buttons[i].SetSkill(skills[i], SelectSkill);
            }
        }
    }

    //スキル選択時の処理
    private void SelectSkill(Skill skill)
    {

        if (_onSelected != null)
        {
            _onSelected.Invoke(skill);
        }
        else
        {
            Debug.LogError("[SkillSelectUI] _onSelected is NULL!");
        }

        Close();
    }

    //UIを閉じる
    public void Close()
    {
        gameObject.SetActive(false);
    }
}