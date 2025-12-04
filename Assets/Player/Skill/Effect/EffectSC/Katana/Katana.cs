using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    [SerializeField] private EffectFolder _effectFolder;
    public override void Apply(PlayerStatus player)
    {
        _effectFolder.Katana.SetActive(true);
    }
}