using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/inventory")]


public class Inventory : ScriptableObject
{
    [SerializeField] private ItemSlot testItemSlot;

    public ItemContainer ItemContainer { get; } = new ItemContainer(25);


    [ContextMenu("Test Add")]
    public void TestAdd()
    {
        ItemContainer.AddItem(testItemSlot);
    }

}
