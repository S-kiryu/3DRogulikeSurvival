using UnityEngine;
using System.Collections;

public class DropXp : MonoBehaviour
{
    [SerializeField] private int xpAmount = 1;
    [SerializeField] private float dropRadius = 1.0f;
    [SerializeField] private float dropInterval = 0.05f;

    private Coroutine _dropCoroutine;
    private int _jimenLayerMask;

    private void Awake()
    {
        _jimenLayerMask = LayerMask.GetMask("jimen");
    }

    public void Drop()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 rayStartPos = transform.position + new Vector3(randomOffset.x, 5f, randomOffset.y);

            if (Physics.Raycast(rayStartPos, Vector3.down, out RaycastHit hit, 10f, _jimenLayerMask))
            {
                Vector3 dropPosition = hit.point + Vector3.up * 0.05f;
                XPpool.Instance.Get(dropPosition);
            }
        }
    }


    private IEnumerator DropRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(dropInterval);

        for (int i = 0; i < xpAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 rayStartPos = transform.position + new Vector3(randomOffset.x, 5f, randomOffset.y);

            if (Physics.Raycast(rayStartPos, Vector3.down, out RaycastHit hit, 10f, _jimenLayerMask))
            {
                Vector3 dropPosition = hit.point + Vector3.up * 0.05f;
                XPpool.Instance.Get(dropPosition);
            }

            yield return wait;
        }

        _dropCoroutine = null;
    }

    private void OnDisable()
    {
        if (_dropCoroutine != null)
        {
            StopCoroutine(_dropCoroutine);
            _dropCoroutine = null;
        }
    }
}