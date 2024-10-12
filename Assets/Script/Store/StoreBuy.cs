using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreBuy : MonoBehaviour
{
    Currency currency;
    Item item;
    int totalPrice;
    int quantity;

    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI quantityText;
    public TextMeshProUGUI buyPriceText;
    public Slider quantitySlider;
    public Button buyButton;
    public GameObject buyPanel;

    private void Start()
    {
        buyPanel.SetActive(false);
        currency = new Currency(1000,1000);
        quantitySlider.minValue = 1;
        quantitySlider.onValueChanged.AddListener(Quantity);
        buyButton.onClick.AddListener(Buy);
    }

    public void BuyPanel(Item _item)
    {
        item = _item;
        if(currency.gold >= item.price)
        {
            Quantity(1);
            buyPanel.SetActive(true);
        }
        else
        {
            print("k đủ tiền");
        }
    }

    public void Quantity(float _quantity)
    {
        quantitySlider.maxValue = currency.gold / item.price;
        if(quantitySlider.maxValue > 99)
            quantitySlider.maxValue = 99;

        quantitySlider.value = _quantity;
        totalPrice = item.price * (int)_quantity;

        quantityText.text = _quantity.ToString();
        buyPriceText.text = totalPrice.ToString();
        descriptionText.text = "Bạn có muốn dùng " + totalPrice + " mua " + _quantity + " " + item.itemName + " không?";
        quantity = (int)_quantity;
    }

    public void Buy()
    {
        if(currency.gold >= totalPrice)
        {
            currency.gold -= totalPrice;
            InventoryManager.instance.AddItem(item, quantity);
            buyPanel.SetActive(false);
            quantitySlider.value = quantity;
        }
    }

}
