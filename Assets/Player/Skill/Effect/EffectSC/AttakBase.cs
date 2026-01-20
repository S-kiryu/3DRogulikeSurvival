using UnityEngine;

public abstract class AttakBase : MonoBehaviour, IAttack
{
    protected PlayerStatus _playerStatus;

    protected virtual void Awake()
    {
        _playerStatus = GetComponentInParent<PlayerStatus>();
        
        if (_playerStatus == null)
        {
            Debug.LogError("PlayerStatus ‚ªe‚ÉŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        }

    }

    public abstract EffectType Type { get; }

    public abstract float CoolTime { get; }

    public abstract void Attack();
}
