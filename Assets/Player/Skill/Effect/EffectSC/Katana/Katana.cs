using UnityEngine;
[CreateAssetMenu(fileName = "KatanaEffect", menuName = "Game/SkillEffect/GatKatana")]
public class Katana : SkillEffect
{
    public override void Apply(PlayerStatus player)
    {
        player.KatanaObject.SetActive(true);
    }
}