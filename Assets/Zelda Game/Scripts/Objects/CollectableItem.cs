using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public ItemType ItemType;
    public int Amount;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterInventoryModel inventoryModel = collider.GetComponent<CharacterInventoryModel>();//The inventory of player

        if (inventoryModel != null)
        {
            if (gameObject.tag == "Health") //If this is a health object
            {
                CharacterHealthModel healthModel = collider.GetComponent<CharacterHealthModel>(); //Get the health model
                if (healthModel != null) //Make sure it is created correctly
                {
                    healthModel.AddHealth(Amount);//Just add the health to player and not inventory
                }
            }

            inventoryModel.AddItem(ItemType, Amount); //Add item and amount to inv
            Destroy(gameObject); //Destroy the pickup
        }
    }
}