using UnityEngine;

public class InteractableChest : InteractableBase
{
    public Sprite OpenChestSprite; //The sprite to be used when the chest has been opened
    public ItemType ItemInChest; //What item is in the chest
    public int Amount; //How much of it is in there

    private bool m_IsOpen; //Is this chest open?
    private SpriteRenderer m_Renderer; //The sprite renderer so we can change sprites

    private void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>(); //Get the renderer
    }

    public override void OnInteract(Character character)
    {
        if (m_IsOpen == true) return; //If it is open then we cant do anything more

        character.Inventory.AddItem(ItemInChest, Amount); //Add the item to the player inv
        m_Renderer.sprite = OpenChestSprite; //set sprite to show is open
        m_IsOpen = true; //Our chest is open forever now
    }
}