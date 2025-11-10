using UnityEngine;
public class RigidbodyMovement : MonoBehaviour, IMovement
{
    //移動速度
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 input)
    {
        //引数を元にRigidbodyの速度を設定する、ベクトルの長さをに１正規化してから速度を掛ける
        Vector3 move = new Vector3(input.x, 0f, input.y).normalized;
        Vector3 moveVelocity = move * _speed;
        //Y軸の速度はそのままにする
        Vector3 newVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
        //Velocityを更新
        _rb.linearVelocity = newVelocity;
    }
}
