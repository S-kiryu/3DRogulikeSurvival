using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackEffect", menuName = "Game/SkillEffect/AreaAttack")]
public class AreaAttack: SkillEffect
{

    public override void Apply(PlayerStatus player) 
    {
        if (player.effectController.GetEffectActive(EffectType.AreaAttack) == false)
        {
            player.effectController.SetEffect(EffectType.AreaAttack, true);
        }
    }
}
