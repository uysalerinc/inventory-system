using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Item : MonoBehaviour{
        public ItemData itemData = new ItemData();

    }
    [System.Serializable]
    public class ItemData{
        public Sprite UI_Sprite;
        public int amount = 1;
        public bool canStack = true;
        
        public ItemData(){

        }
    }

}