using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;
using Devdog.InventoryPro;
using Devdog.General.ThirdParty.UniLinq;

public class RSL_PlayerKeepHandler : MonoBehaviour {

    public BankUI bankUI;
    public RSL_QuestManager questManager;

    private string itemCategoryName = "";

    private void Awake() {
        bankUI.OnAddedItem += BankUI_OnAddedItem;
    }

    private void BankUI_OnAddedItem(IEnumerable<Devdog.InventoryPro.InventoryItemBase> items, uint amount, bool cameFromCollection) {
        //Debug.Log("Item Added: " + item.name + " Amount: " + amount + " From inventory: " + cameFromCollection);
        
        switch (items.First().name) {
            case "Apple":
            case "Pear":
            case "Banana":
                itemCategoryName = "fruit";
                break;
        }

        questManager.AddItemToList(itemCategoryName, (int)amount);
    }
}
