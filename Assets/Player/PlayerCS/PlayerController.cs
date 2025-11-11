using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input;
    private DashMovement _dashMovement;

    private void Awake()
    {
        _dashMovement = GetComponent<DashMovement>();
        _input = GetComponent<IPlayerInput>();
    }

    private void Update()
    {
        if (_dashMovement.IsDashing)
        {
            // ダッシュ中はDashMovement側が処理するので何もしない
            return;
        }

        if ((_input as PlayerInputReader).IsDashing)
        {
                _dashMovement.StartDash(_input.MoveInput);
            Debug.Log("Dash!");
        }
        else
        {
            //移動入力を取得して移動処理を呼び出す
            _dashMovement.Move(_input.MoveInput);
            Debug.Log("Move");
        }
    }
}
