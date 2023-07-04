using System;

using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    [CreateAssetMenu(fileName = "Inventory", menuName = "RPG-Sample/Inventory", order = 0)]
    public class Inventory : ScriptableObject {
        public event EventHandler OnItemListChanged;
        private List<Tuple<Item, int>> itemList = new List<Tuple<Item, int>>();
        public void AddItem(Item item){
            Tuple<Item, int> newItem = Tuple.Create(item, item.amount);
            Tuple<Item, int> inventoryItem =SearhItemInInventoryByID(item.itemData.itemID);

            if (item.itemData.canStack && inventoryItem != null){
                    int oldAmount = inventoryItem.Item2;
                    itemList.Remove(inventoryItem);
                    itemList.Add(Tuple.Create(item, (item.amount+ oldAmount)));
                    Debug.Log(SearhItemInInventoryByID(item.itemData.itemID));
                } else {
                    // itemList.Add(Tuple.Create(item, item.amount));
                    itemList.Add(newItem);
                    Debug.Log(newItem.Item1);
                    // Debug.Log(SearhItemInInventoryByID(item.itemData.itemID));
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
            public bool ConsumeItem(Item item){
                bool isRemoved;
                Tuple<Item, int> inventoryItem =SearhItemInInventoryByID(item.itemData.itemID);
                if (inventoryItem.Item2 == 1){
                    itemList.Remove(inventoryItem);
                    isRemoved = true;
                } else {
                    int newAmount = inventoryItem.Item2 -1;
                    itemList.Remove(inventoryItem);
                    itemList.Add(Tuple.Create(item, newAmount));
                    isRemoved = false;
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return isRemoved;

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
            public void DestroyItemFromList(Item item){
                itemList.Remove(SearhItemInInventoryByID(item.itemData.itemID));
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
            public void SetItems(){
                itemList = new List<Tuple<Item, int>>();
                }
            }
    }
