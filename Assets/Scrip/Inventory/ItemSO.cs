using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{


    [CreateAssetMenu(menuName = "Inventory/Item")]
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField][field: TextArea] public string Description { get; set; }
        [field: SerializeField] public Sprite ItemImage { get; set; }

        // ✅ Thêm dòng này để phân loại item
        [field: SerializeField] public ItemType Type { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
