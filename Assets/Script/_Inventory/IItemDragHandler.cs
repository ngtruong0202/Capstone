using UnityEngine.EventSystems;

public interface IItemDragHandler
{
    ItemSlotUI ItemSlotUI { get; }

    void OnDrag(PointerEventData eventData);
    void OnPointerDown(PointerEventData eventData);
    void OnPointerUp(PointerEventData eventData);
}