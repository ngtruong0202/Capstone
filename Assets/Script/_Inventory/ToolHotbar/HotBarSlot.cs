using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotBarSlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI itemQuantityText;

    private HotbarItem slotItem = null;

    protected override void Start()
    {
        inventory.ItemContainer.OnItemUpdated += UpdateSlotUI;
    }

    public override HotbarItem SlotItem
    {
        get { return slotItem; }
        set { slotItem = value; UpdateSlotUI(); }
    }

    public bool AddItem(HotbarItem itemToAdd)
    {
        if (slotItem != null) return false;
        slotItem = itemToAdd;
        return true;
    }

    public void UseSlot(int index)
    {
        if(index != SlotIndex) return;
        //use item
    }

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if(itemDragHandler == null) return;

        InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
        if(inventorySlot != null)
        {
            SlotItem = inventorySlot.ItemSlot.item;
            return;
        }

        HotBarSlot hotBarSlot = itemDragHandler.ItemSlotUI as HotBarSlot;
        if(hotBarSlot != null)
        {
            HotbarItem oldItem = SlotItem; 
            SlotItem = hotBarSlot.SlotItem;
            hotBarSlot.SlotItem = oldItem;
            return;
        }
    }

    protected override void UpdateSlotUI()
    {
        if (SlotItem == null)
        {
            EnableSlotUI(false);
            return;
        }

        itemIconImage.sprite = SlotItem.Icon;
        EnableSlotUI(true);
        SetIemQuantityUI();
    }

    private void SetIemQuantityUI()
    {
        if(SlotItem is InventoryItems inventoryItems)
        {
            if(inventory.ItemContainer.HasItem(inventoryItems))
            {
                int quantityCount = inventory.ItemContainer.GetTotalQuantity(inventoryItems);
                itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
            }
            else
            {
                SlotItem = null;
            }
        }
        else
        {
            itemQuantityText.enabled = false;
        }
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;

    }
}
