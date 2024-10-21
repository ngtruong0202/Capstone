using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItems : HotbarItem
{
    [Header("Item Data")]
    [SerializeField] private Rarity rarity;
    [SerializeField] [Min(0)] private int sellPrice = 1;
    [SerializeField] [Min(1)] private int maxStack = 1;
     
    public override string ColouredName
    {
        get
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
            return $"<color=#{hexColour}>{Name}</color>";
        }
    }

    public int SellPrice => sellPrice;
    public int MaxStack => maxStack;
    public Rarity Rarity => rarity;
}
