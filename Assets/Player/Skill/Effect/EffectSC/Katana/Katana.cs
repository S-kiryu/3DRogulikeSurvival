using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    public override void Apply(PlayerStatus player)
    {
        player.effectController.SetEffect(EffectType.Katana, true);
    }
}