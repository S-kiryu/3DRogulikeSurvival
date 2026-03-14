using UnityEngine;

public class UiOn : MonoBehaviour
{
    [SerializeField] private GameObject[] uiObjects;
    public void SetUION()
    {
        foreach (var obj in uiObjects)
        {
            obj.SetActive(true);
        }
    }
}
