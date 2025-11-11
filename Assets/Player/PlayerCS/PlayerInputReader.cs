using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    private Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;

    private bool _isDashing = false;
    public bool IsDashing => _isDashing;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        _isDashing = context.ReadValueAsButton();
    }
}
