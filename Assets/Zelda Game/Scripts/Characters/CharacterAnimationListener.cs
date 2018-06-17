using UnityEngine;

public class CharacterAnimationListener : MonoBehaviour
{
    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;

    //When attack starts, do the animation to show weapon etc.
    public void OnAttackStarted(AnimationEvent animationEvent)
    {
        //Make sure the movement system is set up and do attack
        if (MovementModel != null) MovementModel.OnAttackStarted();
        if (MovementView != null) MovementView.OnAttackStarted();

        ShowWeapon(); //Render our weapon
        SetSortingOrderOfWeapon(animationEvent.intParameter);
        SetShieldDirection(animationEvent.stringParameter);
    }

    //Attack finished, hide our weapon
    public void OnAttackFinished()
    {
        //Make sure the movement system is set up and finish attack
        if (MovementModel != null) MovementModel.OnAttackFinished();
        if (MovementView != null)
        {
            MovementView.OnAttackFinished();
            MovementView.ReleaseShieldDirection();
        }

        HideWeapon(); //Hide our weapon
    }

    //Shows the weapon
    public void ShowWeapon()
    {
        if (MovementView != null) MovementView.ShowWeapon();
    }

    //Hides the weapon
    public void HideWeapon()
    {
        if (MovementView != null) MovementView.HideWeapon();
    }

    //Correctly sets sorting position of the weapon
    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        if (MovementView != null) MovementView.SetSortingOrderOfWeapon(sortingOrder);
    }

    //Correctly sets sorting position of the items
    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        if (MovementView != null) MovementView.SetSortingOrderOfPickupItem(sortingOrder);
    }

    //Properly renders shield based off of our character's facing direction
    public void SetShieldDirection(string direction)
    {
        if (MovementView == null || direction == "") return;

        switch (direction)
        {
            default:
                Debug.LogWarning("Shield direction '" + direction + "' does not exist."); //Should never be reached
                break;

            case "Front":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Front);
                break;

            case "Back":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Back);
                break;

            case "Left":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Left);
                break;

            case "Right":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Right);
                break;

            case "FrontHalf":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.FrontHalf);
                break;

            case "BackHalf":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.BackHalf);
                break;
        }
    }
}