using UnityEngine;

[CreateAssetMenu(fileName = "KabotyItem", menuName = "ScriptableObjects/Item/KabotyItem")]
public class KabotyItem : ItemAction
{
    public override void Execute(ItemData item)
    {
        PlayerStatusManager.Instance.AddAttack(5);
    }
}