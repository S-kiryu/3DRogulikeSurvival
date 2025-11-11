using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private bool _isGrounded = true;
    private CharacterController _controller;
    private Vector2 _moveInput;
    private Rigidbody _rb;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    //On○○という名前のメソッドはInput Systemから自動的に呼び出される
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        //インプットシステムからの移動入力を取得
        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;
        //移動速度を計算
        Vector3 moveVelocity = move * _speed;
        //Rigidbodyの速度を更新
        Vector3 newVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);

        _rb.linearVelocity = newVelocity;
    }
}
