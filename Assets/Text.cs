using UnityEngine;

public class Text : MonoBehaviour
{

    private void Awake() => DontDestroyOnLoad(this.gameObject);
}
