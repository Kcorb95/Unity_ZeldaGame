using UnityEngine;

public class CharacterMovementAdvancedModel : MonoBehaviour
{
    public float PushingSpeed;

    private CharacterMovementModel m_MovementModel;
    private CharacterInteractionModel m_InteractionModel;

    private Vector3 m_LastPosition;
    private float m_LastPositionTime;
    private float m_MovementStartTime;
    private bool m_WasMoving;
    private Pushable m_ClosestPushable;
    private Transform m_ClosestPushableParent;
    private Collider2D m_Collider;

    //Init things
    private void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
        m_Collider = GetComponent<Collider2D>();
    }

    //Update things
    private void Update()
    {
        UpdatePushing();
        UpdateWasMoving();
        UpdatePushableObjects();
    }

    //We were moving, now we are again though
    private void UpdateWasMoving()
    {
        if (m_WasMoving == false && m_MovementModel.IsMoving() == true)
        {
            m_MovementStartTime = Time.realtimeSinceStartup;
        }

        m_WasMoving = m_MovementModel.IsMoving();
    }

    //Update the object being pushed
    private void UpdatePushableObjects()
    {
        if (m_ClosestPushable != null)
        {
            if (m_MovementModel.IsMoving() == false)
            {
                StopPushingClosestPushable();
            }

            return;
        }

        if (IsPushing() == false)
        {
            return;
        }

        m_ClosestPushable = FindClosestPushable();

        if (m_ClosestPushable == null)
        {
            return;
        }

        StartPushingClosestPushable();
    }

    //Done being pushed, stop the updating for this object
    private void StopPushingClosestPushable()
    {
        Collider2D closestCollider = m_ClosestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(m_Collider, closestCollider, false); //allow collision to work again

        m_ClosestPushable.transform.parent = m_ClosestPushableParent;
        m_ClosestPushable = null;

        m_MovementModel.SetFrozen(false, false, false); //Freeze object again
        m_MovementModel.SetOverrideSpeedEnabled(false);
    }

    //Start pushing an object
    private void StartPushingClosestPushable()
    {
        m_ClosestPushableParent = m_ClosestPushable.transform.parent;
        m_ClosestPushable.transform.parent = transform;

        Collider2D closestPushableCollider = m_ClosestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(m_Collider, closestPushableCollider); //turn off collision

        m_MovementModel.SetFrozen(false, true, false); //allow to move
        m_MovementModel.SetOverrideSpeedEnabled(true, PushingSpeed); //set speed so it moves etc.
    }

    //Find the closest object that which we may push (similar to closest interactable)
    private Pushable FindClosestPushable()
    {
        Collider2D[] closeColliders = m_InteractionModel.GetCloseColliders();

        Pushable closestPushable = null;
        float angleToClosestPushable = Mathf.Infinity;

        for (int i = 0; i < closeColliders.Length; ++i)
        {
            Pushable colliderPushable = closeColliders[i].GetComponent<Pushable>();

            if (colliderPushable == null)
            {
                continue;
            }

            Vector3 directionToPushable = closeColliders[i].transform.position - transform.position;

            float angleToPushable = Vector3.Angle(m_MovementModel.GetFacingDirection(), directionToPushable);

            Debug.Log(i + ": " + angleToPushable);
            if (angleToPushable < 40)
            {
                if (angleToPushable < angleToClosestPushable)
                {
                    closestPushable = colliderPushable;
                    angleToClosestPushable = angleToPushable;
                }
            }
        }

        return closestPushable;
    }

    //Update the position of the object being pushed
    private void UpdatePushing()
    {
        Vector3 position = transform.position;

        if (Vector3.Distance(position, m_LastPosition) > 0.005f)
        {
            m_LastPosition = position;
            m_LastPositionTime = Time.realtimeSinceStartup;
        }
    }

    //How long we have been moving
    private float GetMovingDuration()
    {
        if (m_MovementModel.IsMoving() == false)
        {
            return 0f;
        }

        return Time.realtimeSinceStartup - m_MovementStartTime;
    }

    //How long we have been standing still
    private float GetTimeInSamePosition()
    {
        return Time.realtimeSinceStartup - m_LastPositionTime;
    }

    //Are we pushing something currently?
    public bool IsPushing()
    {
        if (m_MovementModel.IsMoving() == false || m_WasMoving == false) return false; //If we are not moving or were moving, then nah

        if (m_ClosestPushable != null) return true; //If there is a close pushable set, then ye

        if (m_MovementModel.IsFrozen() == true) return false; //if we are frozen, then nah

        if (GameCamera.Instance.IsSwitchingScene() == true) return false; //if we are currently switching scenes, nah

        return GetMovingDuration() > 0.1f &&
               GetTimeInSamePosition() > 0.1f; //if the moving duration is less than this value AND we are in the same position for this value
    }

    //are we pushing AND moving
    public bool IsPushingAndWalking()
    {
        if (IsPushing() == false) return false; //if we arent even pushing then no

        return GetTimeInSamePosition() < 0.1f; //if we are pushing, then if we are also not in the same position for this value
    }
}