using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    [SerializeField] private PlayerEffectController _playerEffectController;
    public override void Apply(PlayerStatus player)
    {
        _playerEffectController.SetEffect(EffectType.Katana, true);
    }
}