﻿using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IItemDragHandler
{
    [SerializeField] protected ItemSlotUI itemSlotUI = null;

    private CanvasGroup canvasGroup = null;
    private Transform originalParent = null;
    private bool isHovering = false;

    public ItemSlotUI ItemSlotUI => itemSlotUI;

    private void Start() => canvasGroup = GetComponent<CanvasGroup>();

    private void OnDisable()
    {
        isHovering = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        isHovering = false;
    }
}