using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input;
    private IMovement _movement;
    private PlayerInputReader _playerInputReader;

    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerInputReader>();
        _input = GetComponent<IPlayerInput>();
        _movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        if(_playerInputReader.IsDashing == true)
        {
            var dashMovement = GetComponent<DashMovement>();
            if (dashMovement != null)
            {
                dashMovement.Dash(_playerInputReader.MoveInput);
            }
            else
            {
                Debug.LogError("DashMovement コンポーネントがないよ");
            }
        }
        else
        {
            //移動入力を取得して移動処理を呼び出す
            _movement.Move(_input.MoveInput);
        }
    }
}
