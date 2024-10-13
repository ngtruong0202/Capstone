using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI areYouSureText;

    private int slotIndex = 0;

    private void OnDisable() => slotIndex = -1;

    public void Activate(ItemSlot itemSlot, int slotIndex)
    {
        this.slotIndex = slotIndex;
        areYouSureText.text = $"Hủy vật phẩm {itemSlot.quantity} {itemSlot.item.ColouredName}?";

        gameObject.SetActive(true);
    }

    public void DesTroy()
    {
        inventory.ItemContainer.RemoveAt(slotIndex);
        gameObject.SetActive(false);
    }

}
