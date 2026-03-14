using UnityEngine;

[CreateAssetMenu(fileName = "AreaAttackUp", menuName = "Game/SkillEffect/AreaAttack/SpeedUp")]
public class AreaUpSpeed : SkillEffect
{
    [SerializeField] private float _coolTime = 5f;
    public override void Apply(PlayerStatus player)
    {
        var area = player.GetComponentInChildren<AreaMove>();
        if (area == null)
            Debug.Log("ƒGƒŠƒAmove‚ª‚È‚¢‚æ[");

        area.ReduceCoolTime(_coolTime);
    }
}