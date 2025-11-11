using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input;
    private DashMovement _dashMovement;
    private PlayerInputReader _inputReader;

    private void Awake()
    {
        _input = GetComponent<IPlayerInput>();
        _dashMovement = GetComponent<DashMovement>();
        _inputReader = GetComponent<PlayerInputReader>();
    }

    private void Update()
    {
        // ダッシュ状態を更新
        _dashMovement.SetDashing(_inputReader.IsDashing);

        // 常に入力方向に移動
        _dashMovement.Move(_input.MoveInput);
    }
}
