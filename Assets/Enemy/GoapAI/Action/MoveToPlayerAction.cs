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
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }

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

        //NavMeshAgent 設定
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updatePosition = true;
        _navMeshAgent.stoppingDistance = stopDistance;
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

        //毎フレーム目的地を更新するだけ
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(player.position);

        return false;
    }

    private void StopAgent()
    {
        if (_navMeshAgent == null) return;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }

    private bool IsPlayerAlive()
    {
        return _playerStatus != null && _playerStatus.IsAlive;
    }

    private void OnDisable()
    {
        StopAgent();
    }
}
