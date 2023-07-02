using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;

namespace RPG.UI{
    public class InventoryUI : MonoBehaviour{
        [SerializeField] Inventory inventory;
        [SerializeField] private Transform itemSlotContainer;
        [SerializeField] private Transform itemSlotTemplate;
        private void Start() {
            RefreshInventoryItems();
        }
        private void RefreshInventoryItems(){
            int x =0;
            int y = 0;
            int xOfset = -54;
            int yOfset = 19;
            float itemSlotCellSize = 36f;
            foreach(Item item in inventory.itemList){
                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + xOfset, y*itemSlotCellSize + yOfset);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.itemData.UI_Sprite;
                x++;
                if (x >= 4){
                    x= 0;
                    y--;
                }


            }
        } 
       
    }
}
