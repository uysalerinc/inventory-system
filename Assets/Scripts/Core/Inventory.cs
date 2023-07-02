using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
    [CreateAssetMenu(fileName = "Inventory", menuName = "RPG-Sample/Inventory", order = 0)]
    public class Inventory : ScriptableObject {
        public List<Item> itemList;
        

       public void AddItem(Item item){
        itemList.Add(item);
        
       }

    }
}
