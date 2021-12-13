using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.UI;

namespace TL.Core
{
    public class NPCInventory : StorageInventory
    {
        [SerializeField] private int _maxCapacity;
        [SerializeField] private Billboard InventoryUI;

        public delegate void InventoryChangedHandler();
        public event InventoryChangedHandler OnInventoryChanged;

        // Start is called before the first frame update
        void Start()
        {
            InitializeInventory();
            SetMaxCapacity(_maxCapacity);
        }

        private void OnEnable()
        {
            OnInventoryChanged += InventoryChanged;
        }

        private void OnDisable()
        {
            OnInventoryChanged -= InventoryChanged;
        }


        public void SetMaxCapacity(int capacity)
        {
            MaxCapacity = capacity;
        }

        public void SetUI(Billboard b)
        {
            InventoryUI = b;
        }

        public override void AddResource(ResourceType r, int amount)
        {
            int amountInInventory = CheckInventoryCount();
            if (amountInInventory + amount > MaxCapacity)
            {
                int amountCanAdd = MaxCapacity - amountInInventory;
                Inventory[r] += amountCanAdd;
            }
            else
            {
                Inventory[r] += amount;
            }
            OnInventoryChanged?.Invoke();
        }

        public void RemoveAllResource()
        {
            List<ResourceType> types = new List<ResourceType>();
            foreach(ResourceType r in Inventory.Keys)
            {
                types.Add(r);
            }

            foreach(ResourceType r in types)
            {
                Inventory[r] = 0;
            }

            OnInventoryChanged?.Invoke();
        }

        public override void RemoveResource(ResourceType r, int amount)
        {
            if (Inventory[r] - amount < 0)
            {
                Inventory[r] = 0;
            }
            else
            {
                Inventory[r] -= amount;
            }
            OnInventoryChanged?.Invoke();
        }        

        public void InventoryChanged()
        {
            InventoryUI.UpdateInventoryText(Inventory[ResourceType.wood], Inventory[ResourceType.stone], Inventory[ResourceType.food]);
        }
    }
}
