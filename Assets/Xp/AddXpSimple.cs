using UnityEngine;
using UnityEngine.InputSystem;

public class AddXpSimple : MonoBehaviour
{
    private PlayerStatus _status;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            _status.AddExp(5);
            Debug.Log("KÉLÅ[Ç≈XPÇí«â¡ÅI");
        }
    }
}
