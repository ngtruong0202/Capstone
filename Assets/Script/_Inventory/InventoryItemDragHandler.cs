using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryItemDragHandler : ItemDragHandler
{
    [SerializeField] private ItemDestroyer itemDestroyer = null;

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);

            if (eventData.hovered.Count == 0)
            {
                InventorySlot thisSlot = itemSlotUI as InventorySlot;
                itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
            }
        }
    }
}
