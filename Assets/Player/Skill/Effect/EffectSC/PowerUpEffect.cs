using UnityEngine;
[CreateAssetMenu(fileName = "PowerUpEffect", menuName = "Game/SkillEffect/PowerUp")]
public class PowerUpEffect : SkillEffect
{
    public float increaseAmount = 1.5f;

    public override void Apply(PlayerStatus player)
    {
        player.AddAttackPower(increaseAmount);
    }
}
