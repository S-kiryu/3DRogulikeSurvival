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

            var skills = _database.skills
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
        skill.skillEffect.Apply(_player);
    }
}