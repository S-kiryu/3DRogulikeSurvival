using UnityEngine;

public class RigidbodyMovementExecutor : MonoBehaviour, IMovementExecutor
{
    private Rigidbody _rb;
    private Vector3 _targetVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Execute(Vector2 direction, float speed)
    {
        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused)
        {
            _targetVelocity = Vector3.zero;
            return;
        }

        if (direction.sqrMagnitude <= 0.01f)
        {
            _targetVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 moveDirection =
            transform.forward * direction.y +
            transform.right * direction.x;

        moveDirection.Normalize();

        _targetVelocity = moveDirection * speed;
        _targetVelocity.y = _rb.linearVelocity.y;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _targetVelocity;
    }

    public void Stop()
    {
        _targetVelocity = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
    }
}
