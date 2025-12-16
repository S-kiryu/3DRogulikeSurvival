using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerAction : GoapAction
{
    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    private PlayerStatus _playerStatus;

    private void Awake()
    {
        Preconditions = new Dictionary<string, bool> {
            { "PlayerVisible", true }
        };
        Effects = new Dictionary<string, bool> {
            { "IsNearPlayer", true }
        };
    }

    private void Start()
    {
        // PlayerStatusをキャッシュ
        if (player != null)
        {
            _playerStatus = player.GetComponent<PlayerStatus>();
            if (_playerStatus == null)
            {
                Debug.LogError($"PlayerStatus not found on player object: {player.name}");
            }
        }
    }

    public override bool CheckCanExecute()
    {
        if (player == null)
        {
            Debug.LogWarning("MoveToPlayerAction: player is not assigned");
            return false;
        }

        if (_playerStatus == null)
        {
            Debug.LogWarning("MoveToPlayerAction: PlayerStatus not found");
            return false;
        }

        return true;
    }

    public override bool ExecuteAction()
    {
        // プレイヤーが死んでいたらこのアクションを終了
        if (!IsPlayerAlive())
        {
            Debug.Log("Player is dead → stop chasing");
            return true;
        }

        // プレイヤーとの距離
        float distance = Vector3.Distance(transform.position, player.position);

        // stopDistanceより遠ければ移動（常に追い続ける）
        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            return false; // まだ追跡中
        }

        // プレイヤーに到達したが、離れたらまた追尾するため false を返す
        Debug.Log($"Player reached! Distance: {distance:F2}");
        return false; // ずっと実行し続ける
    }

    private bool IsPlayerAlive()
    {
        if (_playerStatus == null)
        {
            Debug.LogWarning("PlayerStatus is null in IsPlayerAlive");
            return false;
        }
        return _playerStatus.IsAlive;
    }
}