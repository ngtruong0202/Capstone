using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]

public class Item : ScriptableObject
{
    public ItemType itemtype;
    public string itemID;
    public string itemName;
    public string itemDescription;
    public string boderURL;
    public string spriteURL;
    public Sprite image;

    public int quantity;
    public bool useItem;
    public bool stackable;
    public int maxStackable;

    public int price;

    public enum ItemType
    {
        ingredient,
        items,
        quest,
    }
}
