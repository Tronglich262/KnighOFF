using UnityEngine;
using Inventory.Model;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private Transform textSpawnTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null && item.Quantity > 0)
        {
            int reminder = inventoryData.AddItem(item.inventoryItem, item.Quantity);
            if (reminder == 0)
            {
                FloatingTextSpawner.Instance.SpawnText($"Đã nhặt {item.inventoryItem.name}", textSpawnTarget.position);
                item.DestroyItem();
            }
            else
            {
                FloatingTextSpawner.Instance.SpawnText($"Không đủ chỗ cho {item.inventoryItem.name}, còn lại: {reminder}", textSpawnTarget.position);
                item.Quantity = reminder;
            }
        }
    }
}
