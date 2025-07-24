using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        public List<InventoryItem> Items = new List<InventoryItem>();

        
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
        

        public int AddItem(ItemSO item, int quantity,List<ItemParameter> itemState = null)
        {
            if (!item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)// Sửa điều kiện vòng lặp
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item,quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddNonStackableItem(ItemSO item, int quantity)
        {
            int addedCount = 0; // Số lượng thực tế đã thêm vào túi

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem { item = item, quantity = 1 };
                    addedCount++;
                    quantity--;

                    if (quantity <= 0) // Nếu đã thêm hết thì dừng vòng lặp
                        break;
                }
            }

            return addedCount; // Trả về số lượng đã thêm
        }


        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTaKe = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;
                    if (quantity > amountPossibleToTaKe)
                    {
                        inventoryItems[i] = inventoryItems[i].changeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTaKe;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .changeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item,newQuantity);
            }

            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }


        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int index1, int index2)
        {
            if (index1 == index2) return; // Không swap nếu cùng vị trí
            if (inventoryItems[index1].IsEmpty && inventoryItems[index2].IsEmpty) return; // Nếu cả hai rỗng, không làm gì

            var temp = inventoryItems[index1];
            inventoryItems[index1] = inventoryItems[index2];
            inventoryItems[index2] = temp;

            OnInventoryUpdated?.Invoke(GetCurrentInventoryState()); // Cập nhật UI
        }




        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if(reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].changeQuantity(reminder);
                InformAboutChange();
            }
        }
        //them 
        public void SaveInventory()
        {
            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("SavedInventory", json);
            PlayerPrefs.Save();
        }

        public void LoadInventory()
        {
            if (PlayerPrefs.HasKey("SavedInventory"))
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("SavedInventory"), this);
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem changeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
        
    }
}
