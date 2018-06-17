using UnityEngine;

//This class is used to make sure a character has all of the necessary systems, but also to allow easier access between systems
public class Character : MonoBehaviour
{
    public CharacterMovementModel Movement;
    public CharacterInteractionModel Interaction;
    public CharacterMovementView MovementView;
    public CharacterInventoryModel Inventory;
    public CharacterHealthModel Health;
    public CharacterBombModel Bomb;
}