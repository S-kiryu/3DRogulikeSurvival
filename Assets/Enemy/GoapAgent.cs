using System.Collections.Generic;
using UnityEngine;

public class GoapAgent : MonoBehaviour
{
    public List<GoapAction> actions = new List<GoapAction>();
    public List<Goal> goals = new List<Goal>();

    private Queue<GoapAction> currentPlan;

    private void Start()
    {
        // このAIが使える行動を登録
        actions.AddRange(GetComponents<GoapAction>());

        // 目標を設定
        goals.Add(new Goal(
            new Dictionary<string, bool> { { "IsNearPlayer", true } },
            priority: 2
        ));

        Plan();
    }

    private void Update()
    {
        if (currentPlan == null || currentPlan.Count == 0) return;

        var action = currentPlan.Peek();

        if (!action.CheckCanExecute()) return;

        // 行動中
        if (action.ExecuteAction())
        {
            // 成功 → 次へ
            currentPlan.Dequeue();
        }
    }

    private void Plan()
    {
        // ここでは簡易的に１番最初のGoalに対応するActionを選ぶ例
        currentPlan = new Queue<GoapAction>();

        foreach (var action in actions)
        {
            if (action.Effects.ContainsKey("IsNearPlayer"))
            {
                currentPlan.Enqueue(action);
                break;
            }
        }
    }
}
