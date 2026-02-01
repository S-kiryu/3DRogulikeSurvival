using UnityEngine;
[CreateAssetMenu(fileName = "PowerUpEffect", menuName = "Game/SkillEffect/PowerUp")]
public class PowerUpEffect : SkillEffect
{
    [SerializeField] private float _increaseAmount = 5f;

    public override void Apply(PlayerStatus player)
    {
        player.AddAttackPower(_increaseAmount);
    }
}
