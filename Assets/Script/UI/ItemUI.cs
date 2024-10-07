using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ItemUI : MonoBehaviour 
{

    public InventoryData inventoryData;

    private int selectedItemIndex = -1;

    public int GetItemQuatity(Item item)
    {
        item.quantity -= item.maxStackable;
        return item.quantity;
    }

    public void UpdateInventoryUI(ItemType itemType, GameObject inventoryPrefab, Transform contentPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    {
        foreach (Transform item in contentPos)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < inventoryData.categorizedInventory[itemType].Count; i++)
        {
            GameObject newItem = Instantiate(inventoryPrefab, contentPos);
            newItem.GetComponent<InventoryItem>().UpdateItem(inventoryData.categorizedInventory[itemType][i]);

            SelectItem(itemType, 0, contentPos, itemNameText, itemDescriptionText, itemIconImage);
            int index = i;
            newItem.GetComponent<Button>().onClick.AddListener(() => SelectItem(itemType, index, contentPos, itemNameText, itemDescriptionText, itemIconImage));

        }
    }

    public void SelectItem(ItemType itemType, int index, Transform invenPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    {
        if (index >= 0 && index < inventoryData.categorizedInventory[itemType].Count)
        {
            selectedItemIndex = index;
            // Cập nhật UI để hiển thị vật phẩm được chọn
            //Debug.Log("Selected item: " + inventoryData.categorizedInventory[itemType][selectedItemIndex].itemName);
            // Gọi hàm cập nhật UI để làm nổi bật vật phẩm được chọn
            HighlightSelectedItem(itemType, invenPos, itemNameText, itemDescriptionText, itemIconImage);
        }
    }

    private void HighlightSelectedItem(ItemType itemType, Transform invenPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    {
        for (int i = 0; i < invenPos.childCount; i++)
        {
            var itemSlot = invenPos.GetChild(i).GetComponent<InventoryItem>();
            if (itemSlot != null)
            {
                itemSlot.Highlight(i == selectedItemIndex);

            }
            UpdateSelectedItemDetails(itemType, itemNameText, itemDescriptionText, itemIconImage);
        }
    }

    private void UpdateSelectedItemDetails(ItemType itemType, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < inventoryData.categorizedInventory[itemType].Count)
        {
            Item selectedItem = inventoryData.categorizedInventory[itemType][selectedItemIndex];
            itemNameText.text = selectedItem.itemName;
            itemDescriptionText.text = "Description for " + selectedItem.itemName; // Thay bằng mô tả thực tế nếu có
            itemIconImage.sprite = SpriteManager.Instance.GetSpriteByName(selectedItem.spriteURL);
        }
    }
}
