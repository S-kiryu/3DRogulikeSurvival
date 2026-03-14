#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class AutoAddLODToPrefab
{
    [MenuItem("Tools/Prefab/Add LODGroup (Culled)")]
    static void AddLOD()
    {
        GameObject target = Selection.activeGameObject;
        if (target == null)
        {
            Debug.LogWarning("Prefab ‚Ü‚½‚Í GameObject ‚ð‘I‘ð‚µ‚Ä‚­‚¾‚³‚¢");
            return;
        }

        LODGroup lodGroup = target.GetComponent<LODGroup>();
        if (lodGroup == null)
            lodGroup = target.AddComponent<LODGroup>();

        var renderers = target.GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning("MeshRenderer ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
            return;
        }

        // LOD0‚Ì‚Ý + Culled
        LOD[] lods = new LOD[1];
        lods[0] = new LOD(0.15f, renderers);

        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();

        EditorUtility.SetDirty(target);
        Debug.Log($"LODGroup ‚ð {target.name} ‚É’Ç‰Á‚µ‚Ü‚µ‚½");
    }
}
#endif
