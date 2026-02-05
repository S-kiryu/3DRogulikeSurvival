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
    [SerializeField] private float _delayBetweenLevelUps = 0.5f; // レベルアップ間の遅延

    private Queue<int> _levelUpQueue = new Queue<int>();
    //選択されたスキルの保存
    private HashSet<Skill> _selectedSkills = new HashSet<Skill>();
    private List<Skill> _runtimeSkills;
    private HashSet<int> _selectedSkillIds = new HashSet<int>();
    private Dictionary<int, int> _skillSelectCount = new Dictionary<int, int>();

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
        _runtimeSkills = new List<Skill>(_database.skills);
    }

    //キューにレベルアップを追加
    public void OnLevelUp()
    {
        _levelUpQueue.Enqueue(1);

        if (!_isProcessingLevelUp)
        {
            StartCoroutine(ProcessNextLevelUpCoroutine());
        }
    }

    // レベルアップ処理のコルーチン
    private IEnumerator ProcessNextLevelUpCoroutine()
    {
        _isProcessingLevelUp = true;

        while (_levelUpQueue.Count > 0)
        {
            _isPaused = true;

            _levelUpQueue.Dequeue();

            //選択されたスキルに基づいて、まだ選択されていないスキルからランダムに3つ選ぶ
            var skills = _runtimeSkills
                .Where(s => !_selectedSkills.Contains(s))
                .OrderBy(x => Random.value)
                .Take(3)
                .ToList();


            // スキル選択を待つ
            bool skillSelected = false;
            _ui.Open(skills, (skill) => {
                SelectSkill(skill);
                skillSelected = true;
            });

            // スキルが選択されるまで待機
            yield return new WaitUntil(() => skillSelected);

            // 次のレベルアップまで少し待つ
            if (_levelUpQueue.Count > 0)
            {
                yield return new WaitForSecondsRealtime(_delayBetweenLevelUps);
            }
        }

        // すべてのレベルアップが完了
        _isProcessingLevelUp = false;
        _isPaused = false;
    }

    // スキル選択時の処理
    private void SelectSkill(Skill skill)
    {
        // 効果適用
        skill.skillEffect.Apply(_player);

        // 個数を1減らす
        skill.count--;

        // 個数が0になったら選択肢から削除
        if (skill.count <= 0)
        {
            _runtimeSkills.Remove(skill);
        }

        // 派生スキルを実行用リストに追加
        if (skill.upgradeSkills != null)
        {
            foreach (var upgrade in skill.upgradeSkills)
            {
                if (!_runtimeSkills.Contains(upgrade))
                {
                    _runtimeSkills.Add(upgrade);
                }
            }
        }
    }

}