using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Data; //Our database is list

    public ItemData FindItem(ItemType itemType) //Finds an item by its item type
    {
        for (int i = 0; i < Data.Count; ++i) //loop through the list to find it
        {
            if (Data[i].Type == itemType)
            {
                return Data[i]; //if found, return the item
            }
        }

        return null; //otherwise item isnt in db
    }
}

[System.Serializable]
public class ItemData
{
    public enum PickupAnimation //What anims can be played for the item on pickup
    {
        None,
        OneHanded,
        TwoHanded,
    }

    public enum EquipPosition //Where if at all the item should be equipped
    {
        NotEquipable,
        SwordHand,
        ShieldHand,
    }

    public ItemType Type;
    public GameObject Prefab;
    public EquipPosition IsEquipable;
    public PickupAnimation Animation;
}