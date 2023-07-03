using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI{
    public class ItemUI : MonoBehaviour{
       [SerializeField] GameObject buttonPrefab;

       public void AddButton(Action OnClickAction){
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => OnClickAction());
       } 

    }
}
