using UnityEngine;
using UnityEngine.UI;

public class Additem : MonoBehaviour
{
    public Item item;
    public int quantity;

   
    public void BuyItem()
    {
        InventoryManager.instance.AddItem(item,quantity);
        //InventoryManager.instance.UpdateInventoryUI();

    }

    public void PickUpItem()
    {
        InventoryManager.instance.AddItem(item, quantity);
        //InventoryManager.instance.UpdateInventoryUI();

        Destroy(gameObject);
    }
}