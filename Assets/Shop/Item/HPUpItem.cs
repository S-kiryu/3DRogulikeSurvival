using UnityEngine;

[CreateAssetMenu(fileName = "HPUpItem", menuName = "ScriptableObjects/Item/HPUpItem")]
public class HPUpItem : ItemAction
{
    private int AddHP = 20;

    public override void Execute(ItemData item)
    {
        PlayerStatusManager.Instance.AddMaxHP(AddHP);
    }
}