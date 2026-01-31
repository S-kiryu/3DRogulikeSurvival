using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackUp", menuName = "Game/SkillEffect/AreaAttack/AttackUp")]
public class AreaUpGrade : SkillEffect
{
    [SerializeField] private float _attackDamageUp = 5f;
    public override void Apply(PlayerStatus player)
    {
        var area = player.GetComponentInChildren<AreaMove>();
        if (area == null)
            Debug.Log("ƒGƒŠƒAmove‚ª‚È‚¢‚æ[");

        area.AddAttackDamageUp(_attackDamageUp);
    }
}