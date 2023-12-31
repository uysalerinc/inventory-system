using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;
using TMPro;

namespace RPG.UI{
    public class InventoryUI : MonoBehaviour{
        #region Componentes
        private Inventory inventory;
        [SerializeField] private Transform itemSlotContainer;
        [SerializeField] private Transform itemSlotTemplate;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] Player playerData;
        #endregion
        public ItemData selectedItem;
        private void Awake() {
            DontDestroyOnLoad(transform.parent.transform.parent);
        }

        public void SetInventory(Inventory inventory){
            this.inventory = inventory;
            this.inventory.OnItemListChanged += Inventory_OnItemListChanged;
            RefreshInventoryItems();
        }
        private void Inventory_OnItemListChanged(object sender, EventArgs args) {
            RefreshInventoryItems();
        }
        private void RefreshInventoryItems(){
            foreach( Transform child in itemSlotContainer){
                Destroy(child.gameObject);
            }
            int x =0;
            int y = 0;
            int xOfset = -54;
            int yOfset = 19;
            float itemSlotCellSize = 36f;
            foreach(Tuple<ItemData, int> item in inventory.GetItems()){
                RectTransform itemSlotRectTransform = CreateItemSlot(x, y, xOfset, yOfset, itemSlotCellSize);
                SetPropertiesOfItemSlot(item, itemSlotRectTransform);
                AddButton(itemSlotRectTransform.transform, item);
                x++;
                if (x >= 4){ // 4 is inventory len
                    x = 0;
                    y--;
                }
            }
        }

        private static void SetPropertiesOfItemSlot(Tuple<ItemData, int> item, RectTransform itemSlotRectTransform){
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.Item1.UI_Sprite;
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (item.Item2 == 1){
                uiText.text = null;
            } else {
                uiText.text = item.Item2.ToString();
            }
        }

        private RectTransform CreateItemSlot(int x, int y, int xOfset, int yOfset, float itemSlotCellSize){
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize + xOfset, y * itemSlotCellSize + yOfset);
            return itemSlotRectTransform;
        }
        private void AddButton(Transform slotTransform, Tuple<ItemData, int> item){
            GameObject button = Instantiate(buttonPrefab, slotTransform);
            button.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
        }
        public void SelectItem(Tuple<ItemData,int> item){
            selectedItem = inventory.SearhItemInInventoryByID(item.Item1.itemID).Item1;
            Debug.Log(selectedItem);
            Debug.Log(selectedItem.itemType);
        }

    }
}
