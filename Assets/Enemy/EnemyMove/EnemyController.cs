using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float updateInterval = 1f;
    [SerializeField] private float moveSpeed = 3.5f;

    private Transform _player;
    private NavMeshAgent _navMeshAgent;
    private EnemyStatus _enemyStatus;

    private Vector3 _lastDestination;
    private float _nextUpdateTime;
    private float _stopDistanceSq;
    private const float DESTINATION_UPDATE_THRESHOLD = 1f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStatus = GetComponent<EnemyStatus>();
        _stopDistanceSq = stopDistance * stopDistance;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
        }

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.stoppingDistance = stopDistance;
        _lastDestination = Vector3.positiveInfinity;
        _navMeshAgent.speed = moveSpeed;

    }

    private void Update()
    {
        if (_player == null || !_enemyStatus.IsAlive)
        {
            StopAgent();
            return;
        }

        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused)
        {
            StopAgent();
            return;
        }

        Vector3 lookDir = _player.position - transform.position;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.001f)
        {
            // プレイヤーの方向を向く
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 5f
            );
        }

        if (Time.time >= _nextUpdateTime ||
            (_player.position - _lastDestination).sqrMagnitude > DESTINATION_UPDATE_THRESHOLD * DESTINATION_UPDATE_THRESHOLD)
        {
            _navMeshAgent.SetDestination(_player.position);
            _lastDestination = _player.position;
            _nextUpdateTime = Time.time + updateInterval;
            _navMeshAgent.isStopped = false;
        }
    }

    // NavMeshAgentを停止してパスをリセット
    private void StopAgent()
    {
        if (_navMeshAgent == null || !_navMeshAgent.enabled || !_navMeshAgent.isOnNavMesh)
            return;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        _navMeshAgent.velocity = Vector3.zero;
    }

    public void Reset()
    {
        _lastDestination = Vector3.positiveInfinity;
        _nextUpdateTime = 0f;
        StopAgent();
    }
}