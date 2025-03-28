using UnityEngine;

[CreateAssetMenu(fileName = "Garbage Item",menuName = "Inventory/Items/New garbage Item")]
public class GarbageItem : ItemScriptableObject
{
    public float harmAmount;

    private void Start()
    {
        itemType = ItemType.Garbage;
    }
}