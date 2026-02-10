using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayerAction : GoapAction
{
    public Transform player;
    public float stopDistance = 1.5f;

    private PlayerStatus _playerStatus;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;

    // 最適化用
    private Vector3 _lastDestination;
    private float _updateInterval = 1f;
    private float _nextUpdateTime;
    private const float DESTINATION_UPDATE_THRESHOLD = 1f;

    private void Awake()
    {
        Preconditions = new Dictionary<string, bool>
        {
            { "PlayerVisible", true }
        };
        Effects = new Dictionary<string, bool>
        {
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

        // NavMeshAgent 設定
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.stoppingDistance = stopDistance;

        _lastDestination = Vector3.positiveInfinity;
    }

    public override bool CheckCanExecute()
    {
        if (player == null) return false;
        if (_playerStatus == null) return false;
        if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return false;
        return true;
    }

    public override bool ExecuteAction()
    {
        // ポーズ中
        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused)
        {
            StopAgent();
            return false;
        }

        // プレイヤー死亡
        if (!IsPlayerAlive())
        {
            StopAgent();
            return true;
        }

        // プレイヤー方向を向く
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 5f
            );
        }

        // 目的地の更新
        UpdateDestination();

        // 目標到達チェック
        if (IsNearPlayer())
        {
            StopAgent();
            return true;
        }

        return false;
    }

    private void UpdateDestination()
    {
        _navMeshAgent.isStopped = false;

        bool shouldUpdate = false;

        if (Time.time >= _nextUpdateTime)
        {
            shouldUpdate = true;
            _nextUpdateTime = Time.time + _updateInterval;
        }

        float distanceMoved = Vector3.Distance(player.position, _lastDestination);
        if (distanceMoved > DESTINATION_UPDATE_THRESHOLD)
        {
            shouldUpdate = true;
        }

        if (shouldUpdate)
        {
            _navMeshAgent.SetDestination(player.position);
            _lastDestination = player.position;
        }
    }

    private bool IsNearPlayer()
    {
        if (player == null) return false;

        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= stopDistance;
    }

    private void StopAgent()
    {
        // ⭐ NavMeshAgent が有効で NavMesh 上にある場合のみ操作
        if (_navMeshAgent == null) return;
        if (!_navMeshAgent.enabled) return;
        if (!_navMeshAgent.isOnNavMesh) return;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        _navMeshAgent.velocity = Vector3.zero;
    }

    private bool IsPlayerAlive()
    {
        return _playerStatus != null && _playerStatus.IsAlive;
    }

    private void OnDisable()
    {
        // ⭐ StopAgent() を呼ばない（既に無効化されている可能性があるため）
        _lastDestination = Vector3.positiveInfinity;
    }

    // ⭐ プール返却時用
    public void ResetAction()
    {
        // ⭐ StopAgent() を呼ばない（既に EnemyPool.Return で処理済み）
        _lastDestination = Vector3.positiveInfinity;
        _nextUpdateTime = 0f;
    }
}