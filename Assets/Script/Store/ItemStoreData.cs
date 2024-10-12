using System;
using System.Collections.Generic;
using static Item;

[Serializable]

public class ItemStoreData
{
    public Dictionary<ItemType, List<Item>> categorizedInventory = new Dictionary<ItemType, List<Item>>
    {
        { ItemType.items, new List<Item>() },
        { ItemType.ingredient, new List<Item>() },
        { ItemType.quest, new List<Item>() },

    };

    public List<Item> GetItemsByType(ItemType itemType)
    {
        if (categorizedInventory.TryGetValue(itemType, out List<Item> items))
        {
            return items;
        }
        return new List<Item>();
    }
}
