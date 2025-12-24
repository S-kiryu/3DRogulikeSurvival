using System.Collections.Generic;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField] private SkillButton[] _buttons;
    private System.Action<Skill> _onSelected;

    /// <summary>
    /// スキル選択画面を表示
    /// </summary>
    /// <param name="skills"></param>
    /// <param name="onSelected"></param>
    public void Open(List<Skill> skills, System.Action<Skill> onSelected)
    {
        _onSelected = onSelected;
        gameObject.SetActive(true);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetSkill(skills[i], SelectSkill);
        }
    }


    /// <summary>
    /// 選んだスキルを渡して選択画面を閉じる
    /// </summary>
    /// <param name="skill"></param>
    private void SelectSkill(Skill skill)
    {
        _onSelected?.Invoke(skill);
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
