using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    public class Item : MonoBehaviour{
        public ItemData itemData = new ItemData();
        public int amount;
        public static GameObject EquipWeapon(GameObject weaponPrefab, GameObject equippedWeapon){
            if (equippedWeapon != null){
                Destroy(equippedWeapon);
            }
            return weaponPrefab;
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
        public int itemID; // For Searcing items
        public Sprite UI_Sprite; // For ui
        public bool canStack = true;
        public int defaultInventoryIndex; // For UI sorting
        public GameObject weaponPrefab;
        public ItemData(){
        }
        
        public bool UseItem(Player playerData){
            switch (itemType)
            {
                case ItemData.ItemType.HpPotion:
                    playerData.health = (MathF.Min(playerData.max_health , playerData.health+20));
                    Debug.Log("Potion İçtin");
                    return true;
                case ItemData.ItemType.Weapon:
                    playerData.equippedWeapon = Item.EquipWeapon(weaponPrefab, playerData.equippedWeapon);
                    return true;
                default: return false;
            }
        }
    }

}