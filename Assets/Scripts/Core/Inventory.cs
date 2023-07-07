using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    [CreateAssetMenu(fileName = "Inventory", menuName = "RPG-Sample/Inventory", order = 0)]
    public class Inventory : ScriptableObject {
        public event EventHandler OnItemListChanged;
        private List<Tuple<ItemData, int>> itemList = new List<Tuple<ItemData, int>>();
        // Adding item to the list.
        public void AddItem(Item item){
            Tuple<ItemData, int> newItem = Tuple.Create(item.itemData, item.amount);
            Tuple<ItemData, int> inventoryItem =SearhItemInInventoryByID(item.itemData.itemID);

            if (item.itemData.canStack && inventoryItem != null){
                    int oldAmount = inventoryItem.Item2;
                    itemList.Remove(inventoryItem);
                    itemList.Add(Tuple.Create(item.itemData, (item.amount+ oldAmount)));
                    Debug.Log(SearhItemInInventoryByID(item.itemData.itemID));
                } else {
                    itemList.Add(newItem);
                    Debug.Log(newItem.Item1);
                }
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
            // Decrease amount by 1 when using an item
            public bool ConsumeItem(ItemData item){
                bool isRemoved;
                Tuple<ItemData, int> inventoryItem =SearhItemInInventoryByID(item.itemID);
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
            //Basic search for an item with itemid
            public Tuple<ItemData, int> SearhItemInInventoryByID(int itemid){
                foreach (Tuple<ItemData, int> inventoryItem in itemList){
                    if (inventoryItem.Item1.itemID == itemid){
                        return inventoryItem;
                    }
                }
                return null;
            }
            // Sorting Items For UI
            public List<Tuple<ItemData, int>> GetItems(){
                itemList.Sort((a,b) => a.Item1.defaultInventoryIndex.CompareTo(b.Item1.defaultInventoryIndex));
                return itemList;
            }
            // Completely Destroy an item from inventory
            public void DestroyItemFromList(ItemData item){
                itemList.Remove(SearhItemInInventoryByID(item.itemID));
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
            }
            // Reset inventory
            public void SetItems(){
                itemList = new List<Tuple<ItemData, int>>();
            }
        }
    }
