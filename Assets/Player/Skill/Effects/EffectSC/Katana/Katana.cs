using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
     private int bladeCount = 1;

    public override void Apply(PlayerStatus player)
    {
        Debug.Log("カタナスキル効果発動");
        player.KnaifeOrbit.AddSpawnBlades(bladeCount);
    }
}