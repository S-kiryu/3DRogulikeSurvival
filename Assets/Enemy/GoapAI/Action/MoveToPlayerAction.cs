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
        Preconditions = new Dictionary<string, bool> {
            { "PlayerVisible", true }
        };
        Effects = new Dictionary<string, bool> {
            { "IsNearPlayer", true }
        };
    }

    private void Start()
    {
        // NavMeshAgent を取得（なければ追加）
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Rigidbody を取得（移動と回転の制御用）
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;  // NavMesh が制御するため KinematicRigidbody に
        }

        // プレイヤーを自動取得
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // PlayerStatus を取得
        if (player != null)
        {
            _playerStatus = player.GetComponent<PlayerStatus>();
        }

        // NavMeshAgent の基本設定
        if (_navMeshAgent != null)
        {
            _navMeshAgent.updateRotation = false;  // 回転は手動で行う
            _navMeshAgent.updatePosition = true;   // 位置は NavMesh に制御させる
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

        if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("MoveToPlayerAction: NavMeshAgent is not on NavMesh");
            return false;
        }

        return true;
    }

    public override bool ExecuteAction()
    {
        // レベルアップ中は停止
        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused == true)
        {
            if (_navMeshAgent.hasPath)
            {
                _navMeshAgent.velocity = Vector3.zero;
            }
            return false;
        }

        // プレイヤーが死んでいたら終了
        if (!IsPlayerAlive())
        {
            if (_navMeshAgent.hasPath)
            {
                _navMeshAgent.velocity = Vector3.zero;
            }
            return true;
        }

        // プレイヤーとの距離
        float distance = Vector3.Distance(transform.position, player.position);

        // プレイヤー方向に向く
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0f;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 5f  // 滑らかに回転
            );
        }

        // stopDistance より遠ければ移動
        if (distance > stopDistance)
        {
            // プレイヤーへの経路を設定
            if (_navMeshAgent.SetDestination(player.position))
            {
                // 経路設定成功
                _navMeshAgent.isStopped = false;
                return false;  // 追跡中
            }
            else
            {
                // 経路設定失敗（プレイヤーが NavMesh 外など）
                Debug.LogWarning("Could not set destination for NavMeshAgent");
                _navMeshAgent.isStopped = true;
                return false;
            }
        }
        else
        {
            // プレイヤーに到達
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
            return false;  // 追尾継続（到達後も追跡）
        }
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

    /// <summary>
    /// NavMeshAgent の設定をカスタマイズ
    /// </summary>
    public void ConfigureNavMeshAgent(float speed, float stoppingDistance, float acceleration)
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.stoppingDistance = stoppingDistance;
            _navMeshAgent.acceleration = acceleration;
        }
    }

    private void OnDisable()
    {
        // 敵が無効化されたら移動停止
        if (_navMeshAgent != null && _navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.isStopped = true;
        }
    }
}