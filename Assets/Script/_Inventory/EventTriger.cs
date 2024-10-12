using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlot slot;

    private void Start()
    {
        // Đăng ký (subscribe) phương thức OnItemUpdatedHandler để lắng nghe sự kiện OnItemUpdated
        inventory.ItemContainer.OnItemUpdated += OnEnable;
        Debug.Log("OnItemUpdated");
    }

    public void OnEnable()
    {
        Debug.Log("UI đã cập nhật sau khi vật phẩm thay đổi.");
        
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh memory leak
        //inventory.ItemContainer.OnItemUpdated -= OnItemUpdatedHandler;
    }
}
