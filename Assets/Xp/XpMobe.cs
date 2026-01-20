using UnityEngine;

public class XpMobe : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField]private float upDistance = 1f;     // 上に動く距離
    [SerializeField]private float downDistance = 1f;   // 下に動く距離
    [SerializeField]private float speed = 1f;          // 往復スピード

    private Vector3 startPos;
    private Vector3 upPos;
    private Vector3 downPos;

    void Start()
    {
        // 現在の位置を基準にする
        startPos = transform.position;

        // 上下の移動範囲を設定
        upPos = startPos + Vector3.up * upDistance;
        downPos = startPos - Vector3.up * downDistance;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);

        transform.position = Vector3.Lerp(downPos, upPos, t);
    }
}
