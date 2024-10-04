using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    public string description;
    public string icon;
    public ItemType itemType;

    public int quantity;
    public bool useItem;
    public bool stackable;
    public int maxStackable;

    public enum ItemType
    {
        equip,
        ingredient,
        product,
        items,
        other,
        promotion,
        seed
    }

    public abstract void Use();
}
