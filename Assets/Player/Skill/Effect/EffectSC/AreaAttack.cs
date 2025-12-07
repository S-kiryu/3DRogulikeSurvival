using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackEffect", menuName = "Game/SkillEffect/AreaAttack")]
public class AreaAttack: SkillEffect
{
    //[SerializeField] private EffectFolder _effectFolder;

    public override void Apply(PlayerStatus player) 
    {
        Debug.Log("AreaAttack Effect Applied");
        //if (_effectFolder._katanaObject != true)
        //{
        //    _effectFolder._katanaObject.SetActive(true);
        //}
        //else 
        //{
        //    //‚±‚±‚É‘‰Á‚Ìˆ—‚ğ‘‚­
        //}
    }
}
