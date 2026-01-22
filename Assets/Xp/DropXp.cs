using UnityEngine;
using System.Collections;

public class DropXp : MonoBehaviour
{
    // ドロップするXPの量
     private int xpAmount = 1;
    // ドロップ範囲の半径
    [SerializeField] private float dropRadius = 1.0f;
    // ドロップ間隔
    [SerializeField] private float dropInterval = 0.05f;

    public void Drop()
    {
        Debug.Log("XPドロップ開始");
        // Coroutine 開始
        StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = transform.position
                + new Vector3(randomOffset.x, randomOffset.y, 0f);

            XPpool.Instance.Get(dropPosition);

            // 少し待つ
            yield return new WaitForSeconds(dropInterval);
        }
    }
}
