using UnityEngine;

public abstract class AttackBase : MonoBehaviour, IAttack
{
    protected PlayerStatus _playerStatus;

    protected virtual void Awake()
    {
        _playerStatus = GetComponentInParent<PlayerStatus>();

        if (_playerStatus == null)
        {
            Debug.LogError($"{GetType().Name} : PlayerStatus ‚ªe‚ÉŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        }

        //AttackManager ‚É“o˜^
        if (AttackManager.Instance != null)
        {
            AttackManager.Instance.RegisterAttack(this);
        }
        else
        {
            Debug.LogError("AttackManager ‚ª‘¶İ‚µ‚Ü‚¹‚ñ");
        }
    }


    public abstract EffectType Type { get; }
    public abstract float CoolTime { get; }
    public abstract void Attack();
}
