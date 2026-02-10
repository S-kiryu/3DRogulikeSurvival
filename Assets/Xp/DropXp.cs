using UnityEngine;
using System.Collections;

public class DropXp : MonoBehaviour
{
    private int xpAmount = 1;
    [SerializeField] private float dropRadius = 1.0f;
    [SerializeField] private float dropInterval = 0.05f;

    // ⭐ コルーチンの参照を保持
    private Coroutine _dropCoroutine;

    public void Drop()
    {
        Debug.Log("XPドロップ開始");

        // ⭐ 既存のコルーチンがあれば停止
        if (_dropCoroutine != null)
        {
            StopCoroutine(_dropCoroutine);
        }

        _dropCoroutine = StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 rayStartPos = transform.position + new Vector3(randomOffset.x, 5f, randomOffset.y);
            Ray ray = new Ray(rayStartPos, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("jimen")))
            {
                Vector3 dropPosition = hit.point;
                dropPosition += Vector3.up * 0.05f;
                XPpool.Instance.Get(dropPosition);
            }

            yield return new WaitForSeconds(dropInterval);
        }

        // ⭐ コルーチン終了時にクリア
        _dropCoroutine = null;
    }

    // ⭐ 重要！オブジェクトが無効化されたらコルーチンを停止
    private void OnDisable()
    {
        if (_dropCoroutine != null)
        {
            StopCoroutine(_dropCoroutine);
            _dropCoroutine = null;
        }
    }
}