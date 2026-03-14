using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private MovementSettings _settings;

    private IInputProvider _inputProvider;
    private IMovementExecutor _movementExecutor;

    private void Awake()
    {
        _inputProvider = GetComponent<IInputProvider>();
        _movementExecutor = GetComponent<IMovementExecutor>();
    }

    private void Update()
    {
        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsPaused)
        {
            _movementExecutor.Stop();
            return;
        }

        // “ü—ÍŽæ“¾
        Vector2 moveInput = _inputProvider.MoveInput;
        bool isDashing = _inputProvider.IsDashing;


        float speed = _settings.GetSpeed(isDashing);
        _movementExecutor.Execute(moveInput, speed);
    }
}