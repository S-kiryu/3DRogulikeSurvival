using UnityEngine;
using System.Collections;

public class XpMobe : MonoBehaviour
{
    [Header("移動距離")]
    public float minMove = 0.5f;
    public float maxMove = 2.5f;

    [Header("スピード")]
    public float minSpeed = 0.5f;
    public float maxSpeed = 2.0f;

    [Header("待ち時間")]
    public float minWait = 0.2f;
    public float maxWait = 1.0f;

    private Vector3 basePos;
    private Vector3 targetPos;
    private float speed;

    void Start()
    {
        basePos = transform.position;
        DecideNextTarget();
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            DecideNextTarget();
        }
    }

    void DecideNextTarget()
    {
        // 上か下か
        float dir = Random.value > 0.5f ? 1f : -1f;

        float distance = Random.Range(minMove, maxMove);
        speed = Random.Range(minSpeed, maxSpeed);

        // ★ Y軸だけを動かす
        targetPos = new Vector3(
            basePos.x,
            basePos.y + distance * dir,
            basePos.z
        );
    }
}
