using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item;


public class StoreManager : MonoBehaviour
{
    ItemStoreData itemStoreData;
    public Item[] items;

    [Header("Panel")]
    public Button storeButton;
    public Button promotionButton;
    public Button equipButton;
    public Button ingredientButton;
    public Button itemtButton;
    public Button othertButton;

    [Header("Panel")]
    public GameObject storeObj;
    public GameObject promotionPanel;
    public GameObject itemsPanel;

    [Header("Prefab")]
    public GameObject itemsPrefab;
    public GameObject promotionPrefab;

    void Start()
    {
        itemStoreData = new ItemStoreData();
        foreach (Item item in items)
        {
            itemStoreData.categorizedInventory[item.itemtype].Add(item);
        }

        storeObj.SetActive(false);
        storeButton.onClick.AddListener(OnEnableStore);
        promotionButton.onClick.AddListener(OnEnableStore);

        //equipButton.onClick.AddListener(() => OnEnableItemStore(ItemType.equip));
        ingredientButton.onClick.AddListener(() => OnEnableItemStore(ItemType.ingredient));
        itemtButton.onClick.AddListener(() => OnEnableItemStore(ItemType.items));
        //othertButton.onClick.AddListener(() => OnEnableItemStore(ItemType.other));
    }

    public void OnEnableStore()
    {
        //storeObj.SetActive(true);
        itemsPanel.SetActive(false);
        promotionPanel.SetActive(true);

        foreach(Transform item in promotionPanel.transform)
        {
            Destroy(item.gameObject);
        }
       Instantiate(promotionPrefab, promotionPanel.transform);
    }

    public void OnEnableItemStore(ItemType itemType)
    {
        promotionPanel.SetActive(false);
        itemsPanel.SetActive(true);

        foreach(Transform item in itemsPanel.transform)
        {
            Destroy(item.gameObject);
        }

        for(int i = 0; i < itemStoreData.categorizedInventory[itemType].Count; i++)
        {
            GameObject newitem = Instantiate(itemsPrefab, itemsPanel.transform);
            newitem.GetComponent<StoreItem>().UpDateUIStore(itemStoreData.categorizedInventory[itemType][i]);

        }

    }
}
