using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerAction : GoapAction
{
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseDistance = 5f; // 追い続ける最大距離
    public float stopDistance = 1.5f; // 近づいたと見なす距離

    private void Awake()
    {
        Preconditions = new Dictionary<string, bool> {
            { "PlayerVisible", true }
        };

        Effects = new Dictionary<string, bool> {
            { "IsNearPlayer", true }
        };
    }

    public override bool CheckCanExecute()
    {
        return player != null;
    }

    public override bool ExecuteAction()
    {
        // もしプレイヤーが死んでいたらこのアクションを終了
        if (!IsPlayerAlive())
        {
            Debug.Log("Player is dead → stop chasing");
            return true; // アクション成功扱い(終了)
        }

        // プレイヤーとの距離
        float distance = Vector3.Distance(transform.position, player.position);

        // player が遠く離れたら → 追っかけ継続
        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );

            return false;
        }
        return true;
    }

    private bool IsPlayerAlive()
    {
        // プレイヤーにHPが０かどうか
        var health = player.GetComponent<PlayerHealth>();
        return health != null && health.CurrentHP > 0;
    }
}
