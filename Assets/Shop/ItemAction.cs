using Mono.Cecil;
using UnityEngine;

public abstract class ItemAction : ScriptableObject
{
    public abstract void Execute(ItemData item);

    protected PlayerStatusManager Player => PlayerStatusManager.Instance;
}