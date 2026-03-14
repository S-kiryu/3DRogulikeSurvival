using UnityEngine;

public class UiOff : MonoBehaviour
{
    [SerializeField] private GameObject[] uiObjects;

    public void SetUIOFF()
    {
        foreach (var obj in uiObjects)
        {
            obj.SetActive(false);
        }
    }
}