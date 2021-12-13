using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TL.Core;

namespace TL.UI
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private TextMeshProUGUI bestActionText;
        [SerializeField] private TextMeshProUGUI inventoryText;
        private Transform mainCameraTransform;

        // Start is called before the first frame update
        void Start()
        {
            mainCameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
        }

        public void UpdateStatsText(int energy, int hunger, int money)
        {
            statsText.text = $"Energy: {energy}\nHunger: {hunger}\nMoney: {money}";
        }

        public void UpdateBestActionText(string bestAction)
        {
            bestActionText.text = bestAction;
        }

        public void UpdateInventoryText(int wood, int stone, int food)
        {
            inventoryText.text = $"Wood: {wood}\nStone: {stone}\nFood: {food}";
        }
    }
}
