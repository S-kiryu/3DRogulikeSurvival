using UnityEngine;

public class EnemyTeik : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10;

    private float _cooldown = 1f;
    private float _nextHitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < _nextHitTime) return;

        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null && playerStatus.IsAlive)
            {
                playerStatus.TakeDamage(_damageAmount);
                _nextHitTime = Time.time + _cooldown;
            }
        }
    }

}
