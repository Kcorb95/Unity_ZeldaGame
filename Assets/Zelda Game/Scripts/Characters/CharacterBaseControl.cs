using UnityEngine;

public class CharacterBaseControl : MonoBehaviour
{
    //Our different systems (AKA models)
    protected CharacterMovementModel m_MovementModel;

    protected CharacterInteractionModel m_InteractionModel;
    protected CharacterMovementView m_MovementView;
    protected CharacterBombModel m_BombModel;

    //Init all systems by finding their gamecomponent
    private void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_MovementView = GetComponent<CharacterMovementView>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
        m_BombModel = GetComponent<CharacterBombModel>();
    }

    protected Vector2 GetDiagonalizedDirection(Vector2 direction, float threshold)
    {
        if (Mathf.Abs(direction.x) < threshold)
        {
            direction.x = 0;
        }
        else
        {
            direction.x = Mathf.Sign(direction.x);
        }

        if (Mathf.Abs(direction.y) < threshold)
        {
            direction.y = 0;
        }
        else
        {
            direction.y = Mathf.Sign(direction.y);
        }

        return direction;
    }

    protected void SetDirection(Vector2 direction)
    {
        if (m_MovementModel == null)
        {
            return;
        }

        m_MovementModel.SetDirection(direction);
    }

    //When a player interacts with a sign or chest etc.
    protected void OnActionPressed()
    {
        //Make sure the system is init
        if (m_InteractionModel == null) return;

        m_InteractionModel.OnInteract(); //Do the interaction
    }

    protected void OnBombUsed() //When a player tries to drop a bomb
    {
        if (m_BombModel == null) return; //Make sure the bomb system is initialized

        m_BombModel.UseBomb(); //Use the bomb
    }

    protected void OnAttackPressed() //When a player tries to attack
    {
        //Make sure we can both attack, and that the system is initialized
        if (m_MovementModel == null) return;
        if (m_MovementModel.CanAttack() == false) return;

        //Do the attack
        m_MovementModel.DoAttack();
        m_MovementView.DoAttack();
    }
}