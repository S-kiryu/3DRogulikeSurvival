using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    //移動入力
    private Vector2 _moveInput;

    //読み取り専用
    public Vector2 MoveInput => _moveInput;

    //Input Systemから呼び出される
    //On○○の名前はInput Actionの名前と一致させる必要がある
    //ここで値を受け取ってフィールドに保存する
    public void OnMove(InputValue value)　=> _moveInput = value.Get<Vector2>();
}
