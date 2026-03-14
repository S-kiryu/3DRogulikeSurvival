using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    Renderer[] rends;
    Camera cam;
    Plane[] planes;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
        cam = Camera.main;
    }

    void LateUpdate()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);

        foreach (var r in rends)
        {
            if (!r) continue;

            bool visible = GeometryUtility.TestPlanesAABB(planes, r.bounds);
            r.enabled = visible;
        }
    }
}
