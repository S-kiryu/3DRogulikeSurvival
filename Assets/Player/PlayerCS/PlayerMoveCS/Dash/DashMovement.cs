using UnityEngine;

public class DashMovement : RigidbodyMovement
{
    [SerializeField,Tooltip("ダッシュスピード")] private float _dashSpeed = 10f;
    [SerializeField,Tooltip("通常の移動スピード")] private float _normalSpeed = 5f;
    private bool _isDashing = false;

    public bool IsDashing => _isDashing;

    public void SetDashing(bool isDashing)
    {
        _isDashing = isDashing;
    }

    public override void Move(Vector2 input)
    {
        // 入力がない場合は止まる
        if (input.sqrMagnitude <= 0.01f)
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            return;
        }

        // ダッシュ中は速度を上げる
        float currentSpeed = _isDashing ? _dashSpeed : _normalSpeed;

        //引数を元にRigidbodyの速度を設定する、ベクトルの長さをに１正規化してから速度を掛ける
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized * currentSpeed;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _rb.MoveRotation(targetRotation);

        Vector3 moveVelocity = transform.forward * currentSpeed;

        // Y軸の速度は保持（ジャンプなど残す）
        moveVelocity.y = _rb.linearVelocity.y;

        _rb.linearVelocity = moveVelocity;
    }
}
