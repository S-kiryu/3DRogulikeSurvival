using UnityEngine;
using UnityEngine.Windows;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private void FixedUpdate()
    {
        // カメラのY軸の角度を取得
        float targetY = _cameraTransform.eulerAngles.y;

        // プレイヤーの現在の角度
        Vector3 playerRotation = transform.eulerAngles;

        // プレイヤーのYだけをカメラに合わせる
        transform.rotation = Quaternion.Euler(0f, targetY, 0f);
    }
}
