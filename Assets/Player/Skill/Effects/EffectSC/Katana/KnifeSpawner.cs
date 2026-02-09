using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    [SerializeField] private GameObject bladePrefab;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float rotateSpeed = 180f;

    private readonly List<GameObject> blades = new();

    private void Update()
    {
        // 親だけを回す
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void AddSpawnBlades(int addCount = 1)
    {
        for (int i = 0; i < addCount; i++)
        {
            CreateBlade();
        }
        RepositionBlades();
    }

    private void CreateBlade()
    {
        GameObject blade = Instantiate(bladePrefab, transform);
        blades.Add(blade);
    }

    // 円形に均等配置 + 外向き回転
    private void RepositionBlades()
    {
        int count = blades.Count;
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            float angle = i * (360f / count);

            // 円周上の位置
            Vector3 offset =
                Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;

            blades[i].transform.localPosition = offset;

            //外側を向かせる
            blades[i].transform.localRotation =
                Quaternion.LookRotation(-offset.normalized, Vector3.up);
        }
    }
}
