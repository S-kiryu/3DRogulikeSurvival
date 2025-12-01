using UnityEngine;

public class PowerUpEffect : SkillEffect
{
    public float increaseAmount = 5f;

    public override void Apply(PlayerStatus player)
    {
        player.AddAttackPower(increaseAmount);
    }
}
