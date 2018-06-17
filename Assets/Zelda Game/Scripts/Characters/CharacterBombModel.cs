using UnityEngine;

public class CharacterBombModel : MonoBehaviour
{
    public GameObject BombGameObject; //The bomb prefab to Instantiate

    private Character m_Character; //Our character model so that we can remove the item from our inventory and place it at our feet

    private void Awake()
    {
        m_Character = GetComponent<Character>(); //Init our Character object
    }

    //Returns the currently available number of bombs
    public float GetBombs()
    {
        return m_Character.Inventory.GetItemCount(ItemType.Bomb); //Searches our inventory database for the total amount of bombs available
    }

    //Actually usese the bomb
    public void UseBomb()
    {
        if (GetBombs() <= 0) return; //If no bombs, dont do anything

        Instantiate(BombGameObject, transform.position, transform.rotation); //Spawn the bomb at our feet

        m_Character.Inventory.RemoveItem(ItemType.Bomb); //Remove 1 bomb from our inventory
    }
}