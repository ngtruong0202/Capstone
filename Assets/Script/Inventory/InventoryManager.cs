using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using static Item;
using Firebase.Auth;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    FirebaseAuth auth;
    InventoryData inventoryData;

    [Header("Item")]
    public GameObject inventoryPrefab;
    public Transform contentPos;
    public InventorySlot[] inventorySlot;

    [Header("Currency")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI rubyText;

    public Item[] startItem;

  

    [Header("Button")]
    public Button ingredientButton;
    public Button itemsButton;
    public Button questButton;

    private void Awake()
    {
        instance = this;
        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        inventoryData = new InventoryData();

        questButton.onClick.AddListener(() => UpdateInventoryUI(ItemType.items));
        ingredientButton.onClick.AddListener(() => UpdateInventoryUI(ItemType.ingredient));
        itemsButton.onClick.AddListener(() => UpdateInventoryUI(ItemType.quest));

        //ReadData();
    }

    public void ReadData()
    {

        DataSever.Instance.LoadDataFn<Currency>("User/" + auth.CurrentUser.UserId, (loaded) =>
        {
            if (loaded != null)
            {
                goldText.text = loaded.gold.ToString();
                rubyText.text = loaded.ruby.ToString();
            }
            else
            {
                Debug.Log("No data found or failed to load data.");
            }
        });
    }

    public void Test()
    {
        LoadInventory();

        //DataSever.Instance.SaveDataFn("User/" + auth.CurrentUser, inventoryData);
    }

    public void LoadInventory()
    {
        // Tải dữ liệu từ Firebase hoặc bất kỳ nguồn nào
        foreach (var item in startItem)
        {
            AddItem(item, 12);
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
            inventoryData.GetItemsByType(ItemType.items);
        }
        //UpdateInventoryUI(item.itemtype);
        SpawnItem(item);
    }

    public int GetItemQuatity(Item item)
    {
        item.quantity -= item.maxStackable;
        return item.quantity;
    }

    public void UpdateInventoryUI(ItemType itemType)
    {
        foreach (Transform item in contentPos)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < inventoryData.categorizedInventory[itemType].Count; i++)
        {
            GameObject newItem = Instantiate(inventoryPrefab, contentPos);
        }
    }

    public void SpawnItem(Item item)
    {
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemSlot = slot.GetComponent<InventoryItem>();
            if(itemSlot == null)
            {
                GameObject newItem = Instantiate(inventoryPrefab, slot.transform);
                InventoryItem inventoryItem = newItem .GetComponent<InventoryItem>();
                inventoryItem.InitialiseItem(item);
                return;
            }
        }
    }
}
