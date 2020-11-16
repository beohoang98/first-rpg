using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Manager
{
    public class InventoryManager : MonoBehaviour
    {
        private List<GameItemInfo> gameItemInfo = new List<GameItemInfo>();

        [SerializeField] private GameObject inventoryUI;

        public void Add(GameItem gameItem)
        {
            gameItemInfo.Add(gameItem.info);
        }

        private void UpdateUI()
        {
            
        }
    }
}
