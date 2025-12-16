using UnityEngine;

public class EnemyTeik : MonoBehaviour
{
    [SerializeField]private PlayerStatus _playerStatus;
    [SerializeField]private int _damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _playerStatus.TakeDamage(_damageAmount);
        }
    }
}
