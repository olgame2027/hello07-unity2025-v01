using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Default, Food, Weapon, Instrument, Garbage}

public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public int maximumAmount;
    public GameObject itemPrefab;
    public string itemDescription;
    public Sprite icon;
}
