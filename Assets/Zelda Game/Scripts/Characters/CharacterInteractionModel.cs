using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterInteractionModel : MonoBehaviour
{
    private Character m_Character;
    private Collider2D m_Collider;
    private CharacterMovementModel m_MovementModel;
    private InteractablePickup m_PickedUpObject;

    private void Awake()
    {
        m_Character = GetComponent<Character>();
        m_Collider = GetComponent<Collider2D>();
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    //Do interact
    public void OnInteract()
    {
        if (IsCarryingObject() == true) //If the player is currently carrying an object
        {
            ThrowCarryingObject(); //Throw it
            return;
        }

        InteractableBase usableInteractable = FindUsableInteractable(); //Find an object in range that the user can interact with

        if (usableInteractable == null) return; //No interactable found

        usableInteractable.OnInteract(m_Character); //Do the interact
    }

    //Used to find what colliders are closest to the player in order to see what can be interacted with
    public Collider2D[] GetCloseColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        return Physics2D.OverlapAreaAll(
            (Vector2)transform.position + boxCollider.offset + boxCollider.size * 0.6f,
            (Vector2)transform.position + boxCollider.offset - boxCollider.size * 0.6f);
    }

    //This method is used to find the closest interactable to the player
    public InteractableBase FindUsableInteractable()
    {
        Collider2D[] closeColliders = GetCloseColliders(); //Gets an array of the closest interactable to the player

        InteractableBase closestInteractable = null; //Which is closest found one
        float angleToClosestInteractble = Mathf.Infinity; //Facing the interactable

        for (int i = 0; i < closeColliders.Length; ++i) //Loop through our interactables
        {
            InteractableBase colliderInteractable = closeColliders[i].GetComponent<InteractableBase>(); //Get the parent component of the collider

            if (colliderInteractable == null) continue;

            Vector3 directionToInteractble = closeColliders[i].transform.position - transform.position; //The vector between us and the interactable

            float angleToInteractable = Vector3.Angle(m_MovementModel.GetFacingDirection(), directionToInteractble);

            if (angleToInteractable < 40) //if deg is less than fourty, then we are facing it enough
            {
                if (angleToInteractable < angleToClosestInteractble)
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractble = angleToInteractable;
                }
            }
        }

        return closestInteractable;
    }

    //Pick up an object that can later be thrown
    public void PickupObject(InteractablePickup pickupObject)
    {
        m_PickedUpObject = pickupObject;

        //Positon the object over us
        m_PickedUpObject.transform.parent = m_MovementModel.PickupItemParent;
        m_PickedUpObject.transform.localPosition = Vector3.zero;

        //We cant attack because we are carrying something
        m_MovementModel.SetIsAbleToAttack(false);

        //Make it on our sorting layer relative to us
        Helper.SetSortingLayerForAllRenderers(pickupObject.transform, "Characters");

        //Turn off the collider for this object
        Collider2D pickupObjectCollider = pickupObject.GetComponent<Collider2D>();

        if (pickupObjectCollider != null)
        {
            pickupObjectCollider.enabled = false;
        }
    }

    //Throws the currently being carried object
    public void ThrowCarryingObject()
    {
        Collider2D pickupObjectCollider = m_PickedUpObject.GetComponent<Collider2D>();

        //Re-enable the collider and ignore collisions as your throw it
        if (pickupObjectCollider != null)
        {
            pickupObjectCollider.enabled = true;
            Physics2D.IgnoreCollision(m_Collider, pickupObjectCollider);
        }

        //Throoow it
        m_PickedUpObject.Throw(m_Character);
        m_PickedUpObject = null;

        //No longer carrying, can attack
        m_MovementModel.SetIsAbleToAttack(true);
    }

    //Am I carrying something?
    public bool IsCarryingObject()
    {
        return m_PickedUpObject != null;
    }
}