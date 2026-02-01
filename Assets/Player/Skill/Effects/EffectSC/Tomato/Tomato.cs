using UnityEngine;
[CreateAssetMenu(fileName = "TomatoEffect", menuName = "Game/SkillEffect/GatTomato")]
public class Tomato : SkillEffect
{
    public override void Apply(PlayerStatus player)
    {
        player.effectController.SetEffect(EffectType.Tomato, true);
    }
}