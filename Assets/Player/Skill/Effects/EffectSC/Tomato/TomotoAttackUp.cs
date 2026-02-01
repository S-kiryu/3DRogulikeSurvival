using UnityEngine;

[CreateAssetMenu(fileName = "TomatoAttackUp", menuName = "Game/SkillEffect/TomatoAttack/AttackUp")]
public class TomatoAttackUp : SkillEffect
{
    [SerializeField] private float _attackDamageUp = 5f;
    public override void Apply(PlayerStatus player)
    {
        var area = player.GetComponentInChildren<Tomatoshot>();
        if (area == null)
            Debug.Log("Ç∆Ç‹Ç∆Ç™Ç»Ç¢ÇÊÅ[");

        area.AddDamage(_attackDamageUp);
    }
}