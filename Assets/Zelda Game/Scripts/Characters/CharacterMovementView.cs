using UnityEngine;

public class CharacterMovementView : MonoBehaviour
{
    public enum ShieldDirection //What directions the shield may face
    {
        Front,
        Right,
        Left,
        Back,
        FrontHalf,
        BackHalf,
    }

    public Animator Animator;

    private CharacterMovementModel m_MovementModel;

    private void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();

        if (Animator == null)
        {
            Debug.LogError("Character Animator is not setup!");
            enabled = false;
        }
    }

    private void Start()
    {
        SetItemActive(m_MovementModel.WeaponParent, false);
    }

    public void Update()
    {
        UpdateDirection();
        UpdatePickingUpAnimation();
        UpdateHit();
        UpdateShield();
    }

    private void UpdateShield()
    {
        Animator.SetBool("HasShield", m_MovementModel.GetEquippedShield() != ItemType.None);
    }

    //Does the animation etc
    private void UpdatePickingUpAnimation()
    {
        bool isPickingUpOneHanded = false;
        bool isPickingUpTwoHanded = false;

        if (m_MovementModel.IsFrozen() == true)
        {
            ItemType pickupItem = m_MovementModel.GetItemThatIsBeingPickedUp();

            if (pickupItem != ItemType.None)
            {
                ItemData itemData = Database.Item.FindItem(pickupItem);

                switch (itemData.Animation)
                {
                    case ItemData.PickupAnimation.OneHanded:
                        isPickingUpOneHanded = true;
                        break;

                    case ItemData.PickupAnimation.TwoHanded:
                        isPickingUpTwoHanded = true;
                        break;
                }
            }
        }

        Animator.SetBool("IsPickingUpOneHanded", isPickingUpOneHanded);
        Animator.SetBool("IsPickingUpTwoHanded", isPickingUpTwoHanded);
    }

    //Player is hit, do that anim
    private void UpdateHit()
    {
        Animator.SetBool("IsHit", m_MovementModel.IsBeingPushed());
    }

    //Update our direction
    private void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetFacingDirection(); //Where we are facing

        if (direction != Vector3.zero)
        {
            if (direction.x != 1 || direction.y != 1)
            {
                Animator.SetFloat("DirectionX", direction.x);
                Animator.SetFloat("DirectionY", direction.y);
            }
        }

        Animator.SetBool("IsMoving", m_MovementModel.IsMoving()); //Set that we are moving
    }

    public void DoAttack() //For our attack anims
    {
        Animator.SetTrigger("DoAttack");
    }

    public void OnAttackStarted()
    {
    }

    public void OnAttackFinished()
    {
    }

    public void ShowWeapon()
    {
        SetItemActive(m_MovementModel.WeaponParent, true);
    }

    public void HideWeapon()
    {
        SetItemActive(m_MovementModel.WeaponParent, false);
    }

    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        SetSortingOrderOfItem(m_MovementModel.WeaponParent, sortingOrder);
    }

    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        SetSortingOrderOfItem(m_MovementModel.PickupItemParent, sortingOrder);
    }

    public void ShowShield()
    {
        SetItemActive(m_MovementModel.ShieldParent, true);
    }

    public void HideShield()
    {
        SetItemActive(m_MovementModel.ShieldParent, true);
    }

    public void SetSortingOrderOfShield(int sortingOrder)
    {
        SetSortingOrderOfItem(m_MovementModel.ShieldParent, sortingOrder);
    }

    public void ForceShieldDirection(ShieldDirection direction)
    {
        ArmorShieldView shield = m_MovementModel.ShieldParent.GetComponentInChildren<ArmorShieldView>();

        if (shield == null)
        {
            return;
        }

        shield.ForceShieldDirection(direction);
    }

    public void ReleaseShieldDirection()
    {
        ArmorShieldView shield = m_MovementModel.ShieldParent.GetComponentInChildren<ArmorShieldView>();

        if (shield == null)
        {
            return;
        }

        shield.ReleaseShieldDirection();
    }

    private void SetSortingOrderOfItem(Transform itemParent, int sortingOrder)
    {
        if (itemParent == null)
        {
            return;
        }

        SpriteRenderer[] spriteRenderers = itemParent.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }

    private void SetItemActive(Transform itemParent, bool doActivate)
    {
        if (itemParent == null)
        {
            return;
        }

        itemParent.gameObject.SetActive(doActivate);
    }
}