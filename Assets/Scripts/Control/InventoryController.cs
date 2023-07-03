using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UI;

namespace RPG.Control
{
    public class InventoryController : MonoBehaviour{
        [SerializeField] Inventory inventory;
        [SerializeField] InventoryUI inventoryUI;
        [SerializeField] List<Item> starterItems;
        public Player playerData;

        private void Start(){
            inventory.SetItems();
            foreach (Item starterItem in starterItems){
                inventory.AddItem(starterItem);
            }
            inventoryUI.SetInventory(inventory);
            
        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.E)){
                if (inventoryUI.selectedItem == null) return;
                    UseSelectedItem(inventoryUI.selectedItem);
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Item")){
                inventory.AddItem(other.gameObject.GetComponent<Item>());
                other.gameObject.SetActive(false);
            }
        }
        public void UseSelectedItem(Item selectedItem){
            if(selectedItem.UseItem(playerData)){
                if (selectedItem.itemData.itemType == ItemData.ItemType.Weapon){
                    GameObject weapon = Instantiate(selectedItem.itemData.itemPrefab, playerData.weaponPlace.transform.position, Quaternion.identity);
                    weapon.transform.SetParent(playerData.weaponPlace.transform);
                    playerData.equippedWeapon = weapon;
                }
                if (inventory.ConsumeItem(selectedItem)){
                    inventoryUI.selectedItem = null;
                }
            }
        }
    }
}
