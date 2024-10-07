using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseItem;

[Serializable]

public class ItemData
{
    public Dictionary<ItemType, List<BaseItem>> categorizedItems = new Dictionary<ItemType, List<BaseItem>>
        {

        { ItemType.equip, new List<BaseItem>() },
        { ItemType.ingredient, new List<BaseItem>() },
        { ItemType.product, new List<BaseItem>() },
        { ItemType.items, new List<BaseItem>() },
        { ItemType.other, new List<BaseItem>() },
        { ItemType.seed, new List<BaseItem>() },
        };


    public List<BaseItem> GetItemByType(BaseItem.ItemType itemType)
    {
        if(categorizedItems.TryGetValue(itemType, out List<BaseItem> listItem))
        {
            return listItem;
        }

        return new List<BaseItem>();
    }
}
