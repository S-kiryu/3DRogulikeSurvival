using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerAction : GoapAction
{
    public Transform player;
    public float moveSpeed = 3f;

    private void Awake()
    {
        // 前提条件：プレイヤーを発見しているかどうか
        Preconditions = new Dictionary<string, bool> {{ "PlayerVisible", true }};

        // 結果：プレイヤーに近づいた
        Effects = new Dictionary<string, bool> {{ "IsNearPlayer", true }};
    }

    public override bool CheckCanExecute()
    {
        return player != null;
    }

    public override bool ExecuteAction()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );

        // 一定距離なら成功
        return Vector3.Distance(transform.position, player.position) < 1.5f;
    }
}
