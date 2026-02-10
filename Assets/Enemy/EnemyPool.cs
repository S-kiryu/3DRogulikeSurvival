using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _initialCount = 30;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _initialCount; i++)
        {
            Create();
        }
    }

    private GameObject Create()
    {
        var obj = Instantiate(_enemyPrefab);
        obj.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject Get(Vector3 position)
    {
        if (pool.Count == 0)
            Create();

        var obj = pool.Dequeue();
        obj.transform.position = position;

        // NavMeshAgent を再有効化
        var navAgent = obj.GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.enabled = true;

            if (navAgent.isOnNavMesh)
            {
                navAgent.Warp(position);
            }
            else
            {
                navAgent.nextPosition = position;
            }
        }

        // EnemyStatus をリセット
        var status = obj.GetComponent<EnemyStatus>();
        if (status != null)
        {
            status.ResetEnemy();
        }

        // GoapAgent を再初期化
        var goapAgent = obj.GetComponent<GoapAgent>();
        if (goapAgent != null)
        {
            goapAgent.ResetAgent();
        }

        // ⭐ Animator をリセット
        var animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }

        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        // ⭐ 全てのコルーチンを停止（最優先）
        var monoBehaviours = obj.GetComponents<MonoBehaviour>();
        foreach (var mb in monoBehaviours)
        {
            mb.StopAllCoroutines();
        }

        // NavMeshAgent を先に停止
        var navAgent = obj.GetComponent<NavMeshAgent>();
        if (navAgent != null && navAgent.enabled && navAgent.isOnNavMesh)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
            navAgent.velocity = Vector3.zero;
        }

        // Animator を完全リセット
        var animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);

            foreach (var param in animator.parameters)
            {
                switch (param.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        animator.SetBool(param.name, false);
                        break;
                    case AnimatorControllerParameterType.Int:
                        animator.SetInteger(param.name, 0);
                        break;
                    case AnimatorControllerParameterType.Float:
                        animator.SetFloat(param.name, 0f);
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        animator.ResetTrigger(param.name);
                        break;
                }
            }
        }

        // ParticleSystem の完全停止
        var particles = obj.GetComponentsInChildren<ParticleSystem>(true);
        foreach (var ps in particles)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Clear(true);
        }

        // TrailRenderer のクリア
        var trails = obj.GetComponentsInChildren<TrailRenderer>(true);
        foreach (var trail in trails)
        {
            trail.Clear();
        }

        // GoapAgent をリセット
        var goapAgent = obj.GetComponent<GoapAgent>();
        if (goapAgent != null)
        {
            goapAgent.ResetAgent();
        }

        // EnemyStatus をリセット
        var status = obj.GetComponent<EnemyStatus>();
        if (status != null)
        {
            status.ResetEnemy();
        }

        // 最後に NavMeshAgent を無効化
        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}