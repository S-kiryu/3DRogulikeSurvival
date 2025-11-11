using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    private Vector2 _moveInput;
    private bool _isDashing = false;
    public bool IsDashing => _isDashing;
    public Vector2 MoveInput => _moveInput;

    public void OnMove(InputValue value) => _moveInput = value.Get<Vector2>();

    public void OnSprint(InputValue value)
    {

        if (value.isPressed)
        {
            _isDashing = true;

        }
        else
        {
            _isDashing = false;
        }
    }
}