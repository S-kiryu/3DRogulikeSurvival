using System.Collections;
using UnityEngine;

public class DashMovement : RigidbodyMovement
{
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDuration = 0.2f;

    private bool _isDashing = false;
    public bool IsDashing => _isDashing;

    public override void Move(Vector2 input)
    {
        if (_isDashing) return;
        base.Move(input);
    }

    public void StartDash(Vector2 direction)
    {
        if (!_isDashing && direction.sqrMagnitude > 0)
        {
            StartCoroutine(DashCoroutine(direction));
        }
    }

    private IEnumerator DashCoroutine(Vector2 dir)
    {
        _isDashing = true;
        float timer = 0f;

        while (timer < _dashDuration)
        {
            Vector3 move = new Vector3(dir.x, 0f, dir.y).normalized * _dashSpeed;
            _rb.linearVelocity = new Vector3(move.x, _rb.linearVelocity.y, move.z);
            timer += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;
    }
}