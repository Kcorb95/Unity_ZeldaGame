using System.Collections;
using UnityEngine;

public class InteractablePickup : InteractableBase
{
    public float ThrowDistance = 5; //How far can be thrown
    public float ThrowSpeed = 3; //How fast moves while in air

    private Vector3 m_CharacterThrowPosition;
    private Vector3 m_ThrowDirection;

    public override void OnInteract(Character character)
    {
        CharacterInteractionModel interactionModel = character.GetComponent<CharacterInteractionModel>();

        if (interactionModel == null) return;

        BroadcastMessage("OnPickupObject", character, SendMessageOptions.DontRequireReceiver); //For event listener

        interactionModel.PickupObject(this); //Pick it up
    }

    //When we throw it
    public void Throw(Character character)
    {
        StartCoroutine(ThrowRoutine(character.transform.position, character.Movement.GetFacingDirection())); //Perform anim
    }

    //Does the actual throw
    private IEnumerator ThrowRoutine(Vector3 characterThrowPosition, Vector3 throwDirection)
    {
        transform.parent = null; //Has no parent, is a free spirit

        Vector3 targetPosition = characterThrowPosition + throwDirection.normalized * ThrowDistance; //Where it will land

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ThrowSpeed * Time.deltaTime); //Move the object there with a smooth anim
            yield return null;
        }

        BroadcastMessage("OnObjectThrown", SendMessageOptions.DontRequireReceiver);//For event listener
    }
}