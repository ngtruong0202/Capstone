using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory")]

public class Inventory : ScriptableObject
{
    [SerializeField] private ItemSlot testItemSlot;

    public ItemContainer ItemContainer { get; } = new ItemContainer(20);


    [ContextMenu("Test Add")]
    public void TestAdd()
    {
        ItemContainer.AddItem(testItemSlot);
    }

}
