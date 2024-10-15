using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private HotBarSlot[] hotBarSlots = new HotBarSlot[10];


    public void Add(HotbarItem itemToAdd)
    {
        foreach(HotBarSlot hotBarSlot in hotBarSlots)
        {
            if(hotBarSlot.AddItem(itemToAdd)) return;
        }
    }
}
