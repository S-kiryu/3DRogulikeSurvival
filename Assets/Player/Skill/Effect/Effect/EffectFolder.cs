using UnityEngine;

public class EffectFolder : MonoBehaviour
{
    [SerializeField] private GameObject _katanaObject;
    public GameObject Katana => _katanaObject;
    [SerializeField] public GameObject _areaAttack;
    public GameObject AreaAttack => _areaAttack;
}
