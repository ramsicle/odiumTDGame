using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RSL_Item {

    public enum ItemType {
        consumable,
        wearable,
        weapon,
        ammo,
        questItem
    }
    public ItemType itemType;
    public string itemName = "";

}
