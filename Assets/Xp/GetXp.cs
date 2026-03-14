using UnityEngine;

public class GetXp : MonoBehaviour
{
    [SerializeField] private int _xpAmount = 10;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();

            // プレイヤーステータスが存在し、生存している場合にXPを加算
            if (playerStatus != null && playerStatus.IsAlive) 
            {
                playerStatus.AddExp(_xpAmount);
                Debug.Log("プレイヤーがXPを取得しました！");
                Destroy(gameObject);

            }
        }
    }
}
