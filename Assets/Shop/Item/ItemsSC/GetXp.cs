using UnityEngine;

public class GetXp : MonoBehaviour
{
    [SerializeField] private int xpAmount = 10;
    [SerializeField] private PlayerStatus playerStatus;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                playerStatus.AddExp(xpAmount);
                Debug.Log("プレイヤーがXPを取得しました！");
                Destroy(gameObject);
            
        }
    }
}
