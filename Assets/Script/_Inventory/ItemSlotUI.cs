using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Image itemIconImage = null;

    public int SlotIndex { get; private set; }

    public abstract HotbarItem SlotItem { get; set; }

    private void OnEnable() => UpdateSlotUI();
    
    protected virtual void Start()
    {
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    public abstract void OnDrop(PointerEventData eventData);
    protected abstract void UpdateSlotUI();
    protected virtual void EnableSlotUI(bool enable)
    {
        itemIconImage.enabled = enable;
    }
}