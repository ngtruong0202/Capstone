
using System;

[Serializable]
public struct ItemSlot
{
    public InventoryItems item;
    public int quantity;

    public ItemSlot(InventoryItems item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public static bool operator == (ItemSlot a, ItemSlot b) {  return a.Equals(b); }
    public static bool operator != (ItemSlot a, ItemSlot b) {  return a.Equals(b); }
}
