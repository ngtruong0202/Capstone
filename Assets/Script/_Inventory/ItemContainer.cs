﻿using System;
using UnityEngine;

[Serializable]


public class ItemContainer : IItemContainer
{
    private ItemSlot[] itemSlots = new ItemSlot[0];

    public Action OnItemUpdated = delegate { };

    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];
    public ItemContainer(int size) => itemSlots = new ItemSlot[size];

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == itemSlot.item)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                    if (itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;
                        itemSlot.quantity = 0;

                        OnItemUpdated.Invoke();

                        return itemSlot;
                    }
                    else if(slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0;i < itemSlots.Length;i++)
        {
            if (itemSlots[i].item == null)
            {
                if (itemSlot.quantity <= itemSlot.item.MaxStack)
                {
                    itemSlots[i] = itemSlot;
                    itemSlot.quantity = 0;

                    OnItemUpdated.Invoke();

                    return itemSlot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }
            }
        }

        OnItemUpdated.Invoke();

        return itemSlot;
    }

    public int GetTotalQuantity(InventoryItems item)
    {
        int totalCount = 0;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if(itemSlot.item == null) { continue; }
            if(itemSlot.item != item) {continue; }

            totalCount += itemSlot.quantity;
        }


        return totalCount;
    }

    public bool HasItem(InventoryItems item)
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item == null) { continue; }
            if (itemSlot.item != item) { continue; }

            return true;
        }
        return false;
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
       for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null)
            {
                if (itemSlots[i].item == itemSlot.item)
                {
                    if (itemSlots[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity = itemSlots[i].quantity;
                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;

                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            OnItemUpdated.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void RemoveAt(int slotIndex)
    {
        if(slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

        itemSlots[slotIndex] = new ItemSlot();

        OnItemUpdated.Invoke();

    }

    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];
        if(firstSlot == secondSlot) { return; }

        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;
                if(firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;

                    itemSlots[indexOne] = new ItemSlot();

                    OnItemUpdated.Invoke();

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        OnItemUpdated.Invoke();
    }
}