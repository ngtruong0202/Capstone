using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItem : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI quantityTxt;
    public Image highlightImage;
    public Image itemIconImage;

    public void UpdateItem(Item items)
    {
        
        //hiển thị ảnh
        //nameTxt.text = items.itemName;
        itemIconImage.sprite = SpriteManager.Instance.GetSpriteByName(items.spriteURL);
        quantityTxt.text = items.quantity.ToString();
        bool textActive = items.quantity > 1;
        quantityTxt.gameObject.SetActive(textActive);

        highlightImage.enabled = false;
    }

    public void Highlight(bool isHighlighted)
    {
        highlightImage.enabled = isHighlighted;
    }

}
