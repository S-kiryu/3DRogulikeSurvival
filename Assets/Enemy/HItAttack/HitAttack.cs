using UnityEngine;

public class EnemyTeik : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null && playerStatus.IsAlive)
            {
                playerStatus.TakeDamage(_damageAmount);
            }
        }
    }
}
