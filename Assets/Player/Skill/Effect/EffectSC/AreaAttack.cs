using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackEffect", menuName = "Game/SkillEffect/AreaAttack")]
public class AreaAttack: SkillEffect
{
    [SerializeField] private PlayerEffectController _playerEffectController;

    public override void Apply(PlayerStatus player) 
    {
        if (_playerEffectController.GetEffectActive(EffectType.Katana))
        {
            //‚±‚±‚É‘‰Á‚Ìˆ—‚ğ‘‚­
        }
        else
        {
            
        }
    }
}
