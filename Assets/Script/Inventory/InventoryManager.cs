using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using static Item;

public class InventoryManager : ItemUI
{
    public static InventoryManager instance;


    public GameObject inventoryPrefab;
    public Transform invetoryTranform;

    public Item[] startItem;

    [Header("Description")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Image itemIconImage;

    public Button equipButton;
    public Button ingredientButton;
    public Button productButton;
    public Button itemsButton;
    public Button otherButton;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        inventoryData = new InventoryData();

        equipButton.onClick.AddListener( () => UpdateInventoryUI(ItemType.equip, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage));
        ingredientButton.onClick.AddListener( () => UpdateInventoryUI(ItemType.ingredient, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage));
        productButton.onClick.AddListener( () => UpdateInventoryUI(ItemType.product, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage));
        itemsButton.onClick.AddListener( () => UpdateInventoryUI(ItemType.items, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage));
        otherButton.onClick.AddListener( () => UpdateInventoryUI(ItemType.other, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage));

        //SelectItem(Item.ItemType.equip, 0, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage);
    }

    public void Selects()
    {
        SelectItem(ItemType.equip, 0, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage);

    }
    public void Test()
    {
        LoadInventory();

        DataSever.Instance.SaveDataFn("User/" + "iddddd", inventoryData);
    }

    public void LoadInventory()
    {
        // Tải dữ liệu từ Firebase hoặc bất kỳ nguồn nào
        foreach (var item in startItem)
        {
           AddItem(item,11);
        }
    }

    public void AddItem(Item item, int count)
    {
        var check = inventoryData.categorizedInventory[item.itemtype].Any(i => i.itemName == item.itemName
        && i.stackable == true && i.quantity < item.maxStackable);

        if (check)
        {
            item.quantity += count;
                
            if (item.quantity > item.maxStackable)
            {
                item.quantity = item.maxStackable;

                item.quantity = GetItemQuatity(item);
                inventoryData.categorizedInventory[item.itemtype].Add(item);
            }
        }
        else
        {
            item.quantity = count;
            inventoryData.categorizedInventory[item.itemtype].Add(item);
            inventoryData.GetItemsByType(ItemType.equip);
        }
        UpdateInventoryUI(item.itemtype, inventoryPrefab, invetoryTranform, itemNameText, itemDescriptionText, itemIconImage);

    }

    //public int GetItemQuatity(Item item)
    //{
    //    item.quantity -= item.maxStackable;
    //    return item.quantity;
    //}

    //public void UpdateInventoryUI(ItemType itemType, Transform contentPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    //{
    //    foreach (Transform item in contentPos)
    //    {
    //        Destroy(item.gameObject);
    //    }

    //    for (int i = 0; i < inventoryData.categorizedInventory[itemType].Count; i++)
    //    {
    //        GameObject newItem = Instantiate(inventoryPrefab , contentPos);
    //        newItem.GetComponent<InventoryItem>().UpdateItem(inventoryData.categorizedInventory[itemType][i]);

    //        int index = i;
    //        newItem.GetComponent<Button>().onClick.AddListener(() => SelectItem(itemType ,index, contentPos, itemNameText, itemDescriptionText, itemIconImage));
    //    }
    //}

    //public void SelectItem(ItemType itemType , int index, Transform invenPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    //{
    //    if (index >= 0 && index < inventoryData.categorizedInventory[itemType].Count)
    //    {
    //        selectedItemIndex = index;
    //        // Cập nhật UI để hiển thị vật phẩm được chọn
    //        //Debug.Log("Selected item: " + inventoryData.categorizedInventory[itemType][selectedItemIndex].itemName);
    //        // Gọi hàm cập nhật UI để làm nổi bật vật phẩm được chọn
    //        HighlightSelectedItem(itemType, invenPos, itemNameText, itemDescriptionText, itemIconImage);
    //    }
    //}

    //private void HighlightSelectedItem(ItemType itemType, Transform invenPos, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    //{
    //    for (int i = 0; i < invenPos.childCount; i++)
    //    {
    //        var itemSlot = invenPos.GetChild(i).GetComponent<InventoryItem>();
    //        itemSlot.Highlight(i == selectedItemIndex);
    //        UpdateSelectedItemDetails(itemType, itemNameText, itemDescriptionText, itemIconImage);
    //    }
    //}

    //private void UpdateSelectedItemDetails(ItemType itemType, TextMeshProUGUI itemNameText, TextMeshProUGUI itemDescriptionText, Image itemIconImage)
    //{
    //    if (selectedItemIndex >= 0 && selectedItemIndex < inventoryData.categorizedInventory[itemType].Count)
    //    {
    //        Item selectedItem = inventoryData.categorizedInventory[itemType][selectedItemIndex];
    //        itemNameText.text = selectedItem.itemName;
    //        itemDescriptionText.text = "Description for " + selectedItem.itemName; // Thay bằng mô tả thực tế nếu có
    //        itemIconImage.sprite = SpriteManager.Instance.GetSpriteByName(selectedItem.spriteURL);
    //    }
    //}
}
