using UnityEngine;

public class DropXp : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int xpAmount = 5;
    [SerializeField] private float dropRadius = 1.0f;
    public void Drop()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            Instantiate(xpPrefab, dropPosition, Quaternion.identity);
        }
    }
}
