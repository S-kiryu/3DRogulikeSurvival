using UnityEngine;

public class RigidbodyMovementExecutor : MonoBehaviour, IMovementExecutor
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Execute(Vector2 direction, float speed)
    {
        // “ü—Í‚ª‚È‚¢ê‡‚Í’â~
        if (direction.sqrMagnitude <= 0.01f)
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            return;
        }

        // ˆÚ“®•ûŒü‚ğ3D‹óŠÔ‚É•ÏŠ·
        Vector3 moveDirection = transform.forward * direction.y + transform.right * direction.x;
        moveDirection = moveDirection.normalized;

        // ˆÚ“®iY²‘¬“x‚Í•Ûj
        Vector3 velocity = moveDirection * speed;
        velocity.y = _rb.linearVelocity.y;
        _rb.linearVelocity = velocity;
    }
}