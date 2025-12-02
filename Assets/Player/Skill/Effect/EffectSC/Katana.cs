using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    [SerializeField] private GameObject _katana;

    public override void Apply(PlayerStatus player)
    {
        _katana.SetActive(true);
    }
}