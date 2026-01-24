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
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        if (player != null)
        {
            _playerStatus = player.GetComponent<PlayerStatus>();
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
        if(LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused == true)
        {
            Debug.Log(" LevelUp Pause - stop chasing");
            return false; // レベルアップ中は動かない
        }

        // プレイヤーが死んでいたらこのアクションを終了
        if (!IsPlayerAlive())
        {
            Debug.Log(" stop chasing");
            return true;
        }

        // プレイヤーとの距離
        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // stopDistanceより遠ければ移動（常に追い続ける）
        if (distance > stopDistance)
        {
            Vector3 targetPos = new Vector3(
                player.position.x,
                transform.position.y,
                player.position.z
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            return false; // 追跡中
        }

        // プレイヤーに到達したが、離れたらまた追尾するため false を返す
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