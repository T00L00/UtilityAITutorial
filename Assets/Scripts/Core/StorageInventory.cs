using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TL.Core
{
    public abstract class StorageInventory : MonoBehaviour
    {
        public int MaxCapacity { get; protected set; }
        public Dictionary<ResourceType, int> Inventory { get; protected set; }

        public virtual void InitializeInventory()
        {
            Inventory = new Dictionary<ResourceType, int>()
            {
                { ResourceType.food, 0 },
                { ResourceType.stone, 0 },
                { ResourceType.wood, 0 }
            };
        }

        public virtual void AddResource(ResourceType r, int amount) { }

        public virtual void RemoveResource(ResourceType r, int amount) { }

        public virtual int CheckInventoryCount()
        {
            int sum = 0;
            foreach (ResourceType r in Inventory.Keys)
            {
                sum += Inventory[r];
            }
            return sum;
        }

        public virtual bool DoesInventoryHaveItems()
        {
            foreach (ResourceType r in Inventory.Keys)
            {
                if (Inventory[r] > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual float HowFullIsStorage()
        {
            float total = CheckInventoryCount();
            return total / MaxCapacity;
        }

    }
}
