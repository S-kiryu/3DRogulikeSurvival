using UnityEngine;

[CreateAssetMenu(fileName = "Kabotya", menuName = "ScriptableObjects/Item/Kabotya")]
public class Kabotya : ItemAction
{
    public override void Execute(ItemData item)
    {
        Debug.Log("Kabotya ‚ğw“ü!");
    }
}