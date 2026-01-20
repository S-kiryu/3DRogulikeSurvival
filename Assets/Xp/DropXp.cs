using UnityEngine;

public class DropXp : MonoBehaviour
{
    // ドロップするXPの量
    [SerializeField] private int xpAmount = 5;
    // ドロップ範囲の半径
    [SerializeField] private float dropRadius = 1.0f;
    public void Drop()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, randomOffset.x);
            
            XPpool.Instance.Get(dropPosition);
        }
    }
}
