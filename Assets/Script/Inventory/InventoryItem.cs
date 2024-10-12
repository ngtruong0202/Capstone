using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Header("UI")]
    public Image image;
    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;


    //public TextMeshProUGUI quantityTxt;
    //public Image highlightImage;
    //public Image itemIconImage;


    //public void UpdateItem(Item items)
    //{

    //    //hiển thị ảnh
    //    itemIconImage.sprite = SpriteManager.Instance.GetSpriteByName(items.spriteURL);
    //    quantityTxt.text = items.quantity.ToString();
    //    bool textActive = items.quantity > 1;
    //    quantityTxt.gameObject.SetActive(textActive);

    //    highlightImage.enabled = false;
    //}

    //public void Highlight(bool isHighlighted)
    //{
    //    highlightImage.enabled = isHighlighted;
    //}


    public void InitialiseItem( Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

}
