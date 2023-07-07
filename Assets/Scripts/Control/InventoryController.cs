using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UI;

namespace RPG.Control{
    public class InventoryController : MonoBehaviour{
        #region Components
        [SerializeField] Inventory inventory;
        [SerializeField] InventoryUI inventoryUI;
        [SerializeField] List<Item> starterItems;
        public Player playerData;
        #endregion

        private void Start(){
            inventory.SetItems(); // Refresh inventory
            foreach (Item starterItem in starterItems){
                inventory.AddItem(starterItem);
            }
            inventoryUI.SetInventory(inventory);
        }
        private void Update()
        {
            TriggerUseItem();
            ToggleUI();
            DestroySelectedItem();
        }
        private void DestroySelectedItem(){
            if (Input.GetKeyDown(KeyCode.X) && inventoryUI.selectedItem != null){
                inventory.DestroyItemFromList(inventoryUI.selectedItem);
                inventoryUI.selectedItem = null;
            }
        }
        private void TriggerUseItem(){
            if (Input.GetKeyDown(KeyCode.E)){
                if (inventoryUI.selectedItem == null) return;
                UseSelectedItem(inventoryUI.selectedItem);
            }
        }

        private void ToggleUI()
        {
            if (Input.GetKeyDown(KeyCode.I)){
                inventoryUI.gameObject.SetActive(!inventoryUI.isActiveAndEnabled);
                // old script
                // if (inventoryUI.isActiveAndEnabled){
                //     inventoryUI.gameObject.SetActive(false);
                // } else {
                //     inventoryUI.selectedItem = null;
                //     inventoryUI.gameObject.SetActive(true);
                // }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Item")) return; // Dont Try to colletc enemies
            Item newItem = other.GetComponent<Item>();
            // Return if inventory full and item doesnt exist inventory or cant stack
            if (inventory.GetItems().Count == 8 && (inventory.SearhItemInInventoryByID(newItem.itemData.itemID) == null || !newItem.itemData.canStack)){
                // if (inventory.SearhItemInInventoryByID(newItem.itemData.itemID) == null || !newItem.itemData.canStack){
                    return;
                // }
            }
            inventory.AddItem(newItem);
            Destroy(other.gameObject);
        }
        public void UseSelectedItem(ItemData selectedItem){
            if(selectedItem.UseItem(playerData)){
                if (selectedItem.itemType == ItemData.ItemType.Weapon){
                    GameObject weapon = Instantiate(selectedItem.weaponPrefab, playerData.weaponPlace.transform.position, Quaternion.identity); // if used item a weapon instatntiate it
                    weapon.transform.localScale = playerData.weaponPlace.GetComponentInParent<PlayerController>().gameObject.transform.localScale;
                    weapon.transform.SetParent(playerData.weaponPlace.transform);
                    playerData.equippedWeapon = weapon;
                }
                if (inventory.ConsumeItem(selectedItem)){
                    inventoryUI.selectedItem = null; // Deselecet item
                }
            }
        }
    }
}
