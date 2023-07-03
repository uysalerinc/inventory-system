using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    public class Item : MonoBehaviour{
        public ItemData itemData = new ItemData();
        public int amount;
        public bool UseItem(Player playerData){
            switch (this.itemData.itemType)
            {
                case ItemData.ItemType.HpPotion:
                    playerData.health = (MathF.Min(playerData.max_health , playerData.health+20));
                    Debug.Log("Potion İçtin");
                    return true;
                case ItemData.ItemType.Weapon:
                    if (playerData.equippedWeapon != null){
                        Destroy(playerData.equippedWeapon.gameObject);
                    }
                    playerData.equippedWeapon = itemData.itemPrefab;
                    Debug.Log("Silah taktın");
                    return true;
                default: return false;

            }
        }
    }

    [System.Serializable]
    public class ItemData{
        public enum ItemType{
            HpPotion,
            Coin,
            Weapon,
            ManaPotion
        }
        public ItemType itemType;
        public int itemID;
        public Sprite UI_Sprite;
        public bool canStack = true;
        public int defaultInventoryIndex;
        public GameObject itemPrefab;
        public ItemData(){

        }
    }

}