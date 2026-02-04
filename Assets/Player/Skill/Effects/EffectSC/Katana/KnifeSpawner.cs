using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    [SerializeField] private GameObject bladePrefab;
    [SerializeField] private float radius = 2f;
    //ナイフの回転速度
    [SerializeField] private float rotateSpeed = 180f;

    private List<GameObject> blades = new List<GameObject>();

    private void Update()
    {
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

    // 円形に均等再配置
    private void RepositionBlades()
    {
        int count = blades.Count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * (360f / count);
            Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
            blades[i].transform.localPosition = offset;
        }
    }
}
