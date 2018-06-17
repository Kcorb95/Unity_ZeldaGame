using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryModel : MonoBehaviour
{
    private CharacterMovementModel m_MovementModel;

    private Dictionary<ItemType, int> m_Items = new Dictionary<ItemType, int>(); //Our inventory items by Item Type

    private void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    //How many of this item do we have?
    public int GetItemCount(ItemType itemType)
    {
        if (m_Items.ContainsKey(itemType) == false) return 0; //If we dont even have this item, return 0

        return m_Items[itemType];
    }

    //Remove an item
    public void RemoveItem(ItemType itemType)
    {
        if (m_Items.ContainsKey(itemType) == true) m_Items[itemType]--; //Reduce the number of items by 1
    }

    //Add a single item to our inventory
    public void AddItem(ItemType itemType)
    {
        AddItem(itemType, 1);
    }

    //Add X amount of items to our inventory
    public void AddItem(ItemType itemType, int amount)
    {
        if (m_Items.ContainsKey(itemType) == true) m_Items[itemType] += amount; //If item exists, just add more
        else m_Items.Add(itemType, amount); //Otherwise just add the item and its amount as new

        if (amount > 0) //If we have a bunch
        {
            ItemData itemData = Database.Item.FindItem(itemType); //Find the item in the database

            if (itemData != null) //If it is valid data
            {
                if (itemData.Animation != ItemData.PickupAnimation.None) m_MovementModel.ShowItemPickup(itemType); //if using pickup anim, show the anim
                if (itemData.IsEquipable == ItemData.EquipPosition.SwordHand) m_MovementModel.EquipWeapon(itemType); //If is a sword hand item, put it there
                else if (itemData.IsEquipable == ItemData.EquipPosition.ShieldHand) m_MovementModel.EquipShield(itemType); //If is a shield hand item, put it there
            }
        }
    }
}