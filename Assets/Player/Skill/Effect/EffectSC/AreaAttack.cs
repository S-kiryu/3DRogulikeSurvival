using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackEffect", menuName = "Game/SkillEffect/AreaAttack")]
public class AreaAttack: SkillEffect
{
    [SerializeField] private EffectFolder _effectFolder;

    public override void Apply(PlayerStatus player) 
    {
        if (_effectFolder.AreaAttack != true)
        {
            _effectFolder.AreaAttack.SetActive(true);
        }
        else 
        {
            //‚±‚±‚É‘‰Á‚Ìˆ—‚ğ‘‚­
        }
    }
}
