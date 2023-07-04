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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)){
                if (inventoryUI.selectedItem == null) return;
                UseSelectedItem(inventoryUI.selectedItem);
            }
            ToggleUI();
            DestroySelectedItem();
        }
        private void DestroySelectedItem(){
            if (Input.GetKeyDown(KeyCode.X) && inventoryUI.selectedItem != null){
                inventory.DestroyItemFromList(inventoryUI.selectedItem);
                inventoryUI.selectedItem = null;
            }
        }

        private void ToggleUI()
        {
            if (Input.GetKeyDown(KeyCode.I)){
                if (inventoryUI.isActiveAndEnabled){
                    inventoryUI.gameObject.SetActive(false);
                } else {
                    inventoryUI.selectedItem = null;
                    inventoryUI.gameObject.SetActive(true);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (inventory.GetItems().Count == 8) return;
            if (other.CompareTag("Item")){
                inventory.AddItem(other.gameObject.GetComponent<Item>());
                other.gameObject.SetActive(false);
            }
        }
        public void UseSelectedItem(Item selectedItem){
            if(selectedItem.UseItem(playerData)){
                if (selectedItem.itemData.itemType == ItemData.ItemType.Weapon){
                    GameObject weapon = Instantiate(selectedItem.itemData.weaponPrefab, playerData.weaponPlace.transform.position, Quaternion.identity);
                    weapon.transform.localScale = playerData.weaponPlace.GetComponentInParent<PlayerController>().gameObject.transform.localScale;
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
