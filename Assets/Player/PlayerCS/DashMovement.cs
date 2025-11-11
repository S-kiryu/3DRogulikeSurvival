using System.Collections;
using UnityEngine;

public class DashMovement : RigidbodyMovement
{
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDuration = 0.2f;

    public override void Move(Vector2 input)
    {
        base.Move(input);
    }

    public void Dash(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0) // “ü—Í‚ª‚ ‚éŽž‚¾‚¯ƒ_ƒbƒVƒ…
        {
            StartCoroutine(DashCoroutine(direction));
        }
    }

    private IEnumerator DashCoroutine(Vector2 dir)
    {
        Vector3 move = new Vector3(dir.x, 0f, dir.y).normalized * _dashSpeed;
        _rb.linearVelocity = new Vector3(move.x, _rb.linearVelocity.y, move.z);

        yield return new WaitForSeconds(_dashDuration);
    }
}