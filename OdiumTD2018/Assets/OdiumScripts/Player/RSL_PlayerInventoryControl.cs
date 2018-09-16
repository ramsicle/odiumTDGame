using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;
using Devdog.InventoryPro;
using Devdog.General.ThirdParty.UniLinq;

public class RSL_PlayerInventoryControl : MonoBehaviour {

    public InventoryUI invUI;
    public RSL_QuestManager questManager;

    private void Awake() {
        invUI.OnAddedItem += InvUI_OnAddedItem;
    }

    private void InvUI_OnAddedItem(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection) {
        Debug.Log("Item added: " + items.First().name + " Amount: " + amount + " came from Inventory: " + cameFromCollection);
    }
}
