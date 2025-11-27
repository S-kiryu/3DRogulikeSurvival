using UnityEngine;

public class RigidbodyMovementExecutor : MonoBehaviour, IMovementExecutor
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ｘとｚ方向に移動
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public void Execute(Vector2 direction, float speed)
    {
        // 入力がない場合は停止
        if (direction.sqrMagnitude <= 0.01f)
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            return;
        }

        //ローカル方向に変換、forwardとrightでオブジェクトの前と後ろを判定
        Vector3 moveDirection = transform.forward * direction.y + transform.right * direction.x;
        moveDirection = moveDirection.normalized;

        // 移動（Y軸速度は保持）
        Vector3 velocity = moveDirection * speed;
        velocity.y = _rb.linearVelocity.y;
        _rb.linearVelocity = velocity;
    }
}