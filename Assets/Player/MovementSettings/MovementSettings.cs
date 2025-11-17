using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Player/Movement Settings")]
public class MovementSettings : ScriptableObject
{
    [SerializeField] private float _normalSpeed = 5f;
    [SerializeField] private float _dashSpeed = 10f;

    public float GetSpeed(bool isDashing)
    {
        return isDashing ? _dashSpeed : _normalSpeed;
    }
}