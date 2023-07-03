using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    [CreateAssetMenu(fileName = "Inventory", menuName = "RPG-Sample/Inventory", order = 0)]
    public class Inventory : ScriptableObject {
        public event EventHandler OnItemListChanged;
        private List<Tuple<Item, int>> itemList = new List<Tuple<Item, int>>();
        public void AddItem(Item item){
            Tuple<Item, int> inventoryItem =SearhItemInInventoryByID(item.itemData.itemID);

            if (item.itemData.canStack && inventoryItem != null){
                    int oldAmount = inventoryItem.Item2;
                    itemList.Remove(inventoryItem);
                    itemList.Add(Tuple.Create(item, (item.amount+ oldAmount)));
                } else {
                    itemList.Add(Tuple.Create(item, item.amount));
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
            public void ConsumeItem(Item item){
                Tuple<Item, int> inventoryItem =SearhItemInInventoryByID(item.itemData.itemID);
                if (inventoryItem.Item2 == 1){
                    itemList.Remove(inventoryItem);
                } else {
                    int newAmount = inventoryItem.Item2 -1;
                    itemList.Remove(inventoryItem);
                    itemList.Add(Tuple.Create(item, newAmount));
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);

            }
            private Tuple<Item, int> SearhItemInInventoryByID(int itemid){
                foreach (Tuple<Item, int> inventoryItem in itemList){
                    if (inventoryItem.Item1.itemData.itemID == itemid){
                        return inventoryItem;
                    }
                }
                return null;
            }
            public List<Tuple<Item, int>> GetItems(){
                itemList.Sort((a,b) => a.Item1.itemData.defaultInventoryIndex.CompareTo(b.Item1.itemData.defaultInventoryIndex));
                return itemList;
            }
            public void SetItems(){
                itemList = new List<Tuple<Item, int>>();
            }
            public void UseItem(){
                Debug.Log("Item Used");
            }
    }
}
