using UnityEngine;

public class DropXp : MonoBehaviour
{
    [SerializeField] private int xpAmount = 5;
    [SerializeField] private float dropRadius = 1.0f;
    public void Drop()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            
            XPpool.Instance.Get(dropPosition);
        }
    }
}
