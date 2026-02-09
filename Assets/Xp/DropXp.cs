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

            // 少し上空からRayを飛ばす
            Vector3 rayStartPos = transform.position
                + new Vector3(randomOffset.x, 5f, randomOffset.y);

            Ray ray = new Ray(rayStartPos, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("jimen")))
            {
                Vector3 dropPosition = hit.point;

                // 少し浮かせたい場合（埋まり防止）
                dropPosition += Vector3.up * 0.05f;

                XPpool.Instance.Get(dropPosition);
            }

            yield return new WaitForSeconds(dropInterval);
        }
    }

}
