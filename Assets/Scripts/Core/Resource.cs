using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TL.Core
{
    public enum ResourceType
    {
        food,
        stone,
        wood
    }

    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        public ResourceType ResourceType
        {
            get
            {
                return resourceType;
            }
            set
            {
                resourceType = value;
            }
        }

        [SerializeField] private int initialAmount;
        public int InitialAmount
        {
            get
            {
                return initialAmount;
            }
            set
            {
                initialAmount = value;
            }
        }

        [SerializeField] private int amountAvailable;
        public int AmountAvailable
        {
            get
            {
                return amountAvailable;
            }
            set
            {
                amountAvailable = value;
            }
        }

        public delegate void ResourceExhausted();
        public event ResourceExhausted OnResourceExhausted;

        // Start is called before the first frame update
        void Start()
        {
            AmountAvailable = InitialAmount;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RemoveAmount(int amountToRemove, NPCController npc)
        {
            if (amountToRemove <= AmountAvailable)
            {
                AmountAvailable -= amountToRemove;
                npc.Inventory.AddResource(resourceType, amountToRemove);
            }
            
            if (amountToRemove > AmountAvailable)
            {
                npc.Inventory.AddResource(resourceType, AmountAvailable);
                AmountAvailable = 0;
            }

            if (AmountAvailable <= 0)
            {
                OnResourceExhausted?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}