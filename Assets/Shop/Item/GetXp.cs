using UnityEngine;

public class GetXp : MonoBehaviour
{
    [SerializeField] private int _xpAmount = 10;
    [SerializeField] private PlayerStatus _playerStatus;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                _playerStatus.AddExp(_xpAmount);
                Debug.Log("プレイヤーがXPを取得しました！");
                Destroy(gameObject);
            
        }
    }
}
