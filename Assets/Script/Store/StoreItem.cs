using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StoreItem : MonoBehaviour
{
    Item item;
    StoreBuy storeBuy;

    public Image image;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button buybutton;

    private void Awake()
    {
        storeBuy = FindAnyObjectByType<StoreBuy>();
    }

    private void Start()
    {

        buybutton.onClick.AddListener(EnableBuyPanel);

    }

    public void EnableBuyPanel()
    {
        storeBuy.BuyPanel(item);

    }

    public void UpDateUIStore(Item _item)
    {
        item = _item;

        priceText.text = item.price.ToString();
        nameText.text = item.itemName.ToString();
        image.sprite = SpriteManager.Instance.GetSpriteByName(item.spriteURL);
    }

}
