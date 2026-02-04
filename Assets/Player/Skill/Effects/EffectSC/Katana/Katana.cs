using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    [SerializeField] private int bladeCount = 1;

    public override void Apply(PlayerStatus player)
    {
        player.KnaifeOrbit.AddSpawnBlades(bladeCount);
    }
}