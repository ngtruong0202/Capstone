using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
[Serializable]
public class InventorySlot : ItemSlotUI, IDropHandler
{
    public Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    protected override void Start()
    {
        base.Start();
        inventory.ItemContainer.OnItemUpdated += UpdateSlotUI;
    }

    public override HotbarItem SlotItem
    {
        get { return ItemSlot.item; }
        set { }
    }

    public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if(itemDragHandler == null) { return; }

        if((itemDragHandler.ItemSlotUI as InventorySlot ) != null)
        {
            inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
        }
    }

    protected override void UpdateSlotUI()
    {
        if (ItemSlot.item == null)
        {
            EnableSlotUI(false);
            return;
        }

        EnableSlotUI(true);

        itemIconImage.sprite = ItemSlot.item.Icon;
        itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }
}