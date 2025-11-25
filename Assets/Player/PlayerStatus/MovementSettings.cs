using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Player/Movement Settings")]
public class MovementSettings : ScriptableObject
{
    [SerializeField] private float _normalSpeed = 5f;
    [SerializeField] private float _dashSpeed = 10f;

    /// <summary>
    /// isDashingがtrueならダッシュ速度、falseなら通常速度を返す
    /// </summary>
    /// <param name="isDashing"></param>
    /// <returns></returns>
    public float GetSpeed(bool isDashing)
    {
        return isDashing ? _dashSpeed : _normalSpeed;
    }
}