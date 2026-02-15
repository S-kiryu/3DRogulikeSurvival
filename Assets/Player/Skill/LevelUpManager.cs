using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance { get; private set; }
    [SerializeField] private SkillDatabase _database;
    [SerializeField] private SkillSelectUI _ui;
    [SerializeField] private PlayerStatus _player;
    [SerializeField] private float _delayBetweenLevelUps = 0.5f;

    private Queue<int> _levelUpQueue = new Queue<int>();
    private HashSet<Skill> _selectedSkills = new HashSet<Skill>();
    private List<SkillInstance> _runtimeSkills;
    private bool _isProcessingLevelUp = false;
    private bool _isPaused = false;

    public bool IsPaused => _isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ランタイム用スキルリストの初期化
        _runtimeSkills = new List<SkillInstance>();
        foreach (var skill in _database.skills)
        {
            _runtimeSkills.Add(new SkillInstance(skill));
        }
    }

    public void OnLevelUp()
    {
        _levelUpQueue.Enqueue(1);
        if (!_isProcessingLevelUp)
        {
            StartCoroutine(ProcessNextLevelUpCoroutine());
        }
    }

    private IEnumerator ProcessNextLevelUpCoroutine()
    {
        _isProcessingLevelUp = true;
        while (_levelUpQueue.Count > 0)
        {
            _isPaused = true;
            _levelUpQueue.Dequeue();

            var skills = _runtimeSkills
                .Where(s => !_selectedSkills.Contains(s.skill))
                .OrderBy(x => Random.value)
                .Take(3)
                .Select(s => s.skill)  // UI表示用にSkillを取得
                .ToList();

            bool skillSelected = false;
            _ui.Open(skills, (skill) => {
                SelectSkill(skill);
                skillSelected = true;
            });

            yield return new WaitUntil(() => skillSelected);

            if (_levelUpQueue.Count > 0)
            {
                yield return new WaitForSecondsRealtime(_delayBetweenLevelUps);
            }
        }

        _isProcessingLevelUp = false;
        _isPaused = false;
    }

    private void SelectSkill(Skill skill)
    {
        // ランタイムデータを検索
        var skillInstance = _runtimeSkills.FirstOrDefault(s => s.skill == skill);
        if (skillInstance == null) return;

        // 効果適用
        skill.skillEffect.Apply(_player);

        // ランタイムデータのみを更新
        skillInstance.remainingCount--;
        if (skillInstance.remainingCount <= 0)
        {
            _runtimeSkills.Remove(skillInstance);
        }

        _selectedSkills.Add(skill);

        // 派生スキル追加
        if (skill.upgradeSkills != null)
        {
            foreach (var upgrade in skill.upgradeSkills)
            {
                if (!_runtimeSkills.Any(s => s.skill == upgrade))
                {
                    _runtimeSkills.Add(new SkillInstance(upgrade));
                }
            }
        }
    }
}