using System.Linq;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] private SkillDatabase _database;
    [SerializeField] private SkillSelectUI _ui;
    [SerializeField] private PlayerStatus _player;

    public void OnLevelUp()
    {
        var skills = _database.skills
            .OrderBy(x => Random.value)
            .Take(3)
            .ToList();

        _ui.Open(skills, SelectSkill);
    }

    private void SelectSkill(Skill skill)
    {
        skill.skillEffect.Apply(_player);
    }
}
