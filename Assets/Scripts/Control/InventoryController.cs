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
                UseSelectedItem(inventoryUI.selectedItem);
            }
        }
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Item")){
                inventory.AddItem(other.GetComponent<Item>());
                Destroy(other.gameObject);
            }
        }
        public void UseSelectedItem(Item selectedItem){
            if(selectedItem.UseItem(playerData)){
                if (selectedItem.itemData.itemType == ItemData.ItemType.Weapon){
                    GameObject weapon = Instantiate(selectedItem.itemData.weaponPrefab, playerData.weaponPlace.transform.position, Quaternion.identity);
                    weapon.transform.SetParent(playerData.weaponPlace.transform);
                }
                inventory.ConsumeItem(selectedItem);
            }
        }
    }
}
