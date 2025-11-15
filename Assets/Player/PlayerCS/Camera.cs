using UnityEngine;

public class PlayerDirectionByMoveAndCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    public void RotateByInput(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01f) return;

        // カメラ基準の移動方向（カメラスティック操作式）
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 dir = camForward * input.y + camRight * input.x;

        transform.rotation = Quaternion.LookRotation(dir);
    }
}
